import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class ProductService
{
    bsConfiguration
    http
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

    ProductsAsync(dboproducts){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/article/store', {
        method: 'POST',
        body: json(dboproducts)
      });
    }

    ProductAsync(articleId) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/article/detail/' + articleId);
    }

    SaveProductAsync(product) {
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/article', {
            method: 'POST',
            body: json(product)
        });
    }
}
