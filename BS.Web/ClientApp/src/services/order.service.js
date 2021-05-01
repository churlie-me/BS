import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class OrderService
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

  ordersAsync(storeId){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/store/' +  storeId);
  }

  orders(dboOrder)
  {
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/filter', {
      method: 'POST',
      body: json(dboOrder)
  });
  }

  userOrdersAsync(userId){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/customer/' +  userId);
  }

  getOrder(orderId){
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/' +  orderId);
  }

  getAppointmentOrder(appointmetId){
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/appointment/' +  appointmetId);
  }

  save(order) {
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/', {
          method: 'POST',
          body: json(order)
      });
  }

  branchRevenueAsync(dboReport)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/report/branch/', {
      method: 'POST',
      body: json(dboReport)
    });
  }

  productRevenueAsync(dboReport)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/report/product/', {
      method: 'POST',
      body: json(dboReport)
    });
  }

  serviceRevenueAsync(dboReport)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/report/service/', {
      method: 'POST',
      body: json(dboReport)
    });
  }

  seatRevenueAsync(dboReport)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/report/seat/', {
      method: 'POST',
      body: json(dboReport)
    });
  }

  branchRevenueAsync(dboReport)
  {
    debugger
    return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/order/report/branch/', {
      method: 'POST',
      body: json(dboReport)
    });
  }
}
