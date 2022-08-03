var furtherCategoryId;
var subCategoryId;
var categoryId;

function selectCategory(catId, subId, furtherId) {
    furtherCategoryId = furtherId;
    subCategoryId = subId;
    categoryId = catId;

    // Highlight selected category
    if (categoryId != null && categoryId != undefined) {
        $("a[id^='subcategory_']").removeClass('categoryHover');
        $("a[id^='category_']").removeClass('categoryHover');
        $("a#category_" + categoryId).addClass('categoryHover');
    }

    if (subCategoryId != null && subCategoryId != undefined) {
        $("a[id^='category_']").removeClass('categoryHover');
        $("a[id^='subcategory_']").removeClass('categoryHover');
        $("a#subcategory_" + subCategoryId).addClass('categoryHover');
        $("a#subcategory_" + subCategoryId).parents().eq(2).find('a').eq(0).addClass('categoryHover');
    }

    filterProducts();
}