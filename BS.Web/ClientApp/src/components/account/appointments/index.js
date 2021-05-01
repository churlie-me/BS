import { AppointmentService } from "../../../services/appointment.service";
import { AuthenticationService } from "../../../services/authentication.service";
import { inject } from 'aurelia-framework';
import { ModelDialog } from "../../../extensions/modal.dialog";
import { DateConverter } from "../../../extensions/date.converter";
import { TimeValueConverter } from "../../../extensions/time.converter";
import { StoreService } from "../../../services/store.service";
import { InvoiceModule } from "../../admin/invoice/invoice.module";

@inject(AuthenticationService, AppointmentService, ModelDialog, DateConverter, TimeValueConverter, StoreService, InvoiceModule)
export class Index
{
    _authenticationService
    _appointmentService
    _modalDialog
    _dateConverter
    timeValueConverter
    _storeService
    dboAppointments
    _invoice
    statuses = [ {code: 0, name : 'Reserved'}, {code : 1, name : 'InProgress'}, {code: 2, name : 'Complete'},{code: 3, name :'Invoinced'} ]
    constructor(_authenticationService, _appointmentService, _modalDialog, _dateConverter, _timeValueConverter, _storeService, _invoice)
    {
        this._authenticationService = _authenticationService
        this._appointmentService = _appointmentService
        this._modalDialog = _modalDialog
        this.dateConverter = _dateConverter
        this.timeValueConverter = _timeValueConverter
        this._storeService = _storeService
        this._invoice = _invoice
        this.dboAppointments = 
        {
          page : 1,
          customerId: this._authenticationService.account.user.id
        }
        this.initAppointments()
    }

    initAppointments()
    {
        debugger
        this._appointmentService.SortedAppointmentsAsync(this.dboAppointments).then(response => response.json()).then(data => {
            debugger
        this.dboAppointments = data;
        })
    }

  goToPage(page)
  {
    debugger
    this.dboAppointments.page = page;
    this.dboAppointments.appointments = undefined

    this.initAppointments()
  }

  next()
  {
    if(this.dboAppointments.page < this.dboAppointments.pageCount)
    {
      this.dboAppointments.page += 1;
      this.dboAppointments.appointments = undefined
      this.initAppointments()
    }
  }

  previous()
  {
    if(this.dboAppointments.page > 1)
    {
      this.dboAppointments.page -= 1;
      this.dboAppointments.appointments = undefined
      this.initAppointments()
    }
  }

  initAppointment(appointment)
  {
    debugger
    this.appointment = appointment
    this.appointmentTime = this.appointment.appointmentDate? this.timeValueConverter.view(this.appointment.appointmentDate) : undefined
    this.appointmentDate = this.appointment.appointmentDate? this.dateConverter.form(this.appointment.appointmentDate) : undefined
    this._modalDialog.showModal("#_appointment")
  }

  viewInvoice()
  {
    debugger
    this._invoice.getAppointmentOrder(this.appointment.id)
    this.modal.hideModal("#appointment")
  }
}
