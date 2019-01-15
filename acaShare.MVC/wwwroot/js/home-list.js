function handleListIconClick() {
    let list = document.getElementsByClassName('path-list-item');

    for (let listItem of list) {
        let a = listItem.firstElementChild;
        let icon = listItem.children[1];
        icon.onclick = () => { a.click(); };
    }
}