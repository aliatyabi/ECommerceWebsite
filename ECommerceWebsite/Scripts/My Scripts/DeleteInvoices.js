function deleteInvoice(id) {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + DeleteInvoice + '</strong>')
    $("#modalBody").load("/admin/Invoice/delete/" + id);
}

function closeModal() {
    //$("#modal-responsive").modal('hide');
    $(".close").click();
}