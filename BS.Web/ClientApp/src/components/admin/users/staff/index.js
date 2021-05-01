import {inject, bindable} from 'aurelia-framework';
import { InfoDialog } from '../../../../extensions/info/info.dialog';
import { ModelDialog } from "../../../../extensions/modal.dialog";
import { ProgressDialog } from "../../../../extensions/progress/progress.dialog";
import { AuthenticationService } from '../../../../services/authentication.service';
import { ServiceService } from '../../../../services/service.service';
import { StoreService } from '../../../../services/store.service';
import { UserService } from "../../../../services/user.service";
import { Router } from 'aurelia-router';
import { HolidayService } from '../../../../services/holiday.service';
import { DateConverter } from '../../../../extensions/date.converter';

@inject(HolidayService, UserService, ServiceService, StoreService, AuthenticationService, ProgressDialog, ModelDialog, InfoDialog, Router, DateConverter)
export class Index
{
  account
  title = "Message"
  message = "Staff changes saved"
  routers
  isBusy = false
  dboservices
  progressDialog
  constructor(holidayService, userService, serviceService, storeService, authenticationService, progressDialog, modalDialog, infoDialog, router, dateConverter)
  {
    this.userService = userService
    this.progressDialog = progressDialog
    this.modalDialog = modalDialog
    this.serviceService = serviceService
    this.storeService = storeService
    this.infoDialog = infoDialog
    this.authenticationService = authenticationService
    this.holidayService = holidayService
    this.router = router
    this.dateConverter = dateConverter
  }

  activate(params) {
    debugger
    this.id = params.id;
    if(this.id)
    {
      this.getStaff(this.id)
      this.getHolidays()
    }

    this.dboservices = {
      page : 1
    }
    this.initServices()
  }

  isChecked(service)
  {
    debugger
    let affiliated = this.account.accountBranchServices.find(x => x.service.id == service.id)
    if(affiliated)
      return true
    else
      return false
  }

  initServices()
  {
    debugger
    this.serviceService.ServicesAsync(this.dboservices).then(response => response.json()).then(data => {
      //this.busyIndicator.off();
        debugger
      this.dboservices = data;
    })
  }

  onServiceSelected(service)
  {
    debugger
    if(!this.account.accountBranchServices)
      this.account.accountBranchServices = []

    if(!this.account.accountBranchServices.find(x => x.serviceId == service.id))
      this.account.accountBranchServices.push({ branchId: this.branchId, accountId: this.account.id, serviceId: service.id, service: service })

    return true;
  }

  getStaff()
  {
    debugger
    this.userService.getStaff(this.id).then(response => response.json()).then(data => {
        debugger
      this.account = data;
      if(this.account.accountBranchServices.length > 0)
        this.branchId = this.account.accountBranchServices[0].branchId

      if(this.account.branchId)
      {
        this.branchId = this.account.branchId
        this.getBranchSeats(this.account.branchId)
      }
    })
  }

  onInitHoliday(holiday)
  {
    debugger
    if(holiday)
    {
     this.holiday = holiday
     this.holiday.from = this.dateConverter.form(this.holiday.from)
     this.holiday.to = this.dateConverter.form(this.holiday.to)
    }
    else
     this.holiday = undefined
  }

  getHolidays()
  {
    this.holidayService.HolidaysAsync(this.id).then(response => response.json()).then(data => {
      debugger
      this._holidays = data;
    })
  }

  onSaveHoliday()
  {
    debugger
    this.holiday.accountId = this.account.id
    this.isBusy = true
    this.holidayService.save(this.holiday).then(response => response.json()).then(data => {
      debugger
      this.modalDialog.hideModal("#_holiday");
      this.isBusy = false
      this._holidays = undefined
      this.getHolidays()
    })
  }

  onDeleteAffiliatedService()
  {
    this.abs.deleted = 1;
    this.modalDialog.hideModal("#_deleteService")
  }

  initService(abs)
  {
    this.abs = abs
  }

  onDeleteHoliday()
  {
    this.holiday.deleted = 1
    this.onSaveHoliday()
    this.modalDialog.hideModal("#_deleteHoliday");
  }

  getBranchSeats(branchId)
    {
      debugger
      let branch = this.storeService.store.branches.find(b => b.id == branchId)
      this.account.branch = branch
      if(branch)
        this.storeService.getBranchSeats(branch.id).then(response => response.json()).then(data => {
          debugger
          this.seats = data;
        })
        .catch(error => {
            debugger
            let e = error
        })
    }

    onSave()
  {
    debugger
    if(!this.branchId)
    {
      this.message = {title : "Error", message: "No branch selected attached"}
      this.infoDialog.show()
      return
    }

    if(this.account.seat)
    {
      if(this.account.seat.branchId == undefined)
        this.message = {title : "Error", message: "No branch affiliated to staff"}
      else if(this.account.seat.id == undefined)
        this.message = {title : "Error", message: "No seat attached to staff"}
      
      if(this.message == undefined)
      {
        this.infoDialog.show()
        return
      }
    }

    this.progressDialog.show()

    this.authenticationService.update(this.account).then(response => response.json()).then(data => {
        debugger
        this.progressDialog.hide()
    }).catch(error => {
        debugger
        this.progressDialog.hide()
        this.title = "Ërror"
        this.message = error
        this.infoDialog.show()
    })
  }

  goToPage(page)
  {
    debugger
    this.dboservices.page = page;
    this.dboservices.services = undefined

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
