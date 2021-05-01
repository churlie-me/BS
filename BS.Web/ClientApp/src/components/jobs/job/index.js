import { inject } from 'aurelia-framework';
import { ImageConverter } from '../../../extensions/image.converter';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { AuthenticationService } from '../../../services/authentication.service';
import { JobService } from '../../../services/job.service';
import { StoreService } from '../../../services/store.service';
@inject(JobService, StoreService, ModelDialog, ImageConverter, AuthenticationService)

export class Index{
  _jobService
  _storeService
  isBusy = false
  application
  _modalDialog
  _documentConverter
  _authenticationService
  jobId
  constructor(_jobService, _storeService, _modalDialog, _documentConverter, _authenticationService)
  {
    this._jobService = _jobService
    this._storeService = _storeService
    this._modalDialog = _modalDialog
    this._documentConverter = _documentConverter
    this._authenticationService = _authenticationService
  }

  activate(params)
  {
    debugger
    this.jobId = params.id
    this.initJob(params.id)
  }

  initJob(id)
  {
    this._jobService.JobAsync(id).then(response => response.json()).then(data => {
      debugger
      this.job = data;
    })
  }

  bindBackgroundStyles()
  {
    debugger
    let style = 'padding: 50px 0px;'
    if( this._storeService.store.storeImage )
    {
      style += 'background-image: url("data:image/jpeg;base64,' + this._storeService.store.storeImage + '"); background-size: cover;'
    }
    else if(this._storeService.store.primaryColor)
    {
      style += 'background: ' + this._storeService.store.primaryColor
    }
    return style;
  }

  apply()
  {
    if(this._authenticationService.account)
      this.application = { user : this._authenticationService.account.user }
  }

  submitApplication()
  {
    this.isBusy = true
    this.application.jobId = this.jobId
    this.application.submittedOn = new Date()
    this._jobService.ApplyAsync(this.application).then(response => response.json()).then(data => {
      debugger
      this.isBusy = false
      if(data)
      {
        this.application = undefined
        this._modalDialog.hideModal('#_application')
        this._modalDialog.showModal('#_application_notice')
      }
    })
  }

  async onFileChanged(event)
  {
    debugger
    let _lg = document.getElementById("resume")
    this._documentConverter.ConvertToBase64(_lg);
    await new Promise(resolve => setTimeout(resolve, 1000))

    let _resume_lbl = document.getElementById("resume-lbl")
    _resume_lbl.innerHTML = _lg.value.split("\\").pop()

    if(!this.application)
      this.application = {resume: this._documentConverter.base64String }
    else
      this.application.resume = this._documentConverter.base64String
  }
}
