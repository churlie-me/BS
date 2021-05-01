import {inject, bindable} from 'aurelia-framework';
import { AuthenticationService } from './services/authentication.service';
import { Router, RouterConfiguration, Activation } from 'aurelia-router';
import { StoreService } from './services/store.service';
import { CartService } from './services/cart.service';
import { BSConfiguration } from './configuration/bs.configuration';
import { InfoDialog } from './extensions/info/info.dialog';
import { PageService } from './services/page.service';
import { Index } from './components/main/index';

@inject(RouterConfiguration, Router, AuthenticationService, StoreService, CartService, BSConfiguration, InfoDialog, PageService, Index)
export class App {
  authenticationService
  cartService
  main
  @bindable storeService
  constructor(config, router, authenticationService, storeService, cartService, configuration, dialog, pageService, _mainPage) {
    this.authenticationService = authenticationService
    this.storeService = storeService
    this.cartService = cartService
    this.configuration = configuration
    this.dialog = dialog
    this.pageService = pageService
    this.authenticationService.localUser()
    this._mainPage = _mainPage

    this.getStore()
  }

  configureRouter(config, router) {
    debugger
    this.router = router;
    config.title = 'BS';
    config.map([
      { route: '', name: 'main', moduleId: PLATFORM.moduleName('components/main/index') },
      { route: 'cart', name: 'cart', moduleId: PLATFORM.moduleName('components/cart/index') },
      { route: 'checkout', name: 'checkout', moduleId: PLATFORM.moduleName('components/checkout/index') },
      { route: 'shop', name: 'shop', moduleId: PLATFORM.moduleName('components/shop/index') },
      { route: 'page/:slug', name: 'pages', moduleId: PLATFORM.moduleName('components/page/index') },
      { route: 'services', name: 'services', moduleId: PLATFORM.moduleName('components/services/index') },
      { route: 'reservation', name: 'reservation', moduleId: PLATFORM.moduleName('components/reservation/index') },
      { route: 'registration', name: 'registration', moduleId: PLATFORM.moduleName('components/registration/index') },
      { route: 'signin', name: 'signin', moduleId: PLATFORM.moduleName('components/signin/index') },
      { route: 'signup', name: 'signup', moduleId: PLATFORM.moduleName('components/signup/index') },
      { route: 'product', name: 'product', moduleId: PLATFORM.moduleName('components/product/index') },
      { route: 'account-sidebar', name: 'account-sidebar', moduleId: PLATFORM.moduleName('components/account/index.html') },
      { route: 'account', name: 'account', moduleId: PLATFORM.moduleName('components/account/detail/index'), layoutView: 'components/account/index.html' },
      { route: 'account/password', name: 'account-password', moduleId: PLATFORM.moduleName('components/account/password/index') , layoutView: 'components/account/index.html' },
      { route: 'account/appointments', name: 'account-appointments', moduleId: PLATFORM.moduleName('components/account/appointments/index'), layoutView: 'components/account/index.html' },
      { route: 'account/orders', name: 'orders', moduleId: PLATFORM.moduleName('components/account/orders/index'), layoutView: 'components/account/index.html' },
      { route: 'admin', name: 'admin', moduleId: PLATFORM.moduleName('components/admin/index'), nav: true },
      { route: 'admin/calendar', name: 'calendar', moduleId: PLATFORM.moduleName('components/admin/calendar/index') },
      { route: 'admin/branches', name: 'admin-store-branches', moduleId: PLATFORM.moduleName('components/admin/branches/index') },
      { route: 'admin/store', name: 'admin-store', moduleId: PLATFORM.moduleName('components/admin/store/index') },
      { route: 'admin/holidays', name: 'admin-store-holidays', moduleId: PLATFORM.moduleName('components/admin/holidays/index') },
      { route: 'admin/services', name: 'admin-services', moduleId: PLATFORM.moduleName('components/admin/services/index') },
      { route: 'admin/orders', name: 'admin-orders', moduleId: PLATFORM.moduleName('components/admin/orders/index') },
      { route: 'admin/pages', name: 'admin-pages', moduleId: PLATFORM.moduleName('components/admin/pages/index') },
      { route: 'admin/pages/page', name: 'admin-page', moduleId: PLATFORM.moduleName('components/admin/page/index') },
      { route: 'admin/appointments', name: 'admin-appointments', moduleId: PLATFORM.moduleName('components/admin/appointments/index') },
      { route: 'admin/service', name: 'admin-service', moduleId: PLATFORM.moduleName('components/admin/service/index') },
      { route: 'admin/products', name: 'admin-products', moduleId: PLATFORM.moduleName('components/admin/products/index') },
      { route: 'admin/product', name: 'admin-product', moduleId: PLATFORM.moduleName('components/admin/product/index') },
      { route: 'admin/users', name: 'admin-users', moduleId: PLATFORM.moduleName('components/admin/users/index') },
      { route: 'admin/users/staff', name: 'admin-staff', moduleId: PLATFORM.moduleName('components/admin/users/staff/index') },
      { route: 'admin/users/customers', name: 'store-customers', moduleId: PLATFORM.moduleName('components/admin/customers/index') },
      { route: 'admin/profile', name: 'admin-profile', moduleId: PLATFORM.moduleName('components/admin/profile/index') },
      { route: 'admin/requests', name: 'admin-contact-requests', moduleId: PLATFORM.moduleName('components/admin/requests/index') },
      { route: 'admin/jobs', name: 'admin-jobs', moduleId: PLATFORM.moduleName('components/admin/jobs/index') },
      { route: 'admin/reports', name: 'admin-reports', moduleId: PLATFORM.moduleName('components/admin/reports/index') },
      { route: 'jobs', name: 'jobs', moduleId: PLATFORM.moduleName('components/jobs/index') },
      { route: 'jobs/job/:id', name: 'job', moduleId: PLATFORM.moduleName('components/jobs/job/index') },
      { route: 'admin/jobs/job/applications/:id', name: 'admin-applications', moduleId: PLATFORM.moduleName('components/admin/jobs/applications/index') },
      { route: 'admin/posts', name: 'admin-posts', moduleId: PLATFORM.moduleName('components/admin/posts/index') },
      { route: 'news', name: 'news', moduleId: PLATFORM.moduleName('components/posts/index') },
      { route: 'news/post/:id', name: 'post', moduleId: PLATFORM.moduleName('components/posts/post/index') }
    ]);
  }

