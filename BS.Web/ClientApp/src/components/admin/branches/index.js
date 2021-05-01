import { inject } from 'aurelia-framework';
import { StoreService } from '../../../services/store.service';
import { Router } from 'aurelia-router';
import { Guid } from '../../../extensions/guid';
import { UserService } from '../../../services/user.service';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';

@inject(StoreService, Router, Guid, ModelDialog, UserService, ProgressDialog)
export class Index {
    branches;
    _storeService
    _userService
    router
    seat
    branch
    guid
    employees
    modalDialog
    progressDialog
    constructor(_storeService, router, guid, modalDialog, _userService, progressDialog) {
        debugger
        this._storeService = _storeService
        this.router = router
        this.guid = guid
        this.modalDialog = modalDialog
        this.progressDialog = progressDialog
        this._userService = _userService
        this.initBranches()
        this.initStoreStaff();
    }

    initBranches()
    {
        debugger
        this._storeService.getBranches(this._storeService.store.id).then(response => response.json()).then(data => {
            debugger
          this.branches = data;
        })
        .catch(error => {
            debugger
            let e = error
        })
    }

    onDeleteSeat()
    {
      this.seat.deleted = 1
      this.modalDialog.hideModal("#_delete");
    }

    onDeleteBranch()
    {
      this.branch.deleted = 1
      this.onSaveBranch()
      this.modalDialog.hideModal("#_delete");
    }

    initStoreStaff()
    {
      debugger
      this.dboAccounts = { page : 1 }
      this._userService.getUsers(this.dboAccounts).then(response => response.json()).then(data => {
        debugger
        this.employees = data.accounts;
      })
    }

    validateAssignment(seat)
    {
      debugger
      let allocations = this.branch.seats.filter(s => s.accountId == seat.accountId)
      if(allocations.length == 2)
      {
        seat.accountId = undefined
        this.warning = "The staff selected is already assigned a seat, please select another one"
      }
      else
        this.warning = undefined
    }

    onInitBranch(id)
    {
      debugger
      if(id != undefined)
        this.branch = this.branches.find(b => b.id == id)
      else
        this.branch = { seats : [], storeId: this._storeService.store.id}
    }

    onInitSeat(id)
    {
      debugger
      if(id != undefined)
        this.seat = this.branch.seats.find(x => x.id == id)
      else
        this.seat = { id : this.guid.create() }
    }

    onSaveSeat()
    {
      debugger
      if(this.seat.name)
      {
        var _seat = this.branch.seats.find(s => s.name == this.seat.name)
        if(!_seat)
        {
          this.seat.deleted = 0
          this.branch.seats.push(this.seat);
        }

        this.modalDialog.hideModal("#seat")
      }
    }

    deleteBranch()
    {
      this.branch.deleted = true
      this.modalDialog.hideModal("#warning")
      this.onSaveBranch()
    }

    onSaveBranch()
    {
      debugger
      this.modalDialog.hideModal("#branch")
      this.progressDialog.show()
      debugger
      this._storeService.saveBranchAsync(this.branch).then(response => response.json()).then(data => {
          debugger
          this.progressDialog.hide()
          this.branches = undefined
          this.initBranches()
      })
    }
}
