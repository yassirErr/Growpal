$('.publish-button').click(function () {
    var contentId = $('#content-id').val();
    var videoIds = [];

    $('input[name="video-checkbox"]:checked').each(function () {
        videoIds.push($(this).val());
    });

    $.ajax({
        url: publishUrl,
        type: 'POST',
        data: {
            contentId: contentId,
            videoIds: videoIds
        },
        success: function (result) {
      
            alert('Videos saved successfully!');
        },
        error: function (xhr, status, error) {
            alert('Error: ' + xhr.responseText);
        }
    });
});