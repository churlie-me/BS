import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class AppointmentService
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

    AppointmentsAsync(dboAppointments){
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/appointment/calendar/', {
          method: 'POST',
          body: json(dboAppointments)
        });
    }

    CheckStylistAvailability(StylistSchedule)
    {
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/appointment/stylist/availability', {
        method: 'POST',
        body: json(StylistSchedule)
      });
    }

    SortedAppointmentsAsync(_json){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/appointment/sort/', {
        method: 'POST',
        body: json(_json)
      });
    }

    ClientAppointmentsAsync(userId){
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/appointment/client/' + userId);
    }

    AppointmentAsync(appointmentId) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/appointment/detail/' + appointmentId);
    }

    SaveAppointmentAsync(appointment) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/appointment/', {
            method: 'POST',
            body: json(appointment)
        });
    }
}
