import { inject, BindingEngine } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { ReasonService } from '../../../services/reason.service';
import { RequestService } from '../../../services/request.service';

@inject(Router, RequestService, ReasonService)
export class Index {
  dborequests
  _router
  _requestService
  _reasonService
  reasons
  constructor(router, requestService, _reasonService) {
    debugger        
    this._router = router
    this._requestService = requestService
    this._reasonService = _reasonService
  }

  activate(params) {
    debugger
    this.dborequests = { 
      page : 1
    }

    this.initRequests()
    this.initReasons()
  }

  initRequests()
  {
    debugger
    this._requestService.RequestsAsync(this.dborequests).then(response => response.json()).then(data => {
        debugger
      this.dborequests = data;
    })
  }

  initReasons()
  {
    this._reasonService.ReasonsAsync().then(response => response.json()).then(data => {
      debugger
      this.reasons = data;
    })
  }

  onFilterApplied()
  {
    debugger
    this.dborequests.requests = undefined
    if(this.dborequests.from  == "")
      this.dborequests.from = undefined
    if(this.dborequests.to == "")
      this.dborequests.to = undefined
    this.initRequests()
  }

  isActive(index, page)
  {
    debugger
    let active = (index == page)
    return active
  }

  goToPage(page)
  {
    debugger
    this.dborequests.page = page;
    this.dborequests.customers = undefined

    this.initRequests()
  }

  next()
  {
    if(this.dborequests.page < this.dborequests.pageCount)
    {
      this.dborequests.page += 1;
      this.initRequests()
    }
  }

  previous()
  {
    if(this.dborequests.page > 1)
    {
      this.dborequests.page -= 1;
      this.initRequests()
    }
  }
}
