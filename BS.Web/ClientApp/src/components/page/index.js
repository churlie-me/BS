import { inject } from 'aurelia-framework';
import { PageService } from '../../services/page.service';
import { StoreService } from '../../services/store.service';

@inject(PageService, StoreService)
export class Index
{
  constructor(pageService, storeService)
  {
    this.pageService = pageService
    this.storeService = storeService
  }

  activate(params) 
  {
    this.slug = params.slug
    this._page = this.storeService.store.pages.find(x => x.slug == this.slug)
  }

  bindBackgroundStyles(_row)
  {
    debugger
    let style = 'padding: 50px 0px;'
    if(_row.backgroundImage)
    {
      style += 'background-image: url("data:image/jpeg;base64,' + _row.backgroundImage + '"); background-size: cover;'
    }
    else if(_row.backgroundColor)
    {
      style += 'background: ' + _row.backgroundColor
    }
    return style;
  }
}
