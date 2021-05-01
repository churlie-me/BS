import { inlineView, inject } from 'aurelia-framework';
import { Router } from "aurelia-router";
import { DateConverter } from '../../../../extensions/date.converter';
import { Guid } from '../../../../extensions/guid';
import { ModelDialog } from '../../../../extensions/modal.dialog';
import { ProgressDialog } from '../../../../extensions/progress/progress.dialog';
import { TimeValueConverter } from '../../../../extensions/time.converter';
import { AppointmentService } from '../../../../services/appointment.service';
import { AuthenticationService } from '../../../../services/authentication.service';
import { OrderService } from '../../../../services/order.service';
import { ServiceService } from '../../../../services/service.service';
import { StoreService } from '../../../../services/store.service';
import { UserService } from '../../../../services/user.service';
import { InvoiceModule } from '../../invoice/invoice.module';
@inject(Guid, ModelDialog, ProgressDialog, StoreService, AppointmentService, ServiceService, UserService, DateConverter, TimeValueConverter, Router, InvoiceModule, OrderService, AuthenticationService)
export class AppointmentModule
{
  guid
  appointment
  modal
  progressDialog
  dboStaff
  branches = []
  storeservice
  appService
  serviceService
  isBusy
  userService
  appointmentservice
  dateConverter
  timeValueConverter
  _orderService
  _router
  invoice
  authenticationService
  statuses = [{id: 0, status :'Reserved'},{id: 1, status :'InProgress'},{id: 2, status :'Complete'},{id: 3, status :'Invoinced'}]
  constructor(guid, modal, progressDialog, storeservice, appService, serviceService, userService, dateConverter, timeValueConverter, _router, invoice, _orderService, authenticationService)
  {
    this.modal = modal
    this.progressDialog = progressDialog
    this.storeservice = storeservice
    this.appService = appService
    this.serviceService = serviceService
    this.isBusy = false
    this.userService = userService
    this.dateConverter = dateConverter
    this.timeValueConverter = timeValueConverter
    this._router = _router
    this.guid = guid
    this._orderService = _orderService
    debugger
    this.invoice = invoice
    this.authenticationService = authenticationService
  }

  initiateAppointment()
  {
    debugger
    if( this.appointment.status != 3 && this.dboservices)
    {
      if(this.dboservices.services.length == 0)
      {
        this.dboservices = {
          page : 1,
          pageSize : 200
        }

        this.initServices()
      }
    }
    else if(this.appointment.status == 3 && this.dboservices == undefined)
    {
      this.dboservices = {
        page : 1,
        pageSize : 200,
        services : []
      }
    }
    else
    {
      this.dboservices = {
        page : 1,
        pageSize : 200
      }

      this.initServices()
    }

    if(!this.appointment)
      this.appointment = { status : 0 }

    if(!this.appointment.services)
      this.appointment.services = []

    this.appointmentTime = this.appointment.appointmentDate? this.timeValueConverter.view(this.appointment.appointmentDate) : undefined
    this.appointmentDate = this.appointment.appointmentDate? this.dateConverter.form(this.appointment.appointmentDate) : undefined
    this.modal.showModal('#appointment')
  }

  initServices()
  {
    debugger
    this.serviceService.ServicesAsync(this.dboservices).then(response => response.json()).then(data => {
        debugger
      this.dboservices = data;
      this.initBranches()
    })
  }

  initBranches()
  {
    this.storeservice.getBranches().then(response => response.json()).then(async data => {
      this.branches = data
    });
  }

  saveAppointment()
  {
    debugger
    this.isBusy = true
    this.appointment.appointmentDate = this.appointmentDate + 'T' + this.appointmentTime;
    this.appService.SaveAppointmentAsync(this.appointment).then(response => response.json()).then(data => {
      debugger
      this.isBusy = false
      if(this.appointment.status == 3)
        this.viewInvoice();   
      else
        this.modal.hideModal("#appointment")   
    })
  }

