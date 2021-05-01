import { inlineView } from 'aurelia-framework';
export class InfoDialog
{
    constructor(){}

    show()
    {
        debugger
        $('#info').modal('show');
    }

    hide()
    {
        debugger
        $('#info').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }
}
