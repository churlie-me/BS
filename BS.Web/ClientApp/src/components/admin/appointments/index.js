import { inject, BindingEngine } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';
import { AppointmentService } from '../../../services/appointment.service';
import { AuthenticationService } from '../../../services/authentication.service';
import { ReasonService } from '../../../services/reason.service';
import { RequestService } from '../../../services/request.service';
import { DateConverter } from '../../../extensions/date.converter';
import { AppointmentModule } from './appointment/appointment.module';
import { UserService } from '../../../services/user.service';
import { StoreService } from '../../../services/store.service';

@inject(Router, AppointmentService, RequestService, ReasonService, ModelDialog, ProgressDialog, AuthenticationService, DateConverter, AppointmentModule, UserService, StoreService)
export class Index {
  dboAppointments
  appointment
  reasons
  request
  employees
  storeId
  _router
  _appointmentService
  _requestService
  _reasonService
  _modalDialog
  _progressDialog
  _authentication
  dateConverter
  isBusy = false
  statuses = [ {code: 0, name : 'Reserved'}, {code : 1, name : 'InProgress'}, {code: 2, name : 'Complete'},{id: 3, name :'Invoinced'} ]
  constructor(router, appointmentService, _requestService, _reasonService, _modalDialog, _progressDialog, _authentication, dateConverter, appmodule, userService, _storeService) {
      debugger        
      this._router = router
      this._appointmentService = appointmentService
      this._requestService = _requestService
      this._reasonService = _reasonService
      this._modalDialog = _modalDialog
      this._authentication = _authentication
      this._progressDialog = _progressDialog
      this.dateConverter = dateConverter
      this.appmodule = appmodule
      this.userService = userService
      this._storeService = _storeService
  }

  activate(params) {
    debugger
    this.storeId = params.id;
    this.dboAppointments = {
      page : 1
    }
    this.dboStaff = { page:1, pageSize : 50 }
    
    if(this._authentication.account.type == 2)
      this.dboAppointments = { stylistId : this._authentication.account.id }

    this.initAppointments()
    this.initReasons()
    this.initStaff()
  }

  initAppointments()
  {
    debugger
    this._appointmentService.SortedAppointmentsAsync(this.dboAppointments).then(response => response.json()).then(data => {
        debugger
      this.dboAppointments = data;

      if(this.dboAppointments.from!="0001-01-01T00:00:00" && this.dboAppointments.to!="0001-01-01T00:00:00")
      {
        this.dboAppointments.from =  this.dateConverter.form(this.dboAppointments.from)
        this.dboAppointments.to =  this.dateConverter.form(this.dboAppointments.to)
      }
    })
  }

  initStaff()
  {
    debugger
    this.userService.getUsers(this.dboStaff).then(response => response.json()).then(data => {
        debugger
      this.dboStaff = data;
    })
  }

  isActive(index, page)
  {
    debugger
    let active = (index == page)
    return active
  }

  goToPage(page)
  {
    debugger
    this.dboAppointments.page = page;
    this.dboAppointments.appointments = undefined
    this.dboAppointments.from = (this.dboAppointments.from == "")? undefined : this.dboAppointments.from
    this.dboAppointments.to = (this.dboAppointments.to == "")? undefined : this.dboAppointments.to
    this.initAppointments()
  }

  next()
  {
    debugger
    if(this.dboAppointments.page < this.dboAppointments.pageCount)
    {
      this.dboAppointments.page += 1;
      this.dboAppointments.appointments = undefined
      this.dboAppointments.from = (this.dboAppointments.from == "")? undefined : this.dboAppointments.from
      this.dboAppointments.to = (this.dboAppointments.to == "")? undefined : this.dboAppointments.to
      this.initAppointments()
    }
  }

  previous()
  {
    if(this.dboAppointments.page > 1)
    {
      this.dboAppointments.page -= 1;
      this.dboAppointments.appointments = undefined
      this.dboAppointments.from = (this.dboAppointments.from == "")? undefined : this.dboAppointments.from
      this.dboAppointments.to = (this.dboAppointments.to == "")? undefined : this.dboAppointments.to
      this.initAppointments()
    }
  }

  initReasons()
  {
    this._reasonService.ReasonsAsync(this.storeId).then(response => response.json()).then(data => {
      debugger
      this.reasons = data;
    })
  }

  requestCustomerContact(appointment)
  {
    debugger
    this.appointment = appointment
    this.request = { AccountId : this._authentication.account.id, StoreId: this.storeId, UserId: this.appointment.user.id, RequestedOn: new Date()}
  }

  onSaveReason()
  {
    debugger
    this.isBusy = true
    this._requestService.Save(this.request).then(response => response.json()).then(async data => {
      debugger
      this.isBusy = false
      this._modalDialog.hideModal("#contact-request");
      await new Promise(resolve => setTimeout(resolve, 1000));
      this._modalDialog.showModal("#customer-contact");
    })
    .catch(error => {
      debugger
      this.isBusy = false
    })
  }

  viewAppointment(appointment)
  {
    debugger
    this.appointment = appointment
    this.appmodule.appointment = this.appointment
    this.appmodule.dboStaff = this.dboStaff
    this.appmodule.initiateAppointment()
  }

  onFilterApplied()
  {
    debugger
    this.dboAppointments.appointments = undefined
    this.dboAppointments.from = (this.dboAppointments.from == "")? undefined : this.dboAppointments.from
    this.dboAppointments.to = (this.dboAppointments.to == "")? undefined : this.dboAppointments.to
    this.initAppointments()
  }

  onSave()
  {
    debugger
    this.isBusy = true
    this._appointmentService.SaveAppointmentAsync(this.appointment).then(response => response.json()).then(data => {
      debugger
      this.isBusy = false
      if(data)
        this._modalDialog.hideModal("#_appointment")
    });
  }
}
