import { AuthenticationService } from "../../../services/authentication.service";
import { inject } from 'aurelia-framework';
import { ModelDialog } from "../../../extensions/modal.dialog";
import { InfoDialog } from "../../../extensions/info/info.dialog";
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { BootstrapFormRenderer } from '../../bootstrap-form-renderer';

@inject(AuthenticationService, ModelDialog, InfoDialog, ValidationControllerFactory)
export class Index
{
    dboAccount = { currentPassword : "", newPassword : "", confirmedPassword : "" }
    _authenticationService
    _modalDialog
    isBusy = false
    error
    constructor(_authenticationService, _modalDialog, infoDialog, controllerFactory)
    {
        this._authenticationService = _authenticationService
        this._modalDialog = _modalDialog
        this._infoDialog = infoDialog
        
        this.controller = controllerFactory.createForCurrentScope();
        this.controller.addRenderer(new BootstrapFormRenderer());
        this.bindValidation()
    }

    bindValidation()
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
      .ensure('currentPassword').required()
      .ensure('newPassword').required().minLength(8).when((account) => account.password)
      .withMessage("Password must have a minimum of 8 characters")
      .ensure('confirmedPassword').required()
      .satisfiesRule('matchesProperty', 'newPassword')
      .on(this.dboAccount)
    }

    changePassword()
    {
      debugger
      var isValid = this.controller.validate()
      this.valid = true;

      if(this.controller.results.length < 4)
        this.valid = false
      else
        for (let result of this.controller.results) {
          this.valid = this.valid && result.valid;
        }

      if(this.valid)
      {
        this.isBusy = true
        this.dboAccount.Account = this._authenticationService.account
        this._authenticationService.changePwd(this.dboAccount).then(response => response.json()).then(async data => {
          debugger
          this.isBusy = false
          if(data.succeeded != true)
          {
            this.msg = { title : "Fout", message : "Het ingevoerde wachtwoord is onjuist" }
            this._infoDialog.show()
          }
          else
          {
            this._message = { title : "Bevestigingsbericht", message : "Uw wachtwoord is gewijzigd, u wordt nu uitgelogd." }
            this._modalDialog.showModal("#logInfo")
          }
        }).catch(error => {
          debugger
          this.isBusy = false
          this.error = error
        })
      }
    }

    logout()
    {
      debugger
      this._modalDialog.hideModal("#logInfo")
      this._authenticationService.logout()
    }
}
