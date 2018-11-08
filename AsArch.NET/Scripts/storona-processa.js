$('#Attrs_0__CHAR_VALUE').on('change', function () {
    //alert(this.value);
    let fiz_lico = '.fiz-lico';
    let ur_lico = '.ur-lico';
    if (this.value === 'Физическое лицо') {
        $(fiz_lico).show({ duration: 800, easing: "linear", queue: false });
        $(ur_lico).hide({ duration: 800, easing: "linear", queue: false });
    } else if (this.value === 'Юридическое лицо') {
        $(ur_lico).show({ duration: 800, easing: "linear", queue: false });
        $(fiz_lico).hide({ duration: 800, easing: "linear", queue: false });
    } else {
        $(fiz_lico).hide({ duration: 800, easing: "linear", queue: false });
        $(ur_lico).hide({ duration: 800, easing: "linear", queue: false });
    }
});

$("#Attrs_0__CHAR_VALUE").trigger("change");

//function(el) {
//    let s = el.value;
//    if (s == 'Физическое лицо') {
//        $('.clico').css('display:""');
//        $('.ulico').css('display:none');
//    } else if (s == 'Юридическое лицо') {
//        $('.ulico').css('display:""');
//        $('.clico').css('display:none');
//    }
//    else {
//        $('.clico').css('display:none');
//        $('.ulico').css('display:none');
//    }
//};

//$(document).ready(function () {
//    $('.toggle').
//});