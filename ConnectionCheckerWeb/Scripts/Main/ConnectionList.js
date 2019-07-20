function UpdateConnectionStatus() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "@(Url.Action("GetConnectionList", "ConnectionController"))",

        success: function (data) {

            if (data) { // check if data is defined
                $("#connection-list").html(data);
            } else {
                $("#connection-list").html("Sorry, but something went wrong.");
            }

        },
        error: function (xhr, err) {
            $("#connection-list").html("Sorry, but something went wrong.");
        }
    });
}


// For the first result
$(document).ready(function () {
    UpdateConnectionStatus();
});

// Check server every 5 second
setInterval(UpdateConnectionStatus, 5000);