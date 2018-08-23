$(document).ready(function () {
    //вешаем обработчик 
    $(".StoronaProc").change(function () {
        $.getJSON("/NODES/GetStoronaProc", this.value)
            .done(function (data) {
                console.log(data);
            });


        //StoronaProc("/NODES/GetStoronaProc/" + $(this).);
    });
});
function StoronaProc(url) {
    // Запустим ajax-запрос, установим обработчики его выполнения и
    // сохраним объект jqxhr данного запроса для дальнейшего использования.
    //var jqxhr =
    $.getJSON(url/*, function () {console.log("success");}*/)
        .done(function (data) {
            //console.log("second success");
            console.log(data.ip);
        });
    //.fail(function () {
    //    console.log("error");
    //})
    //.always(function (data) {
    //    console.log("complete");
    //    console.log(data.ip);
    //});
}

