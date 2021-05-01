import { inject, BindingEngine } from 'aurelia-framework';
import { Router } from 'aurelia-router';import { ModelDialog } from '../../../extensions/modal.dialog';
;
import { ServiceService } from '../../../services/service.service';

@inject(Router, ServiceService, ModelDialog)
export class Index {
  dboproducts
  router
  _serviceService
  modaldialog
  constructor(router, _serviceService, modaldialog) {
    this.router = router
    this._serviceService = _serviceService
    this.modaldialog = modaldialog
      debugger        
  }

  activate(params) 
  {
      debugger
      this.dboservices = {
        page : 1
      }
      this.initServices()
  }

  initService(service)
  {
    this.service = service
  }

  initServices()
  {
    debugger
    this._serviceService.ServicesAsync(this.dboservices).then(response => response.json()).then(data => {
      //this.busyIndicator.off();
        debugger
      this.dboservices = data;
    })
  }

  save() {
    this.isBusy = true    
    this.service.deleted = true
    this._serviceService.SaveServiceAsync(this.service).then(response => response.json()).then(data => {
        debugger
        if(JSON.parse(JSON.stringify(data)))
        {
          this.modaldialog.hideModal('#warning')
          this.dboservices.services = undefined
          this.initServices()
        }
    }).catch(error => {
        this.progressDialog.hide()
    })
  }

  _search()
  {
    debugger
    if(this.dboservices.search == "")
      this.dboservices.search = undefined
    this.initServices()
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
    this.dboservices.page = page;
    this.initServices()
  }

  next()
  {
    if(this.dboservices.page < this.dboservices.pageCount)
    {
      this.dboservices.page += 1;
      this.initServices()
    }
  }

  previous()
  {
    if(this.dboservices.page > 1)
    {
      this.dboservices.page -= 1;
      this.initServices()
    }
  }
}
