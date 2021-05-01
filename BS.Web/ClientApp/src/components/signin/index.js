import { inject } from 'aurelia-framework';
import { Router } from "aurelia-router";
//import { BusyIndicator } from "../resources/elements/busy-indicator";
import { AuthenticationService } from '../../services/authentication.service';
import { StoreService } from '../../services/store.service';

@inject(AuthenticationService, Router, StoreService)
export class SignIn {
    authenticationFailed
    email = '';
    password = '';
    authenticationService
    storeService
    _router
    stores = []
    isBusy = false
    constructor(_authenticationService, router, _storeService) {
        debugger
        this.authenticationService = _authenticationService
        this.storeService = _storeService
        this._router = router
        this.authenticationFailed = false;
        if(this.authenticationService.IsLoggedIn)
          this._router.navigateToRoute('account');
    }

    attached()
    {
      debugger
      this.storeService.loadStyles(this.storeService.store)
    }

    login() {
        this.isBusy = true
        debugger
        this.authenticationService.login({
            email: this.email,
            password: this.password
        }).then(response => response.json()).then(data => {
            this.isBusy = false
            debugger
            this.authenticationService.IsLoggedIn = true;
            if(JSON.parse(JSON.stringify(data)))
            {
                localStorage.setItem("account", JSON.stringify(data))
                this.authenticationService.account = data
                this._router.navigateToRoute('account');
            }
        }).catch(error => {
            debugger
            this.isBusy = false
            localStorage.clear()
            this.authenticationService.account = undefined
            this.authenticationFailed = true;
        })
    }
}
