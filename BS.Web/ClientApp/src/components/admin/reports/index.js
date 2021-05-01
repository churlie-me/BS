import { inject, bindable, BindingEngine } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { DateConverter } from '../../../extensions/date.converter';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';
import { OrderService } from '../../../services/order.service';
import { SeatService } from '../../../services/seat.service';
import { StoreService } from '../../../services/store.service';

@inject(Router, OrderService, StoreService, ProgressDialog, ModelDialog, DateConverter, SeatService)
export class Index {
  @bindable _dboBranchRevenues
  @bindable _store
  _orderService
  _storeService
  _progressDialog
  _dboProductRevenues
  _dboServiceRevenues
  _dboSeatRevenues
  _dboBranchRevenues
  _seatService
  _dateConverter
  @bindable _datePrinted

  constructor(_router, _orderService, _storeService, _progressDialog, _modalDialog, _dateConverter, _seatService) {
    this._orderService = _orderService
    this._storeService = _storeService
    this._progressDialog = _progressDialog
    this._modalDialog = _modalDialog
    this._dateConverter = _dateConverter
    this._seatService = _seatService
  }

  activate(params, routeConfig, navigationInstruction) {
    this.getSeats()
  }

  getBranchRevenue()
  {
    debugger
    this._datePrinted = this._dateConverter.form(new Date())
    this._orderService.branchRevenueAsync(this.dboReport).then(response => response.json()).then(data => {
      debugger
      this._dboBranchRevenues = data;
    })
  }

  getSeats()
  {
    this._seatService.SeatsAsync().then(response => response.json()).then(data => {
      debugger
      this._seats = data;
    })
  }

  getProductRevenue()
  {
    this._datePrinted = this._dateConverter.form(new Date())
    this._modalDialog.hideModal("#_productRevenue")
    this._progressDialog.show()
    debugger
    this._orderService.productRevenueAsync(this.dboReport).then(response => response.json()).then(data => {
      debugger
      this._dboProductRevenues = data;
      this._progressDialog.hide()

      this._modalDialog.showModal("#productRevenueReport")
    })
  }

  getServiceRevenue()
  {
    this._datePrinted = this._dateConverter.form(new Date())
    this._modalDialog.hideModal("#_serviceRevenue")
    this._progressDialog.show()
    debugger
    this._orderService.serviceRevenueAsync(this.dboReport).then(response => response.json()).then(data => {
      debugger
      this._dboServiceRevenues = data;
      this._progressDialog.hide()

      this._modalDialog.showModal("#serviceRevenueReport")
    })
  }

  getSeatRevenue()
  {
    this._datePrinted = this._dateConverter.form(new Date())
    this._modalDialog.hideModal("#_seatRevenue")
    this._progressDialog.show()
    debugger
    this._orderService.seatRevenueAsync(this.dboServicePerSeatReport).then(response => response.json()).then(data => {
      debugger
      this._dboServicePerSeatRevenues = data;
      this.dboServicePerSeatReport.seat = this._seats.find(x => x.id == this.dboServicePerSeatReport.seatId)
      this._progressDialog.hide()

      this._modalDialog.showModal("#seatRevenueReport")
    })
  }

  getBranchRevenue()
  {
    debugger
    this._datePrinted = this._dateConverter.form(new Date())
    this._modalDialog.hideModal("#_branchRevenue")
    this._progressDialog.show()
    debugger
    this._orderService.branchRevenueAsync(this.dboReport).then(response => response.json()).then(data => {
      debugger
      this._dboBranchRevenues = data;
      this._progressDialog.hide()

      this._modalDialog.showModal("#branchRevenueReport")
    })
  }
}
