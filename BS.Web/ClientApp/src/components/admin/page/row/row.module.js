import { inlineView, inject } from 'aurelia-framework';
import { Guid } from '../../../../extensions/guid';
import { ImageConverter } from '../../../../extensions/image.converter';
import { ModelDialog } from '../../../../extensions/modal.dialog';
@inject(Guid, ModelDialog, ImageConverter)
export class RowModule
{
  contentTypes = ['Header', 'Text', 'Image', 'Link', 'Space', 'Row']
  index
  row
  column
  nestingColumn
  constructor(guid, modal, imageConverter)
  {
    this.guid = guid
    this.modal = modal
    this.imageConverter = imageConverter
  }

  setColumn(column, nestingColumn)
  {
    debugger
    this.column = column
    this.nestingColumn = nestingColumn
  }

  setColumns(row, cols)
  {
    debugger
    if(row.columns.length < cols)
    {
      let _columns = (cols - row.columns.length)
      for(let i = 0; i < _columns; i++)
      {
        let column = { id : this.guid.create(), contents : [], deleted : 0, index : row.columns.length }
        row.columns.push(column)
      }
    }
    else
    {
      let _reducedColumns = (row.columns.length - cols)
      for(let j = 1; j <= _reducedColumns; j++)
      {
        let index = row.columns.length - j
        row.columns[index].deleted = 1
      }
    }
    row.columns = this.filterDeletedColumns(row.columns)
  }

  filterDeletedColumns(columns)
  {
    debugger
    let cols = columns.filter(c => c.deleted == 0)
    return cols
  }

  deleteRow()
  {
    debugger
    this.row.deleted = 1
  }

  setRow(row)
  {
    this.row = row
    this.item = 'Row'
  }

  setColumnRow(row)
  {
    debugger

    this.row = row
    this.item = 'Row'
  }

  async deleteContent()
  {
    this.modal.hideModal('#header')
    this.modal.hideModal('#text')
    this.modal.hideModal('#link')
    this.modal.hideModal('#_image')
    this.modal.hideModal('#space')
    await new Promise(resolve => setTimeout(resolve, 1000))
    this.item = "Content"

    this.modal.showModal("#delete")
  }

  confirmContentDelete()
  {
    this.content.deleted = true
  }

  setContent(contentType)
  {
    debugger
    this.modal.hideModal('#builder')
    switch(contentType)
    {
      case 0:
        this.content = {id : this.guid.create(), contentType : 0, deleted : 0 }
        this.modal.showModal('#header')
        break;
      case 1:
        this.content = { id : this.guid.create(), contentType : 1, fontSize : 14, textColor : '', deleted : 0 }
        this.modal.showModal("#text")
        break;
      case 2:
        this.content = { id : this.guid.create(), contentType : 2, deleted : 0 }
        this.modal.showModal("#_image")
        break;
      case 3:
        this.content = { id : this.guid.create(), contentType : 3, deleted : 0 }
        this.modal.showModal("#link")
        break;
      case 4:
        this.content = { id : this.guid.create(), contentType : 4, deleted : 0 }
        this.modal.showModal("#space")
        break;
      case 5:
        this.content = { 
          id : this.guid.create(), 
          contentType : 5, 
          deleted : 0,
          row : { id : this.guid.create(), columns : [{ id : this.guid.create(), contents : [], deleted : 0, index : 0 }], deleted : 0  }
        }
        break;
    }

    if(this.content)
    {
      this.content.index = this.column.contents.length
      this.column.contents.push(this.content)
    }
  }

  rowSettings(row)
  {
    this.row = row
    this.column = undefined
    this.modal.showModal('#_rowsettings')
  }

  columnSettings(column)
  {
    this.column = column
    this.row = undefined
    this.modal.showModal('#_columnsettings')
  }

  async OnImageChanged()
  {
    debugger
        let _lg = document.getElementById("image")
        this.imageConverter.ConvertToBase64(_lg);
        await new Promise(resolve => setTimeout(resolve, 1000))
        this.content.image = this.imageConverter.base64String
  }

  async OnBackgroundImageChanged()
  {
    debugger
    let _bi = document.getElementById("backgroundImage")
    this.imageConverter.ConvertToBase64(_bi);
    await new Promise(resolve => setTimeout(resolve, 1000))

    if(this.row)
      this.row.backgroundImage = this.imageConverter.base64String
    else
      this.column.backgroundImage = this.imageConverter.base64String
  }

  initContent(_content)
    {
      debugger
      this.content = _content

      switch(_content.contentType)
      {
        case 0:
          this.modal.showModal('#header')
          break;
        case 1:
          this.modal.showModal("#text")
          break;
        case 2:
          this.modal.showModal("#_image")
          break;
        case 3:
          this.modal.showModal("#link")
          break;
        case 4:
          this.modal.showModal("#space")
          break;
      }
    }

  allowDrop(ev) {
    ev.preventDefault();
  }
  
  drag(content, ev) {
    this.content = content
    this.draggedItem = ev.target
    ev.dataTransfer.setData("tonberry", ev.target.id);
    return true;
  }
  
  drop(column, ev) {
    debugger
    this.column = column
    ev.preventDefault();
    var data = ev.dataTransfer.getData("tonberry");
    ev.currentTarget.appendChild(document.getElementById(data));
    let index = Array.from(ev.currentTarget.children).indexOf(this.draggedItem)
    let displacedContent = this.column.contents.find(x => x.index == index)
    if(displacedContent)
      displacedContent.index = this.content.index
      
    this.content.index = index
    debugger
  }

  dropRow(ev) {
    debugger
    ev.preventDefault();
    var data = ev.dataTransfer.getData("tonberry");
    ev.currentTarget.appendChild(document.getElementById(data));
    let index = Array.from(ev.currentTarget.children).indexOf(this.draggedItem)
    this.row.index = index
    debugger
  }

  dragRow(row, ev)
  {
    this.row = row
    this.draggedItem = ev.target
    ev.dataTransfer.setData("tonberry", ev.target.id);
    return true;
  }
}
