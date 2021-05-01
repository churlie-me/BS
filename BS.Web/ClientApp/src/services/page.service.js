import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class PageService
{
  http
  bsConfiguration
  constructor(http, bsConfiguration)
  {
    this.http = http
    this.bsConfiguration = bsConfiguration
  }

  save(page)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/page/', {
        method: 'POST',
        body: json(page)
    });
  }

  getPages(dbopages)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/page/pages', {
      method: 'POST',
      body: json(dbopages)
  });
  }

  getPage(pageid)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/page/detail/' +  pageid);
  }

  getStorePages()
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/page/detailed/');
  }
}
