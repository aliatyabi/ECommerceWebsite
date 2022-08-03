function FillDivisions(id) {
    // prevent close drop down list when some item has been selected.
    event.stopPropagation();
    // put tick beside of selected item
    $("li#" + id).toggleClass("opt selected", "opt");

    // if one item selected
    if ($("li#" + id).hasClass("selected")) {
        var selected = $("li#" + id).find("a span").text();
        $("#btnSubCategory span:first-child").html(selected);

        // Disable other items
        $("ul.dropdown-menu#subCategory li").each(function (index) {
            $("ul.dropdown-menu#subCategory li:not(#" + id + ") a").addClass("ui-state-disabled");
        });

        $(function () {
            GetSportsCode(id);
        });
    }
    else {
        $("#btnSubCategory span:first-child").html(ChooseOneOrMore);

        // Enable all items
        $("ul.dropdown-menu#subCategory li").each(function (index) {
            $("ul.dropdown-menu#subCategory li:not(#" + id + ") a").removeClass("ui-state-disabled");
        });

        $("#SportsCodeDiv").html('');
    }
}