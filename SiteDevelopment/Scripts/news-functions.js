function RenderImage() {
    var image = document.getElementById("image");
    $("#render-image").attr("src", image.value);
}

function AddComment(data) {
    $("#comments").append(data);
    $("#text-area").val("");
}