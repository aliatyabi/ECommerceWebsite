function showImage(image) {
    if (image.files && image.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#selectedimage').attr('src', e.target.result);
        }

        reader.readAsDataURL(image.files[0]);
    }
}

$("#imageName").change(function () {
    showImage(this);
});