  attached() {
    debugger
  }

  getStore() {
    debugger
    this.storeService.getStore().then(response => response.json()).then(data => {
      debugger
      if(data.name != undefined)
      {
        debugger
        this.getStorePages(data)
      }
      else
        this.router.navigateToRoute('registration');
    }).catch(error => {
      debugger
    })
  }

  getStorePages(store)
  {
    this.pageService.getStorePages(store.id).then(response => response.json()).then(data => {
      debugger
        store.pages = data
        if(store.pages.length > 0)
        {
          this.storeService.mainpage = store.pages.find(p => p.isHomePage == true)

          if(!this.storeService.store)
          {
            this._mainPage._storeService = this.storeService;

            /*this.router.navigateToRoute(
              this.router.currentInstruction.config.name,
              this.router.currentInstruction.params,
              { replace: true }
            );   */

            if(this.storeService.mainpage)
            {
              if(this.storeService.mainpage.slug)
                this.router.navigateToRoute('pages', { slug : this.storeService.mainpage.slug })
                //this.router.navigateToRoute('main', this.router.currentInstruction.params, {replace : true})
                //this.router.navigate('')
                //this.router.refreshNavigation()
            }
          }
        }
        
        this.storeService.store = store 
        localStorage.removeItem("store")
        localStorage.setItem("store", JSON.stringify(this.storeService.store));
    })
  }
}
