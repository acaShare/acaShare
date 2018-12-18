window.onload(() => {
    let actionsContainer = document.getElementById('moderator-actions');
    actionsContainer.childNodes.forEach(n => n.addEventListener("onclick", () => { n.classList.toggle('action-active'); }));
});