import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class BrandService
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

    BrandsAsync(storeId){
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/brand/');
    }

    SaveBrandAsync(brand) {
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/brand/', {
            method: 'POST',
            body: json(brand)
        });
    }
}