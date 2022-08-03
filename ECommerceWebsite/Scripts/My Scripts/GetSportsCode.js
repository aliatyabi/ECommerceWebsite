function GetSportsCode(divisionId) {
    $.ajax({
        url: '/admin/products/FillSportsCode',
        type: "GET",
        data: { divisionId: divisionId },
        async: false,
        error: function (request, status, error) {
            alert(request.responseText);
        },
        success: function (data) {
            if (data != null) {
                $("#SportsCodeDiv").html(data);
                $('#SportsCodeDiv').fadeIn('slow');
            }
        }
    });
}