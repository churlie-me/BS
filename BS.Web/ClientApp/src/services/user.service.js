import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class UserService
{
    http
    bsConfiguration
    constructor(http, bsConfiguration){
        this.bsConfiguration = bsConfiguration
        this.http = http
        /*this.http.configure(config => {
            config.withDefaults({
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('auth_token')
            }
            })
        });*/
    }

    getUsers(dboStaff){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/user/staff', {
        method: 'POST',
        body: json(dboStaff)
      });
    }

    searchUsers(name)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/user/search/' + name);
    }

    getCustomers(dbocustomers)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/user/customer/', {
        method: 'POST',
        body: json(dbocustomers)
      })
    }

    getStaff(id)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + "/account/staff/" + id)
    }
}
