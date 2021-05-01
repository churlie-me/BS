import { inject } from 'aurelia-framework';
import { Router } from "aurelia-router";
//import { BusyIndicator } from "../resources/elements/busy-indicator";
import { AuthenticationService } from '../../services/authentication.service';
import { StoreService } from '../../services/store.service';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { BootstrapFormRenderer } from '../bootstrap-form-renderer';

@inject(AuthenticationService, Router, StoreService, ValidationControllerFactory)
export class SignUp {
    authenticationFailed
    email = '';
    password = ''
    fname = ''
    lname = ''
    errormessage = ''
    _authenticationService
    _storeService
    _router
    isBusy = false
    constructor(authenticationService, router, _storeService, controllerFactory) {
        this.authenticationFailed = false;
        localStorage.removeItem("auth_token");
        this._authenticationService = authenticationService
        this._router = router
        this._storeService = _storeService

        this.controller = controllerFactory.createForCurrentScope();
        this.controller.addRenderer(new BootstrapFormRenderer());

        this.bind()
    }

    attached()
    {
      this._storeService.loadStyles(this._storeService.store)
    }

    activate(params) {
      if(this._authenticationService.account)
        localStorage.removeItem("account")
        
      if(params.role)
        this.role = params.role
      if(params.role)
        this.email = params.email
    }

    bind()
    {
      ValidationRules.customRule(
        'matchesProperty',
        (value, obj, otherPropertyName) =>
          value === null
          || value === undefined
          || value === ''
          || obj[otherPropertyName] === null
          || obj[otherPropertyName] === undefined
          || obj[otherPropertyName] === ''
          || value === obj[otherPropertyName],
        '${$displayName} must match ${$getDisplayName($config.otherPropertyName)}', otherPropertyName => ({ otherPropertyName })
      );

      ValidationRules
      .ensure("vatNo").required()
      .ensure("street").required()
      .ensure("city").required()
      .ensure("zipCode").required()
      .ensure('email').required()
      .ensure('password').required().minLength(8).when((account) => account.password)
      .withMessage("Password must have a minimum of 8 characters")
      .ensure('confirmPassword').required()
      .satisfiesRule('matchesProperty', 'password')
      .ensure("firstName").required()
      .ensure("lastName").required()
      .ensure("phone").required()
      .on(this)
    }

    register() {
      debugger
      var isValid = this.controller.validate()
      this.valid = true;

      if(this.controller.results.length < 12)
        this.valid = false
      else
        for (let result of this.controller.results) {
          this.valid = this.valid && result.valid;
        }

      if(this.valid)
      {
        this.isBusy = true
        debugger
        let json = 
        {
            email: this.email,
            password: this.password,
            isAdmin : false,
            user: {
                FirstName: this.firstName,
                LastName: this.lastName,
                Contact : { email : this.email, Phone : this.phone },
                Address : { street : this.street, city : this.city, zipCode : this.zipCode }
            }
        }

        if(this.role)
          json.type = this.role
        else
          json.type = 0 //set to client account by default

        this._authenticationService.register(json).then(response => response.json()).then(data => {
            debugger
            this.isBusy = false
            if(data.succeeded == true)
                this._router.navigateToRoute('signin');
            else
                this.errormessage = data.errors[0].description
        }).catch(error => {
            this.isBusy = false
            debugger
            this.authenticationFailed = true;
        })
      }
    }
}
