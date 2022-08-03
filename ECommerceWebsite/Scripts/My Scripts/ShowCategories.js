function createCategory() {
    var ref = $('#tree1').jstree(true),
    id = ref.get_selected();
    if (id[0] == undefined) { id[0] = null; }
    else { id = id[0]; }

    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + CreateCategory + '</strong>')
    $("#modalBody").load("/admin/product_categories/create/" + id);
}

function editCategory() {
    var ref = $('#tree1').jstree(true),
    id = ref.get_selected();
    if (id[0] == undefined) { id[0] = null; }
    else { id = id[0]; }

    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + EditCategory + '</strong>')
    $("#modalBody").load("/admin/product_categories/edit/" + id);
}

function deleteCategory() {
    var ref = $('#tree1').jstree(true),
    id = ref.get_selected();
    if (id[0] == undefined) { id[0] = null; }
    else { id = id[0]; }

    $("#modal-responsive").modal();
    $("#modalTitle").html('<strong>' + DeleteCategory + '</strong>')
    $("#modalBody").load("/admin/product_categories/delete/" + id);
}

function closeModal() {
    //$("#modal-responsive").modal('hide');
    $(".close").click();
}