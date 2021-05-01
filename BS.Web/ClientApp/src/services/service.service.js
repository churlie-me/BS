import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class ServiceService
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

    ServicesAsync(dboservices){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/service/store', {
          method: 'POST',
          body: json(dboservices)
      });
    }

    ServiceAsync(serviceId) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/service/detail/' + serviceId);
    }

    SaveServiceAsync(service) {
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/service', {
            method: 'POST',
            body: json(service)
        });
    }
}
