import { AuthenticationService } from "./authentication.service";
import {inject} from 'aurelia-framework';
import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";

@inject(HttpClient, BSConfiguration)
export class BaseService
{
  constructor(http, bsConfiguration){
    debugger
    this.http = http
    this.bsConfiguration = bsConfiguration
    this.http.configure(config => {
        config.withDefaults({
        headers: {
            'Authorization': 'Bearer ' + getUser().refreshToken
        }
      })
    });
  }

  getUser()
  {
    let json = localStorage.getItem("account");
      if(json)
      {
          return JSON.parse(json)
      }
  }
}
