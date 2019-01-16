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

$(document).ready(function () {
    if (window.innerWidth > 600) {
        $('.tooltipped').tooltip();
    }
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

window.onload = () => {
    replaceMaterialHeaderClasses();
    if (typeof handleListIconClick === "function")
        handleListIconClick();
};

function replaceMaterialHeaderClasses() {
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
}