import { inject, BindingEngine } from 'aurelia-framework';
import { ProductService } from '../../../services/product.service';
import { Router } from 'aurelia-router';
import { StoreService } from '../../../services/store.service';
import { DateConverter } from '../../../extensions/date.converter';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';
import { HolidayService } from '../../../services/holiday.service';

@inject(Router, StoreService, ModelDialog, DateConverter, ProgressDialog, HolidayService)
export class Index {
  holidays
  storeId
  router
  _storeService
  _modalDialog
  holiday
  dateConverter
  holidayService
  constructor(_router, storeService, modalDialog, dateConverter, progressDialog, holidayService) {
    this.router = _router
    this._storeService = storeService
    this._modalDialog = modalDialog
    this.progressDialog = progressDialog
    this.dateConverter = dateConverter
    this.holidayService = holidayService
    debugger        
  }

  activate(params) {
    debugger
    this.dboholiday = { page : 1 }
    this.initHolidays()
  }

  onInitHoliday(holiday)
  {
    debugger
    if(holiday)
    {
      this.holiday = holiday
      this.holiday.from =  this.dateConverter.form(this.holiday.from)
      this.holiday.to =  this.dateConverter.form(this.holiday.to)
    }
    else
    {
      this.holiday = { storeId: this.storeId }
    }
  }

  initHolidays()
  {
    debugger
    this._storeService.getStoreHolidays(this.dboholiday).then(response => response.json()).then(data => {
        debugger
      this.dboholiday = data;
    })
  }

  deleteHoliday()
  {
    this.holiday.deleted = true
    this.onSubmitHoliday()
  }

  onSubmitHoliday()
  {
    debugger
    this.isBusy = true
    this.holidayService.save(this.holiday).then(response => response.json()).then(data => {
      this.isBusy = false
      this._modalDialog.hideModal("#_holiday")
      this._modalDialog.hideModal("#warning")
      this.initHolidays()
        debugger
    })
  }

  goToPage(page)
  {
    debugger
    this.dboholiday.page = page;
    this.dboholiday.holidays = undefined
    this.dboholiday.from = (this.dboholiday.from == "")? undefined : this.dboholiday.from
      this.dboholiday.to = (this.dboholiday.to == "")? undefined : this.dboholiday.to
    this.initHolidays()
  }

  next()
  {
    debugger
    if(this.dboholiday.page < this.dboholiday.pageCount)
    {
      this.dboholiday.page += 1;
      this.dboholiday.holidays = undefined
      this.dboholiday.from = (this.dboholiday.from == "")? undefined : this.dboholiday.from
      this.dboholiday.to = (this.dboholiday.to == "")? undefined : this.dboholiday.to
      this.initHolidays()
    }
  }

  previous()
  {
    if(this.dboholiday.page > 1)
    {
      this.dboholiday.page -= 1;
      this.dboholiday.from = (this.dboholiday.from == "")? undefined : this.dboholiday.from
      this.dboholiday.to = (this.dboholiday.to == "")? undefined : this.dboholiday.to
      this.initHolidays()
    }
  }
}
