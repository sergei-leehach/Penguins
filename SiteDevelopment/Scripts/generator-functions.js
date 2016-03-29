$(document).ready(function () {
    $(".team-dropdown").change(function () { $(this).addClass("team-dropdown-clean"); });
    $("#datetimepicker2").datetimepicker({
        locale: "ru"
    });
});

function GetPlace(name) {
    $.ajax({
        //url: "/Generator/GetPlace?name=" + name,
        url: "/Generator/GetPlace",
        data: {name},
        datatype: "text",
        method: "POST",
        success: function(data) {
            $("#city").val(data.city);
            $("#arena").val(data.arena);
        }
    });
}

function SetPreview() {
    $("#scoreboard").hide();
    $("#three-stars").hide();
    var x = $("#type");
    x.val("Preview");
}

function SetRecap() {
    $("#scoreboard").show();
    $("#three-stars").show();
    var x = $("#type");
    x.val("Recap");
}

function SetImage(data) {
    var path = data.getAttribute("src");
    var x = $("#background-image");
    x.val(path);
}
