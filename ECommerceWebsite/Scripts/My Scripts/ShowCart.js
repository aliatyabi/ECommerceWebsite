function ShowCart() {
    $.ajax({
        url: '/ShowCart',
        type: "GET",
        success: function (data) {
            $("#Cart").html(data);
        }
    });
};