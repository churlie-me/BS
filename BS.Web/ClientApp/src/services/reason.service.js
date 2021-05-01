import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class ReasonService
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

    ReasonsAsync(){
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/reason/');
    }

    Save(reason) {
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/reason/', {
            method: 'POST',
            body: json(reason)
        });
    }
}
