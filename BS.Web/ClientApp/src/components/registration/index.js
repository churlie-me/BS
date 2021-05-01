import { inject } from 'aurelia-framework';
import { saveAs } from '../../../static/assets/js/filesaver.min';
import { Router } from "aurelia-router";
import { Guid } from '../../extensions/guid';
import { AuthenticationService } from '../../services/authentication.service';
import { StoreService } from '../../services/store.service';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { BootstrapFormRenderer } from '../bootstrap-form-renderer';

@inject(AuthenticationService, Router, Guid, StoreService, ValidationControllerFactory)

export class Index {
    authenticationFailed
    store = { name : '' }
    errormessage = ''
    tel
    street
    firstName
    lastName
    _authenticationService
    storeService
    _router
    guid
    account = { email : '', password : '', cpassword : '', user : { firstName : '', lastName : '' } }
    isBusy = false
    constructor(authenticationService, router, guid, storeService, controllerFactory) {
      this.authenticationFailed = false;
      localStorage.removeItem("auth_token");
      this._authenticationService = authenticationService
      this._router = router
      this.guid = guid
      this.storeService = storeService
      this.controller = controllerFactory.createForCurrentScope();
      this.controller.addRenderer(new BootstrapFormRenderer());

      this.bind()
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
      .ensure('email').required()
      .ensure('password').required().minLength(8).when((account) => account.password)
      .withMessage("Password must have a minimum of 8 characters")
      .ensure('confirmPassword').required()
      .satisfiesRule('matchesProperty', 'password')
      .on(this.account)

      ValidationRules
        .ensure("firstName").required()
        .ensure("lastName").required()
        .on(this.account.user)

      ValidationRules.ensure('name').required()
      .on(this.store)

      ValidationRules
        .ensure("tel").required()
        .ensure("street").required()
        .on(Index)
    }

    register()
    {
      debugger
      var isValid = this.controller.validate()
      this.valid = true;

      if(this.controller.results.length < 8)
        this.valid = false
      else
        for (let result of this.controller.results) {
          this.valid = this.valid && result.valid;
        }

      if(this.valid)
      {
        this.isBusy = true
        this.account.id = this.guid.create()
        this.account.isAdmin = false
        this.account.type = 1 //Management Account
        this.account.user.contact = { email : this.account.email }

        this.store.id = this.guid.create()
        this.store.accountId = this.account.id
        this.store.branches = [{ name: 'Main Branch', contact: { tel: this.tel }, address : { street: this.street } }]

        this.storeService.SaveStoreAsync(this.store).then(response => response.json()).then(data => {
          this.getStore()
        }).catch(error => {
          console.error(error)
          this.isBusy = false
          debugger
          this.authenticationFailed = true;
        })
      }
    }

    getStore()
    {
      debugger
      this.storeService.getStore().then(response => response.json()).then(async data => {
        this.storeService.store = data
        localStorage.removeItem("store")
        localStorage.setItem("store", JSON.stringify(this.storeService.store));
        this.signup()
      }).catch(error => {
        console.error(error)
      })
    }

    async signup() {
      debugger
      this._authenticationService.register(this.account).then(response => response.json()).then(data => {
          debugger
          let json = data
          if(json.succeeded == true)
              this.login()
          else
          {
              this.isBusy = false
              this.errormessage = json.errors[0].description
          }
      }).catch(error => {
          this.isBusy = false
          debugger
          this.authenticationFailed = true;
      })
    }

    login() {
        debugger
        this._authenticationService.login({
          email: this.account.email,
          password: this.account.password
        }).then(response => response.json()).then(data => {
          debugger
          this.isBusy = false
          if(JSON.parse(JSON.stringify(data)))
          {
              this.account = data
              localStorage.setItem("account", JSON.stringify(this.account))
              this._authenticationService.account = this.account

              //Set user mode
              localStorage.removeItem('mode')
              localStorage.setItem('mode', 'admin')
              this._authenticationService.mode = 'admin'

              //navigate to store settings
              this._router.navigateToRoute('admin-store');
              location.reload(true)
          }
        }).catch(error => {
            debugger
            this.isBusy = false
            localStorage.clear()
            this._authenticationService.account = undefined
            this.authenticationFailed = true;
        })
    }
}


