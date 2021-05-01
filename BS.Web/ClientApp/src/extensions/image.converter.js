export class ImageConverter
{
    base64String

     ConvertToBase64(input){
         debugger
        var file = input.files[0];
        if (file) {
            var reader = new FileReader();
  
          reader.onload =this._handleReaderLoaded.bind(this);
  
          reader.readAsBinaryString(file);
      }
    }
  
    _handleReaderLoaded(readerEvt) {
        debugger
        var binaryString = readerEvt.target.result;

        debugger
        this.base64String = btoa(binaryString);
    }

    ConvertToImage(base64string) {
        debugger
        return base64string.replace(/^data:image\/(png|jpg);base64,/, "");
    }


    ConvertBase64ToImage(base64)
    {
        debugger
        return "data:'image/jpeg';base64," + base64;
    }
}