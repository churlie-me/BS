import { inject, bindable} from 'aurelia-framework';
import '../../../../node_modules/jquery/dist/jquery.js';
import '../../../../node_modules/jquery-ui/ui/widgets/draggable.js';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';
import { AppointmentService } from '../../../services/appointment.service';
import { StoreService } from '../../../services/store.service';
import { UserService } from '../../../services/user.service';
import { Router } from "aurelia-router";
import { ModelDialog } from '../../../extensions/modal.dialog';
import { AuthenticationService } from '../../../services/authentication.service.js';
import { AppointmentModule } from '../appointments/appointment/appointment.module.js';
@inject(UserService, AppointmentService, StoreService, ProgressDialog, Router, ModelDialog, AuthenticationService, AppointmentModule)
export class Index
{
  appointmentService
  userService
  storeService
  progressDialog
  router
  modalDialog
  source = []
  isActive = true
  serviceProvided = false
  appointment
  authenticationService
  appmodule
  statuses = [ {code: 0, name : 'Reserved'}, {code : 1, name : 'InProgress'}, {code: 2, name : 'Complete'} ]
  constructor(userService, appointmentService, storeService, progressDialog, router, modalDialog, authenticationService, appmodule)
  {
    this.appointmentService = appointmentService
    this.userService = userService
    this.storeService = storeService
    this.progressDialog = progressDialog
    this.router = router
    this.modalDialog = modalDialog
    this.authenticationService = authenticationService
    this.appmodule = appmodule
  }

  activate()
  {
    this.initStaff()
  }

  attached()
  {
    if(this.authenticationService.account.type == 2)
        this.dboAppointments = { stylistId : this.authenticationService.account.id }

    this.initAppointments()
  }

  initStaff()
  {
    this.dboStaff = { page : 1, pageSize: 200 }
    this.userService.getUsers(this.storeService.store.id).then(response => response.json()).then(data => {
      this.dboStaff = data;
    })  
  }

  async initAppointments()
  {
    this.appointmentService.AppointmentsAsync(this.dboAppointments).then(response => response.json()).then(async data => {
      this.dboAppointments = data
      data.appointments.forEach(appointment => {

        let stylist = undefined
        if(appointment.services.length > 0)
        {
          if(appointment.services[0].stylist != undefined)
            stylist = this.source.find(s => s.desc == appointment.services[0].stylist.user.firstName)
          else
            stylist = this.source.find(s => s.desc == 'N/A')
        }
        else
          stylist = this.source.find(s => s.desc == 'N/A')
        
        if(stylist != undefined)//Is the previous appointment by the same stylist
        {
          let value = {
            id: appointment.id,
            from: moment(new Date(appointment.appointmentDate)).valueOf(),
            label: appointment.user.firstName,
            dataObj: appointment.id
          }
          if(appointment.services.length > 0)
            value.to = moment(new Date(appointment.appointmentDate).setMinutes( new Date(appointment.appointmentDate).getMinutes() + appointment.services[0].service.duration )).valueOf()
          
          value.customClass = (appointment.status == 1)? "ganttOrange" : (appointment.status == 2)? "ganttGreen" : "ganttRed"
          stylist.values.push(value)
        }
        else
        {
          let _value = {
            id : appointment.id,
            from: moment(new Date(appointment.appointmentDate)).valueOf(),
            label: appointment.user.firstName,
            dataObj: appointment.id
          }
          if(appointment.services.length > 0)
            _value.to = moment(new Date(appointment.appointmentDate).setMinutes( new Date(appointment.appointmentDate).getMinutes() + appointment.services[0].service.duration )).valueOf()
          
          let event = {
            values: [_value]
          }

          if(appointment.services.length > 0)
          {
            event.id = appointment.services[0].accountId
            event.desc = appointment.services[0].stylist.user.firstName
          }
          else
          {
            event.id = 0
            event.desc = 'N/A'
          }

          debugger
          event.values[0].customClass = (appointment.status == 1)? "ganttOrange" : (appointment.status == 2)? "ganttGreen" : "ganttRed"

          let branch = this.source.find(s => s.name == appointment.branch.name)//Check for existing branch
          if(branch == undefined)
            event.name = appointment.branch.name

          this.source.push(event)
        }
      });
      
      this.initGantt()
    });
  }

