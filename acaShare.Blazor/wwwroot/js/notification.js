window.addEventListener('load', () => {
    let notificationBell = document.getElementById('notification-bell-mobile');

    if (window.innerWidth <= 992) {
        notificationBell.classList.add('show-flex');
    }
});

function dropDown() {
    let classList = document.getElementById("notificationList").classList;
    if (!classList.contains("show")) {
        getData();
    }
    classList.toggle("show");
}

function getData() {
    let xhttp = new XMLHttpRequest();

    xhttp.onload = () => {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            let data = JSON.parse(xhttp.responseText);
            let ul = document.getElementById("notificationList");
            while (ul.firstChild) {
                ul.removeChild(ul.firstChild);
            }
            
            for (var i in data)
            {
                let li = document.createElement("li");
                li.setAttribute("class", "notification-list-element");

                let a = document.createElement("a");
                a.setAttribute("class", "notification-list-button");

                let contentDiv = document.createElement("div");
                contentDiv.setAttribute("class", "content-div");

                let dateDiv = document.createElement("div");
                dateDiv.setAttribute("class", "date-div");

                let date = new Date(data[i].date);

                let fullContent = data[i].content;
                let maxLength = 100;
                fillContentDiv(contentDiv, li, fullContent, maxLength);

                dateDiv.appendChild(document.createTextNode(("0" + date.getHours()).slice(-2) + ":" +
                                                      ("0" + date.getMinutes()).slice(-2) + ",  " +
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
            }

            if (data.length === 0) {
                fillWithEmptyMessage(ul);
            }
        }
    };
    xhttp.open("GET", "/Main/Notification/NotificationData");
    xhttp.send();
}

function fillContentDiv(contentDiv, wrapper, fullContent, maxLength) {
    let isFullTextShownAttributeName = "data-is-full-text-shown";

    let visibleContent;
    if (fullContent.length > maxLength) {
        visibleContent = fullContent.substr(0, maxLength) + " [...]";
        wrapper.setAttribute('title', 'Pokaż więcej');
        wrapper.style.cursor = "pointer";
    }
    else {
        visibleContent = fullContent;
    }

    contentDiv.appendChild(document.createTextNode(visibleContent));
    contentDiv.dataset.fullText = fullContent;
    contentDiv.addEventListener('click', function () {
        if (this.hasAttribute(isFullTextShownAttributeName)) {
            this.innerHTML = visibleContent;
            this.removeAttribute(isFullTextShownAttributeName);
        }
        else {
            this.innerHTML = fullContent;
            contentDiv.setAttribute(isFullTextShownAttributeName, '');
        }
    });
}

function fillWithEmptyMessage(ul) {
    let li = document.createElement("li");
    li.setAttribute("class", "notification-list-element");

    let a = document.createElement("a");
    a.setAttribute("class", "notification-list-button");

    let contentDiv = document.createElement("div");
    contentDiv.setAttribute("class", "content-div");

    let dateDiv = document.createElement("div");
    dateDiv.setAttribute("class", "date-div");

    contentDiv.appendChild(document.createTextNode("Brak powiadomień"));

    li.appendChild(contentDiv);

    a.appendChild(li);
    ul.appendChild(a);
}

window.addEventListener('click', function (event) {
    let notificationsArea = document.querySelectorAll('.notificationList *, #notifications-li *, .notifications-bell-mobile-wrapper *');

    if (!Array.from(notificationsArea).includes(event.target)) {
        let notf = document.getElementById('notificationList');
        notf.scrollTo(0, 0);
        notf.classList.remove("show");
        document.body.onclick = null;
    }
});
