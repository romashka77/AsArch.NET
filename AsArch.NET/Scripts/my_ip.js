$(document).ready(function () {
    //вешаем обработчик 
    $("#RefNode").change(function () {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            url: "/Home/GetIP",
            success: function (data) { alert(data.ip); }
        });
    });
});

//window.onload = function () {
//    document.querySelector("#but_my_ip").onclick = function () {
//        ajaxGet();
//    }
//}

//function ajaxGet() {
//    var request = new XMLHttpRequest();

//    request.onreadystatechange = function () {
//        if (request.readyState == 4 /*&& request.status == 200*/) {
//            document.querySelector("#lbl_my_ip").innerHTML = request.responseText;
//        }
//    }
//    request.open('GET', 'Home/GetIP');
//    request.send();
//}