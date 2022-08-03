function FillGenders() {
    // get selected value of dropdownlist
    var selected = $("#CategoryId").val();
    if (selected != null)  // if one category selected others will disable and when it deselect genders will remove.
    {
        // if just one item was selected disable other options
        if (selected.length == 1) {
            $("#Category ul.dropdown-menu li:not(.selected)").each(function (index) {
                $("#Category ul.dropdown-menu li:not(.selected) a").addClass("ui-state-disabled");
            });

            // if there is second drop down list then hide it
            $("#DivisionDiv").html('');
            $("#SportsCodeDiv").html('');

            // if there is any sub category they will remove
            if ($('#addedCategory').length) {
                $("dt#addedCategory").remove();
                $(".div-contain").remove();
                $("li[class^=addedCategory]").removeClass("selected");
                $("[class^=addedCategory]").remove();
                $("optgroup#addedLabel").remove();

                // if uncheck parent category then all parent categories will enable
                if (!$("#Category li").hasClass("selected")) {
                    $("#Category ul.dropdown-menu li").each(function (index) {
                        $("#Category ul.dropdown-menu li a").removeClass("ui-state-disabled");
                    });

                    // no item selected
                    $("#CategoryId").val(null);

                    //change title of drop down list
                    $("button.selectpicker").prop("title", ChooseOneOrMore);
                    $("button.selectpicker span.filter-option").text(ChooseOneOrMore);
                }

                $("[class^=addedCategory]").each(function (index) {
                    $("a.ui-state-disabled").removeClass("ui-state-disabled");
                });
            }
        }

        var brandId = selected[0];
        var genderId = selected[1];
    }
        // if first sub category uncheck then parent category uncheck and there is no item selected
        // then all sub categories will remove
    else {
        $("#Category ul.dropdown-menu li").each(function (index) {
            $("#Category ul.dropdown-menu li a").removeClass("ui-state-disabled");
        });
        var brandId = null;
        $("dt#addedCategory").remove();
        $(".div-contain").remove();
        $("[class^=addedCategory]").remove();
    }

    // if one sub category is selected
    if ($("#Category li").hasClass("selected")) {
        if ($('#addedCategory').length) {
            $("[class^=addedCategory]").each(function (i, obj) {
                if ($(obj).is('#' + genderId)) {
                    $(obj).addClass('selected');
                }
            });

            // disable other sub categories
            $("ul.dropdown-menu li[class^=addedCategory]:not(.selected)").each(function (index) {
                $("ul.dropdown-menu li[class^=addedCategory]:not(.selected) a").addClass("ui-state-disabled");
            });
        }
            // when one parent category is selected then it will show its sub categories
        else {
            $.ajax({
                url: '/admin/products/FillGenders',
                type: "GET",
                dataType: "JSON",
                data: { brand: brandId },
                async: false,
                error: function (request, status, error) {
                    alert(request.responseText);
                },
                success: function (genders) {
                    if ($("#Category ul.dropdown-menu li").hasClass('selected') && genders.length > 0) {
                        if ($("select.form-control").find("#addedLabel").length) {
                            $("select.form-control").find("#addedLabel").remove();
                        }

                        $("select.form-control#CategoryId").append(
                                '<optgroup id="addedLabel" label=' + Gender + '></optgroup>');

                        $.each(genders, function (i, gender) {
                            $("#Category ul.dropdown-menu:not(#subCategory)").append(
                                        '<li id="' + gender.CategoryId + '" class="addedCategory_' + i + '">' +
                                        '<a tabindex="0" class="opt" style="" onclick="GetDivisions(' + gender.CategoryId + ')">' +
                                            '<span class="text">' + gender.CategoryTitle + '</span>' +
                                            '<i class="fa fa-check icon-ok check-mark"></i>' +
                                        '</a>' +
                                        '</li>');

                            $("#addedLabel").append('<option value=' + gender.CategoryId + '>' + gender.CategoryTitle + '</option>');
                        });

                        // add option label to group of sub categories
                        $("li.addedCategory_0").prepend(
                        '<div class="div-contain"><div class="divider"></div></div>' +
                        '<dt id="addedCategory"><span class="text">' + Gender + '</span></dt>');
                    }
                }
            });
        }
    }
}