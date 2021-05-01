import { inject } from 'aurelia-framework';
import { PageService } from '../../services/page.service';
import { StoreService } from '../../services/store.service';

@inject(StoreService, PageService)
export class Index {
    _storeService
    slug
    page
    constructor(storeService, pageService) {
      this._storeService = storeService
      this.pageService = pageService
      debugger        
    }

    activate(params) 
    {
      debugger
      this.slug = params.slug;
      if(this.slug)
      {
          this.getStore()
      }
      else
        alert("Not a valid store")
  }

  getStore() {
    debugger
      this._storeService.getStoreBySlug(this.slug).then(response => response.json()).then(data => {
          debugger
          this.getStorePages(data)
      }).catch(error => {

      })
  }

  bindBackgroundStyles(_row)
  {
    debugger
    let style = 'padding: 50px 0px;'
    if(_row.backgroundImage)
    {
      style += 'background-image: url("data:image/jpeg;base64,' + _row.backgroundImage + '"); background-size: cover;'
    }
    else if(_row.backgroundColor)
    {
      style += 'background: ' + _row.backgroundColor
    }
    return style;
  }

  getStorePages(store)
  {
    debugger
    this.pageService.getStorePages(store.id).then(response => response.json()).then(data => {
        debugger
        store.pages = data
        this._storeService.store = store
        localStorage.removeItem("store")
        localStorage.setItem("store", JSON.stringify(data));

        if(this._storeService.store.pages.length > 0)
          this.page = this._storeService.store.pages.find(p => p.isHomePage == true)
    }).catch(error => {

    })
  }
}
