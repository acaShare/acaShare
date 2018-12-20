let editRequestContainerHtml;
let materialContainerHtml;

function handleShowPreviousMaterialVersion(materialId) {
    let thisContainer = document.getElementById('material-container');
    editRequestContainerHtml = thisContainer.innerHTML;

    // do it only once and then use saved innerHtmls to display contents of edit request and prev material
    if (!materialContainerHtml) {
        let request = new XMLHttpRequest();
        request.onload = () => {
            if (request.status === 200) {
                // get whole body
                let materialFullHtml = request.responseText;

                // parse it to get document that we can operate on
                var materialDocument = new DOMParser().parseFromString(materialFullHtml, "text/html");

                // get only container
                let materialContainer = materialDocument.getElementsByClassName('material-container')[0];

                // replace existing icons with back icon
                let iconsBox = materialDocument.getElementsByClassName('material-header-action-buttons')[0];
                while (iconsBox.firstChild) {
                    iconsBox.removeChild(iconsBox.firstChild);
                }
                //// since we stored current edit request container's html in a variable, we can display it back using onclick function
                iconsBox.innerHTML =
                    `<a class="sep action-button tooltipped"
                        data-position="top" data-tooltip="Wróć do widoku sugestii edycji"
                        onclick="handleShowEditRequest()">
                        <i class="material-icons lh">replay</i>
                    </a>`;
                thisContainer.innerHTML = materialContainer.innerHTML;

                // save prev material's content so we don't have to make a request for it every time
                materialContainerHtml = materialContainer.innerHTML;
            }
        };
        request.open("GET", `/Main/Materials/Material?materialId=${materialId}`);
        request.send();
    }
    else {
        // switch to saved prev material's view
        thisContainer.innerHTML = materialContainerHtml;
    }
}

function handleShowEditRequest() {
    // get material-container which now contains previous material's data
    let thisContainer = document.getElementById('material-container');

    // display edit request
    thisContainer.innerHTML = editRequestContainerHtml;
}