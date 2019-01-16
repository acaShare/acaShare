var isDone = false;

window.addEventListener('load', () => {
    document.getElementById('alert-wrapper').addEventListener('click', (event) => { closeAlertOnBackgroundClick(event); });

    let approveMaterialElements = document.getElementsByClassName('approve-material');
    if (approveMaterialElements) {
        Array.from(approveMaterialElements).forEach(el =>
            el.addEventListener('click', (event) => { triggerAlert("Czy na pewno chcesz zaakceptować ten materiał?", false, event); })
        );
    }

    let declineMaterialElements = document.getElementsByClassName('reject-material');
    if (declineMaterialElements) {
        Array.from(declineMaterialElements).forEach(el =>
            el.addEventListener('click', (event) => { triggerAlert("Czy na pewno chcesz odrzucić ten materiał?", false, event); })
        );
    }

    let approveDeleteRequestElements = document.getElementsByClassName('approve-delete-request');
    if (approveDeleteRequestElements) {
        Array.from(approveDeleteRequestElements).forEach(el => 
            el.addEventListener('click', (event) => { triggerAlert("Czy na pewno chcesz zaakceptować tę sugestię usunięcia?", true, event); })
        );
    }

    let approveEditRequestElements = document.getElementsByClassName('approve-edit-request');
    if (approveEditRequestElements ) {
        Array.from(approveEditRequestElements).forEach(el =>
            el.addEventListener('click', (event) => { triggerAlert("Czy na pewno chcesz zaakceptować tę sugestię edycji?", false, event); })
        );
    }
});

function triggerAlert(message, isDeletion, e) {
    if (!isDone) {
        e.preventDefault();

        alert(
            message,
            () => { isDone = true; return e.target.click(); },
            () => { isDone = false; },
            isDeletion
        );
    }
    else {
        isDone = false;
    }
}

function alert(message, yesCallback, noCallback, isDeletion) {

    let alertTitle = document.getElementById('alert-title');
    alertTitle.innerHTML = message;

    toggleAlert();

    let confirmButton = document.getElementById('alert-confirm-button');
    let declineButton = document.getElementById('alert-decline-button');

    if (isDeletion) {
        confirmButton.classList.add('delete-button');
    }
    else {
        confirmButton.classList.remove('delete-button');
    }

    confirmButton.onclick = () => {
        yesCallback();
        toggleAlert();
    };

    declineButton.onclick = () => {
        noCallback();
        toggleAlert();
    };
}

function toggleAlert() {
    let alertWrapper = document.getElementById('alert-wrapper');
    alertWrapper.classList.toggle('visible');
}

function closeAlertOnBackgroundClick(event) {
    if (!Array.from(document.querySelectorAll('.alert-wrapper *')).includes(event.target)) {
        toggleAlert();
    }
}