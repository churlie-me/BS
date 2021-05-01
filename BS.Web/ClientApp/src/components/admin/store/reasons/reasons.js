import { inject, BindingEngine } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { ModelDialog } from '../../../../extensions/modal.dialog';
import { ProgressDialog } from '../../../../extensions/progress/progress.dialog';
import { ReasonService } from '../../../../services/reason.service';

@inject(Router, ReasonService, ModelDialog, ProgressDialog)
export class ReasonsModule {
  reasons
  storeId
  _router
  reason
  _reasonService
  modalDialaog
  progressDialog
  constructor(router, _reasonService, modalDialaog, progressDialog) {
      debugger        
      this._router = router
      this._reasonService = _reasonService
      this.modalDialaog = modalDialaog
      this.progressDialog = progressDialog
  }

  onInitReason(id)
    {
      debugger
      if(id != undefined)
        this.reason = this.reasons.find(r => r.id == id)
      else
        this.reason = { createdOn : new Date(), storeId: this.storeId }
    }

  onSaveReason()
  {
    debugger
    this.modalDialaog.hideModal("#reason")
    this.progressDialog.show()
    this._reasonService.Save(this.reason).then(response => response.json()).then(data => {
      debugger
      this.progressDialog.hide()

      this.initReasons(this.storeId)
    })
  }

  onDeleteReason()
  {
    this.reason.deleted = 1
    this.modalDialaog.hideModal("#delete")
    this.progressDialog.show()
    this._reasonService.Save(this.reason).then(response => response.json()).then(data => {
      debugger
      this.progressDialog.hide()
      this.reasons = undefined
      this.initReasons(this.storeId)
    })
  }

  initReasons(id)
  {
      debugger
      this.storeId = id
      this._reasonService.ReasonsAsync(this.storeId).then(response => response.json()).then(data => {
          debugger
        this.reasons = data;
      })
  }
}
