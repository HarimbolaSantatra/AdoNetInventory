// Show the popup
$(document).ready(function () {
    $('#createUserSuccess').modal('toggle');

    function successFunc(data, status) {
        //confirm("Press a button!");
        confirm(data);
    }
    function errorFunc() {
        alert('error');
    }
    var serviceURL = '/User/Create';
    successFunc("Samori Touré");
    $.ajax({
        url: serviceURL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
}
