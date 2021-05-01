import { inject } from 'aurelia-framework';
import { StoreService } from '../../services/store.service';
@inject(StoreService)
export class Index
{
  _storeService
  constructor(_storeService){
    this._storeService = _storeService
  }

  activate()
  {
    this._storeService.loadStyles()
  }
}
