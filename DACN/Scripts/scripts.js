function ShowImagePreview(imageUploader, previewImage, formUpload) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(formUpload).hide();
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}