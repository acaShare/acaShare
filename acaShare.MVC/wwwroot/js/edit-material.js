function deleteFile(fileId, event) {
    event.stopPropagation();
    document.getElementById(fileId).remove();
}

function showUploadedFiles() {
    var input = document.getElementById('file-picker');
    var output = document.getElementById('teeest');

    for (var i = 0; i < input.files.length; ++i) {
        output.innerHTML += input.files.item(i).name;
    }
}