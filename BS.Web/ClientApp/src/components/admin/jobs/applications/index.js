import { JobService } from "../../../../services/job.service";
import { inject } from 'aurelia-framework';

@inject(JobService)

export class Index
{
  _jobService
  job
  constructor(_jobService)
  {
    this._jobService = _jobService
  }

  activate(params)
  {
    debugger
    this.dboJobApplication = {
      jobId : params.id,
      page : 1
    }
    this.initJobApplications()
    this.initJob(params.id)
  }

  initJob(id)
  {
    this._jobService.JobAsync(id).then(response => response.json()).then(data => {
      debugger
      this.job = data;
    })
  }

  applyFilter()
  {
    this.dboJobApplication.applications = undefined
    this.initJobApplications()
  }

  initJobApplications()
  {
    this._jobService.JobApplicationsAsync(this.dboJobApplication).then(response => response.json()).then(data => {
      debugger
      this.dboJobApplication = data;
    })
  }

  goToPage(page)
  {
    debugger
    this.dboJobApplication.page = page;
    this.dboJobApplication.applications = undefined

    this.initJobApplications()
  }

  next()
  {
    if(this.dboJobApplication.page < this.dboJobApplication.pageCount)
    {
      this.dboJobApplication.page += 1;
      this.initJobApplications()
    }
  }

  previous()
  {
    if(this.dboJobApplication.page > 1)
    {
      this.dboJobApplication.page -= 1;
      this.initJobApplications()
    }
  }

  base64toPDF(application) {
    debugger
    var bufferArray = this.base64ToArrayBuffer(application.resume);
    var blobStore = new Blob([bufferArray], { type: "application/pdf" });
    if (window.navigator && window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveOrOpenBlob(blobStore);
        return;
    }
    var data = window.URL.createObjectURL(blobStore);
    var link = document.createElement('a');
    document.body.appendChild(link);
    link.href = data;
    link.download = application.user.firstName + " " + application.user.lastName + ".pdf";
    link.click();
    window.URL.revokeObjectURL(data);
    link.remove();
  }

  base64ToArrayBuffer(data) {
    var bString = window.atob(data);
    var bLength = bString.length;
    var bytes = new Uint8Array(bLength);
    for (var i = 0; i < bLength; i++) {
        var ascii = bString.charCodeAt(i);
        bytes[i] = ascii;
    }
    return bytes;
  }
}
