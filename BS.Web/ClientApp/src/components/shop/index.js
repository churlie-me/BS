import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/category.service';
import { BrandService } from '../../services/brand.service';
import { StoreService } from '../../services/store.service';
import { CartService } from '../../services/cart.service';

@inject(Router, ProductService, CategoryService, BrandService, StoreService, CartService)
export class Index {
  dboproducts
  categories = []
  brands = []
  storeId = ""
  _productService
  _categoryService
  _brandService
  _storeService
  _cart
  constructor(router, productService, categoryService, brandService, storeService, _cart) {
    this._storeService = storeService
    this._productService = productService
    this._brandService = brandService
    this._categoryService = categoryService
    this._cart = _cart
      debugger        
  }

  attached()
  {
    this._storeService.loadStyles(this._storeService.store)
  }

  activate(params) 
  {
    debugger
    this.dboproducts = 
    {
      page : 1,
      brandId : params.brand,
      categoryId : params.category
    }
    this.initProducts()
    this.initCategories()
    this.initBrands()
  }

  initProducts()
  {
    debugger
    this._productService.ProductsAsync(this.dboproducts).then(response => response.json()).then(data => {
        debugger
      this.dboproducts = data;
    })
  }

  bindBackgroundStyles()
  {
    debugger
    let style = 'padding: 50px 0px;'
    if( this._storeService.store.storeImage )
    {
      style += 'background-image: url("data:image/jpeg;base64,' + this._storeService.store.storeImage + '"); background-size: cover;'
    }
    else if(this._storeService.store.primaryColor)
    {
      style += 'background: ' + this._storeService.store.primaryColor
    }
    return style;
  }

  initCategories()
  {
    debugger
    this.dbocategory = { type : 0, page : 1, pageSize: 200 }
    this._categoryService.CategoriesAsync(this.dbocategory).then(response => response.json()).then(data => {
        debugger
      this.categories = data.categories;
    })
  }

  initBrands()
  {
    debugger
    this._brandService.BrandsAsync(this.storeId).then(response => response.json()).then(data => {
        debugger
      this.brands = data;
    })
  }

  addItem(product)
  {
    debugger
    this._cart.addItem(product, 1)
  }

  filter(categoryId, brandId, search)
  {
    debugger
    this.dboproducts.categoryId = categoryId 
    this.dboproducts.brandId = brandId
    this.dboproducts.search = search
    this.dboproducts.products = undefined
    this.initProducts()  
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
