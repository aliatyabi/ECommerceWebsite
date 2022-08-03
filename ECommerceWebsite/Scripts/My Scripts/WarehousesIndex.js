function CreateWarehouse() {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + CreateWarehouse + '</strong>')
    $("#modalBody").load("/admin/Warehouses/Create");
}

function EditWarehouse(id) {
    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + EditWarehouse + '</strong>')
    $("#modalBody").load("/admin/Warehouses/Edit/" + id);
}

function closeModal() {
    //$("#modal-responsive").modal('hide');
    $(".close").click();
}