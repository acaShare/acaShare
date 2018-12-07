function openModal() {
    document.getElementById('gallery-modal').style.display = "flex";
}

function closeModal() {
    document.getElementById('gallery-modal').style.display = "none";
}

function closeModalOnBackgroundClick(event) {
    if (!Array.from(document.querySelectorAll('.gallery-item img, .prev, .next, .close')).includes(event.target)) {
        document.getElementById('gallery-modal').style.display = "none";
    }
}

var slideIndex = 1;

// Next/previous controls
function plusSlides(n) {
  showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("gallery-item");
    var numberText = document.getElementById("current-slide-info");

    if (n > slides.length) { slideIndex = 1; }
    if (n < 1) { slideIndex = slides.length; }

    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    slides[slideIndex - 1].style.display = "block";
    numberText.innerHTML = slideIndex + " / " + slides.length;
}

document.onkeydown = function(e) {
    if (e.keyCode == '37') {
        // left arrow
        plusSlides(-1);
    }
    else if (e.keyCode == '39') {
        // right arrow
        plusSlides(1);
    }
};
