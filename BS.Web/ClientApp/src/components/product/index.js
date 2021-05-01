import { ProductService } from "../../services/product.service";
import { inject } from "aurelia-framework";
import { CartService } from "../../services/cart.service";

@inject( ProductService, CartService )
export class Index
{
    product
    articleId
    _productService
    _cart
    qty = 1
    constructor(_productService, cart){
        this._productService = _productService
        this._cart = cart
    }

    activate(params) {
        debugger
        this.articleId = params.id;

        if(this.articleId)
        {
            this.InitProduct()
        }
    }

    InitProduct()
    {
        this._productService.ProductAsync(this.articleId).then(response => response.json()).then(data => {
            //this.busyIndicator.off();
            debugger
            this.product = data
        })
    }

    minus()
    {
      debugger
      if(this.qty != 1)
        this.qty--
    }

    plus()
    {
      debugger
      this.qty++
    }

    addItem()
    {
        debugger
        this._cart.addItem(this.product, this.qty)
    }
}
