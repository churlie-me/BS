import { View, inject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { DateConverter } from "../../extensions/date.converter";
import { InfoDialog } from "../../extensions/info/info.dialog";
import { TimeValueConverter } from "../../extensions/time.converter";
import { AppointmentService } from "../../services/appointment.service";
import { AuthenticationService } from "../../services/authentication.service";
import { ServiceService } from "../../services/service.service";
import { StoreService } from "../../services/store.service";
import { ServiceTypeService } from "../../services/type.service";

@inject(ServiceService, StoreService, AppointmentService, Router, AuthenticationService, TimeValueConverter, DateConverter, ServiceTypeService, InfoDialog)
export class Reservation {
    nxtBtn
    prevBtn
    branch
    isBusy = false
    currentPage = 0
    service
    time
    account
    user
    stylist
    storeId
    stores
    branches
    store
    _timeValueConverter
    times
    AppointmentDate
    _serviceService
    _storeService
    _dateConverter
    _appointmentService
    _router
    genders
    _authenticationService
    _infoDialog
    constructor(serviceService, storeService, appointmentService, router, _authenticationService, _timeValueConverter, _dateConverter, _typeService, _infoDialog) {
        debugger
        this._serviceService = serviceService
        this._storeService = storeService
        this._appointmentService = appointmentService
        this._router = router
        this._authenticationService = _authenticationService
        this._timeValueConverter = _timeValueConverter
        this._dateConverter = _dateConverter
        this._typeService = _typeService
        this._infoDialog = _infoDialog
    }


    attached()
    {
      if(this._storeService.store)
        {
          if(this._storeService.store.schedules.length > 0)
            this.initBranches()
          else
            this.msg = { title : "Info", message : "De winkelinstellingen zijn nog niet ingesteld. Neem contact op met het management voor hulp" }
        }
        else
        {
          this.msg = { title : "Info", message : "De winkelinstellingen zijn nog niet ingesteld. Neem contact op met het management voor hulp" }
        }

      if(this.msg)
        this._infoDialog.show()
    }

    segmentTime()
    {
      debugger
      this.day = this._storeService.store.schedules.find(x => x.day == new Date(this.AppointmentDate).getDay())
      let segment = this.day.openAt
      this.times = []
      if(this.day.status == 0)
      {
        while(new Date(segment).getHours() <= new Date(this.day.closedAt).getHours())
        {
          if(new Date(segment).getHours() == new Date(this.day.closedAt).getHours())
          {
            if(new Date(segment).getMinutes() < new Date(this.day.closedAt).getMinutes())
            {
              this.times.push({ interval :  this._timeValueConverter.view(segment), dateTime : this.AppointmentDate })
            }
          }
          else
          {
            this.times.push({ interval :  this._timeValueConverter.view(segment), dateTime : this.AppointmentDate })
          }

          segment = new Date(segment).setMinutes(new Date(segment).getMinutes() + 10)
        }

        if(this.stylist)
        {
          this.availableslots = undefined
          this._appointmentService.CheckStylistAvailability({ stylistId : this.stylist.accountId, appointmentTimes : this.times }).then(response => response.json()).then(async data => {
            this.availableslots = data.appointmentTimes
          });
        }
        else
          this.availableslots = this.times
      }
    }

    initTypes()
    {
        debugger
        this.dbotype = { page : 1, pageSize: 200, forReservation : true }
        this._typeService.TypesAsync(this.dbotype).then(response => response.json()).then(data => {
            debugger
            this.types = data.types;
            this.init()
        })
    }

    initBranches()
    {
      this._storeService.getBranches().then(response => response.json()).then(async data => {
        this.branches = data
        if(this.branches.length == 1)
          this.branch = this.branches[0]
        this.initTypes()
      });
    }

    onBranchSelected(branch)
    {
      this.branch = branch
      this.branches.forEach(s => {s.active = ''})
      this.branch.active = 'active'
    }

    init()
    {
        debugger

        this.fixStepIndicator(0)

        //auto select branch if only one exists for a single store
        if(this._storeService.store.branches.length == 1)
          this.branch = this._storeService.store.branches[0];

        //Calendar Control
        $('#datepicker').datepicker({
            todayHighlight: true
        });
    }

    showTab(n) {
      debugger
      
      this.nxtBtn = document.getElementById("nextBtn")
        this.prevBtn = document.getElementById("prevBtn")
        // This function will display the specified tab of the form...
        var x = document.getElementsByTagName("fieldset")
        x[n].style.display = "block";
        //... and fix the Previous/Next buttons:
        if (n == (x.length - 2)) {
          this.nxtBtn.innerHTML = "Verzenden";
        }
        else if (n == (x.length - 1))
        {
          this.prevBtn.style.display = "none";
          this.nxtBtn.innerHTML = "Gedaan";
        } 
        else {
          this.nxtBtn.innerHTML = "De volgende";
        }
        //... and run a function that will display the correct step indicator:
        this.fixStepIndicator(n)
      }

      fixStepIndicator(n) {
        // This function removes the "active" class of all steps...
        var i, x = document.getElementsByClassName("step")
        //... and adds the "active" class on the current step:
        x[n].className += " active";
      }

      nextPrev(n) 
      {
        debugger
        if(this.branches.length > 1) //More pages incase we need to choose a branch for more than one branches
          switch(this.currentPage)
          {
            case 0: //page 1
              if(this.type)
              {
                this.navigatePage(n)
              }
              else
              {
                this.msg = { title : "Waarschuwing", message : "Selecteer een type service om door te gaan" }
                this._infoDialog.show()
              }
            break;
            case 1: //page 2
            this.navigatePage(n)
            break;
            case 2: //page 3
              if(!this.service)
              {
                this.msg = { title : "Waarschuwing", message : "Selecteer een service om door te gaan" }
                this._infoDialog.show()
                return
              }
              this.navigatePage(n)
            break;
            case 3: //page 4
            this.navigatePage(n)
            break;
            case 4: //page 5
              if(n > 0)
              {
                if(!$('#datepicker').datepicker('getDate'))
                {
                  this.msg = { title : "Waarschuwing", message : "Selecteer een afspraakdatum om door te gaan" }
                  this._infoDialog.show()
                  return
                }

                this.AppointmentDate = moment($('#datepicker').datepicker('getDate')).format('YYYY-MM-DD');
                this.segmentTime()
              }
              this.navigatePage(n)
            break;
            case 5: //page 6
              if(n > 0)
                if(!this.slot)
                {
                  this.msg = { title : "Waarschuwing", message : "Selecteer een tijdvak voor uw afspraak" }
                  this._infoDialog.show()
                  return
                }
              this.navigatePage(n)
            break;
            case 6: //page 7
            this.navigatePage(n)
            break;
            case 7: //page 8
              if(n >= 0)
                this.SubmitAppointment()
              else
                this.navigatePage(n)
              return
            break;
            default:
              debugger
              this.branch.active = ''
              this.branch = undefined
              this.service.active = ''
              this.service = undefined
              this.slot.active = ''
              this.slot = undefined
              this.stylist.active = ''
              this.stylist = undefined
              this.type.active = ''
              this.type = undefined
              var x = document.getElementsByTagName("fieldset")
              x[this.currentPage].style.display = "none";
              this.currentPage = 0
              this.showTab(this.currentPage)

              debugger
              var steps = document.getElementsByClassName("step")
              for (let w = 0; w < steps.length; w++) {
                let step = steps[w];
                if(w == 0)
                  step.className = "step active"
                else
                  step.className = "step"
              }
          }
        else
          // Exit the function if any field in the current tab is invalid:
          switch(this.currentPage)
          {
            case 0: //page 1
              if(this.type)
              {
                this.navigatePage(n)
              }
              else
              {
                this.msg = { title : "Waarschuwing", message : "Selecteer een type service om door te gaan" }
                this._infoDialog.show()
              }
            break;
            case 1: //page 2
              if(n > 0)
                if(!this.service)
                {
                  this.msg = { title : "Waarschuwing", message : "Selecteer een service om door te gaan" }
                  this._infoDialog.show()
                  return
                }
              this.navigatePage(n)
            break;
            case 2: //page 3
            this.navigatePage(n)
            break;
            case 3: //page 4
              if(n > 0)
              {
                if(!$('#datepicker').datepicker('getDate'))
                {
                  this.msg = { title : "Waarschuwing", message : "Selecteer een afspraakdatum om door te gaan" }
                  this._infoDialog.show()
                  return
                }
                this.AppointmentDate = moment($('#datepicker').datepicker('getDate')).format('YYYY-MM-DD');
                this.segmentTime()
              }
              this.navigatePage(n)
            break;
            case 4: //page 5
              if(n > 0)
                if(!this.slot)
                {
                  this.msg = { title : "Waarschuwing", message : "Selecteer een tijdvak voor uw afspraak" }
                  this._infoDialog.show()
                  return
                }
              this.navigatePage(n)
            break;
            case 5: //page 6
            this.navigatePage(n)
            break;
            case 6: //page 7
            if(n >= 0)
              this.SubmitAppointment()
            else
              this.navigatePage(n)
              return
            break;
            default:
              this.branch.active = ''
              this.branch = undefined
              this.service.active = ''
              this.service = undefined
              this.slot.active = ''
              this.slot = undefined
              this.stylist.active = ''
              this.stylist = undefined
              this.type.active = ''
              this.type = undefined
              var x = document.getElementsByTagName("fieldset")
              x[this.currentPage].style.display = "none";
              this.currentPage = 0
              this.showTab(this.currentPage)

              debugger
              var steps = document.getElementsByClassName("step")
              for (let v = 0; v < steps.length; v++) {
                let _step = steps[v];
                if(v == 0)
                  _step.className = "step active"
                else
                  _step.className = "step"
              }
              break;
          }
    }

    navigatePage(n)
    {
      // This function will figure out which tab to display
      var x = document.getElementsByTagName("fieldset")
      // Hide the current tab:
      x[this.currentPage].style.display = "none";
      // Increase or decrease the current tab by 1:
      this.currentPage = this.currentPage + n;
      // if you have reached the end of the form...
      if (this.currentPage >= x.length) {
        // ... the form gets submitted:
        return false;
      }
      // Otherwise, display the correct tab:
      this.showTab(this.currentPage);
    }

    checkAvailabilty()
    {
      
    }

    onTypeSelected(type)
    {
      debugger
      this.type = type
      this.types.forEach(s => {s.active = ''})
      this.type.active = 'active'

      this.dboservices = 
      {
        page : 1,
        typeId : this.type.id
      }
      this.initServices()
    }

    initServices() {
      debugger
      
      this._serviceService.ServicesAsync(this.dboservices).then(response => response.json()).then(data => {
          //this.busyIndicator.off();
          debugger
          this.dboservices = data;
      })
    }

    initStores()
    {
        debugger
        this._storeService.StoresAsync().then(response => response.json()).then(data => {
          //this.busyIndicator.off();
            debugger
          this.stores = data;
          if(this.stores.length == 1)
            this.onSelectStore(this.stores[0])
        })
    }

    onSelectStore(store)
    {
      debugger
      this._storeService.store = store
      localStorage.setItem('store', JSON.stringify(store))
      this.initServices();
    }

    onServiceSelected(id) {
      debugger
        this.service = this.dboservices.services.find(s => s.id == id)
        this.dboservices.services.forEach(s => {s.active = ''})
        this.service.active = 'active'
    }

    onNoPreferenceSelected(event)
    {
      debugger
      if(this.stylist)
        this.stylist.active = ''
      this.stylist = undefined
    }

    onStylistSelected(stylist)
    {
      debugger
      this.stylist = this.service.accountBranchServices.find(s => s.accountId == stylist.accountId && s.serviceId == stylist.serviceId && s.storeId == stylist.storeId)
      this.service.accountBranchServices.forEach(s => {s.active = ''})
      this.stylist.active = 'active'
    }

    onTimeSelected(interval)
    {
      debugger
      this.slot = this.availableslots.find(t => t.interval == interval)
      this.availableslots.forEach(t => {t.active = ''})
      this.slot.active = 'active'

      //this.AppointmentDate = this._dateConverter.form($('#datepicker').datepicker('getDate')).toDateString()
      //this.AppointmentDate = new Date(this.AppointmentDate)
      this.attachSelectedTime()
    }

    attachSelectedTime()
    {
      debugger
      /*let parts = this.slot.interval.match(/(\d+)\:(\d+)/);
      let hours = /am/i.test(parts[3]) ? parseInt(parts[1], 10) : parseInt(parts[1], 10) + 12;
      let minutes = parseInt(parts[2], 10);

      this.AppointmentDate.setHours(hours);
      this.AppointmentDate.setMinutes(minutes);*/

      this.AppointmentDate += 'T' + this.slot.interval
    }

    SubmitAppointment()
    {
      debugger
      this.isBusy = true
      let appointmentservice = {
        serviceId : this.service.id
      }

      if(this.stylist)
      {
        appointmentservice.stylistId = this.stylist.accountId
        appointmentservice.seatId = this.stylist.account.seat.id
      }
      let appointment = {
        appointmentDate: this.AppointmentDate,
        services : [appointmentservice],
        status : 0,
        user: this._authenticationService.account.user,
        branchId: this.branch.id
      }


      if(this.stylist)
        if(this.stylist.account.seat)
          appointment.seatId = this.stylist.account.seat.id

      this._appointmentService.SaveAppointmentAsync(appointment).then(response => response.json()).then(async data => {
        debugger
        this.isBusy = false
        await new Promise(resolve => setTimeout(resolve, 1000))
        this.navigatePage(1)
      })
    }

    startOver()
    {
      this.currentPage = 0
      this.navigatePage(this.currentPage)
    }

    goToPage(page)
  {
    debugger
    this.dboservices.page = page;
    this.initServices()
  }

  next()
  {
    if(this.dboservices.page < this.dboservices.pageCount)
    {
      this.dboservices.page += 1;
      this.initServices()
    }
  }

  previous()
  {
    if(this.dboservices.page > 1)
    {
      this.dboservices.page -= 1;
      this.initServices()
    }
  }
}
