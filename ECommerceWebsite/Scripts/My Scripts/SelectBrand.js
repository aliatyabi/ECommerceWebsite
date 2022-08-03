$(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var selectedBrand = urlParams.getAll('brands');
    $("input:not(#" + selectedBrand + ")").prop("checked", false);

    $("input#" + selectedBrand).prop("checked", true);
});