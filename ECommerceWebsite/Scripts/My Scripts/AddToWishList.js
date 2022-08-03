function AddToWishList() {
    $.ajax({
        url: '/AddToWishList',
        type: "GET",
        data: { id: id },
        error: function (request, status, error) {
            alert(request.responseText);
        },
        success: function (data) {
        }
    });
};