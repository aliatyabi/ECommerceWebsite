$(function () {
    $("#images").click(function () {
        //$("input[name *='gallery']").click();
        CheckImages();
    });
})

function FillGenders(x) {
    // get selected value of dropdownlist
    var selected = $("#CategoryId").val();

    if(typeof x != 'undefined')
    {
        selected.push(x);
    }

    if (selected != null)  // if one category selected others will disable and when it deselect genders will remove.
    {
        // if just one item was selected disable other options
        if (selected.length == 1) {
            $("ul.dropdown-menu li:not(.selected)").each(function (index) {
                $("ul.dropdown-menu li:not(.selected) a").addClass("ui-state-disabled");
            });

            // if there is second drop down list then hide it
            $("#DivisionDiv").html('');
            $("#SportsCodeDiv").html('');

            // if there is any sub category they will remove
            if ($('#addedCategory').length) {
                $("dt#addedCategory").remove();
                $(".div-contain").remove();
                $("li[class*=addedCategory]").removeClass("selected");
                $("[class*=addedCategory]").remove();
                $("optgroup#addedLabel").remove();

                // if uncheck parent category then all parent categories will enable
                if (!$("li").hasClass("selected")) {
                    $("ul.dropdown-menu li").each(function (index) {
                        $("ul.dropdown-menu li a").removeClass("ui-state-disabled");
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
        $("ul.dropdown-menu li").each(function (index) {
            $("ul.dropdown-menu li a").removeClass("ui-state-disabled");
        });
        var brandId = null;
        $("dt#addedCategory").remove();
        $(".div-contain").remove();
        $("[class^=addedCategory]").remove();
    }

    // if one sub category is selected
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
                if ($("ul.dropdown-menu li").hasClass('selected') && genders.length > 0) {
                    if ($("select.form-control").find("#addedLabel").length) {
                        $("select.form-control").find("#addedLabel").remove();
                    }

                    $("select.form-control#CategoryId").append(
                            '<optgroup id="addedLabel" label=' + Gender + '></optgroup>');

                    $.each(genders, function (i, gender) {
                        $("ul.dropdown-menu:not(#subCategory)").append(
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

function GetDivisions(genderId) {
    FillGenders(genderId);
    $.ajax({
        url: '/admin/products/FillDivisions',
        type: "GET",
        data: { genderId: genderId },
        async: false,
        error: function (request, status, error) {
            alert(request.responseText);
        },
        success: function (data) {
            if (data != null) {
                $("#DivisionDiv").html(data);
                $('#DivisionDiv').fadeIn('slow');
            }
        }
    });

    $("#DivisionDiv ul.dropdown-menu li:not(.selected) a").removeClass("ui-state-disabled");
}

function GetSportsCode(divisionId) {
    $.ajax({
        url: '/admin/products/FillSportsCode',
        type: "GET",
        data: { divisionId: divisionId },
        async: false,
        error: function (request, status, error) {
            alert(request.responseText);
        },
        success: function (data) {
            if (data != null) {
                $("#SportsCodeDiv").html(data);
                $('#SportsCodeDiv').fadeIn('slow');
            }
        }
    });
}

function FillDivisions(id) {
    // prevent close drop down list when some item has been selected.
    event.stopPropagation();
    // put tick beside of selected item
    $("li#" + id).toggleClass("selected", "");

    // if one item selected
    if ($("li#" + id).hasClass("selected")) {
        var selected = $("li#" + id).find("a span").text();
        $("#DivisionDiv button span:first-child").html(selected);

        // Disable other items
        $("#DivisionDiv ul.dropdown-menu li").each(function (index) {
            $("#DivisionDiv ul.dropdown-menu li:not(#" + id + ") a").addClass("ui-state-disabled");
        });

        $(function () {
            GetSportsCode(id);
        });
    }
    else {
        $("#DivisionDiv button span:first-child").html(ChooseOneOrMore);

        // Enable all items
        $("#DivisionDiv ul.dropdown-menu li").each(function (index) {
            $("#DivisionDiv ul.dropdown-menu li:not(#" + id + ") a").removeClass("ui-state-disabled");
        });

        $("#SportsCodeDiv").html('');
    }
}

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

window.onload = function () {
    //Check File API support
    if (window.File && window.FileList && window.FileReader) {
        $('#imageUpload').live("click change", function (event) {

            var files = event.target.files; //FileList object
            var output = $("#product-review").find('tbody');
            var count = $("#product-review > tbody > tr").length;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                //Only pics
                // if(!file.type.match('image'))
                if (file.type.match('image.*')) {
                    if (this.files[0].size < 2097152) {
                        // continue;
                        var picReader = new FileReader();
                        picReader.fileName = file.name;
                        if (i == 0 && count == 0) { picReader.tagName = "product-main", picReader.checked = "checked", picReader.id = i }
                        else if (i != 0 && count == 0) { picReader.tagName = "product" + i + "-gallery", picReader.checked = "checked", picReader.id = i }
                        else if (count != 0) {
                            var name = "product" + count + "-gallery";
                            var t = $('input[name="' + name + '"]').length;
                            while($('#product-review tbody input[name="' + name + '"]').length > 0){
                                count++;
                                name = "product" + count + "-gallery";
                            }
                            picReader.tagName = "product" + count + "-gallery", picReader.checked = "checked", picReader.id = count,count++
                        }
                        picReader.addEventListener("load", function (event) {
                            var picFile = event.target;
                            output.append($('<tr>' +
                                        '<td style="width:20%">' +
                                            '<a href="/Areas/Admin/assets/img/gallery/animal1.jpg" class="magnific" title="Nature 1">' +
                                                '<img src=' + picFile.result + ' alt="' + picFile.fileName + '" class="img-responsive">' +
                                            '</a>' +
                                        '</td>' +
                                        '<td>' +
                                            '<input type="text" class="form-control m-t-10" value="' + picFile.fileName + '">' +
                                        '</td>' +
                                        '<td>' +
                                            '<div class=" ui-radio">' +
                                            '<label class="ui-corner-all ui-btn-inherit ui-btn-icon-left ui-radio-on m-t-30"></label>' +
                                            '<input type="radio" name="' + picFile.tagName + '" class="mainPic' + picFile.id + '"' + picFile.checked + ' data-cacheval="false" onclick="PicClick(' + picFile.id + ')"></div>' +
                                        '</td>' +
                                        //'<td>' +
                                        //    '<div class=" ui-radio">' +
                                        //    '<label class="ui-corner-all ui-btn-inherit ui-btn-icon-left ui-radio-on m-t-30"></label>' +
                                        //    '<input type="radio" name="' + picFile.tagName + '" class="thumbPic' + picFile.id + '"' + picFile.check + ' data-cacheval="false" onclick="PicClick(' + picFile.id + ')"></div>' +
                                        //'</td>' +
                                        '<td>' +
                                            '<div class=" ui-radio">' +
                                            '<label class="ui-corner-all ui-btn-inherit ui-btn-icon-left ui-radio-on m-t-30"></label>' +
                                            '<input type="radio" name="' + picFile.tagName + '" class="galleryPic' + picFile.id + '"' + picFile.checked + ' data-cacheval="false" onclick="PicClick(' + picFile.id + ')"></div>' +
                                        '</td>' +
                                        '<td class="text-center">' +
                                            '<a id="remove' + picFile.id + '" onclick="remove(' + picFile.id + ')" class="delete-img btn btn-sm btn-default m-t-10"><i class="fa fa-times-circle"></i> Remove</a>' +
                                        '</td>' +
                                    '</tr>'));
                        });
                        //Read the image
                        picReader.readAsDataURL(file);

                    } else {
                        alert("Image Size is too big. Minimum size is 2MB.");
                        $(this).val("");
                    }
                } else {
                    alert("You can only upload image file.");
                    $(this).val("");
                }
            }

            files = null;
        });
    }
    else {
        console.log("Your browser does not support File API");
    }
}

var removedPics = [];
function remove(id) {
    var row = $("#remove" + id).parent().parent();
    var removedPic = $(row).children().eq(1).find('input').val();
    row.css("background-color","#897e7e");

    row.fadeOut(400, function(){
        $(this).remove();
    });
    if (row.find("input[class^='mainPic']").is(":checked")) {
        $("#btnSubmit").prop('disabled', true);
    }

    removedPics.push(removedPic);
};

$("#form1").submit(function(event){
    event.preventDefault();

    PicClick(0);

    $.ajax({
        url: '/admin/products/RemoveImages',
        type: "Get",
        dataType: "JSON",
        traditional: true,
        data: { removedPics: removedPics },
        async: false,
        error: function (request, status, error) {
            alert(request.responseText);
        },
        success: function (data) {
            if (data != null) {
            }
        }
    });

    $(this).unbind('submit').submit();
});

function CheckImages(){
    //Check other images as gallery image
    $("input[class *='gallery']").each(function (index) {
        if(index != 0)
        {
            $(this).prop('checked','checked');
        }
    });
}

function PicClick(id) {
    var mainPicCount = 0;
    //var thumbPicCount = 0;

    // Count how many mainpic are checked
    $("input[class^=mainPic]").each(function () {
        if ($(this).is(":checked")) {
            mainPicCount += 1;
        }
    });

    if (mainPicCount == 0) {
        $("#btnSubmit").prop('disabled', true);
        alert("Main picture must be at least one image");
    }

    // if mainpic selected are above one then submit button will disabled
    if (mainPicCount > 1) {
        $("#btnSubmit").prop('disabled', true);
        alert("Main picture must be one image");
    }
    else {
        $("#btnSubmit").prop('disabled', false);
    }

    //// Count how many mainpic are checked
    //$("input[class^=thumbPic]").each(function () {
    //    if ($(this).is(":checked")) {
    //        thumbPicCount += 1;
    //    }
    //});

    //// if thumbpic selected are above one then submit button will disabled
    //if (thumbPicCount > 1) {
    //    $("#btnSubmit").prop('disabled', true);
    //    alert("Thumbnail picture must be one image");
    //}
    //else {
    //    $("#btnSubmit").prop('disabled', false);
    //}

    // if mainpic and thumbpic are equal one then submit button will enabled
    if (mainPicCount == 1 /*&& thumbPicCount == 1*/) {

        var mainPicParent;
        var mainPicRow;
        var mainPicName;
        //var thumbPicParent;
        //var thumbPicRow;
        //var thumbPicName;

        $("input[class^=mainPic]").each(function () {
            if ($(this).is(":checked")) {
                mainPicParent = $(this).parents()[2];
                mainPicRow = $(mainPicParent).children()[1];
                mainPicName = $(mainPicRow).find("input").val();
            }
        });

        //$("input[class^=thumbPic]").each(function () {
        //    if ($(this).is(":checked")) {
        //        var x = 2;
        //        thumbPicParent = $(this).parents()[2];
        //        thumbPicRow = $(thumbPicParent).children()[1];
        //        thumbPicName = $(thumbPicRow).find("input").val();
        //    }
        //});
        //var radioParent = $("input.mainPic" + id).parents()[2];
        //var mainPicRow = $(radioParent).children()[1];
        //var imageName = $(mainPicRow).find("input").val();

        $("#btnSubmit").prop('disabled', false);

        $.ajax({
            url: '/admin/products/Images',
            type: "Get",
            dataType: "JSON",
            data: { mainPicName: mainPicName /*, thumbPicName: thumbPicName*/ },
            async: false,
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });
    }
    else {
        $("#btnSubmit").prop('disabled', true);
    }
}

$("#parentCategoryDiv").on('click',function () {
    //$("#CategoryId").find(":selected").removeAttr("selected");
    //var selectedItems = [];

    //$("#CategoryId option:selected").each(function(index){
    //    selectedItems.push($(this).val());
    //});
    var selectedCount = $("#parentCategoryDiv ul.dropdown-menu li.selected").length;

    if(selectedCount > 1)
    {
        $("#parentCategoryDiv ul.dropdown-menu li:not(.selected) a").addClass("ui-state-disabled");
    }

    $("#CategoryId").children().eq(1).prop('id','addedLabel');

    $("#parentCategoryDiv show-tick ul.dropdown-menu li:not(.selected)").each(function (index) {
        if($(this).hasClass("addedCategory_"))
        {

        }
        else
        {
            $("parentCategoryDiv ul.dropdown-menu li:not(.selected) a").addClass("ui-state-disabled");
        }
    });

    var i = 0;

    $("div#parentCategoryDiv div.show-tick div.dropdown-menu ul.selectpicker li").each(function (index) {
        if(index > brandCount - 1){
            $(this).addClass('addedCategory_' + i);
            i++;
        }
    });

    $("li.addedCategory_0 dt").prop('id','addedCategory');
});

//$("#DivisionDiv").click(function(){
//    //$("ul.dropdown-menu li:not(.selected) a").addClass("ui-state-disabled");

//    var x = [];
//    $("#FurtherSubCategoryId option").each(function(index){
//        x.push($(this).val());
//    });

//    $("#DivisionDiv ul.dropdown-menu li").each(function(index){
//        $(this).prop('id',x[index]);
//    });

//    $("#DivisionDiv ul.dropdown-menu li a").each(function(index){
//        $(this).attr('onclick','FillDivisions('+x[index]+')');
//    });
//});

//$("#DivisionDiv").one('click',function(){
//    $("#parentCategoryDiv").click();
//    var selectedGender = $("#CategoryId optgroup#addedLabel option:selected").val();
//    var selectedDivision = $("#FurtherSubCategoryId option:selected").val();
//    var selectedSportsCode = $("#FurthestSubCategoryId option:selected").val();

//    GetDivisions(selectedGender);
//    FillDivisions(selectedDivision);
//    GetSportsCode(selectedDivision);

//    var k = $("#SportsCodeDiv #subCategories li").prop('id',selectedSportsCode);
//});

$("#DivisionDiv").on('click',function(){
    if(!$("#DivisionDiv ul.dropdown-menu li").hasClass('selected'))
    {
        $("#DivisionDiv ul.dropdown-menu li a").removeClass("ui-state-disabled");
    }
    else
    {
        $("#DivisionDiv ul.dropdown-menu li:not(.selected) a").addClass("ui-state-disabled");

        var x = [];
        $("#SubCategoryId option").each(function(index){
            x.push($(this).val());
        });

        $("#DivisionDiv ul.dropdown-menu li").each(function(index){
            $(this).prop('id',x[index]);
        });

        $("#DivisionDiv ul.dropdown-menu li a").each(function(index){
            $(this).attr('onclick','FillDivisions('+x[index]+')');
        });
    }
});

$("#SportsCodeDiv").on('click',function(){
    if(!$("#SportsCodeDiv ul.dropdown-menu li").hasClass('selected'))
    {
        $("#SportsCodeDiv ul.dropdown-menu li a").removeClass("ui-state-disabled");
    }
    else
    {
        $("#SportsCodeDiv ul.dropdown-menu li:not(.selected) a").addClass("ui-state-disabled");

        var x = [];
        $("#SubCategoriesId option").each(function(index){
            x.push($(this).val());
        });

        $("#SportsCodeDiv ul.dropdown-menu li").each(function(index){
            $(this).prop('id',x[index]);
        });

        $("#SportsCodeDiv ul.dropdown-menu li a").each(function(index){
            $(this).attr('onclick','FillSportsCode('+x[index]+')');
        });
    }
});


function AddFeature() {
    $.ajax({
        url: "/admin/products/AddFeature",
        type: "Get",
        data: { id: $("#FeaturesId :selected").val(), featureValue: $("#FeatureValues :selected").text() }
    }).done(function (result) {
        $("#showFeatures").html(result);
        $('#FeatureValues').empty();
        $('#FeatureValues').append('<option value="">' + ChooseOneItem + '</option>')
    });
}

function DeleteFeature(id, featureValue) {
    $.ajax({
        url: "/admin/products/DeleteFeature",
        type: "Get",
        data: { id: id, featureValue: featureValue }
    }).done(function (result) {
        $("#showFeatures").html(result);
    });
}

$('#FeaturesId').change(function () {
    if ($(this).attr('value') == 0 || $(this).attr('value') == "") {
        $('#FeatureValues').empty();
        $('#FeatureValues').append('<option value="">' + ChooseOneItem + '</option>')
        $("#FeatureValues").selectpicker("refresh");
    }
    else {
        $.ajax({
            dataType: 'json',
            url: "/admin/products/ShowFeatureValues",
            type: "Get",
            data: { id: $(this).attr('value') }
        }).done(function (data) {
            $('#FeatureValues').empty();
            $.each(data, function (i) {
                var option = $('<option value=' + data[i].FeatureValueId + '>' + data[i].FeatureValue + '</option>');
                $("#FeatureValues").append(option);
                $("#FeatureValues").selectpicker("refresh");
            });
        });
    }
});