function PicClick(id) {
    var mainPicCount = 0;
    //var thumbPicCount = 0;

    // Count how many mainpic are checked
    $("input[class^=mainPic]").each(function () {
        if ($(this).is(":checked")) {
            mainPicCount += 1;
        }
    });

    if (mainPicCount == 0) {
        $("#btnSubmit").prop('disabled', true);
        alert("Main picture must be at least one image");
    }

    // if mainpic selected are above one then submit button will disabled
    if (mainPicCount > 1) {
        $("#btnSubmit").prop('disabled', true);
        alert("Main picture must be one image");
    }
    else {
        $("#btnSubmit").prop('disabled', false);
    }

    // Count how many mainpic are checked
    //$("input[class^=thumbPic]").each(function () {
    //    if ($(this).is(":checked")) {
    //        thumbPicCount += 1;
    //    }
    //});

    // if thumbpic selected are above one then submit button will disabled
    //if (thumbPicCount > 1) {
    //    $("#btnSubmit").prop('disabled', true);
    //    alert("Thumbnail picture must be one image");
    //}
    //else {
    //    $("#btnSubmit").prop('disabled', false);
    //}

    // if mainpic and thumbpic are equal one then submit button will enabled
    if (mainPicCount == 1 /*&& thumbPicCount == 1*/) {

        var mainPicParent;
        var mainPicRow;
        var mainPicName;
        //var thumbPicParent;
        //var thumbPicRow;
        //var thumbPicName;

        $("input[class^=mainPic]").each(function () {
            if ($(this).is(":checked")) {
                mainPicParent = $(this).parents()[2];
                mainPicRow = $(mainPicParent).children()[1];
                mainPicName = $(mainPicRow).find("input").val();
            }
        });

        //$("input[class^=thumbPic]").each(function () {
        //    if ($(this).is(":checked")) {
        //        var x = 2;
        //        thumbPicParent = $(this).parents()[2];
        //        thumbPicRow = $(thumbPicParent).children()[1];
        //        thumbPicName = $(thumbPicRow).find("input").val();
        //    }
        //});
        //var radioParent = $("input.mainPic" + id).parents()[2];
        //var mainPicRow = $(radioParent).children()[1];
        //var imageName = $(mainPicRow).find("input").val();

        $.ajax({
            url: '/admin/products/Images',
            type: "Get",
            dataType: "JSON",
            data: { mainPicName: mainPicName /*, thumbPicName: thumbPicName*/ },
            async: false,
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (data) {
                if (data != null) {
                }
            }
        });
    }
    else {
        $("#btnSubmit").prop('disabled', true);
    }
}