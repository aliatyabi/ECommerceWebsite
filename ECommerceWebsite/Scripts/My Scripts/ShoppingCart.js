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
};

function ChangeQuantity(productId, sizeId, quantity) {
    var i = "Qty_" + productId + "_" + sizeId;
    var qty = $("#" + i).val();
    $("#loadingImage").show();
    $.ajax({
        url: "/ChangeQuantity",
        type: "Get",
        data: { productId: productId, sizeId: sizeId, quantity: $(qty).val() },
    }).success(function (result) {
        $("#divshoppingCart").html(result);
    });

    UpdateSmallCart();
}

function IncreaseQuantity(productId, sizeId, quantity) {
    var i = "Qty_" + productId + "_" + sizeId;
    var qty = $("#" + i).val();
    $("#loadingImage").show();
    $.ajax({
        url: "/ChangeQuantity",
        type: "Get",
        data: { productId: productId, sizeId: sizeId, quantity: parseInt(qty, 10) + 1 },
    }).success(function (result) {
        $("#divshoppingCart").html(result);
    });

    UpdateSmallCart();
}

function DecreaseQuantity(productId, sizeId, quantity) {
    var i = "Qty_" + productId + "_" + sizeId;
    var qty = $("#" + i).val();
    $("#loadingImage").show();
    $.ajax({
        url: "/ChangeQuantity",
        type: "Get",
        data: { productId: productId, sizeId: sizeId, quantity: parseInt(qty, 10) - 1 },
    }).success(function (result) {
        $("#divshoppingCart").html(result);
    });

    UpdateSmallCart();
}

function UpdateSmallCart() {
    $.ajax({
        url: "/ShowCart",
        type: "Get",
    }).success(function (result) {
        $("#Cart").html(result);
    });
    $("#loadingImage").hide();
}