  initGantt()
  {
    var offset = new Date().setHours(0, 0, 0, 0) -
    new Date(this.source[0].values[0].from).setDate(100);
    for (var i = 0, len = this.source.length, value; i < len; i++) {
      if(this.source[i].values.length > 0)
      {
        value = this.source[i].values[0];
        value.from += offset;
        value.to += offset;
      }
    }

    let calendarObj = this
    $(".gantt").gantt({
      source: this.source,
      navigate: "scroll",
      scale: "days", 
      maxScale: "months",
      minScale: "hours",
      itemsPerPage: 20,
      scrollToToday: true,
      useCookie: true,
      onItemClick: function(data) {
        debugger
        calendarObj.editAppointment(data)
      },
      onAddClick: function(dt, rowId) {
        debugger  
      },
      onRender: function() {
          if (window.console && typeof console.log === "function") {
              
          }
          /*$('.ganttRed').draggable({
            axis:'y',
            start: function(event, ui, x, y, z) {
              $(this).data("startx", $(this).css('left').split("px")[0]);
              $(this).data("starty", $(this).css('top').split("px")[0]);
            },
            stop: function(event, ui) {
              debugger
              var change = $(this).offset().left - $(this).data("starty");
              var value = $(this).css('margin-top');
              value = value.split("px");
              value = parseInt(value[0]) + change;


              debugger
              var left = parseInt($(this).css('left').split("px")[0]);
              var changex = left - parseInt($(this).data("startx"));
              var top = parseInt($(this).css('top').split("px")[0]);
              top -= top % 24;
              $(this).css('top', top);
              var changey = top - parseInt($(this).data("starty"));
            }
          });*/
      }
    });

    $(".gantt").popover({
      selector: ".bar",
      title: function _getItemText() {
          return this.textContent;
      },
      container: '.gantt',
      content: "Here's some useful information.",
      trigger: "hover",
      placement: "auto right"
    });

    prettyPrint();
  }

  editAppointment(data)
  {
    this.appointment = this.dboAppointments.appointments.find(a => a.id == data)
    //this.modalDialog.showModal("#_appointment");
    this.appmodule.appointment = this.appointment
    this.appmodule.dboStaff = this.dboStaff
    this.appmodule.initiateAppointment()
  }

  navigateScheduler()
  {
    this.router.navigateToRoute('admin');
  }

  activateStylistSelection()
  {
    this.isActive = false
  }

  checkServiceProvision(employeeId)
  {
    this.isBusy = true
    this.userService.getStaff(employeeId).then(response => response.json()).then(data => {
      this.isBusy = false
      this.employee = data;
      this.serviceProvided = this.employee.accountBranchServices.find(a => a.serviceId == this.appointment.serviceId) == undefined
    })  
  }

  checkAvailabilty(event)
  {
    debugger
    let _appointment = this.dboAppointments.appointments.find(a => (a.account.id == event.target.value) && (a.appointmentDate == this.appointment.appointmentDate))
    this.availability = _appointment == undefined
    if(this.availability)
      this.checkServiceProvision(event.target.value)
  }

  onSave()
  {
    debugger
    this.isBusy = true
    this.appointmentService.SaveAppointmentAsync(this.appointment).then(response => response.json()).then(data => {
      debugger
      if(data)
      {
        let event = this.source.find(s => s.id == this.appointment.account.id) //stylist 1 (initial appointment owner)
        let event2 = (this.employee)? this.source.find(s => s.id == this.employee.id) : undefined //stylist 2 (appointment reassigned to)
        let value = undefined
        if(event)
        {
          value = event.values.find(v => v.dataObj == this.appointment.id)
          if(value)
          {
            //Only shift an event from one stylist to another if previous stylist was changed from the appointment
            if(this.appointment.accountId != this.appointment.account.id)
            {
              this.appointment.account = this.employee
              event.values.splice(event.values.indexOf(value), 1)
              if(!event2)
              {
                event2 = {
                            desc : this.appointment.account.user.firstName,
                            id : this.appointment.account.id,
                            values: []
                         }
                event2.values.push(value)
                this.source.push(event2)
              }
              else
                event2.values.push(value)
            }
          }
        }

        this.isBusy = false
        this.modalDialog.hideModal("#_appointment");

        if(this.appointment.status == 2)
          value.customClass = "ganttGreen"
        else if(this.appointment.status == 1)
          value.customClass = "ganttOrange"

        this.initGantt()
      }
    });
  }
}
