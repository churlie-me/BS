import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class JobService
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

    JobsAsync(dbojobs){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/job/listing', {
        method: 'POST',
        body: json(dbojobs)
      });
    }

    JobAsync(jobId) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/job/' + jobId);
    }

    ApplyAsync(application)
    {
      debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/application', {
            method: 'POST',
            body: json(application)
        });
    }

    JobApplicationsAsync(dboJobApplication)
    {
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/application/listing', {
        method: 'POST',
        body: json(dboJobApplication)
      });
    }

    SaveJobAsync(job) {
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/job', {
            method: 'POST',
            body: json(job)
        });
    }
}
