import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject, noView, bindable, bindingMode, customElement, BindingEngine, inlineView } from 'aurelia-framework';
import 'jquery';
import moment from  'moment';

@inject(HttpClient, BSConfiguration, {
    providedIn: 'root',
})
export class StoreService
{
    store
    http
    bsConfiguration
    mainpage
    constructor(http, bsConfiguration){
        this.http = http
        this.bsConfiguration = bsConfiguration
        /*this.http.configure(config => {
            config.withDefaults({
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('auth_token')
            }
            })
        });*/

        //Store
        this.getLocalStore()
    }

    loadStyles(store)
    {
      let btns = document.getElementsByClassName('btn-secondary')
      Array.prototype.forEach.call(btns, function(btn) {
        btn.style.background = store.primaryColor;
        btn.style.border = "1px solid " + store.primaryColor
      });

      Array.prototype.forEach.call(document.getElementsByClassName('btn-outline-secondary'), function(btn) {
        btn.style.color = store.primaryColor;
        btn.style.border = "1px solid " + store.primaryColor
      });

      Array.prototype.forEach.call(document.getElementsByClassName('active'), function(element) {
        element.style.background = store.primaryColor;
        element.style.border = "1px solid " + store.primaryColor
      });

      Array.prototype.forEach.call(document.getElementsByClassName('navbar'), function(element) {
        element.style.background = store.headerColor
        element.style.color = store.headerTextColor
      });
    }

    getLocalStore()
    {
        let json = localStorage.getItem("store");
        if (json)
        {
            this.store = JSON.parse(json);
            this.loadMainPage()
        }
    }

    loadMainPage()
    {
      if(this.store.pages.length > 0)
          this.mainpage = this.store.pages.find(p => p.isHomePage == true)
    }

    getStore() {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/');
    }

    getStoreBySlug(slug)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/slug/' + slug)
    }

    getHomePage()
    {
      if(this.store)
      {
        if(this.store.pages)
        {
          if(this.store.pages.length > 0)
          {
            let page = this.store.pages.find(p => p.isHomePage == true)
            return page.slug
          }
          else
          {
            return undefined
          }
        }
      }

      return undefined
    }

    getBranches() {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/branches/');
    }

    StoresAsync(){
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store');
    }

    getStoreHolidays(dboholiday)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/holiday/fetch/', {
        method: 'POST',
        body: json(dboholiday)
      });
    }

    saveHoliday(holiday)
    {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/holiday/', {
            method: 'POST',
            body: json(holiday)
        });
    }

    getBranchSeats(branchId)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/branch/seat/'+ branchId);
    }

    saveBranchAsync(branch)
    {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/branch/', {
            method: 'POST',
            body: json(branch)
        });
    }

    getUserStores(accountId)
    {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store/user/' + accountId);
    }

    SaveStoreAsync(store)
    {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/store', {
            method: 'POST',
            body: json(store)
        });
    }
}
