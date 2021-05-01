export class TimeValueConverter
{
    view(value) {
        var val = moment(value).format('HH:mm');
        if(val == 'Invalid date')
            return value
        else
            return val
    }
}
