import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { CategoryService } from '../../services/category.service';
import { ServiceService } from '../../services/service.service';
import { StoreService } from '../../services/store.service';
@inject(Router, ServiceService, CategoryService, StoreService)
export class Fetchdata {
    services
    categories
    storeId = ""
    _serviceService
    _categoryService
    _storeService
    _router
    constructor(router, serviceService, categoryService, storeService) {
      this._serviceService = serviceService
      this._categoryService = categoryService
      this._storeService = storeService
      this._router = router
        debugger        
    }

    activate(params) {
        debugger
        this.dboservices = 
        {
          page : 1
        }

        this.InitServices()
        this.InitCategories()
    }

    InitServices()
    {
        debugger
        this._serviceService.ServicesAsync(this.dboservices).then(response => response.json()).then(data => {
          //this.busyIndicator.off();
            debugger
          this.dboservices = data;
        })
    }

    InitCategories()
    {
        debugger
        this.dbocategory = { type : 1, page : 1, pageSize: 200 }
        this._categoryService.CategoriesAsync(this.dbocategory).then(response => response.json()).then(data => {
          //this.busyIndicator.off();
            debugger
          this.categories = data.categories;
        })
    }

  filter(categoryId, search)
  {
    debugger
    this.dboservices.categoryId = categoryId 
    this.dboservices.search = search
    this.dboservices.products = undefined
    this.InitServices()  
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
}
