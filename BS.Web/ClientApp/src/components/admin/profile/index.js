import { inject } from "aurelia-framework";
import { AuthenticationService } from "../../../services/authentication.service";

@inject(AuthenticationService)
export class Index {
    roles
    name
    account
    authenticationService
    constructor(_authenticationService) {
        debugger
        this.authenticationService = _authenticationService
        this.account = _authenticationService.account
        this.getRoles()
    }

    getRoles()
    {
        this.authenticationService.getUserRoles().then(response => response.json()).then(data => {
            //this.busyIndicator.off();
              debugger
            this.roles = data;
        })
    }

    OnCreateRole()
    {
        debugger
        let role = {
            name: this.name
        }

        this.authenticationService.addRole(role).then(response => response.json()).then(data => {
            //this.busyIndicator.off();
            debugger
            this.HideModal('#role')
            this.getRoles()
        })
    }

    SaveProfile()
    {
        debugger
        this.authenticationService.update(this.account).then(response => response.json()).then(data => {
            //this.busyIndicator.off();
            debugger
            if(JSON.parse(JSON.stringify(data)))
                {
                    localStorage.clear();
                    localStorage.setItem("account", JSON.stringify(this.account))
                }
        })
    }

    HideModal(id)
    {
        $(id).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }
}