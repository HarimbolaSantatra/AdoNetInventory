// Show the popup
$(document).ready(function () {
  
    function checkFunc() {
        // data is the json file
        if (data)
            if (data.error == '0'){
                // No error
                successFunc();
            }
    };

    function successFunc() {
        $('#createUserSuccess').modal('toggle');
    }
    function errorFunc() {
        $('#createUserSuccess').modal('toggle');
        var text = $('#createUserSuccess');
    }
    
    $('#createUserBtn').click(function () {
        var serviceURL = '/User/Create';
        $.ajax({
            url: serviceURL,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });
        return 0
    })
});