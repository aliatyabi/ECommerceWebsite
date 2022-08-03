$('#StateId').change(function () {
    var x = $(this).prop('value');
    if ($(this).prop('value') == 0 || $(this).prop('value') == "") {
        $('#cities').empty();
        $('#cities').append('<option value="">' + ChooseOneItem + '</option>')
    }
    else {
        $.ajax({
            dataType: 'json',
            url: "/ShoppingCart/ShowCities",
            type: "Get",
            data: { id: $(this).prop('value') }
        }).done(function (data) {
            $('#cities').empty();
            $.each(data, function (i) {
                var option = $('<option value=' + data[i].CityId + '>' + data[i].CityName + '</option>');
                $("#cities").append(option);
            });
        });
    }
});