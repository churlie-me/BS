import { StoreService } from "../../../services/store.service"
import { inject } from "aurelia-framework"
import { Router } from "aurelia-router"
import { ImageConverter } from "../../../extensions/image.converter"
import { AuthenticationService } from "../../../services/authentication.service";
import { Guid } from "../../../extensions/guid";
import { TimeValueConverter } from "../../../extensions/time.converter";
import { ModelDialog } from "../../../extensions/modal.dialog";
import { ProgressDialog } from "../../../extensions/progress/progress.dialog";
import { ReasonsModule } from "./reasons/reasons";

@inject(StoreService, AuthenticationService, Router, ImageConverter, Guid, ModelDialog, TimeValueConverter, ProgressDialog, ReasonsModule)
export class Index {
    _storeId
    store
    name
    logo
    street
    housenumber
    city
    url
    storeService
    authenticationService
    router
    imageConverter
    guid
    modalDialog
    progressDialog
    schedule
    timeValueConverter
    week = new Array("sunday","monday","tuesday","wednesday","thursday","friday","saturday");
    constructor(storeService, authenticationService, router, imageConverter, guid, modalDialog, timeValueConverter, progressDialog, reasonsModule) {
        this.storeService = storeService
        this.authenticationService = authenticationService
        this.router = router
        this.imageConverter = imageConverter
        this.guid = guid
        this.modalDialog = modalDialog
        this.timeValueConverter = timeValueConverter
        this.progressDialog = progressDialog
        this.reasonsModule = reasonsModule
    }

    initialisePlugins()
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

    activate(params) {
      this.store = {schedules : []}
      this.initialisePlugins()
    }

    attached() {
      this.getStore();
    }

  getStore()
  {
    this.progressDialog.show()
      this.storeService.getStore().then(response => response.json()).then(async data => {
        debugger
        await new Promise(resolve => setTimeout(resolve, 1000));
          this.progressDialog.hide()
          this.store = data
          if(this.store.schedules == 0)
          {
              this.store.schedules = [
                  { id: this.guid.create(), day: 0, status: 1 },
                  { id: this.guid.create(), day: 1, status: 1 },
                  { id: this.guid.create(), day: 2, status: 1 },
                  { id: this.guid.create(), day: 3, status: 1 },
                  { id: this.guid.create(), day: 4, status: 1 },
                  { id: this.guid.create(), day: 5, status: 1 },
                  { id: this.guid.create(), day: 6, status: 1 }
              ];
          }
          this.initialisePlugins()
          this.reasonsModule.initReasons(this.store.id)
      }).catch(error => {
        console.error(error)
        this.progressDialog.hide()
        this.getStore()
      })
  }

  StatusChanged(schedule)
  {
      this.schedule = schedule    
      if(this.schedule.status == 1)
      {
        this.schedule.openAt = undefined
        this.schedule.closedAt = undefined
      }
      else
      {
        this.schedule.openAt = this.schedule.openAt? this.timeValueConverter.view(this.schedule.openAt) : undefined
        this.schedule.closedAt = this.schedule.closedAt? this.timeValueConverter.view(this.schedule.closedAt) : undefined
      }
      
      return true;
  }

  onSubmitSchedule()
  {
    this.modalDialog.hideModal("#_schedule")
  }

  SaveStore() 
  {
    this.progressDialog.show()
    if(!this.store.id)
    {
        this.store.id = this.guid.create()
        this.store.accounts = []
        this.store.accounts.push({storeId: this.store.id, accountId: this.authenticationService.account.id, accountType: 1})
    }

      this.store.primaryColor = document.getElementById("primary-color").value
      this.store.secondaryColor = document.getElementById("secondary-color").value
      this.store.headerBackgroundColor = document.getElementById("header-background-color").value
      this.store.headerTextColor = document.getElementById("header-text-color").value
      this.store.footerBackgroundColor = document.getElementById("footer-background-color").value
      this.store.footerTextColor = document.getElementById("footer-text-color").value

    this.storeService.SaveStoreAsync(this.store).then(response => response.json()).then(data => {
        this.progressDialog.hide()
    }).catch(error => {
        console.error(error)
        this.progressDialog.hide()
    })
  }

    async OnLogoChanged()
    {
        let _lg = document.getElementById("logo")
        this.imageConverter.ConvertToBase64(_lg);

        await new Promise(resolve => setTimeout(resolve, 1000))
        this.store.logo = this.imageConverter.base64String 
    }

    async OnImageChanged()
    {
        let _lg = document.getElementById("storeImage")
        this.imageConverter.ConvertToBase64(_lg);

        await new Promise(resolve => setTimeout(resolve, 1000))
        this.store.storeImage = this.imageConverter.base64String 
    }
}
