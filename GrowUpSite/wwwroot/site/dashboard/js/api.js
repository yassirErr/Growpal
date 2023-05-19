$("#search-button").click(function () {
    var channelUrl = $("#channel-url").val();
    var channelId = channelUrl.split("/").pop();
    var apiUrl = "https://www.googleapis.com/youtube/v3/search?key=AIzaSyB2AH0E1hfHbYjZG6cjnUe6chhXHXrWeQg&channelId=" + channelId + "&part=snippet,id&order=date&maxResults=20";


    var selectedVideos = []; // Initialize an empty array to store selected video IDs

    $.get(apiUrl, function (data) {
        var videoList = data.items;
        var tableHtml = "";

        $.each(videoList, function (index, video) {
            tableHtml += '<tr>';
            tableHtml += '<td><img src="' + video.snippet.thumbnails.medium.url + '" alt="' + video.snippet.title + '"></td>';
            tableHtml += '<td>' + video.snippet.title + '</td>';
            tableHtml += '<td> <a class="btn  btn-primary mb-3" type="button" href="https://www.youtube.com/watch?v=' + video.id.videoId + '" target="_blank"> View Video </a></td>';
            tableHtml += '<td> <input type="checkbox" name="video-checkbox" value="' + video.id.videoId + '"></td>';
            tableHtml += '</tr>';
        });

        $("#video-table tbody").html(tableHtml);

        // Handle change event for video checkboxes
        $(document).on("change", "input[name='video-checkbox']", function () {
            var videoId = $(this).val();
            if ($(this).is(":checked")) {
                selectedVideos.push(videoId);
            } else {
                selectedVideos = selectedVideos.filter(function (value, index, arr) {
                    return value !== videoId;
                });
            }
        });

        // Handle click event for "Select All" checkbox
        $("#select-all-checkbox").click(function () {
            var isChecked = $(this).prop("checked");
            $("input[name='video-checkbox']").prop("checked", isChecked);
            if (isChecked) {
                selectedVideos = videoList.map(function (video) {
                    return video.id.videoId;
                });
            } else {
                selectedVideos = [];
            }
        });
    });

    //$('.publish-button').click(function () {
    //    var contentId = $('#content-id').val();
    //    var videoIds = [];

    //    $('input[name="video-checkbox"]:checked').each(function () {
    //        videoIds.push($(this).val());
    //    });

    //    $.ajax({
    //        url: '/UserDashboard/Content/SaveVideos', // Replace this URL with the URL for the SaveVideos action
    //        type: 'POST',
    //        data: {
    //            contentId: contentId,
    //            videoIds: videoIds
    //        },
    //        success: function (result) {
    //            alert('Videos saved successfully!');
    //        },
    //        error: function (xhr, status, error) {
    //            alert('Error: ' + xhr.responseText);
    //        }
    //    });
    //});

});


