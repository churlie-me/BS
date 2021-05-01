import { inject } from "aurelia-framework"
import { Router } from "aurelia-router"
import { ImageConverter } from "../../../extensions/image.converter"
import { ProductService } from "../../../services/product.service";
import { CategoryService } from "../../../services/category.service";
import { BrandService } from "../../../services/brand.service";
import { Guid } from "../../../extensions/guid";
import { ModelDialog } from "../../../extensions/modal.dialog";
import { ProgressDialog } from "../../../extensions/progress/progress.dialog";

@inject(Router, ProductService, CategoryService, BrandService, ImageConverter, Guid, ModelDialog, ProgressDialog)
export class Index {
    productId
    storeId
    product = {}
    categories
    brands
    image
    background
    instruction
    title
    idescription
    category = {}
    brand = {}
    data_category
    data_brand
    _productService
    _categoryService
    _router
    Guid
    modalDialog
    progressDialog
    constructor(router, productService, categoryService, brandService, imageConverter,Guid, modalDialog, progressDialog) {
        this._productService = productService
        this._categoryService = categoryService
        this._brandService = brandService
        this.imageConverter = imageConverter
        this._router = router;
        this.Guid = Guid
        this.modalDialog = modalDialog
        this.progressDialog = progressDialog
    }

    activate(params) {
        debugger
        this.productId = params.productId
        this.storeId = params.storeId
        this.InitCategories()
        this.InitBrands()

        if(this.productId)
            this.InitProduct();
        else
            this.product = { instructions : []}
    }

    onInitInstruction(instruction)
    {
        debugger
        if(instruction)
            this.instruction = instruction
        else
            this.instruction = { id : this.Guid.create() }
    }

    InitProduct()
    {
        this._productService.ProductAsync(this.productId).then(response => response.json()).then(data => {
            //this.busyIndicator.off();
            debugger
            this.product = data;
        })
    }

    SaveStore() {
        this.progressDialog.show()
        debugger
        this.product.saleItem.name = this.product.name
        if(!this.product.id)
            this.product.id = this.Guid.create()

        this._productService.SaveProductAsync(this.product).then(response => response.json()).then(data => {
            debugger
            this.progressDialog.hide()
            if(JSON.parse(data.isSuccessStatusCode))
            {
                this.router.navigateToRoute('admin/product/', "productId", this.product.id);
            }
        }).catch(error => {
            this.progressDialog.hide()
        })
    }

    async OnArticleChanged()
    {
        debugger
        let _lg = document.getElementById("image")
        this.imageConverter.ConvertToBase64(_lg);
        await this.Wait();
        this.product.image = this.imageConverter.base64String
    }

    async OnBackgroundChanged()
    {
        debugger
        let _lg = document.getElementById("backgroundImage")
        this.imageConverter.ConvertToBase64(_lg);
        await this.Wait();
        this.product.background = this.imageConverter.base64String
    }

    Wait()
    {
        return new Promise(resolve => setTimeout(resolve, 1000));
    }

    InitCategories()
    {
        debugger
        this.dbocategory = { type : 0, page : 1, pageSize: 200 }
        this._categoryService.CategoriesAsync(this.dbocategory).then(response => response.json()).then(data => {
            //this.busyIndicator.off();
            debugger
            this.categories = data.categories;
        })
    }

    InitBrands()
    {
        debugger
        this._brandService.BrandsAsync(this.storeId).then(response => response.json()).then(data => {
            //this.busyIndicator.off();
            debugger
            this.brands = data;
        })
    }

    OnSaveInstruction()
    {
        debugger
        if(!this.product.instructions.find(i => i.id == this.instruction.id))
          this.product.instructions.push(this.instruction)
        debugger
        this.modalDialog.hideModal('#instruction')
    }

    deleteInstruction()
    {
      this.instruction.deleted = true
      this.modalDialog.hideModal('#warning')
    }

    OnCreateCategory()
    {
        debugger
        let category = {
            id: this.Guid.create(),
            name: this.data_category,
            type : 0
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

    OnCreateBrand()
    {
        debugger
        let brand = {
            id: this.Guid.create(),
            name: this.data_brand
        }

        this._brandService.SaveBrandAsync(brand).then(response => response.json()).then(data => {
            debugger
            if(JSON.parse(JSON.stringify(data)))
            {
                this.brands.push(brand)
                this.modalDialog.hideModal('#brand')
            }
        }).catch(error => {
          
        })
    }
}
