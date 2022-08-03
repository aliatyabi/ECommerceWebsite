window.onload = function () {
    $("#CategoryId").val("0");

    //Check File API support
    if (window.File && window.FileList && window.FileReader) {
        $('#imageUpload').live("change", function (event) {
            //// By default submit button is disabled because product doesn't have thumb pic.
            $("#btnSubmit").prop('disabled', true);

            var files = event.target.files; //FileList object
            var output = $("#product-review").find('tbody');

            var count = $("#product-review > tbody > tr").length;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                //Only pics
                // if(!file.type.match('image'))
                if (file.type.match('image.*')) {
                    if (this.files[0].size < 2097152) {
                        // continue;
                        var picReader = new FileReader();
                        picReader.fileName = file.name;
                        if (i == 0 && count == 0) { picReader.tagName = "product-main", picReader.checked = "checked", picReader.id = i }
                        else if (i != 0 && count == 0) { picReader.tagName = "product" + i + "-gallery", picReader.checked = "checked", picReader.id = i }
                        else if (count != 0) { picReader.tagName = "product" + count + "-gallery", picReader.checked = "checked", picReader.id = count }
                        picReader.addEventListener("load", function (event) {
                            var picFile = event.target;
                            output.append($('<tr>' +
                                        '<td style="width:20%">' +
                                            '<a href="/Areas/Admin/assets/img/gallery/animal1.jpg" class="magnific" title="Nature 1">' +
                                                '<img src=' + picFile.result + ' alt="' + picFile.fileName + '" class="img-responsive">' +
                                            '</a>' +
                                        '</td>' +
                                        '<td>' +
                                            '<input type="text" class="form-control m-t-10" value="' + picFile.fileName + '">' +
                                        '</td>' +
                                        '<td>' +
                                            '<div class=" ui-radio">' +
                                            '<label class="ui-corner-all ui-btn-inherit ui-btn-icon-left ui-radio-on m-t-30"></label>' +
                                            '<input type="radio" name="' + picFile.tagName + '" class="mainPic' + picFile.id + '"' + picFile.checked + ' data-cacheval="false" onclick="PicClick(' + picFile.id + ')"></div>' +
                                        '</td>' +
                                        //'<td>' +
                                        //    '<div class=" ui-radio">' +
                                        //    '<label class="ui-corner-all ui-btn-inherit ui-btn-icon-left ui-radio-on m-t-30"></label>' +
                                        //    '<input type="radio" name="' + picFile.tagName + '" class="thumbPic' + picFile.id + '"' + picFile.check + ' data-cacheval="false" onclick="PicClick(' + picFile.id + ')"></div>' +
                                        //'</td>' +
                                        '<td>' +
                                            '<div class=" ui-radio">' +
                                            '<label class="ui-corner-all ui-btn-inherit ui-btn-icon-left ui-radio-on m-t-30"></label>' +
                                            '<input type="radio" name="' + picFile.tagName + '" class="galleryPic' + picFile.id + '"' + picFile.checked + ' data-cacheval="false" onclick="PicClick(' + picFile.id + ')"></div>' +
                                        '</td>' +
                                        '<td class="text-center">' +
                                            '<a id="remove' + picFile.id + '" onclick="remove(' + picFile.id + ')" class="delete-img btn btn-sm btn-default m-t-10"><i class="fa fa-times-circle"></i> Remove</a>' +
                                        '</td>' +
                                    '</tr>'));
                        });
                        //Read the image
                        picReader.readAsDataURL(file);

                    } else {
                        alert("Image Size is too big. Minimum size is 2MB.");
                        $(this).val("");
                    }
                } else {
                    alert("You can only upload image file.");
                    $(this).val("");
                }
            }
        });
    }
    else {
        console.log("Your browser does not support File API");
    }
}