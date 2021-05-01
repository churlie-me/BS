import { inject, BindingEngine } from 'aurelia-framework';
import { ProductService } from '../../../services/product.service';
import { Router } from 'aurelia-router';
import { ModelDialog } from '../../../extensions/modal.dialog';

@inject(Router, ProductService, ModelDialog)
export class Index {
  dboproducts
  categories
  brands
  router
  _productService
  modal
  constructor(_router, productService, modal) {
    this.router = _router
    this._productService = productService
    this.modal = modal
    debugger        
  }

  activate(params) {
    debugger
    this.dboproducts = 
    {
      page : 1
    };
    this.initProducts()
  }

  initProducts()
  {
    debugger
    this._productService.ProductsAsync(this.dboproducts).then(response => response.json()).then(data => {
        debugger
      this.dboproducts = data;
    })
  }

  initProduct(product)
  {
    this.product = product
  }

  _search()
  {
    debugger
    if(this.dboproducts.search == "")
      this.dboproducts.search = undefined
    this.initProducts()
  }

  deleteProduct()
  {
    this.product.deleted = true
    this.isBusy = true
    this._productService.SaveProductAsync(this.product).then(response => response.json()).then(data => {
      debugger
      this.isBusy = false
      this.modal.hideModal("#warning")
      this.dboproducts.products = undefined
      this.initProducts()
    }).catch(error => {
        this.isBusy = false
    })
  }

  isActive(index, page)
  {
    debugger
    let active = (index == page)
    return active
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
    if(this.dboproducts.page < this.dboproducts.pageCount)
    {
      this.dboproducts.page += 1;
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
