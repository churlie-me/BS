import { AuthenticationService } from "./authentication.service";
import {inject} from 'aurelia-framework';
import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";

@inject(HttpClient, BSConfiguration, AuthenticationService)
export class HolidayService
{
  constructor(http, bsConfiguration, authenticationService){
    debugger
    this.http = http
    this.bsConfiguration = bsConfiguration
    this.http.configure(config => {
        config.withDefaults({
        headers: {
            'Authorization': 'Bearer ' + authenticationService.account.refreshToken
        }
        })
    });
  }

  HolidaysAsync(id){
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/holiday/' + id);
  }

  StaffAvailabilityAsync(id, appointmentDate){
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/holiday/availability/staff/'+ id + '/appointmentDate/' + appointmentDate);
  }

  StoreAvailabilityAsync(appointmentDate){
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/holiday/availability/store/' + appointmentDate);
  }

  save(holiday) {
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/holiday/', {
          method: 'POST',
          body: json(holiday)
      });
  }
}
