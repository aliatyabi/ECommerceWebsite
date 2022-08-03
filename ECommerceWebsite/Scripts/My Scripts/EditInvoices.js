function DisableValidator() {
    if ($(".table tbody").length) {
        $("#invoiceForm").validate().settings.ignore = "*";
    }
}

function AddInvoiceItem() {
    var rowCount = $("#ShowInvoiceItems table tr").length;
    $.ajax({
        url: "/admin/Invoice/AddInvoiceItems",
        type: "Get",
        data: {
            id: rowCount, quantity: $("#Quantity").val(), fee: $("#Fee").val(), discountFee: $("#DiscountFee").val(),
            productId: $("#ProductId").find("option:selected").val(), sizeId: $("#SizeId").find("option:selected").val()
        }
    }).done(function (result) {
        $("#ShowInvoiceItems").html(result);
        //ClearAll();
    });
}

function ClearAll() {
    $("#Quantity").val("");
    $("#Fee").val("");
    $("#DiscountFee").val("");
    $("#ProductId").selectpicker('deselectAll');
    $("#ProductId").selectpicker('refresh');
    $("#SizeId").selectpicker('deselectAll');
    $("#SizeId").prop('disabled', true);
    $("#SizeId").selectpicker('refresh');
    $("#productImage").prop('src', '/Images/no-image.png');
    $("#productImage").prop('alt', 'no-image.png');
}

function DeleteInvoiceItem(id) {
    $.ajax({
        url: "/admin/Invoice/DeleteInvoiceItem",
        type: "Get",
        data: { id: id }
    }).done(function (result) {
        $("#ShowInvoiceItems").html(result);
    });
}

$(function () {
    $("#SizeId").prop("disabled", true);

    $('#ProductId').on('change', function () {

        $.ajax({
            url: "/admin/Invoice/GetProductImage",
            type: "Get",
            data: { id: $(this).find("option:selected").val() },
            dataType: "json"
        }).done(function (result) {
            $("#productImage").prop('src', "/Images/Products/Thumb/" + result);
            $("#productImage").prop('alt', result);
        });

        if ($(this).find("option:selected").val() === undefined || $(this).find("option:selected").val() === null || $(this).find("option:selected").val() === "") {
            $("#productImage").prop('src', "/Images/no-image.png");
            $("#productImage").prop('style', "width:150px;");
            $("#productImage").prop('alt', "no-image.png");
        }
    });
});

function FillSizes() {
    if ($('#ProductId').find("option:selected").val() === undefined || $('#ProductId').find("option:selected").val() === null || $('#ProductId').find("option:selected").val() === "") {
        $("#SizeId").prop('disabled', true);
        $('#SizeId').selectpicker('refresh');
    }

    $.ajax({
        url: "/admin/Invoice/FillSizes",
        type: "Get",
        dataType: "JSON",
        data: { id: $("#ProductId").find("option:selected").val() },
    }).done(function (sizes) {
        $("#SizeId").prop('disabled', false);
        $("#SizeId").html("");
        $("#SizeId").append(
            $('<option></option>').html(chooseOneItem));
        $.each(sizes, function (i, size) {
            $("#SizeId").append(
                    $('<option></option>').val(size.SizeId).html(size.Size));
            $('#SizeId').selectpicker('refresh');
        });
    });
}

$(function () {
    if ($('#TransactionType').find("option:selected").val() === undefined || $('#TransactionType').find("option:selected").val() === null || $('#TransactionType').find("option:selected").val() === "") {
        $("#Quantity").prop('disabled', true);
        $("#Fee").prop('disabled', true);
        $("#DiscountFee").prop('disabled', true);
        $("#ProductId").prop('disabled', true);
        $("#btnSubmit").prop('disabled', true);
        $("#addItems").addClass("disabled").click(function (e) {
            e.preventDefault();
        });
    }

    $('#TransactionType').change(function () {
        var x = $('#TransactionType').find("option:selected").val();
        if ($('#TransactionType').find("option:selected").val() === undefined || $('#TransactionType').find("option:selected").val() === null || $('#TransactionType').find("option:selected").val() === "") {
            $("#Quantity").prop('disabled', true);
            $("#Fee").prop('disabled', true);
            $("#DiscountFee").prop('disabled', true);
            $("#ProductId").prop('disabled', true);
            $('#ProductId').selectpicker('refresh');
            $("#btnSubmit").prop('disabled', true);
            $("#addItems").addClass("disabled").click(function (e) {
                e.preventDefault();
            });
        }
        else {
            $("#Quantity").prop('disabled', false);
            $("#Fee").prop('disabled', false);
            $("#DiscountFee").prop('disabled', false);
            $("#ProductId").prop('disabled', false);
            $('#ProductId').selectpicker('refresh');
            $("#addItems").removeClass("disabled");
            $("#btnSubmit").prop('disabled', false);
        }
    });
});