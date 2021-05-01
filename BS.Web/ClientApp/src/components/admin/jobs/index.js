import { inject, BindingEngine } from 'aurelia-framework';
import { JobService } from '../../../services/job.service';
import { Router } from 'aurelia-router';
import { StoreService } from '../../../services/store.service';
import { DateConverter } from '../../../extensions/date.converter';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';

@inject(Router, JobService, StoreService, DateConverter, ProgressDialog)
export class Index {
  dbojobs
  router
  _jobService
  _storeService
  _dateConverter
  _progressDialog
  job
  constructor(_router, jobService, _storeService, _dateConverter, _progressDialog) {
    this.router = _router
    this._jobService = jobService
    this._storeService = _storeService
    this._dateConverter = _dateConverter
    this._progressDialog = _progressDialog
    debugger        
  }

  activate(params) {
    debugger
    this.dbojobs = 
    {
      page : 1
    };
    this.initJobs()
  }

  initJobs()
  {
    debugger
    this._jobService.JobsAsync(this.dbojobs).then(response => response.json()).then(data => {
        debugger
      this.dbojobs = data;
    })
  }

  _search()
  {
    debugger
    /*
    if(this.dbojobs.search == "")
      this.dbojobs.search = undefined
    this.initJobs()
    */
  }

  isActive(index, page)
  {
    debugger
    let active = (index == page)
    return active
  }

  initJob(job)
  {
    debugger
    this.job = job
    this.job.deadline = this._dateConverter.form(this.job.deadline)
  }

  goToPage(page)
  {
    debugger
    this.dbojobs.page = page;
    this.dbojobs.jobs = undefined

    this.initJobs()
  }

  next()
  {
    if(this.dbojobs.page < this.dbojobs.pageCount)
    {
      this.dbojobs.page += 1;
      this.initJobs()
    }
  }

  previous()
  {
    if(this.dbojobs.page > 1)
    {
      this.dbojobs.page -= 1;
      this.initJobs()
    }
  }

  saveJob()
  {
    this._progressDialog.show()
    debugger

    this._jobService.SaveJobAsync(this.job).then(response => response.json()).then(data => {
      debugger
      this._progressDialog.hide()
      if(JSON.parse(JSON.stringify(data)))
      {
          this.job = undefined
          this.dbojobs.jobs = undefined
          this.initJobs()
      }
    }).catch(error => {
      this._progressDialog.hide()
    })
  }

  onDeleteJob()
  {
    debugger
    this.job.deleted = true
    this.saveJob()
  }
}
