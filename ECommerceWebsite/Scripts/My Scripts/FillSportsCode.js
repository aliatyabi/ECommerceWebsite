function FillSportsCode(id) {
    // prevent close drop down list when some item has been selected.
    event.stopPropagation();
    // put tick beside of selected item
    $("li#" + id).toggleClass("opt selected", "opt");

    // if one item selected
    if ($("li#" + id).hasClass("selected")) {
        var selected = $("li#" + id).find("a span").text();
        $("#btnSubCategories span:first-child").html(selected);

        // Disable other items
        $("ul.dropdown-menu#subCategories li").each(function (index) {
            $("ul.dropdown-menu#subCategories li:not(#" + id + ") a").addClass("ui-state-disabled");
        });

        $.ajax({
            url: '/admin/products/GetSportsCode',
            type: "GET",
            data: { sportsCodeId: id },
            async: false,
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (data) {
                if (data != null) {
                }
            }
        });
    }
    else {
        $("#btnSubCategories span:first-child").html(ChooseOneOrMore);

        // Enable all items
        $("ul.dropdown-menu#subCategories li").each(function (index) {
            $("ul.dropdown-menu#subCategories li:not(#" + id + ") a").removeClass("ui-state-disabled");
        });
    }
}