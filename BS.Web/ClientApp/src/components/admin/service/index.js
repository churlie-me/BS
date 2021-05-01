import { inject } from "aurelia-framework"
import { Router } from "aurelia-router"
import { Guid } from "../../../extensions/guid";
import { ImageConverter } from "../../../extensions/image.converter"
import { ModelDialog } from "../../../extensions/modal.dialog";
import { ProgressDialog } from "../../../extensions/progress/progress.dialog";
import { AuthenticationService } from "../../../services/authentication.service";
import { CategoryService } from "../../../services/category.service";
import { ServiceService } from "../../../services/service.service";
import { StoreService } from "../../../services/store.service";
import { ServiceTypeService } from "../../../services/type.service";
import { UserService } from "../../../services/user.service";

@inject(Router, ServiceService, CategoryService, ImageConverter,AuthenticationService,Guid, ModelDialog, ProgressDialog, StoreService, UserService, ServiceTypeService)
export class Index {
    serviceId
    storeId
    service = {}
    categories
    image
    title
    _stylist
    idescription
    category = {}
    data_category
    router
    _serviceService
    _categoryService
    _authenticationService
    imageConverter
    modalDialog
    progressDialog
    storeService
    branches
    Guid
    types = []
    _userService
    constructor(router, _serviceService, _categoryService, imageConverter, authenticationService, Guid, modalDialog, progressDialog, storeService, _userService, _typeService) {
        debugger
        this.router = router
        this._serviceService = _serviceService
        this._categoryService = _categoryService
        this._authenticationService = authenticationService
        this.imageConverter = imageConverter
        this.Guid = Guid
        this.modalDialog = modalDialog
        this.progressDialog = progressDialog
        this.storeService = storeService
        this._userService = _userService
        this._typeService = _typeService
    }

    activate(params) {
        debugger
        this.serviceId = params.serviceId
        this.storeId = params.storeId

        this.initCategories()
        this.initBranches()
        this.initTypes()
        this.dboStaff = { page : 1 }
        this.initStaff()
        if(this.serviceId)
            this.initService();
        else
            this.service = { id : this.Guid.create(), accountBranchServices : [], branchServices : [] }
    }

    initStaff()
    {
      debugger
      this._userService.getUsers(this.dboStaff).then(response => response.json()).then(data => {
          debugger
        this.dboStaff = data;
      })
    }

    initService()
    {
        this._serviceService.ServiceAsync(this.serviceId).then(response => response.json()).then(data => {
            //this.busyIndicator.off();
            debugger
            this.service = data;
        })
    }

    initBranches()
    {
      this.storeService.getBranches().then(response => response.json()).then(async data => {
        this.branches = data
      });
    }

    initBranch(branchService)
    {
      this.branchService = this.service.branchServices.find(b => b.branchId == branchService.branchId)
    }

    onDeleteAffiliatedBranch()
    {
      debugger
      if(this.branchService)
        this.branchService.deleted = true

      this.modalDialog.hideModal("#_deleteBranch")
    }

    isChecked(branch)
    {
      debugger
      if(this.service.branchServices)
      {
      let affiliated = this.service.branchServices.find(x => x.branchId == branch.id)
      if(affiliated)
        return true
      else
        return false
      }

      return false
    }

    onBranchSelected(branch)
    {
      debugger
      if(!this.service.branchServices)
        this.service.branchServices = []

      if(!this.service.branchServices.find(x => x.branchId == branch.id))
        this.service.branchServices.push({ branchId: branch.id, branch : branch, serviceId: this.service.id})

      return true;
    }

    onStaffSelected(staff)
    {
      debugger
      if(!this.service.accountBranchServices)
        this.service.accountBranchServices = []

      if(!this.service.accountBranchServices.find(x => x.accountId == staff.id))
        this.service.accountBranchServices.push({ serviceId: this.service.id, accountId: staff.id, account : staff})

      return true;
    }

