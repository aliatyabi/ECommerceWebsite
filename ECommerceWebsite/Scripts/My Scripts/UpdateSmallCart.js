function UpdateSmallCart() {
    $.ajax({
        url: "/ShowCart",
        type: "Get",
    }).success(function (result) {
        $("#Cart").html(result);
        $("#loadingImage").hide();
    });
}