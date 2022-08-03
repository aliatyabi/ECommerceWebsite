function deleteProduct(id) {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + deleteProduct + '</strong>')
    $("#modalBody").load("/admin/Products/delete/" + id);
}

function closeModal() {
    //$("#modal-responsive").modal('hide');
    $(".close").click();
}