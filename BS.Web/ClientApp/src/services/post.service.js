import { HttpClient, json } from "aurelia-fetch-client";
import { BSConfiguration } from "../configuration/bs.configuration";
import { inject } from "aurelia-framework";

@inject(HttpClient, BSConfiguration)
export class PostService
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

    PostsAsync(dboPosts){
      debugger
      return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/post/listing', {
        method: 'POST',
        body: json(dboPosts)
      });
    }

    PostAsync(postId) {
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/post/' + postId);
    }

    SaveAsync(post) {
        debugger
        return this.http.fetch(this.bsConfiguration.REST_API_SERVICE + '/post/', {
            method: 'POST',
            body: json(post)
        });
    }
}