    onDeleteAffiliatedAccount()
    {
      debugger
      if(this.serviceAccount)
        this.serviceAccount.deleted = true

      this.modalDialog.hideModal("#_deleteAccount")
    }

    isStaffChecked(staff)
    {
      debugger
      if(this.service.accountBranchServices)
      {
      let affiliated = this.service.accountBranchServices.find(x => x.accountId == staff.id && !x.deleted)
      if(affiliated)
        return true
      else
        return false
      }

      return false
    }

    SaveService() {
      this.progressDialog.show()
      debugger
      this.service.createdBy = this._authenticationService.account.id
      this._serviceService.SaveServiceAsync(this.service).then(response => response.json()).then(data => {
          debugger
          this.progressDialog.hide()
          if(JSON.parse(JSON.stringify(data)))
              this.router.navigateBack()
      }).catch(error => {
          this.progressDialog.hide()
      })
    }

    onServiceImageChanged()
    {
        debugger
        let _lg = document.getElementById("image")
        this.imageConverter.ConvertToBase64(_lg);
        new Promise(resolve => setTimeout(resolve, 1000))
        this.service.image = this.imageConverter.base64String
    }

    initCategories()
    {
        debugger
        this.dbocategory = { type : 1, page : 1, pageSize: 200 }
        this._categoryService.CategoriesAsync(this.dbocategory).then(response => response.json()).then(data => {
            debugger
            this.categories = data.categories;
        })
    }

    initTypes()
    {
        debugger
        this.dbotype = { page : 1, pageSize: 200 }
        this._typeService.TypesAsync(this.dbotype).then(response => response.json()).then(data => {
            debugger
            this.types = data.types;
        })
    }

    OnCreateType()
    {
        debugger
        this.isBusy = true
        this._typeService.SaveType(this.servicetype).then(response => response.json()).then(data => {
            debugger
            this.isBusy = false
            if(JSON.parse(JSON.stringify(data)))
            {
                this.types.push(this.servicetype)
                this.modalDialog.hideModal('#_type')
            }
        }).catch(error => {
            
        })
    }

    initAccount(serviceAccount)
    {
      this.serviceAccount = serviceAccount
    }

    OnCreateCategory()
    {
        debugger
        let category = {
            id: this.Guid.create().toString(),
            name: this.data_category,
            type : 1
        }

        this._categoryService.SaveCategory(category).then(response => response.json()).then(data => {
            debugger
            if(JSON.parse(JSON.stringify(data)))
            {
                this.categories.push(category)
                this.modalDialog.hideModal('#category')
            }
        }).catch(error => {
            
        })
    }

    initType()
    {
      debugger
      this.servicetype = { id: this.Guid.create().toString() }
      this.modalDialog.showModal("#_type")
    }


    goToPage(page)
  {
    debugger
    this.dboAccounts.page = page;
    this.dboAccounts.accounts = undefined

    this.initStaff()
  }

  next()
  {
    debugger
    if(this.dboAccounts.page < this.dboAccounts.pageCount)
    {
      this.dboAccounts.page += 1;
      this.dboAccounts.accounts = undefined
      this.dboAccounts.from = (this.dboAccounts.from == "")? undefined : this.dboAccounts.from
      this.dboAccounts.to = (this.dboAccounts.to == "")? undefined : this.dboAccounts.to
      this.initStaff()
    }
  }

  previous()
  {
    if(this.dboAccounts.page > 1)
    {
      this.dboAccounts.page -= 1;
      this.initStaff()
    }
  }

    async OnLightIconChanged()
    {
      debugger
        let _lg = document.getElementById("lighticon")
        this.imageConverter.ConvertToBase64(_lg);

        await new Promise(resolve => setTimeout(resolve, 1000))
        this.servicetype.lightIcon = this.imageConverter.base64String 
    }

    async OnDarkIconChanged()
    {
        let _lg = document.getElementById("darkicon")
        this.imageConverter.ConvertToBase64(_lg);

        await new Promise(resolve => setTimeout(resolve, 1000))
        this.servicetype.darkIcon = this.imageConverter.base64String 
    }
}
