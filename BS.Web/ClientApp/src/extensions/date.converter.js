export class DateConverter {
    view(value) {
        debugger
        return moment(value).format('DD/MM/YYYY');
    }

    form(value)
    {
      return moment(value).format('YYYY-MM-DD');
    }
}
  
