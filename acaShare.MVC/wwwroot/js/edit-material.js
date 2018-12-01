var filesToUpload = [];

window.onload = function () {
    initializeDragAndDropArea();
};

function initializeDragAndDropArea() {
    let dragAndDropArea = document.getElementById('files-wrapper');
    
    let counter = 0;
    dragAndDropArea.ondragenter = () => {
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
        storeUploadedFiles(e.dataTransfer.files, false);
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

function storeUploadedFiles(files, isFromInput) {
    let output = document.getElementById('files-wrapper');
    let id = output.children.length === 1 ? 0 : parseInt(output.lastElementChild.id) + 1;

    for (let i = 0; i < files.length; i++ , id++) {
        if (!filesToUpload.map(f => f.file).includes(files.item(i))) {
            filesToUpload.push({ id: id, file: files.item(i), isFromInput: isFromInput });
        }
    }
}

function deleteFile(fileId, event) {
    event.stopPropagation();

    // remove visually and possibly refresh existing files list indexes to satisfy model binder's requirements 
    let elementToRemove = document.getElementById(fileId);
    elementToRemove.remove();
    if (!elementToRemove.classList.contains('added-through-upload')) {
        refreshExistingFilesIndexes();
    }

    // remove from (hidden) gallery
    document.getElementById('gallery-item-' + fileId).remove();

    // remove from filesToUpload
    filesToUpload = filesToUpload.filter(f => f.id !== fileId);

    // refresh slides indexes for gallery management
    refreshSlidesIndexes();
}

function refreshExistingFilesIndexes() {
    let existingFilesWrappers = document.querySelectorAll('.material-file-edit-mode-wrapper:not(.added-through-upload)');
    for (let i = 0; i < existingFilesWrappers.length; i++) {
        let inputs = existingFilesWrappers[i].querySelectorAll('input');
        
        for (let j = 0; j < inputs.length; j++) {
            inputs[j].id = inputs[j].id.replace(/\d+/, i);
            inputs[j].name = inputs[j].name.replace(/\d+/, i);
        }
    }
}

function refreshSlidesIndexes() {
    let elemsThatFireModal = document.querySelectorAll('.material-file-edit-mode-wrapper .material-file');
    let slideIdx = 1;
    elemsThatFireModal.forEach(e =>
        e.onclick = function (slideIndex) { return () => { openModal(); currentSlide(slideIndex); }; }(slideIdx++)
    );
}

function handleUploadFromInputElement() {
    removePreviousInputFiles();
    let input = document.getElementById('file-picker');
    storeUploadedFiles(input.files, true);
    showUploadedFiles(input.files);
}

function removePreviousInputFiles() {
    filesToUpload
        .filter(f => f.isFromInput)
        .map(f => f.id)
        .forEach(id => {
            deleteFile(id, event);
        });
}

function showUploadedFiles(files) {
    let output = document.getElementById('files-wrapper');
    let slideIdx = output.children.length === 1 ? 1 : parseInt(output.lastElementChild.id) + 2; // +1 to get next free idx
    let id = output.children.length === 1 ? 0 : parseInt(output.lastElementChild.id) + 1; // lenght 1 because there is hidden drop area div
    let modalContent = document.getElementById('modal-content');

    for (var i = 0; i < files.length; i++ , slideIdx++ , id++) {
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
            `
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
                <input type="text" id="FormFile[${id}]__FileName" value="${fileName}" class="validate" data-length="50" data-val="true" data-val-maxlength="Nazwa materia�u nie mo�e przekracza� 50 znak�w" data-val-maxlength-max="50" data-val-required="Pole &quot;Nazwa&quot; jest wymagane" >
                <span class="text-danger input-error-small field-validation-valid" data-valmsg-for="FormFile[${id}]__FileName" data-valmsg-replace="true"></span>
            `
        );

        let wrapperDiv = createNode("div", "material-file-edit-mode-wrapper added-through-upload", "", null, id);
        wrapperDiv.setAttribute('upload-file-id', id);
        output.appendChild(wrapperDiv);

        if (isImage(file)) {
            wrapperDiv.appendChild(imageMaterialFileDiv);

            let galleryItemDiv = createNode(
                "div",
                "gallery-item added-through-upload",
                `<img src="${URL.createObjectURL(file)}" alt="${fileName}" />`,
                null,
                'gallery-item-' + id
            );

            modalContent.appendChild(galleryItemDiv);
        }
        else {
            wrapperDiv.appendChild(otherFormatMaterialFileDiv);
        }

        wrapperDiv.appendChild(nameInputsDiv);
    }

    refreshSlidesIndexes();
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


function submitForm(e) {
    e.preventDefault();

    // create FormData from existing form
    let form = document.getElementById('edit-form');
    let formData = new FormData(form);

    // remove files from the input element to prevent interference with the data retreived above
    document.getElementById('file-picker').value = "";

    // append newly uploaded files (from input and from drag and drop area)
    for (let i = 0; i < filesToUpload.length; i++) {
        let id = filesToUpload[i].id;
        let file = filesToUpload[i].file;

        let nameInput = document.getElementById(`FormFile[${id}]__FileName`);
        let extension = getExtensionFromFileName(file.name);
        let newFileName = nameInput.value;
        if (extension) {
            newFileName += "." + extension;
        }
        formData.append('FormFiles', file, newFileName);
    }

    // submit form - send data to a controller action
    let request = new XMLHttpRequest();
    request.onload = () => {
        if (request.status === 200) {
            let materialId = JSON.parse(request.responseText);
            window.location.href = `Material?materialId=${materialId}`;
        }
        else {
            let materialId = JSON.parse(request.responseText);
            window.location.href = `Edit?materialId=${materialId}`;
        }
    };
    request.open("POST", form.action);
    request.send(formData);
}









//// get visible files (the ones that weren't removed by clicking an 'x' button)
//let newFilesWrappers = document.querySelectorAll('.material-file-edit-mode-wrapper.added-through-upload');
//let newFilesIds = [];
//for (let i = 0; i < newFilesWrappers.length; i++) {
//    newFilesIds.push(newFilesWrappers[i].getAttribute('upload-file-id'));
//}

//// append only these files that are visible
//let input = document.getElementById('file-picker');
//let inputFiles = input.files;

//for (let i = 0; i < inputFiles.length; i++) {
//    if (newFilesIds.includes(i + "")) {
//        let file = inputFiles.item(i);
//        let nameInput = document.getElementById(`FormFile[${i}]__FileName`);
//        let extension = getExtensionFromFileName(file.name);
//        let newFileName = nameInput.value;
//        if (extension) {
//            newFileName += "." + extension;
//        }
//        formData.append('FormFiles', file, newFileName);
//    }
//}





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