$(document).ready(function () {
    $('#slideTitle').change(function (e) {
        var fileName = e.target.files[0].name;
        alert('The file "' + fileName + '" has been selected.');
    });
});

function validate() {
    $("#addFileForm").validate({
        rules: {
            slideTitle: {
                required: true,
            },
            slideImage: "required",
            Link: "required",
        },
        messages: {
            slideTitle: {
                required: SlideTitleRequired,
            },
            slideImage: SlideImageRequired,
            Link: SlideUrlRequired
        }
    });
}

function createSlider() {
    $.ajax({
        url: "/Admin/Sliders/Create",
        type: "Get",
        data: {}
    }).done(function (result) {
        //$("#addFile").modal('show');
        $("#modalBody").html(result);
    });
}

function editSlider(id) {
    $.ajax({
        url: "/Admin/Sliders/Edit/" + id,
        type: "Get",
        data: {}
    }).done(function (result) {
        //$("#addFile").modal('show');
        $("#modalBody").html(result);
    });
}

function deleteSlider(id) {
    $.ajax({
        url: "/Admin/Sliders/Delete/" + id,
        type: "Get",
        data: {}
    }).done(function (result) {
        //$("#addFile").modal('show');
        $("#modalBody").html(result);
    });
}