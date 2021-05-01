import { inject } from 'aurelia-framework';
import { DateConverter } from '../../extensions/date.converter';
import { PostService } from '../../services/post.service';
import { StoreService } from '../../services/store.service';
@inject(PostService, StoreService, DateConverter)
export class Index {
  _postService
  _storeService
  _dateConverter
  constructor(_postService, _storeService, _dateConverter)
  {
    this._postService = _postService
    this._storeService = _storeService
    this._dateConverter = _dateConverter
  }

  activate(params)
  {
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

  initPost(post)
  {
    debugger
    this.post = post
    this.post.postedOn = this._dateConverter.form(this.post.postedOn)
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

  bindBackgroundStyles()
  {
    debugger
    let style = 'padding: 50px 0px;'
    if( this._storeService.store.storeImage )
    {
      style += 'background-image: url("data:image/jpeg;base64,' + this._storeService.store.storeImage + '"); background-size: cover;'
    }
    else if(this._storeService.store.primaryColor)
    {
      style += 'background: ' + this._storeService.store.primaryColor
    }
    return style;
  }

  applyFilter()
  {
    this.dboPost.posts = undefined
    this.initPosts()
  }
}
