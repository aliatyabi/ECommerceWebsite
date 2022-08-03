function createFeatureValue() {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + CreateFeatureValue + '</strong>')
    $("#modalBody").load("/admin/Feature_Values/create");
}

function editFeatureValue(id) {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + EditFeatureValue + '</strong>')
    $("#modalBody").load("/admin/Feature_Values/edit/" + id);
}

function deleteFeatureValue(id) {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + DeleteFeatureValue + '</strong>')
    $("#modalBody").load("/admin/Feature_Values/delete/" + id);
}

function closeModal() {
    //$("#modal-responsive").modal('hide');
    $(".close").click();
}