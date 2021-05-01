import { inject, noView, bindable, bindingMode, customElement, BindingEngine, inlineView } from 'aurelia-framework';
import 'jquery';
import moment from  'moment';
import {fullCalendar} from 'fullcalendar';
import { AuthenticationService } from '../../services/authentication.service';
import { StoreService } from '../../services/store.service';
import { AppointmentService } from '../../services/appointment.service'
import { ProgressDialog } from '../../extensions/progress/progress.dialog';
import { UserService } from '../../services/user.service';
import { ModelDialog } from '../../extensions/modal.dialog';
import { DateConverter } from '../../extensions/date.converter';
import { Router } from "aurelia-router";
import { AppointmentModule } from './appointments/appointment/appointment.module';

@customElement('calendar')
@inject(Element, BindingEngine, AuthenticationService, StoreService, AppointmentService, ProgressDialog, UserService, ModelDialog, DateConverter, Router, AppointmentModule)
export class Index
{
  @bindable weekends = true;
  @bindable dayClick;
  @bindable eventClick;
  @bindable events = [];
  @bindable dboAppointments
  @bindable options;
  @bindable branches;
  @bindable view;
  @bindable authenticationService
  @bindable storeService
  @bindable appointmentService
  @bindable dboStaff
  modalDialog
  dateConverter
  router
  appmodule

  subscription = null;

  constructor(element, bindingEngine, authenticationService, storeService, appointmentService, progressDialog, userService, modalDialog, dateConverter, router, appmodule) {
      debugger
      this.element = element;
      this.bindingEngine = bindingEngine;
      this.authenticationService = authenticationService
      this.storeService = storeService
      this.appointmentService = appointmentService
      this.progressDialog = progressDialog
      this.userService = userService
      this.modalDialog = modalDialog
      this.dateConverter = dateConverter
      this.router = router
      this.appmodule = appmodule
      this.subscription = this.bindingEngine.collectionObserver(this.events).subscribe( (splices) => {this.eventListChanged(splices)});
  }

  activate(params) {
      debugger
      this.dboAppointments = {
        sortOrder : 'appointmentdate',
        sortType : 'asc',
        page : 1
      }

      if(this.authenticationService.account.type == 2)
        this.dboAppointments.stylistId = this.authenticationService.account.id

      this.dboStaff = { page:1, pageSize : 50 }
      this.initStaff()
      this.initBranches()
  }

  onClearFilters()
  {
    debugger
    if(this.dboAppointments)
    {
      this.dboAppointments.branchId = undefined
      this.dboAppointments.stylistId = (this.authenticationService.account.type == 2)? this.authenticationService.account.id : undefined
      this.dboAppointments.from = undefined
      this.dboAppointments.to = undefined
    }
  }

  initStaff()
  {
    debugger
    this.userService.getUsers(this.dboStaff).then(response => response.json()).then(data => {
        debugger
      this.dboStaff = data;
    })
  }

  attached()
  {
    this.initEvents()
    this.initAppointments()
  }

  eventListChanged(splices) {
      debugger
      if(this.calendar)
          this.calendar.fullCalendar('refetchEvents');
  }
    
  eventsChanged(newValue) {
      debugger
      if(this.subscription !== null) {
          this.subscription.dispose();
      }

      this.subscription = this.bindingEngine.collectionObserver(this.events).subscribe( (splices) => {this.eventListChanged(splices)});

      if(this.calendar)
          this.calendar.fullCalendar('refetchEvents');
  }

  initEvents() {
    debugger
    this.calendar = $('.calendar')
    let eventSource = (start, end, timezone, callback) => {
        callback(this.events);
    }

    let defaultValues = {
        defaultView: this.view || 'month',
        handleWindowResize: true,  
        weekends: this.weekends,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        selectable: true,
        firstDay: 1,
        eventClick: (event) => this.eventClicked(event),
        events: eventSource
    }

    this.calendar.fullCalendar(Object.assign(defaultValues, this.options));
  }

  eventClicked(event, jsEvent, view) {
    debugger
    if (event.attribute) {
        if (event.attribute == "disabled") {
            return;
        }
    }
    this.editEvent(event._id);
  
  }

  editEvent(appointment_id)
  {
    this.appointment = this.dboAppointments.appointments.find(a => a.id == appointment_id)
    this.addOrModifyAppointment(this.appointment)
  }

  initBranches()
  {
    this.storeService.getBranches().then(response => response.json()).then(async data => {
      this.branches = data
    });
  }

  async initAppointments()
  {
    debugger
    this.progressDialog.show()
    this.appointmentService.AppointmentsAsync(this.dboAppointments).then(response => response.json()).then(async data => {
      debugger
      this.dboAppointments = data
      if(this.dboAppointments.from!="0001-01-01T00:00:00" && this.dboAppointments.to!="0001-01-01T00:00:00")
      {
        this.dboAppointments.from =  this.dateConverter.form(this.dboAppointments.from)
        this.dboAppointments.to =  this.dateConverter.form(this.dboAppointments.to)
      }
      //this.progressDialog.hide()
      this.events = []
      data.appointments.forEach(appointment => {
        let event = {
          _id: appointment.id,
          title : appointment.user.firstName,
          start: appointment.appointmentDate,
          end: moment(new Date(appointment.appointmentDate).setMinutes( new Date(appointment.appointmentDate).getMinutes() + 18 )).format('YYYY-MM-DDTHH:MM:SS')
        }

        /*appointment.services.forEach(appointmentservice => {
          let event = {
            _id: appointment.id,
            title : appointment.user.firstName,
            start: appointment.appointmentDate,
            end: moment(new Date(appointment.appointmentDate).setMinutes( new Date(appointment.appointmentDate).getMinutes() + appointmentservice.service.duration )).format('YYYY-MM-DDTHH:MM:SS')
          }
        });*/
        this.events.push(event)
      });

      await new Promise(resolve => setTimeout(resolve, 1000));
      this.progressDialog.hide()
    })
  }

  onApplyFilters()
  {
    debugger
    if(this.dboAppointments.from == "")
      this.dboAppointments.from = undefined

    if(this.dboAppointments.to == "")
      this.dboAppointments.to = undefined

    this.initAppointments()
  }

  onSaveAppointment()
  {
    this.modalDialog.hideModal("#_appointment");
    this.progressDialog.show()
    this.appointmentService.SaveAppointmentAsync(this.appointment).then(response => response.json()).then(data => {
      debugger
      this.progressDialog.hide()
    })
  }

  navigateScheduler()
  {
    debugger
    this.router.navigateToRoute('calendar');
  }

  newAppointment()
  {
    debugger
    this.appointment = { services : [] }
    this.addOrModifyAppointment()
  }

  addOrModifyAppointment()
  {
    debugger
    this.appmodule.appointment = this.appointment
    this.appmodule.dboStaff = this.dboStaff
    this.appmodule.initiateAppointment()
  }
}
