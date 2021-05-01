import { inject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { Guid } from "../../../extensions/guid";
import { ModelDialog } from "../../../extensions/modal.dialog";
import { ProgressDialog } from "../../../extensions/progress/progress.dialog";
import { AuthenticationService } from "../../../services/authentication.service";
import { ServiceService } from "../../../services/service.service";
import { StoreService } from "../../../services/store.service";
import { UserService } from "../../../services/user.service";

@inject(UserService, ServiceService, ModelDialog, AuthenticationService, Guid, StoreService, ProgressDialog, Router)
export class Index
{
  accounts
  services
  _userService
  _serviceService
  storeId
  account
  email
  guid
  isBusy = false
  isValid = true
  branchId
  _modalDialog
  _progressDialog
  _authenticationService
  storeService
  _router
  message
  constructor(userService, serviceService, modalDialog, authenticationService, guid, storeService, _progressDialog, _router){
    this._userService = userService
    this._serviceService = serviceService
    this._modalDialog = modalDialog
    this._authenticationService = authenticationService
    this.guid = guid
    this.storeService = storeService
    this._progressDialog = _progressDialog
    this._router = _router
  }

  activate(params) {
    debugger
    this.dboAccounts = { page : 1 }
    this.initUsers()
    this.initServices()
  }
  
  initUsers()
  {
    debugger
    this._userService.getUsers(this.dboAccounts).then(response => response.json()).then(data => {
        debugger
      this.dboAccounts = data;
    })
  }

  initServices()
  {
    this._serviceService.ServicesAsync(this.storeId).then(response => response.json()).then(data => {
      //this.busyIndicator.off();
        debugger
      this.services = data;
    })
  } 

  saveStaff()
  {
    debugger
    if(!this.account)
    {
      alert("No account attached")
      return
    }
    this._modalDialog.hideModal('#staff')
    this._progressDialog.show()

    if(!this.account.stores.find(x => x.storeId == this.storeId && x.accountId == this.account.id))
      this.account.stores.push({storeId: this.storeId, accountId: this.account.id})

    this._authenticationService.update(this.account).then(response => response.json()).then(data => {
        debugger
        this._progressDialog.hide()
    }).catch(error => {
        debugger
        this._progressDialog.hide()
    })
  }

  async findAccount()
  {
    debugger
    this.isBusy = true
    this._authenticationService.findAccount(this.email).then(response => response.json()).then(async data => {
        debugger
        this.isBusy = false
        if(data)
          this.account = data
        else
        {
          this._modalDialog.hideModal("#staff")
          await new Promise(resolve => setTimeout(resolve, 1000));
          this.message = { title : "Account Status", message : "No registered account with the email " + this.email + " exists, you can proceed and send an invitation" }
          this._modalDialog.showModal("#invitation")
        }
    }).catch(async error => {
        debugger
        this.isBusy = false
        this._modalDialog.hideModal("#staff")
          await new Promise(resolve => setTimeout(resolve, 1000));
          this.message = { title : "Account Status", message : "No registered account with the email " + this.email + " exists, you can proceed and send an invitation" }
          this._modalDialog.showModal("#invitation")
    })
  }

  sendInvitation()
  {
    this.account.email = this.email

    this.isBusy = true

    debugger
    
    this._authenticationService.sendInvitation(this.account).then(response => response.json()).then(async data => {
      debugger
      this.isBusy = false
      this._modalDialog.hideModal("#invitation")
      if(data)
        this.message = { title : "Invitation Status", message : "An invitation has been sent to " + this.email }
      else
        this.message = { title : "Invitation Status", message : "Either your store url or email configurations might be faulty, please visit the store settings page and fill in the necessary details" }

      this.email = undefined
      this.account = undefined
      this._modalDialog.showModal("#info")
    })
  }

  accountChanged()
  {
    debugger
    if(this.account)
      if(this.account.email != this.email)
        this.account = undefined
  }

  AttachRoles(service)
  {
    debugger

    if(this.account.type == 2 && this.account.branchId == null)
    {
      alert("Select a branch to attach staff")
      return false;
    }

    if(this.account.branchId != null && this.account.type != 2)
    {
      alert("You cannot assign a branch to a non employee account")
      return false
    }

    if(this.account.branchId == null && this.account.type != 2 && this.account.type != 1)
    {
      alert("Nothing has changed, cancel to continue")
      return
    }

    this.isBusy = true
    this._authenticationService.update(this.account).then(response => response.json()).then(async data => {
      this.isBusy = false
      if(data)
      {
        await new Promise(resolve => setTimeout(resolve, 1000));
        this._modalDialog.hideModal('#staff')
        await new Promise(resolve => setTimeout(resolve, 1000));
        this._router.navigateToRoute('admin-staff', {id: this.account.id});
      }
    }).catch(error => {
      debugger
      this._progressDialog.hide()
    });
  }

  goToPage(page)
  {
    debugger
    this.dboAccounts.page = page;
    this.dboAccounts.accounts = undefined

    this.initUsers()
  }

  next()
  {
    debugger
    if(this.dboAccounts.page < this.dboAccounts.pageCount)
    {
      this.dboAccounts.page += 1;
      this.dboAccounts.accounts = undefined
      this.dboAccounts.from = (this.dboAccounts.from == "")? undefined : this.dboAccounts.from
      this.dboAccounts.to = (this.dboAccounts.to == "")? undefined : this.dboAccounts.to
      this.initUsers()
    }
  }

  previous()
  {
    if(this.dboAccounts.page > 1)
    {
      this.dboAccounts.page -= 1;
      this.initUsers()
    }
  }
}
