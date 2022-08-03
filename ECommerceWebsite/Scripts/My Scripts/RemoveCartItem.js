function RemoveCartItem(productId, sizeId) {
    $("#loadingImage").show();
    $.ajax({
        url: "/RemoveCartItem",
        type: "Get",
        data: { productId: productId, sizeId: sizeId },
    }).success(function (result) {
        $("#divshoppingCart").html(result);
    });

    UpdateSmallCart();
}