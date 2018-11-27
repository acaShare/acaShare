function deleteFile(fileId, event) {
    event.stopPropagation();
    document.getElementById(fileId).remove();
}

function showUploadedFiles() {
    removePreviousInputFiles();
    let input = document.getElementById('file-picker');
    let output = document.getElementById('files-wrapper');
    let slideIdx = output.lastElementChild === null ? 1 : parseInt(output.lastElementChild.id) + 1; // +1 to get next free idx
    let id = output.lastElementChild === null ? 0 : parseInt(output.lastElementChild.id) + 1;
    let modalContent = document.getElementById('modal-content');

    for (var i = 0; i < input.files.length; i++ , slideIdx++, id++) {
        let file = input.files.item(i);

        let imageMaterialFileDiv = createNode(
            "div",
            "material-file",
            `
                <div class="delete-file" onclick="deleteFile(${id}, event)">
                    <span class="delete-file-x">&#10005;</span>
                </div>
                <img src="${URL.createObjectURL(file)}" alt="${file.name}-thumbnail" />
            `,
            () => { openModal(); currentSlide(slideIdx); }
        );

        let otherFormatMaterialFileDiv = createNode(
            "div",
            "material-file",
            `
                <div class="delete-file" onclick="deleteFile(${id}, event)">
                    <span class="delete-file-x">&#10005;</span>
                </div>
                <i class="material-icons">description</i>
            `
        );

        let nameInputsDiv = createNode(
            "div",
            "input-field material-file-edit-mode-filename",
            `
                <input class="validate" data-length="50" type="text" id="${i}" name="NewFilesNames[${i}]" value="${file.name}" aria-invalid="false">
                <span class="text-danger input-error-small field-validation-valid" data-valmsg-for="NewFilesNames[${i}]" data-valmsg-replace="true"></span>
            `
        );

        let wrapperDiv = createNode("div", "material-file-edit-mode-wrapper added-through-input", "", null, id);
        output.appendChild(wrapperDiv);

        if (isImage(file)) {
            wrapperDiv.appendChild(imageMaterialFileDiv);

            let galleryItemDiv = createNode(
                "div",
                "gallery-item added-through-input",
                `<img src="${URL.createObjectURL(file)}" alt="${file.name}" />`
            );

            modalContent.appendChild(galleryItemDiv);
        }
        else {
            wrapperDiv.appendChild(otherFormatMaterialFileDiv);
        }

        wrapperDiv.appendChild(nameInputsDiv);
        output.appendChild(document.createElement('div'));
    }
}

let imagesExtensions = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'ico', 'svg', 'tif', 'tiff', 'webp'];

function isImage(file) {
    let extension = getFileExtension(file);
    return imagesExtensions.includes(extension);
}

function getFileExtension(file) {
    return file.name.split('.').pop();
}


function createNode(elementName, className, innerHTML, onclick = null, id = -1) {
    let node = document.createElement(elementName);

    node.className = className;
    node.innerHTML = innerHTML;
    if (onclick !== null) {
        node.onclick = onclick;
    }
    if (id !== -1) {
        node.id = id;
    }

    return node;
}

function removePreviousInputFiles() {
    let elementsToRemove = document.getElementsByClassName('added-through-input');
    while (elementsToRemove.length > 0) {
        elementsToRemove[0].parentNode.removeChild(elementsToRemove[0]);
    }
}