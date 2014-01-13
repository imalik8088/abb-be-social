function registerAvatarUpload(userId) {
    var reader;
    var SIZE_LIMIT = 204800; //200kB

    function errorHandler(evt) {
        switch (evt.target.error.code) {
            case evt.target.error.NOT_FOUND_ERR:
                alert('File Not Found!');
                break;
            case evt.target.error.NOT_READABLE_ERR:
                alert('File is not readable');
                break;
            case evt.target.error.ABORT_ERR:
                break; // noop
            default:
                alert('An error occurred reading this file.');
        };
    }

    function handleFileSelect(evt) {

        reader = new FileReader();
        reader.onerror = errorHandler;
        reader.onabort = function (e) {
            var uploadedFile = document.getElementById("fileAvatar").files[0];
            var size = uploadedFile.size;

            if (!uploadedFile.type.match('image.*')) {
                alert('Unable to upload, file not a picture!');
            }
            else if (size > SIZE_LIMIT)
                alert('Unable to upload, file larger than 200KB!');
        };

        reader.onloadstart = function (e) {
            var uploadedFile = document.getElementById("fileAvatar").files[0];
            var size = uploadedFile.size;

            //check if the uploaded file is a picture
            if (!uploadedFile.type.match('image.*')) {
                reader.abort();
            }
            else if (size > SIZE_LIMIT) {
                //check the picture size. if it's over the size limit, abort the upload
                reader.abort();
            }
        };
        reader.onload = function (e) {
            //update the avatar and post the change to db
            AjaxChangeAvatar(userId, e.target.result);
            $("#feedAvatar").attr("src", e.target.result);
        }

        // Read in the image file as a binary string.
        reader.readAsDataURL(evt.target.files[0]);
    }

    //add the event listener for the file selection event on the filepicture input
    document.getElementById('fileAvatar').addEventListener('change', handleFileSelect, false);
}

function AjaxChangeAvatar(userId, base64String) {
    PageMethods.AjaxChangeAvatar(userId, base64String, AjaxChangeAvatarSuccess);
}
function AjaxChangeAvatarSuccess(result, userContext, methodName) {
}