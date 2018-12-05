var ClearPreview = function() {
    $('#imageBrowes').val('');
    $('#description').text('');
    $('#imgPreview').hide();
}

var UploadImage = function() {
    //var file = $('#imageBrowes').get(0).files;
    //var data = new FormData;
    //data.append("imageFile", file[0]);

    $.ajax({
        type: "Post",
        url: "/Account/UploadImage",
        success: function(response) {
            $('#uploadedImage').append('<img src="/Content/' + response + '" class="img-responsive thumbnail"/>');
        }
    });

}



