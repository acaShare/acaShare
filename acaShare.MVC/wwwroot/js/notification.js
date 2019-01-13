function dropDown() {
    getData();
    document.getElementById("notificationList").classList.toggle("show");
}

function getData() {
    var xhttp = new XMLHttpRequest();

    xhttp.onload = () => {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            let data = JSON.parse(xhttp.responseText);
            var ul = document.getElementById("notificationList");
            for (var i in data)
            {
                var li = document.createElement("li");
                li.setAttribute("class", "notification-list-element");

                var a = document.createElement("a");
                a.setAttribute("class", "notification-list-button");

                var contentDiv = document.createElement("div");
                contentDiv.setAttribute("class", "content-div");

                var dateDiv = document.createElement("div");
                dateDiv.setAttribute("class", "date-div");

                var date = new Date(data[i].date);

                contentDiv.appendChild(document.createTextNode(data[i].content));

                dateDiv.appendChild(document.createTextNode(("0" + date.getHours()).slice(-2) + ":" +
                                                      ("0" + date.getMinutes()).slice(-2) + "  " +
                                                      ("0" + date.getDate()).slice(-2) + "-" +
                                                      ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
                                                      date.getFullYear() + " "));

                li.appendChild(contentDiv);
                li.appendChild(dateDiv);

                if (data[i].materialId !== null) {
                    a.href = "/Main/Materials/Material?materialId=" + data[i].materialId;
                    li.classList.add("clickable");
                }

                a.appendChild(li);
                ul.appendChild(a);
                ul.appendChild(document.createElement("hr"));
            }
        }
    };
    xhttp.open("GET", "/Main/Notification/NotificationData");
    xhttp.send();
}

window.onclick = function (event) {
        if (!Array.from(document.querySelectorAll('.notificationList *, #notification')).includes(event.target)) {
            let notf = document.getElementById('notificationList');
            notf.classList.remove("show");
            document.body.onclick = null;
        }
}
