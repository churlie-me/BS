export class ModelDialog
{
    constructor(){ }

    hideModal(id)
    {
        debugger
        $(id).modal('hide');
        $('body').removeClass('modal-open');
    }

    showModal(id)
    {
        debugger
        $(id).modal({
          show: true,
          keyboard: false,
          backdrop: 'static'
      });
    }
}
