function GetDivisions(genderId) {
    $.ajax({
        url: '/admin/products/FillDivisions',
        type: "GET",
        data: { genderId: genderId },
        async: false,
        error: function (request, status, error) {
            alert(request.responseText);
        },
        success: function (data) {
            if (data != null) {
                $("#DivisionDiv").html(data);
                $('#DivisionDiv').fadeIn('slow');
            }
        }
    });
}