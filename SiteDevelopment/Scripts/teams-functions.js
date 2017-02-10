function Lock() {
    var away = $("#AwayTeamScore").val();
    var home = $("#HomeTeamScore").val();

    var ratio = Math.abs(away - home);
    if (ratio >= 2) {
        $('select[name="result"]').val("FT");
        $('option[value="OT"]').hide();
        $('option[value="SO"]').hide();        
    } else {
        $('select[name="result"]').val("summary");
        $('option[value="OT"]').show();
        $('option[value="SO"]').show();
    }
}

$("form").submit(function() {

});
