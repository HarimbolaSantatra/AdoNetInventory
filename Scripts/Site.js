// Custom Unobstrusive form validation for the Create & Edut User page
$(function () {
    var emailForm = $($('input#Email'));
    var passwordForm = $($('input#Password'));

    // If document initialized, deactivate error msg
    $( document ).ready(function() {

        // Change the value of the aria-invalid attribute
        emailForm.attr("aria-invalid", "false");
        passwordForm.attr("aria-invalid", "false");
        
        // Change the class
        emailForm.removeClass("input-validation-error");
        emailForm.addClass("valid");
        passwordForm.removeClass("input-validation-error");
        passwordForm.addClass("valid");
    });

    // If user typed in the input
    emailForm.focus(function() {
        var emailInput = emailForm.val();
        if(emailInput.valid())
        {
            
        }
    })
    passwordForm.focus(function() {
        var passwordInput = passwordForm.val();
    })
);