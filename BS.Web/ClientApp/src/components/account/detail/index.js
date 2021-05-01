import { inject } from 'aurelia-framework';
import { InfoDialog } from '../../../extensions/info/info.dialog';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { AuthenticationService } from "../../../services/authentication.service";
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { BootstrapFormRenderer } from '../../bootstrap-form-renderer';

@inject(AuthenticationService, ModelDialog, InfoDialog, ValidationControllerFactory)
export class Index
{
    _authenticationService
    _modalDialog
    _infoDialog
    isBusy = false
    error
    constructor(_authenticationService, _modalDialog, _infoDialog, controllerFactory)
    {
        this._authenticationService = _authenticationService
        this._modalDialog = _modalDialog
        this._infoDialog = _infoDialog

        this.controller = controllerFactory.createForCurrentScope();
        this.controller.addRenderer(new BootstrapFormRenderer());
        
        if(!this._authenticationService.account.address)
          this._authenticationService.account.user.address = { street:"", zipCode:"", city:"", }
    }

    bind()
    {
      debugger
      ValidationRules
      .ensure('firstName').required()
      .ensure('lastName').required()
      .on(this._authenticationService.account.user)

      ValidationRules
      .ensure('street').required()
      .ensure('zipCode').required()
      .ensure('city').required()
      .on(this._authenticationService.account.user.address)

      ValidationRules
      .ensure('email').required()
      .ensure('phone').required()
      .on(this._authenticationService.account.user.contact)
    }

    onSaveAccount()
    {
      debugger
      var isValid = this.controller.validate()
      this.valid = true;

      if(this.controller.results.length < 7)
        this.valid = false
      else
        for (let result of this.controller.results) {
          this.valid = this.valid && result.valid;
        }

      if(this.valid)
      {
        this.isBusy = true
        this._authenticationService.update(this._authenticationService.account).then(response => response.json()).then(data => {
            debugger
            this.isBusy = false
            if(data != true)
                this.error = data
            else
            {
                //remove previous stored item
                localStorage.removeItem("account");
                //save updated account object
                localStorage.setItem("account", JSON.stringify(this._authenticationService.account))

                this._modalDialog.hideModal("#_account")
                this.msg = { title : "Kennisgeving", message : "Je account is bijgewerkt"}
                this._infoDialog.show()
            }
        }).catch(error => {
            debugger
            this.isBusy = false
            this.error = error
            this.message = {title : "Error", message : error}
            this._infoDialog.show()
        })
      }
    }
}
