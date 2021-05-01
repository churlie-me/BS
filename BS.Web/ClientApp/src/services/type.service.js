import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class ServiceTypeService
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

    SaveType(type) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/servicetype', {
            method: 'POST',
            body: json(type)
        });
    }

    TypesAsync(dbotype){
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/servicetype/sort/', {
          method: 'POST',
          body: json(dbotype)
      });
    }
}
