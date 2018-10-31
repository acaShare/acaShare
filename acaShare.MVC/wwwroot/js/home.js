document.addEventListener('DOMContentLoaded', function() {
    var elems = document.querySelectorAll('.sidenav');
    var instances = M.Sidenav.init(elems, options);
});

$(document).ready(function(){
    $('.sidenav').sidenav();
});


$(document).ready(function(){
    $('.tabs').tabs();
});

$(document).ready(function(){
    $('.tooltipped').tooltip();
});