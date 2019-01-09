function dropDown() {
    getData();
    document.getElementById("notificationList").classList.toggle("show");
}

function getData() {
    var xhttp = new XMLHttpRequest();

    xhttp.onload = () => {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            let data = JSON.parse(xhttp.responseText);
            document.getElementById("notificationList").innerHTML = data;
        }
    };
    xhttp.open("GET", "/Main/Notification/NotificationData");
    xhttp.send();
}

window.onclick = function (event) {
    if (!event.target.matches('#notification')) {
        var dropdowns = document.getElementsByClassName("notificationList");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}