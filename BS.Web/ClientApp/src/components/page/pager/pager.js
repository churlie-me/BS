export class Pager
{
  
  bindBackgroundStyles(_row)
  {
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
