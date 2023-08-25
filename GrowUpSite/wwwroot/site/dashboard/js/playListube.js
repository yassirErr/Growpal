$(document).ready(function () {
    $('#range-select').on('change', function () {
        var rangeValue = $(this).val();
        var url = '@Url.Action("Playlistube", "Content")?range=' + rangeValue;
        window.location.href = url;
    });
});


function handleClick(reactubeId, videoLink) {
    $.ajax({
        url: '@Url.Action("AddToWatchtube", "Content")',
        type: 'POST',
        data: { reactubeId: reactubeId, videoLink: videoLink },
        success: function (data) {
            // find the button element
            var button = document.getElementById('view-video-btn');

            // create a new element for the success message
            var successMessage = document.createElement('div');
            successMessage.className = 'alert icon-custom-alert alert-outline-success alert-success-shadow';
            successMessage.role = 'alert';
            successMessage.innerHTML = '<i class="mdi mdi-check-all alert-icon"></i><div class="alert-text"><strong>Well done!</strong></div>';

            // replace the button with the success message
            button.parentNode.replaceChild(successMessage, button);

            // show an alert message
            alert("Video added to Watchtube");

            // Load a new page
            window.location.href = 'https://localhost:44382/UserDashboard/Content/Playlistube';
        },
        error: function () {
            alert("Error: Failed to add video to Watchtube");
        }
    });
}
//////////////////////////////////////////////////////////////////////////////////////

var timerElement = document.getElementById("timer");
var startTime = localStorage.getItem("startTime");
var isCountingStopped = false;

if (!startTime || @insertedByOthersCount <= @watchtubeCount) {
    startTime = new Date().getTime();
    localStorage.setItem("startTime", startTime);
}

function updateTimer() {
    var currentTime = new Date().getTime();
    var elapsedSeconds = Math.floor((currentTime - startTime) / 1000);
    var hours = Math.floor(elapsedSeconds / 3600);
    var minutes = Math.floor((elapsedSeconds % 3600) / 60);
    var seconds = elapsedSeconds % 60;

    var timerText = hours + ":" + (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
    timerElement.innerText = timerText;

    if (minutes >= 1 && !isCountingStopped) {
        clearInterval(timerInterval); // Stop the timer
        isCountingStopped = true;

        // Make an AJAX call to update the StatusContent to false
        $.ajax({
            url: '@Url.Action("UpdateStatusContent","Content")',
            type: 'POST',
            success: function (response) {
                console.log('StatusContent updated successfully.');
            },
            error: function (error) {
                console.error('Failed to update StatusContent:', error);
            }
        });
    }
}

var timerInterval = setInterval(updateTimer, 1000); // Update timer every 1 second

updateTimer(); // Update timer immediately