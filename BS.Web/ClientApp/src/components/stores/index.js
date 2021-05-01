import { inject, BindingEngine, singleton } from 'aurelia-framework';
import { StoreService } from '../../services/store.service';
import { Router } from "aurelia-router";

@inject(StoreService, BindingEngine, Router)
@singleton()
export class Index {
    stores;
    _storeService
    constructor(storeService, bindingEngine) {
        debugger
        this._storeService = storeService
        this.bindingEngine = bindingEngine
        this.initStores()
    }

    /*configureRouter(config, router) {
      debugger
      //config.title = this.translator.get('MainApp.PageTitleSuffix');

      let routes = [];
      routes.push({ route: ['', 'stores/:id'], name: '', moduleId: PLATFORM.moduleName('./stores/index'), title: 'Stores' })
      routes.push({ route: 'store/:id', name: 'store', moduleId: '../store/index', nav: false, title: 'Detail artikel' });
      config.map(routes);
      this.router = router
    }*/

    initStores()
    {
        debugger
        this._storeService.StoresAsync().then(response => response.json()).then(data => {
            debugger
          this.stores = data;
        })
    }
}
