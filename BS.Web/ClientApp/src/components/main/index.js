import { inject } from 'aurelia-framework';
import { AuthenticationService } from '../../services/authentication.service';
import { StoreService } from '../../services/store.service';
import { Pager } from '../page/pager/pager';
import { Router } from "aurelia-router";

@inject(StoreService, Pager, AuthenticationService, Router)
export class Index {
    _storeService
    slug
    _page
    constructor(storeService, pager, _authenticationService, _router) {
      this._storeService = storeService
      this._pager = pager
      this._authenticationService = _authenticationService
      this._router = _router
      debugger      
    }

    activate(params)
    {
      debugger

      if(this._authenticationService.mode == 'admin')
        this._router.navigateToRoute('admin');
    }
}
