var existingFilesExtensions = [];

window.onload = function () {
    let existingFilesWrappers = document.querySelectorAll('.material-file-edit-mode-wrapper:not(.added-through-input)');
    for (let i = 0; i < existingFilesWrappers.length; i++) {
        let existingFileId = document.getElementById(`Files[${i}].FileId`).value;
        let existingFileNameInput = document.getElementById(`Files[${i}].FileName`);
        let existingFileName = existingFileNameInput.value;
        let extension = getExtensionFromFileName(existingFileName);

        let data = { fileId: existingFileId, fileExtension: extension };
        existingFilesExtensions.push(data);

        existingFileNameInput.value = getFileNameWithoutExtension(existingFileName);
    }

    initializeDragAndDropArea();
};

function initializeDragAndDropArea() {
    let dragAndDropArea = document.getElementById('files-wrapper');

    let counter = 0;
    dragAndDropArea.ondragstart = () => {
        counter++;
        dragAndDropArea.classList.add("drag-area-highlight");
    };

    dragAndDropArea.ondragleave = () => {
        counter--;
        if (counter === 0) {
            dragAndDropArea.classList.remove("drag-area-highlight");
        }
    };

    dragAndDropArea.ondrop = (e) => {
        dragAndDropArea.classList.remove("drag-area-highlight");
        showUploadedFiles(e.dataTransfer.files);
        removeDragData(e);
    };

    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dragAndDropArea.addEventListener(
            eventName,
            (e) => {
                e.preventDefault();
                e.stopPropagation();
            },
            false
        );
    });
}

function removeDragData(ev) {
    if (ev.dataTransfer.items) {
        ev.dataTransfer.items.clear();
    } else {
        ev.dataTransfer.clearData();
    }
}

function deleteFile(fileId, event) {
    event.stopPropagation();
    document.getElementById(fileId).remove();
}

function showFilesFromInputElement() {
    removePreviousInputFiles();
    let input = document.getElementById('file-picker');
    showUploadedFiles(input.files);
}

function showUploadedFiles(files) {
    let output = document.getElementById('files-wrapper');
    let slideIdx = output.lastElementChild === null ? 1 : parseInt(output.lastElementChild.id) + 1; // +1 to get next free idx
    let id = output.lastElementChild === null ? 0 : parseInt(output.lastElementChild.id) + 1;
    let modalContent = document.getElementById('modal-content');

    for (var i = 0; i < files.length; i++ , slideIdx++, id++) {
        let file = files.item(i);
        let fileName = getFileNameWithoutExtension(file.name);

        let imageMaterialFileDiv = createNode(
            "div",
            "material-file",
            `
                <div class="delete-file" onclick="deleteFile(${id}, event)">
                    <span class="delete-file-x">&#10005;</span>
                </div>
                <img src="${URL.createObjectURL(file)}" alt="${fileName}-thumbnail" />
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
                <a href="${URL.createObjectURL(file)}" download="${file.name}" class="existing-file-download">
                    <i class="material-icons">description</i>
                </a>
            `
        );

        let nameInputsDiv = createNode(
            "div",
            "input-field material-file-edit-mode-filename",
            `
                <input type="text" id="FormFile[${i}]__FileName" value="${fileName}" class="validate" data-length="50" data-val="true" data-val-maxlength="Nazwa materia³u nie mo¿e przekraczaæ 50 znaków" data-val-maxlength-max="50" data-val-required="Pole &quot;Nazwa&quot; jest wymagane" >
                <span class="text-danger input-error-small field-validation-valid" data-valmsg-for="FormFile${i}__FileName" data-valmsg-replace="true"></span>
            `
        );

        let wrapperDiv = createNode("div", "material-file-edit-mode-wrapper added-through-input", "", null, id);
        wrapperDiv.setAttribute('input-file-id', i);
        output.appendChild(wrapperDiv);

        if (isImage(file)) {
            wrapperDiv.appendChild(imageMaterialFileDiv);

            let galleryItemDiv = createNode(
                "div",
                "gallery-item added-through-input",
                `<img src="${URL.createObjectURL(file)}" alt="${fileName}" />`
            );

            modalContent.appendChild(galleryItemDiv);
        }
        else {
            wrapperDiv.appendChild(otherFormatMaterialFileDiv);
        }

        wrapperDiv.appendChild(nameInputsDiv);
    }
}


