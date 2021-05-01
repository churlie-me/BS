import { CartService } from "../../services/cart.service";
import { inject } from "aurelia-framework"
import { Router } from "aurelia-router"

@inject(CartService)
export class Index
{
    cart
    total = 0
    constructor(cart)
    {
        this.cart = cart
    }
}