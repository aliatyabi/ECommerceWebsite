$("[id^=productBox_]").mouseenter(function () {
    var productId = $(this).attr("id").match(/[\d]+$/)[0];

    // Show second image
    if ($("#secondaryImage_" + productId).length != 0)
    {
        $("#primaryImage_" + productId).css('opacity', '0');
        $("#secondaryImage_" + productId).css('opacity', '1');
    }

    //if ($("#sizes_" + productId).children().length == 0) {
    if ($("#sizeDiv_" + productId + " span").text() == "" || $("#sizeDiv_" + productId + " span").text() == null) {
        $("#loadingImage_" + productId).show();
        $.ajax({
            url: '/ShowSize',
            type: "GET",
            data: { productId: productId },
            success: function (data) {
                //get data as json from controller and push sizes into array
                var list = [];
                $.each(data, function (index, value) {
                    list.push(index);
                });

                //sort array by size number
                list.sort(function (obj1, obj2) {
                    return obj1 > obj2;
                });

                //show sizes in html
                if (list != null && list != "" && list != undefined) {
                    $.each(list, function (index, value) {
                        $("#sizeDiv_" + productId + " span").text(availableSizes);
                        $("#sizes_" + productId).append('<li style="border:1px solid black; display: inline; padding: 5px; border-top-left-radius: 4px; border-bottom-right-radius:4px; margin:2px;">' + value + '</li>');
                    });
                }
                else {
                    $("#sizeDiv_" + productId + " span").text(notAvailable);
                }
                $("#sizeDiv_" + productId).fadeIn();
                $("#loadingImage_" + productId).hide();
            }
        });
    }
    else {
        $("#sizeDiv_" + productId).fadeIn();
    }
});

$("[id^=productBox_]").mouseleave(function () {
    var productId = $(this).attr("id").match(/[\d]+$/)[0];

    if ($("#secondaryImage_" + productId).length != 0) {
        $("#primaryImage_" + productId).css('opacity', '1');
        $("#secondaryImage_" + productId).css('opacity', '0');
    }

    $("#sizeDiv_" + productId).fadeOut();
    //$("#sizes_" + productId).children("li").remove();
});