let imagesExtensions = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'ico', 'svg', 'tif', 'tiff', 'webp'];

function isImage(file) {
    let extension = getExtensionFromFileName(file.name);
    return imagesExtensions.includes(extension);
}

function getExtensionFromFileName(fileName) {
    let splitted = fileName.split('.');

    if (splitted.length === 1) {
        return "";
    }
    else {
        return splitted.pop();
    }
}

function getFileNameWithoutExtension(fileName) {
    return fileName.split('.').slice(0, -1).join('.');
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


function submitForm(e) {
    e.preventDefault();

    // create FormData from existing form
    let form = document.getElementById('edit-form');
    let formData = new FormData(form);

    // append original extension to filename
    let existingFilesWrappers = document.querySelectorAll('.material-file-edit-mode-wrapper:not(.added-through-input)');
    for (let i = 0; i < existingFilesWrappers.length; i++) {
        let existingFileId = document.getElementById(`Files[${i}].FileId`).value;
        let extension = existingFilesExtensions.filter(efe => efe.fileId === existingFileId).fileExtension;
        let existingFileNameInput = document.getElementById(`Files[${i}].FileName`);

        if (extension) {
            existingFileNameInput.value += "." + extension;
        }
    }

    // get visible files (the ones that weren't removed by clicking an 'x' button)
    let newFilesWrappers = document.querySelectorAll('.material-file-edit-mode-wrapper.added-through-input');
    let newFilesIds = [];
    for (let i = 0; i < newFilesWrappers.length; i++) {
        newFilesIds.push(newFilesWrappers[i].getAttribute('input-file-id'));
    }

    // append only these files that are visible
    let input = document.getElementById('file-picker');
    let inputFiles = input.files;

    for (let i = 0; i < inputFiles.length; i++) {
        if (newFilesIds.includes(i+"")) {
            let file = inputFiles.item(i);
            let nameInput = document.getElementById(`FormFile[${i}]__FileName`);
            let extension = getExtensionFromFileName(file.name);
            let newFileName = nameInput.value;
            if (extension) {
                newFileName += "." + extension;
            }
            formData.append('FormFiles', file, newFileName);
        }
    }

    // remove files from the input element to prevent interference with the data retreived above
    input.value = "";

    // submit form - send data to a controller action
    let request = new XMLHttpRequest();
    request.open("POST", form.action);
    request.send(formData);
}














// Getting values from inputs of existing files... Not anymore needed as we create new FormData() from existing form which already has the necessary data.
//let existingFilesWrappers = document.querySelectorAll('.material-file-edit-mode-wrapper:not(.added-through-input)');
//for (let i = 0; i < existingFilesWrappers.length; i++) {
//    let inputs = existingFilesWrappers[i].querySelectorAll('input');

//    let existingFileId;
//    let existingFileName;
//    let existingFileContentType;

//    for (let j = 0; j < inputs.length; j++) {
//        let elementProp = inputs[j].id;
//        let elementValue = inputs[j].value;

//        if (elementProp.includes("FileId")) {
//            existingFileId = elementValue;
//        }
//        else if (elementProp.includes("FileName")) {
//            existingFileName = elementValue;
//        }
//        else if (elementProp.includes("ContentType")) {
//            existingFileContentType = elementValue;
//        }
//    }

//    formData.append(`Files[${i}].FileId`, existingFileId);
//    formData.append(`Files[${i}].FileName`, existingFileName);
//    formData.append(`Files[${i}].ContentType`, existingFileContentType);
//}