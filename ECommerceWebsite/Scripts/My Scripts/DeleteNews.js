function deleteNews(id) {
    $.ajax({
        url: "/Admin/News/Delete/" + id,
        type: "Get",
        data: {}
    }).done(function (result) {
        $("#modalBody").html(result);
    });
}