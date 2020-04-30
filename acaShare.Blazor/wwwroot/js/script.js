var JsFunctions = window.JsFunctions || {}
var HelperFunctions = window.HelperFunctions || {}

JsFunctions = {
    initializeJs: function () {
        HelperFunctions.Other.replaceMaterialHeaderClasses();
        HelperFunctions.MaterializeCss.initializeMaterializeCssElements();
    },

    initializeNotifications: function (notifications) {
        HelperFunctions.Notifications.initializeNotifications(notifications);
    },

    setDocumentTitle: function (title) {
        document.title = `${title} | acaShare.Blazor`;
    },

    initializeStatistics: function (statistics) {
        HelperFunctions.Other.createChart(statistics);
    }
};

HelperFunctions = {
    Notifications: {
        initializeNotifications: function (notifications) {
            let notificationBell = document.getElementById('notification-bell-mobile');

            if (window.innerWidth <= 992) {
                notificationBell.classList.add('show-flex');
            }

            let onclickTrigger = document.getElementById("notification");
            let onclickMobileTrigger = document.getElementById("notification-mobile");

            [onclickTrigger, onclickMobileTrigger].forEach(a =>
                a.addEventListener('click', function () {
                    HelperFunctions.Notifications.dropDown(notifications);
                })
            );

            window.addEventListener('click', function (event) {
                let notificationsArea = document.querySelectorAll('.notificationList *, #notifications-li *, .notifications-bell-mobile-wrapper *');

                if (!Array.from(notificationsArea).includes(event.target)) {
                    let notf = document.getElementById('notificationList');
                    notf.scrollTo(0, 0);
                    notf.classList.remove("show");
                    document.body.onclick = null;
                }
            });
        },
   
        dropDown: function (notifications) {
            let classList = document.getElementById("notificationList").classList;
            if (!classList.contains("show")) {
                HelperFunctions.Notifications.show(notifications);
            }
            classList.toggle("show");
        },

        show: function (data) {
            let ul = document.getElementById("notificationList");
            while (ul.firstChild) {
                ul.removeChild(ul.firstChild);
            }

            for (var i in data) {
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
                HelperFunctions.Notifications.fillContentDiv(contentDiv, li, fullContent, maxLength);

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
                HelperFunctions.Notifications.fillWithEmptyMessage(ul);
            }
        },

        fillContentDiv: function (contentDiv, wrapper, fullContent, maxLength) {
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
        },

        fillWithEmptyMessage: function (ul) {
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
    },

    Other: {
        replaceMaterialHeaderClasses: function () {
            let elem = document.getElementsByClassName('material-header-title')[0];
            if (elem && (elem.innerHTML.trim().length > 30 || window.innerWidth < 900)) {
                let elem1 = document.getElementsByClassName('material-header')[0];
                let elem2 = document.getElementsByClassName('material-header-action-buttons')[0];

                elem.classList.replace('material-header-title', 'material-header-title-smaller');
                elem1.classList.replace('material-header', 'material-header-smaller');

                if (elem2) {
                    elem2.classList.replace('material-header-action-buttons', 'material-header-action-buttons-smaller');
                }
                else { // if DeleteRequestApproval (without action buttons)
                    elem.classList.add('center');
                }
            }
        },

        createChart: function (statistics) {
            window.addEventListener('load', () => {
                if (window.innerWidth <= 1000) {
                    let breadcrumbs = document.getElementsByClassName('breadcrumb');
                    Array.from(breadcrumbs).slice(0, -1).forEach(b => b.remove());
                }
            });

            let labels = Object.keys(statistics);

            var ctx = document.getElementById("myChart").getContext('2d');

            let values = Object.values(statistics);
            let finalValues = [...values, Math.max(...values) + 1];

            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Liczba sugestii',
                        data: finalValues,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            },
                            scaleLabel: {
                                display: true,
                                labelString: "Liczba sugestii"
                            }
                        }],
                        xAxes: [{
                            ticks: {
                                autoSkip: false
                            }
                        }]
                    },
                    legend: {
                        display: false
                    }
                }
            });
        }
    },

    MaterializeCss: {
        initializeMaterializeCssElements: function () {
            if (window.innerWidth > 600) {
                $('.tooltipped').tooltip();
            }
            $('.sidenav').sidenav();
        }
    }
}