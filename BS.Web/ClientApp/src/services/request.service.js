import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class RequestService
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

    RequestsAsync(dborequests){
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/request/sort/', {
          method: 'POST',
          body: json(dborequests)
        })
    }

    Save(request) {
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/request/', {
            method: 'POST',
            body: json(request)
        });
    }
}
