function remove(id) {
    var row = $("#remove" + id).parent().parent();
    row.remove();
    if (row.find("input[class^='mainPic']").is(":checked")) {
        $("#btnSubmit").prop('disabled', true);
    }
};