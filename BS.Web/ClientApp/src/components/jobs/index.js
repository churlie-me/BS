import { inject } from 'aurelia-framework';
import { JobService } from '../../services/job.service';
import { StoreService } from '../../services/store.service';
@inject(JobService, StoreService)
export class Index {
  _jobService
  _storeService
  constructor(_jobService, _storeService)
  {
    this._jobService = _jobService
    this._storeService = _storeService
  }

  activate(params)
  {
    debugger
    this.dbojobs = 
    {
      page : 1
    };
    this.initJobs()
  }

  attached()
  {
    this._storeService.loadStyles(this._storeService.store)
  }

  initJobs()
  {
    debugger
    this._jobService.JobsAsync(this.dbojobs).then(response => response.json()).then(data => {
        debugger
      this.dbojobs = data;
      this._storeService.loadStyles(this._storeService.store)
    })
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

  applyFilter()
  {
    this.dbojobs.jobs = undefined
    this.initJobs()
  }
}
