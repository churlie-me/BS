import { ProgressDialog } from "../../../extensions/progress/progress.dialog";
import { inject } from "aurelia-framework";
import { PageService } from "../../../services/page.service";
import { StoreService } from "../../../services/store.service";
import { ModelDialog } from "../../../extensions/modal.dialog";

@inject(PageService, ProgressDialog, StoreService, ModelDialog)
export class Index
{
  pageService
  progressDialog
  _storeService
  pages
  page
  modal
  constructor(pageService, progressDialog, _storeService, modal)
  {
    this.pageService = pageService
    this.progressDialog = progressDialog
    this._storeService = _storeService
    this.modal = modal
    this.dbopages = { page : 1 }
    this.initPages()
  }

  initPage(page)
  {
    this.page = page
  }

  initPages()
  {
    this.pageService.getPages(this.dbopages).then(response => response.json()).then(data => {
      debugger
      this.dbopages = data;
    })
  }

  save()
  {
    debugger
    this.page.deleted = true
    this.isBusy = true
    this.pageService.save(this.page).then(response => response.json()).then(data => {
      debugger
      if(JSON.parse(JSON.stringify(data)))          
      {
        this.isBusy = false
        this.modal.hideModal('#warning')
        this.pages = undefined
        this.initPages()
      }
    })
    .catch(error => {
      debugger
      this.isBusy = false
    })
  }

  goToPage(page)
  {
    debugger
    this.dbopages.page = page;
    this.initPages()
  }

  next()
  {
    if(this.dbopages.page < this.dbopages.pageCount)
    {
      this.dbopages.page += 1;
      this.initPages()
    }
  }

  previous()
  {
    if(this.dbopages.page > 1)
    {
      this.dbopages.page -= 1;
      this.initPages()
    }
  }
}
