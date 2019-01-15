window.addEventListener('load', () => {
    if (window.innerWidth <= 1000) {
        let breadcrumbs = document.getElementsByClassName('breadcrumb');
        Array.from(breadcrumbs).slice(0, -1).forEach(b => b.remove());
    }
    //if (window.innerWidth < 1400 && window.innerWidth > 1300) {
    //    decreaseFont('15px');
    //}
    //else if (window.innerWidth <= 1300 && window.innerWidth > 1080) {
    //    decreaseFont('14px');
    //}
    //else if (window.innerWidth <= 1080 && window.innerWidth > 1000) {
    //    decreaseFont('13px');
    //}
    //else if (window.innerWidth <= 1000 && window.innerWidth > 995) {
    //    decreaseFont('12px');
    //}
    //else if (window.innerWidth <= 995) {
    //    let breadcrumbs = document.getElementsByClassName('breadcrumb');
    //    Array.from(breadcrumbs).slice(0, -1).forEach(b => b.remove());
    //}
});

//function decreaseFont(newFontSize) {
//    let breadcrumbs = document.getElementsByClassName('breadcrumb');
//    Array.from(breadcrumbs).forEach(b => b.style.fontSize = newFontSize);
//}