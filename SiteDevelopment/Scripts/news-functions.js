function RenderImage() {
    var image = document.getElementById("image");
    $("#render-image").attr("src", image.value);
}

function AddComment(data) {
    $("#comments").append(data);
    $("#text-area").val("");
}

$(document).ready(function() {
    $("#tags").chosen({
        display_selected_options: false,
        disable_search_threshold: 3
    });

    $("#tags").chosen().data('chosen').container.bind('keypress', function (event) {
        if (event.which === 32) {
            var y = $('#tags_chosen').find('input');
            var x = y[0].value;
            var option = '<option selected="true" value="'+ x + '">' + x + '</option>';
            $("#tags").append(option);
            $("#tags").trigger("chosen:updated");
        }
    });

    //$("#tags").chosen().change(function () {
    //});
});

$(function () {
    $('#form').submit(function () {
        $(".search-choice").add(attr, selected);
        //return true; // return false to cancel form action
    });
});