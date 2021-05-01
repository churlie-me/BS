import { AuthenticationService } from "../../../services/authentication.service";
import { inject, bindable } from 'aurelia-framework';
import { ModelDialog } from "../../../extensions/modal.dialog";
import { OrderService } from "../../../services/order.service";
import { StoreService } from "../../../services/store.service";
import { ProgressDialog } from "../../../extensions/progress/progress.dialog";
import { InvoiceModule } from "../invoice/invoice.module";

@inject(AuthenticationService, OrderService, ProgressDialog, ModelDialog, StoreService, InvoiceModule)
export class Index
{
  _authenticationService
  _orderService
  _storeService
  _modalDialog
  _progressDialog
  orders
  @bindable _order
  @bindable _store
  isBusy = false
  _invoice
  statuses = [{ code : 0, desc : 'Pending' }, { code : 1, desc : 'Cancelled'}, { code : 2, desc : 'InProcess'}, { code : 3, desc : 'Processed'}, { code : 4, desc : 'Completed'}, { code : 5, desc : 'All'}]
  constructor(_authenticationService, _orderService, _progressDialog, _modalDialog, _storeService, _invoice)
  {
      this._authenticationService = _authenticationService
      this._orderService = _orderService
      this._modalDialog = _modalDialog
      this._progressDialog = _progressDialog
      this._storeService = _storeService
      this._invoice = _invoice
      this.dboorder = { page : 1, status : 0 }
      this.initOrders()
  }

    initOrders()
    {
        debugger
        this._orderService.orders(this.dboorder).then(response => response.json()).then(data => {
            debugger
        this.dboorder = data;
        })
    }

    viewOrder(order)
    {
      debugger
      this._order = order
      this._invoice.getOrder(order.id)
    }

    statusChanged()
    {
      this.dboorder.orders = undefined
      this.initOrders()
    }

    cancelOrder(order)
    {
      debugger
      this._order = order
      this._order.status = 1
      this.isBusy = true
      this._orderService.save(this._order).then(response => response.json()).then(data => {
        debugger
        this.isBusy = false
        if(data != true)
            this.error = data
        else
        {
          this._infoDialog.show("Order Notification", "You're order has been cancelled")
          this.initOrders()
        }
      }).catch(error => {
        debugger
        this.isBusy = false
        this.error = error
      })
  }

  completeOrder(order)
  {
    debugger
      this._order = order
      this._order.status = 4
      this.isBusy = true
      this._orderService.save(this._order).then(response => response.json()).then(data => {
        debugger
        this.isBusy = false
        if(data != true)
            this.error = data
        else
        {
          this._infoDialog.show("Order Notification", "Order " + this._order.orderNo + " has been cancelled")
          this.initOrders()
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
    this.dboorder.page = page;
    this.dboorder.customers = undefined

    this.initOrders()
  }

  next()
  {
    if(this.dboorder.page < this.dboorder.pageCount)
    {
      this.dboorder.page += 1;
      this.initOrders()
    }
  }

  previous()
  {
    if(this.dboorder.page > 1)
    {
      this.dboorder.page -= 1;
      this.initOrders()
    }
  }
}
