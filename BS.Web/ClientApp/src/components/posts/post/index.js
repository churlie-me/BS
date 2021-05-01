import { inject } from 'aurelia-framework';
import { ImageConverter } from '../../../extensions/image.converter';
import { ModelDialog } from '../../../extensions/modal.dialog';
import { AuthenticationService } from '../../../services/authentication.service';
import { PostService } from '../../../services/post.service';
import { StoreService } from '../../../services/store.service';
@inject(PostService, StoreService, ModelDialog, ImageConverter, AuthenticationService)

export class Index{
  _postService
  _storeService
  isBusy = false
  _modalDialog
  _documentConverter
  _authenticationService
  jobId
  constructor(_postService, _storeService, _modalDialog, _documentConverter, _authenticationService)
  {
    this._postService = _postService
    this._storeService = _storeService
    this._modalDialog = _modalDialog
    this._documentConverter = _documentConverter
    this._authenticationService = _authenticationService
  }

  activate(params)
  {
    debugger
    this.initPost(params.id)
  }

  initPost(id)
  {
    debugger
    this._postService.PostAsync(id).then(response => response.json()).then(data => {
      debugger
      this.post = data;
    })
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

  share()
  {
    this.isBusy = true
  }
}
