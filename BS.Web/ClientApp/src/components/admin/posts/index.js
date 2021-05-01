import { inject, BindingEngine } from 'aurelia-framework';
import { PostService } from '../../../services/post.service';
import { Router } from 'aurelia-router';
import { StoreService } from '../../../services/store.service';
import { DateConverter } from '../../../extensions/date.converter';
import { ProgressDialog } from '../../../extensions/progress/progress.dialog';
import { ImageConverter } from '../../../extensions/image.converter';
import { AuthenticationService } from '../../../services/authentication.service';

@inject(Router, PostService, StoreService, DateConverter, ProgressDialog, ImageConverter, AuthenticationService)
export class Index {
  dboPost
  router
  _postService
  _storeService
  _dateConverter
  _progressDialog
  fileConverter
  post
  constructor(_router, _postService, _storeService, _dateConverter, _progressDialog, fileConverter, _authenticationService) {
    this.router = _router
    this._postService = _postService
    this._storeService = _storeService
    this._dateConverter = _dateConverter
    this._progressDialog = _progressDialog
    this.fileConverter = fileConverter
    this._authenticationService = _authenticationService
    debugger        
  }

  activate(params) {
    debugger
    this.dboPost = 
    {
      page : 1
    };
    this.initPosts()
  }

  initPosts()
  {
    debugger
    this._postService.PostsAsync(this.dboPost).then(response => response.json()).then(data => {
        debugger
      this.dboPost = data;
    })
  }

  isActive(index, page)
  {
    debugger
    let active = (index == page)
    return active
  }

  initPost(post)
  {
    debugger
    this.post = post
  }

  goToPage(page)
  {
    debugger
    this.dboPost.page = page;
    this.dboPost.posts = undefined

    this.initPosts()
  }

  next()
  {
    if(this.dboPost.page < this.dboPost.pageCount)
    {
      this.dboPost.page += 1;
      this.initPosts()
    }
  }

  previous()
  {
    if(this.dboPost.page > 1)
    {
      this.dboPost.page -= 1;
      this.initPosts()
    }
  }

  savePost()
  {
    this._progressDialog.show()
    debugger
    this.post.accountId = this._authenticationService.account.id
    this._postService.SaveAsync(this.post).then(response => response.json()).then(data => {
      debugger
      this._progressDialog.hide()
      if(JSON.parse(JSON.stringify(data)))
      {
          this.post = undefined
          this.dboPost.posts = undefined
          this.initPosts()
      }
    }).catch(error => {
      this._progressDialog.hide()
    })
  }

  onDeletePost()
  {
    debugger
    this.post.deleted = true
    this.savePost()
  }

  async OnPostImageChanged()
  {
    debugger
    let image = document.getElementById("image")
    this.fileConverter.ConvertToBase64(image);
    await this.Wait();
    if(!this.post)
      this.post = {}
    this.post.image = this.fileConverter.base64String
  }

  Wait()
  {
      return new Promise(resolve => setTimeout(resolve, 1000));
  }
}
