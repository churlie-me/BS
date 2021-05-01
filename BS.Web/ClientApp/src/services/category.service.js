import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class CategoryService
{
    http
    bsConfiguration
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
    }

    SaveCategory(category) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/category', {
            method: 'POST',
            body: json(category)
        });
    }

    CategoriesAsync(dbocategory){
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/category/sort/', {
          method: 'POST',
          body: json(dbocategory)
      });
    }
}
