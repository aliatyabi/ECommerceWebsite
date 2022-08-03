function createFeature() {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + CreateFeature + '</strong>')
    $("#modalBody").load("/admin/Features/create");
}

function editFeature(id) {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + EditFeature + '</strong>')
    $("#modalBody").load("/admin/Features/edit/" + id);
}

function deleteFeature(id) {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + DeleteFeature + '</strong>')
    $("#modalBody").load("/admin/Features/delete/" + id);
}

function closeModal() {
    //$("#modal-responsive").modal('hide');
    $(".close").click();
}