function AddFeature() {
    $.ajax({
        url: "/admin/products/AddFeature",
        type: "Get",
        data: { id: $("#FeaturesId :selected").val(), featureValue: $("#FeatureValues :selected").text() }
    }).done(function (result) {
        $("#showFeatures").html(result);
        $('#FeatureValues').empty();
        $('#FeatureValues').append('<option value="">' + chooseOneItem + '</option>')
    });
}

function DeleteFeature(id, featureValue) {
    $.ajax({
        url: "/admin/products/DeleteFeature",
        type: "Get",
        data: { id: id, featureValue: featureValue }
    }).done(function (result) {
        $("#showFeatures").html(result);
    });
}

$('#FeaturesId').change(function () {
    if ($(this).attr('value') == 0 || $(this).attr('value') == "") {
        $('#FeatureValues').empty();
        $('#FeatureValues').append('<option value="">' + chooseOneItem + '</option>')
        $("#FeatureValues").selectpicker("refresh");
    }
    else {
        $.ajax({
            dataType: 'json',
            url: "/admin/products/ShowFeatureValues",
            type: "Get",
            data: { id: $(this).attr('value') }
        }).done(function (data) {
            $('#FeatureValues').empty();
            $.each(data, function (i) {
                var option = $('<option value=' + data[i].FeatureValueId + '>' + data[i].FeatureValue + '</option>');
                $("#FeatureValues").append(option);
                $("#FeatureValues").selectpicker("refresh");
            });
        });
    }
});