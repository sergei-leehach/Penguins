function Compare() {
    var password = $("#password").val();
    var confirm = $("#confirm").val();
    var input1 = $("#password");
    var input2 = $("#confirm");
    var validation = $("#validation");

    if (password === confirm) {
        validation.text("Passwords match!").css("color", "green");
        input1.css("border-color", "green");
        input2.css("border-color", "green");
    }
    else {
        validation.text("Passwords don't match!").css("color", "red");
        input1.css("border-color", "red");
        input2.css("border-color", "red");
    }
}