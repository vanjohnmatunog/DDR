function OpenWindow(link) {
    window.open(link, null, 'toolbar=1,status=1,location=1,menubar=1,directories=1,resizable=1,scrollbars=1');
    //return true;
}
function changeColor(obj) {
    document.getElementById(obj).className = 'selected';
    document.getElementById(obj).style.color = "#fff";
    document.getElementById(obj).style.letterSpacing = "3px";
    return false;
    //            Ext.getElementById("Menu");         
}
function MenuHide(Menu) {
    document.getElementById(Menu).style.visibility = "hidden";
}
function Loading() {
    var loading = document.createElement("div");
    var pageHeight = $(window).height();
    loading.id = "loading";
    loading.style.display = "table-cell";
    loading.style.height = pageHeight;
    loading.style.width = "100%";
    loading.style.color = "black";
    loading.style.backgroundColor = "#fff";
    loading.style.filter = "alpha(opacity = 50)";
    loading.style.paddingLeft = "0";
    loading.style.paddingRight = "0";
    loading.style.right = "0";
    loading.style.top = "0";

    loading.style.position = "fixed";
    loading.style.zIndex = "9999";
    loading.style.backgroundImage = "url('Media/Images/Loader.gif')";
    loading.style.backgroundPosition = "center";
    loading.style.backgroundRepeat = "no-repeat";
    document.body.appendChild(loading);
}
function Finished() {
    var loading = document.getElementById("loading");
    document.body.removeChild(loading);
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode == 13) {
        return false;
    }
    else if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        return true;
    }
}
function isNumberKeywithDash(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    var charStr = String.fromCharCode(charCode);
    if (charCode == 13) {
        return false;
    }
    else if (charCode > 31 && (charCode < 48 || charCode > 57) && (charStr !== "-")) {
        return false;
    }
    else {
        return true;
    }
}
function isNumberKeywithDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    var charStr = String.fromCharCode(charCode);
    if (charCode == 13) {
        return false;
    }
    else if (charCode > 31 && (charCode < 48 || charCode > 57) && (charStr !== ".")) {
        return false;
    }
    else {
        return true;
    }
}
function isNumberKeywithQuote(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    var charStr = String.fromCharCode(charCode);
    if (charCode == 13) {
        return false;
    }
    else if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode !== 39 && charCode !== 34)) {
        return false;
    }
    else {
        return true;
    }
}
function preventInput(e) {
    var evt = e || window.event;
    if (evt) {
        var keyCode = evt.charCode || evt.keyCode;
        for (i = 0; i <= 256; i++) {
            if (keyCode === i) {
                if (evt.preventDefault) {
                    evt.preventDefault();
                } else {
                    evt.returnValue = false;
                }
            }
        }
    }
}
function checkTextAreaMaxLength(textBox, e) {
    var maxLength = 255;
    if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            e.returnValue = false;
        }
    }
}
function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}
function button_click(objTextBox, objBtnID) {
    if (window.event.keyCode == 13) {
        document.getElementById(objBtnID).focus();
        document.getElementById(objBtnID).click();
    }
}

function pageLoad() {
    $('.datepicker').unbind();
    $('.datepicker').datepicker();

    $(".dataTable").dataTable({
        "stateSave": true
    });

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
}

// jquery ready start
$(document).ready(function () {
    // jQuery code

    //////////////////////// Prevent closing from click inside dropdown
    $(document).on('click', '.dropdown-menu', function (e) {
        e.stopPropagation();
    });

    //$(function () {
    //    $(".dataTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
    //});

    // make it as accordion for smaller screens
    if ($(window).width() < 992) {
        $('.dropdown-menu a').click(function (e) {
            e.preventDefault();
            if ($(this).next('.submenu').length) {
                $(this).next('.submenu').toggle();
            }
            $('.dropdown').on('hide.bs.dropdown', function () {
                $(this).find('.submenu').hide();
            })
        });
    };




}); // jquery end