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

function ShowPreview() {
    $("#scoreboard").hide();
    $("#three-stars").hide();
}

function ShowRecap() {
    $("#scoreboard").show();
    $("#three-stars").show();
}