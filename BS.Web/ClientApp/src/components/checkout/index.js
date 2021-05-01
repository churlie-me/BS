import { CartService } from "../../services/cart.service";
import { inject } from "aurelia-framework"
import { Router } from "aurelia-router"
import { AuthenticationService } from "../../services/authentication.service";
import { Guid } from "../../extensions/guid";
import { OrderService } from "../../services/order.service";
import { StoreService } from "../../services/store.service";
import { InfoDialog } from "../../extensions/info/info.dialog";
import { ModelDialog } from "../../extensions/modal.dialog";

@inject(CartService, AuthenticationService, Guid, OrderService, StoreService, InfoDialog, Router, ModelDialog)
export class Index
{
    cart
    guid
    isBusy = false
    error
    _orderService
    _authenticationService
    _storeService
    _infoDialog
    _router
    _modalDialog
    constructor(cart, _authenticationService, guid, _orderService, _storeService, _infoDialog, _router, _modalDialog)
    {
        this.cart = cart
        this.guid = guid
        this._authenticationService = _authenticationService
        this._storeService = _storeService
        this._orderService = _orderService
        this._infoDialog = _infoDialog
        this._router = _router
        this._modalDialog = _modalDialog
    }

    attached() {
      if(!this.cart.items)
        this._router.navigateToRoute('cart')
    }

    onCreateOrder()
    {
        let order =
        {
            id : this.guid.create(),
            orderDate : new Date(),
            orderTotal : this.cart.getTotal(),
            user : this._authenticationService.account.user,
            orderItems : this.cart.items,
            storeId : this._storeService.store.id
        }

        debugger
        this.isBusy = true
        this._orderService.save(order).then(response => response.json()).then(data => {
            debugger
            this.isBusy = false
            if(data != true)
                this.error = data
            else
            {
              localStorage.removeItem('cart')
              this.cart.items = []
              this.message = { title : "Bestelbericht", message : "Uw bestelling is ontvangen, wacht op een bevestigingsbericht" }
              this._modalDialog.showModal("#info")
            }
        }).catch(error => {
            debugger
            this.isBusy = false
            this.error = error
        })
    }

    navigateToCart()
    {
      debugger
      this._router.navigateToRoute('cart');
    }
}
