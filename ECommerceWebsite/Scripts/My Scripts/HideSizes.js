$("[id^=productBox_]").mouseleave(function () {
    var productId = $(this).attr("id").match(/[\d]+$/)[0];
    $("#sizeDiv_" + productId).fadeOut();
    //$("#sizes_" + productId).children("li").remove();
});