function EmptyInputs() {
    $("#Name").val('');
    $("#Email").val('');
    $("#ReviewText").val('');
}

function ReplyReview(id, parentId) {
    if ($("div[id^='Review_']").length) {
        $("div[id^='Review_']").each(function () {
            $(this).html('');
        });
    };

    $.get("/Product/AddReview/" + id + "?ParentId=" + parentId, function (res) {
        $("#Review_" + parentId).html(res);
    });
}

function SetRating(id) {
    $("li a").each(function () {
        if ($("li a").hasClass('active')) {
            $("li a").removeClass('active');
        }
    });

    $("li#" + id + " a").addClass('active');

    $("li#" + id + " input").prop('checked', true);
}

$(function () {
    $("li#5 input").prop('checked', true);
});

function AddToCart() {
    var id = productId;
    var sizeId = $("#SizeId option:selected").val();
    var quantity = $("#Quantity").val();

    if(sizeId == 0){
        $("#error").fadeIn();
    }
    else
    {
        $("#loadingImage").show();
        $.ajax({
            url: '/AddToCart',
            type: "GET",
            data: { id: id,sizeId: sizeId,quantity: quantity },
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (data) {
                $("#Cart").html(data);
                $("#itemAdded").fadeIn("fast");
                $("#itemAdded").fadeOut(4000);
                $("#loadingImage").hide();
            }
        });
    }

    return false;
};

function ShowCart(){
    $.ajax({
        url: '/ShowCart',
        type: "GET",
        success: function (data) {
            $("#Cart").html(data);
        }
    });
};

function GetQuantityOfSize(){
    if($("#error").is(":visible")){
        $("#error").fadeOut();
    }
    if ($("#SizeId option:selected").val() != 0) {
        $("#loadingImage").show();
        $.ajax({
            url: '/GetQuantityOfSize',
            type: "GET",
            data: { productId: productId, sizeId: $("#SizeId option:selected").val()},
            success: function (data) {
                $("#Quantity").prop('max', data);
                $("#loadingImage").hide();
                if($("#Quantity").val() > data){
                    if (lang === "fa-IR")
                    {
                        alert('تعداد وارد شده بیش از موجودی می باشد!');
                    }
                    else
                    {
                        alert('Quantity is more than stock!');
                    }
                    $("#Quantity").val(data);
                }
            }
        });
    }
}

function AddToWishList(id) {
    if(isFavourite == "False"){
        $("#Wishlist").css("background-color","#bc9e6b");
    }
    else{
        //$("#Wishlist").css("background-color","");
        //$("#Wishlist i").css("background-color","");

        $("#Wishlist").removeAttr("background-color");
    }

    $.ajax({
        url: '/AddToWishList',
        type: "GET",
        data: { id: id },
        error: function (request, status, error) {
            alert(request.responseText);
        },
        success: function (data) {
            $("#AddToWishList").html(data);
        }
    });
};

$(function () {
    $(document).ajaxError(function (e, xhr) {
        var pathname = window.location.pathname;
        if (xhr.status == 403) {
            var response = $.parseJSON(xhr.responseText);
            window.location = response.LogOnUrl + '?ReturnUrl=' + pathname;
        }
    });
});

function ParsUnob() {
    $.validator.unobtrusive.parse("form");
}