import { inlineView, inject } from 'aurelia-framework';
import { DateConverter } from '../../../extensions/date.converter';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';
import { AuthenticationService } from '../../../services/authentication.service';
import { OrderService } from '../../../services/order.service';
import { ProductService } from '../../../services/product.service';
import { StoreService } from '../../../services/store.service';
@inject(OrderService, ModelDialog, ProgressDialog, DateConverter, StoreService, ProductService, AuthenticationService)
export class InvoiceModule
{
  orderstate = "done"
  constructor(orderservice, modal, progressdialog, dateConverter, storeservice, _productservice, authenticationservice)
  {
    this.orderservice = orderservice
    this.modal = modal
    this.progressdialog = progressdialog
    this.dateConverter = dateConverter
    this.storeservice = storeservice
    this._productservice = _productservice
    this.authenticationservice = authenticationservice
  }

  getOrder(orderId)
  {
    debugger
    this.isBusy = true
    this.modal.showModal("#_invoice")
    this.orderservice.getOrder(orderId).then(response => response.json()).then(data => {
      this.order = data
      this.isBusy = false
      this.init()
    })
  }

  getAppointmentOrder(appointmentId)
  {
    debugger
    this.isBusy = true
    this.modal.showModal("#_invoice")
    this.orderservice.getAppointmentOrder(appointmentId).then(response => response.json()).then(data => {
      this.order = data
      this.isBusy = false
      this.init()
    })
  }

  onProductSelected(product)
  {
    debugger
    let orderItem = {
      name : product.name,
      productId : product.id,
      quantity : 1,
      price : product.saleItem.price
    }
    this.order.orderTotal += orderItem.price
    this.order.orderItems.push(orderItem)

    return true;
  }

  qtyChanged()
  {
    debugger
    this.order.orderTotal = 0
    this.order.orderItems.forEach(item => {
      this.order.orderTotal += item.price * item.quantity
    });
  }

  deleteItem(item)
  {
    debugger
    item.deleted = true
    this.order.orderTotal -= item.quantity * item.price
  }

  editOrder()
  {
    this.orderstate = "edit"
  }

  addItems()
  {
    this.modal.showModal("#products")
  }

  isProductChecked(product)
  {
    debugger
    let isChecked = this.order.orderItems.find(x => x.productId == product.id) != undefined
    return isChecked;
  }

  exitCheck()
  {
    debugger
    if(this.orderstate == "edit")
    {
      this.message = { title : "Warning !!!", message : "You have unsaved changes, are you sure you want to exit"}
      this.modal.showModal("#warning")
    }
    else
      this.modal.hideModal("#_invoice")
  }

  confirmClosure()
  {
    if(this.orderstate == "edit")
    {
      this.orderstate = "done"
      this.modal.hideModal("#warning")
      this.modal.hideModal("#_invoice")
    }
    else
      this.modal.hideModal("#warning")
  }

  saveOrder()
  {
    debugger
    this.isBusy = true
    this.orderservice.save(this.order).then(response => response.json()).then(data => {
      debugger
      this.isBusy = false
      if(data != true)
          this.error = data
      else
      {
        this.message = { title : "Confirmation", message : "Your order had been saved, you can now proceed to print"}
        //this.modal.showModal("#warning")
        this.orderstate = "done"
      }
      
    }).catch(error => {
        debugger
        this.isBusy = false
        this.error = error
    })
  }

  init()
  {
    this.dboproducts = 
    {
      page : 1
    };
    this.initProducts()
  }

  initProducts()
  {
    debugger
    this._productservice.ProductsAsync(this.dboproducts).then(response => response.json()).then(data => {
      debugger
      this.dboproducts = data;
    })
  }

  goToPage(page)
  {
    debugger
    this.dboproducts.page = page;
    this.dboproducts.products = undefined

    this.initProducts()
  }

  next()
  {
    debugger
    if(this.dboproducts.page < this.dboproducts.pageCount)
    {
      this.dboproducts.page += 1;
      this.dboproducts.products = undefined
      this.dboproducts.from = (this.dboproducts.from == "")? undefined : this.dboproducts.from
      this.dboproducts.to = (this.dboproducts.to == "")? undefined : this.dboproducts.to
      this.initProducts()
    }
  }

  previous()
  {
    if(this.dboproducts.page > 1)
    {
      this.dboproducts.page -= 1;
      this.initProducts()
    }
  }
}