  searchUsers(name)
  {
    debugger
    this.userService.searchUsers(name).then(response => response.json()).then(async data => {
      this.customers = data
    });
  }

  onUserSelected(user)
  {
    debugger
    this.customers = undefined
    this.user = user.firstName + " " + user.lastName
    this.appointment.user = user
  }

  isServiceChecked(service)
  {
    debugger
    let isChecked = this.appointment.services.find(x => x.serviceId == service.id && x.deleted != true) != undefined
    return isChecked;
  }

  initService(appointmentservice)
  {
    this.appointmentservice = appointmentservice
    this.message = { title : "Warning !!!", message : "Are you sure about deleting this service?"}
    this.modal.showModal("#warning")
  }
  
  deleteService()
  {
    debugger
    this.appointmentservice.deleted = true
  }

  onServiceSelected(service)
  {
    debugger
    this.appointmentservice = { serviceId : service.id, service : service }
    this.stylists = this.dboStaff.accounts.filter((account) => service.accountBranchServices.find(x => x.accountId == account.id))
    this.modal.showModal("#appointmentservice")

    return true
  }

  addService()
  {
    debugger
    this.appointmentservice.stylist = this.dboStaff.accounts.find(a => a.id == this.appointmentservice.stylistId)
    if(this.appointmentservice.stylist.seat)
      this.appointmentservice.seatId = this.appointmentservice.stylist.seat.id

    this.appointment.services.push(this.appointmentservice)
  }

  viewInvoice()
  {
    debugger
    this.invoice.getAppointmentOrder(this.appointment.id)
    this.modal.hideModal("#appointment")
  }

  createInvoice()
  {
    debugger
    this.order = { 
                  id : this.guid.create(),
                  orderType : 1, 
                  status : 0, 
                  orderDate : new Date(), 
                  userId : this.appointment.userId,
                  branchId : this.appointment.branchId,
                  appointmentId : this.appointment.id,
                  orderItems : [],
                  orderTotal : 0
                 }

    this.appointment.services.forEach(appointmentservice => {
      debugger
      let orderItem = this.order.orderItems.find(x => x.serviceId == appointmentservice.serviceId)
      if(orderItem)
      {
        orderItem.quantity += 1
      }
      else
      {
        orderItem = {
                      name : appointmentservice.service.name,
                      serviceId : appointmentservice.serviceId,
                      quantity : 1,
                      price : appointmentservice.service.saleItem.price
                    }
        this.order.orderItems.push(orderItem)
      }
      this.order.orderTotal += orderItem.price
    });

    this.saveOrder()
  }

  saveOrder()
  {
    debugger
    this.isBusy = true
    this._orderService.save(this.order).then(response => response.json()).then(data => {
        debugger
        this.isBusy = false
        if(data != true)
            this.error = data
        else
        {
          this.appointment.status = 3
          this.saveAppointment();
        }
    }).catch(error => {
        debugger
        this.isBusy = false
        this.error = error
    })
  }

  goToPage(page)
  {
    debugger
    this.dboservices.page = page;
    this.dboservices.services = undefined
    this.dboservices.from = (this.dboservices.from == "")? undefined : this.dboservices.from
    this.dboservices.to = (this.dboservices.to == "")? undefined : this.dboservices.to

    this.initServices()
  }

  next()
  {
    debugger
    if(this.dboservices.page < this.dboservices.pageCount)
    {
      this.dboservices.page += 1;
      this.dboservices.services = undefined
      this.dboservices.from = (this.dboservices.from == "")? undefined : this.dboservices.from
      this.dboservices.to = (this.dboservices.to == "")? undefined : this.dboservices.to
      this.initServices()
    }
  }

  previous()
  {
    if(this.dboservices.page > 1)
    {
      this.dboservices.page -= 1;
      this.dboservices.services = undefined
      this.dboservices.from = (this.dboservices.from == "")? undefined : this.dboservices.from
      this.dboservices.to = (this.dboservices.to == "")? undefined : this.dboservices.to
      this.initServices()
    }
  }
}
