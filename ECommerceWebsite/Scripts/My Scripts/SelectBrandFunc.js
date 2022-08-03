function selectBrand(brand) {
    //var selectedBrand = $("div.ui-checkbox").find("input").prop("id", brand);
    var selectedBrand = brand.id;
    //$("input").find(selectedBrand).prop("checked", true);
    //var selectedBrand = $("input#" + selectedBrand).prop('id');
    $("input:not(#" + selectedBrand + ")").prop("checked", false);

    $("input#" + selectedBrand).prop("checked", true);

    filterProducts();
}