document.addEventListener('DOMContentLoaded', function() {
    var elems = document.querySelectorAll('.sidenav');
    var instances = M.Sidenav.init(elems, null);
});

$(document).ready(function(){
    $('.sidenav').sidenav();
});

$(document).ready(function(){
    $('.tabs').tabs();
});

$(document).ready(function(){
    $('.tooltipped').tooltip();
});

function toggleNotifications(e) {
    e.stopPropagation();

    let notf = document.getElementById('notifications-wrapper');
    if (notf.style.display === "none") {
        notf.style.display = "block";
        document.body.onclick = closeOnBackgroundClick;
    }
    else {
        notf.style.display = "none";
        document.body.onclick = null;
    }
}

function closeOnBackgroundClick(event) {
    if (!Array.from(document.querySelectorAll('.notifications-wrapper *, #notifications-button')).includes(event.target)) {
        let notf = document.getElementById('notifications-wrapper');
        notf.style.display = "none";
        document.body.onclick = null;
    }
}