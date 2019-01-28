function handleSelectListChange(selectList) {
    let additionalCommentElement = document.getElementById('additional-comment');
    let additionalCommentLabel = document.getElementById('additional-comment-label');
    
    if (selectList.value === "5") {
        additionalCommentElement.setAttribute('data-val-required', 'Przy wyborze "Innej" przyczyny należy załączyć dodatkowy komentarz (wyjaśnienie).');
        additionalCommentLabel.innerHTML = "Wyjaśnienie";    
    }
    else {
        additionalCommentElement.removeAttribute('data-val-required');
        additionalCommentLabel.innerHTML = "Wyjaśnienie (opcjonalnie)";
    }
    
    refreshjQueryValidation();
}

function refreshjQueryValidation() {
    $('#delete-suggestion-form').removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse('#delete-suggestion-form');
}