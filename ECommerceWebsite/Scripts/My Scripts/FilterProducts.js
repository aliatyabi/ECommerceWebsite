function filterProducts(pageNo) {

    if ($("#inStock").prop("checked")) {
        var inStock = 1;
    }
    else {
        var inStock = 0;
    }

    var sortOrder = $("#orderby :selected").attr('id');

    var colors = [];

    $.each($("input[name='color']:checked"), function () {
        colors.push($(this).val());
    });

    if (colors === undefined || colors === null) {
        colors = null;
    }

    var brands = [];

    $.each($("input[name='brand']:checked"), function () {
        brands.push($(this).val());
    });

    if (brands === undefined || brands === null) {
        brands = null;
    }

    pageNo = pageNo || 1;

    $(".pagination li").removeClass("active");
    $(".pagination li").eq(pageNo - 1).addClass("active");

    $('#cover').show();
    $.ajax({
        type: 'GET',
        url: '/Product/FilterProducts',
        traditional: true,
        data: { sortOrder: sortOrder, search: $("#search").val(), colors: colors, minPrice: minPrice, maxPrice: maxPrice, page: pageNo, brands: brands, categoryId: categoryId, subCategoryId: subCategoryId, furtherCategoryId: furtherCategoryId, inStock: inStock },
        dataType: 'html',
        success: function (result) {
            $("#result").html(result);
            var stateObj = { filterProducts: "FilterProducts" };
            history.pushState(stateObj, "page 2", "?sortOrder=" + sortOrder + "&search=" + $("#search").val() + "&colors=" + colors + "&minPrice=" + minPrice + "&maxPrice=" + maxPrice + "&page=" + pageNo + "&brands=" + brands + "&categoryId=" + categoryId + "&subCategoryId=" + subCategoryId + "&furtherCategoryId=" + furtherCategoryId + "&inStock=" + inStock);
            $("#search").val("");
            $('#cover').hide();
        },
        error: function () {
            alert('error');
        }
    });
}