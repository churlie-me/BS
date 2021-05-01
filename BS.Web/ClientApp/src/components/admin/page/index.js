import { inject } from 'aurelia-framework'
import { Guid } from '../../../extensions/guid'
import { ImageConverter } from '../../../extensions/image.converter'
import { ModelDialog } from '../../../extensions/modal.dialog'
import { PageService } from '../../../services/page.service'
import { Router } from "aurelia-router";
import { StoreService } from '../../../services/store.service'
import { ProgressDialog } from '../../../extensions/progress/progress.dialog'
import { RowModule } from './row/row.module'

@inject(PageService, Guid, ModelDialog, ImageConverter, Router, StoreService, ProgressDialog, RowModule)
export class Index
{
  _pageService
  guid
  page
  modal
  content
  column
  row
  imageConverter
  item
  isBusy = false
  _router 
  _storeService
  _progressDialog
  rowModule
  constructor(_pageService, guid, modal, imageConverter, _router, _storeService, _progressDialog, rowModule)
  {
    this._pageService = _pageService
    this.guid = guid
    this.modal = modal
    this.imageConverter = imageConverter
    this._router = _router
    this._storeService = _storeService
    this._progressDialog = _progressDialog
    this.rowModule = rowModule

    this.initialise()
  }

  activate(params) {
    debugger
    let pageId = params.pageId;
    if(!pageId)
      this.page = { rows : [], storeId : this._storeService.store.id, deleted : 0 }
    else
      this.initPage(pageId)
  }

  initPage(pageId)
  {
    debugger
    this._progressDialog.show()
    this.isBusy = true;
    this._pageService.getPage(pageId).then(response => response.json()).then(data => {
      debugger
      this._progressDialog.hide()
      this.isBusy = false;
      this.page = data;
    })
  }

  addRow()
  {
    debugger
    let row = { id : this.guid.create(), columns : [{ id : this.guid.create(), contents : [], deleted : 0, index : 0 }], deleted : 0 }

    row.index = this.page.rows.length
    this.page.rows.push(row) 
  }

  initialise()
  {
    $(document).ready(function() {
      debugger
      //Color Picket
      $(".colorpicker").asColorPicker();
      $(".complex-colorpicker").asColorPicker({
          mode: 'complex'
      });

      $(".gradient-colorpicker").asColorPicker({
          mode: 'gradient'
      });
    });
  }

  save()
  {
    debugger
    this.isBusy = true
    this._pageService.save(this.page).then(response => response.json()).then(data => {
      debugger
      if(JSON.parse(JSON.stringify(data)))          
      {
        this.isBusy = false
        this._router.navigateBack()
      }
    })
    .catch(error => {
      debugger
      this.isBusy = false
    })
  }
}
