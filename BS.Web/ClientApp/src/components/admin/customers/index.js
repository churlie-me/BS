import { inject } from "aurelia-framework";
import { Guid } from "../../../extensions/guid";
import { ModelDialog } from "../../../extensions/modal.dialog";
import { ProgressDialog } from "../../../extensions/progress/progress.dialog";
import { AppointmentService } from "../../../services/appointment.service";
import { AuthenticationService } from "../../../services/authentication.service";
import { OrderService } from "../../../services/order.service";
import { StoreService } from "../../../services/store.service";
import { UserService } from "../../../services/user.service";

@inject(UserService, AppointmentService, ModelDialog, AuthenticationService, Guid, StoreService, ProgressDialog, OrderService)
export class Index
{
  dbocustomers
  _userService
  storeId
  customer
  _modalDialog
  _progressDialog
  _authenticationService
  storeService
  dboAppointments
  _orderService
  appstatuses = ['Reserved', 'InProgress', 'Completed', 'Invoinced']
  statuses = ['Pending', 'Cancelled', 'InProcess', 'Processed', 'Completed']
  constructor(userService, _appointmentService, modalDialog, authenticationService, guid, storeService, _progressDialog, _orderService){
    this._userService = userService
    this._modalDialog = modalDialog
    this._authenticationService = authenticationService
    this.guid = guid
    this._appointmentService = _appointmentService
    this.storeService = storeService
    this._progressDialog = _progressDialog
    this._orderService = _orderService
  }

  activate(params) {
    debugger
    this.dbocustomers
    { 
      page : 1
    }

    this.initUsers()
  }
  
  initUsers()
  {
    debugger
    this._userService.getCustomers(this.dbocustomers).then(response => response.json()).then(data => {
        debugger
      this.dbocustomers = data;
    })
  }

  setCustomer(customer)
  {
    debugger
    this.customer = customer
    this.dboAppointments = { customerId : this.customer.id, page : 1, pageSize: 5 }
    this.initAppointments()

    this.dboOrders = { UserId : this.customer.id, page : 1, pageSize : 5 }
    this.initOrders()
  }

  initAppointments()
  {
    debugger
    this._appointmentService.SortedAppointmentsAsync(this.dboAppointments).then(response => response.json()).then(data => {
        debugger
    this.dboAppointments = data;
    })
  }

  initOrders()
  {
    debugger
    this._orderService.orders(this.dboOrders).then(response => response.json()).then(data => {
        debugger
      this.dboOrders = data;
    })
  }

  appointmentsPrevious()
  {
    if(this.dboAppointments.page > 1)
    {
      this.dboAppointments.page -= 1;
      this.dboAppointments.from = (this.dboAppointments.from == "")? undefined : this.dboAppointments.from
      this.dboAppointments.to = (this.dboAppointments.to == "")? undefined : this.dboAppointments.to
      this.dboAppointments.appointments = undefined
      this.initAppointments()
    }
  }

  ordersPrevious()
  {
    if(this.dboOrders.page > 1)
    {
      this.dboOrders.page -= 1;
      this.dboOrders.from = (this.dboOrders.from == "")? undefined : this.dboOrders.from
      this.dboOrders.to = (this.dboOrders.to == "")? undefined : this.dboOrders.to
      this.dboOrders.appointments = undefined
      this.initOrders()
    }
  }

  appointmentsGoToPage(page)
  {
    debugger
    this.dboAppointments.page = page;
    this.dboAppointments.from = (this.dboAppointments.from == "")? undefined : this.dboAppointments.from
      this.dboAppointments.to = (this.dboAppointments.to == "")? undefined : this.dboAppointments.to
    this.dboAppointments.appointments = undefined

    this.initAppointments()
  }

  ordersGoToPage(page)
  {
    debugger
    this.dboOrders.page = page;
    this.dboOrders.from = (this.dboOrders.from == "")? undefined : this.dboOrders.from
      this.dboOrders.to = (this.dboOrders.to == "")? undefined : this.dboOrders.to
    this.dboOrders.orders = undefined

    this.initOrders()
  }

  appointmentsNext()
  {
    if(this.dboAppointments.page < this.dboAppointments.pageCount)
    {
      this.dboAppointments.page += 1;
      this.dboAppointments.orders = undefined
      this.dboAppointments.from = (this.dboAppointments.from == "")? undefined : this.dboAppointments.from
      this.dboAppointments.to = (this.dboAppointments.to == "")? undefined : this.dboAppointments.to
      this.initAppointments()
    }
  }

  ordersNext()
  {
    if(this.dboOrders.page < this.dboOrders.pageCount)
    {
      this.dboOrders.page += 1;
      this.dboOrders.appointments = undefined
      this.dboOrders.from = (this.dboOrders.from == "")? undefined : this.dboOrders.from
      this.dboOrders.to = (this.dboOrders.to == "")? undefined : this.dboOrders.to
      this.initOrders()
    }
  }

  _search()
  {
    debugger
    if(this.dbocustomers.search == "")
      this.dbocustomers.search = undefined
    this.initUsers()
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
    this.dbocustomers.page = page;
    this.dbocustomers.customers = undefined

    this.initUsers()
  }

  next()
  {
    if(this.dbocustomers.page < this.dbocustomers.pageCount)
    {
      this.dbocustomers.page += 1;
      this.initUsers()
    }
  }

  previous()
  {
    if(this.dbocustomers.page > 1)
    {
      this.dbocustomers.page -= 1;
      this.initUsers()
    }
  }
}
