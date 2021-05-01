import { inlineView } from 'aurelia-framework';
export class ProgressDialog
{
    constructor(){}

    show()
    {
        debugger
        $('#dialog').modal('show');
    }

    hide()
    {
        debugger
        $('#dialog').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }
}