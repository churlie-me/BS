import { inject } from "aurelia-framework";
import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { Router } from "aurelia-router";
import { BaseService } from "./base.service";

@inject(HttpClient, BSConfiguration, Router)
export class AuthenticationService {
    account
    mode = 'client'
    IsLoggedIn
    http
    bsConfiguration
    router
    constructor(http, bsConfiguration, router) {
      this.http = http
      this.bsConfiguration = bsConfiguration
      this.router = router
      this.localUser()
      this.userMode()
    }

    userMode()
    {
      var _mode = localStorage.getItem("mode");
      if(_mode)
          this.mode = _mode
    }

    localUser()
    {
      let json = localStorage.getItem("account");
      if(json)
      {
          this.IsLoggedIn = true
          this.account = JSON.parse(json)
      }
    }

    login(credentials) {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/account/authenticate', {
          method: 'POST',
          body: json(credentials)
      });
    }

    findAccount(mail)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/account/' + mail);
    }

    logout()
    {
      this.account = undefined
      localStorage.removeItem('account')
      localStorage.removeItem('mode')
      localStorage.setItem('mode', 'client')
      this.mode = 'client'
      
      this.router.navigateToRoute("main")
      location.reload(true)
    }

    grantAccess()
    {
      localStorage.removeItem('mode')
      localStorage.setItem('mode', 'admin')
      this.mode = 'admin'
      this.router.navigateToRoute("admin")
      location.reload(true)
    }

    exitAdmin()
    {
      localStorage.removeItem('mode')
      localStorage.setItem('mode', 'client')
      this.mode = 'client'
      this.router.navigateToRoute("main")
      location.reload(true)
    }

    register(account) {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/account/register', {
          method: 'POST',
          body: json(account)
      });
    }

    sendInvitation(account) {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/account/registration/invite', {
          method: 'POST',
          body: json(account)
      });
    }

    changePwd(dboAccount)
    {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/account/password/change/', {
          method: 'POST',
          body: json(dboAccount)
      });
    }

    update(account) {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/account/update', {
          method: 'POST',
          body: json(account)
      });
    }

    refreshtoken() {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/account/refreshToken', {
          method: 'POST',
          body: json({
              token: localStorage.getItem("auth_token"),
              refreshtoken: localStorage.getItem("refresh_token")
          })
      })
    }

    getUserRoles() {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/role')
    }

    addRole(role) {
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/role', {
          method: 'POST',
          body: json(role)
      });
    }
}
