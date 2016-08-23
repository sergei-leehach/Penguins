$(function () {    
    setNavigation();
});

function setNavigation() {
    var path = window.location.pathname;
    path = path.replace(/\/$/, "");
    path = decodeURIComponent(path);

    $(".nav-align a").each(function () {        
        var href = $(this).attr('href');      
        if (path.substring(0, href.length) === href) {
            $(".nav-align li").removeClass('active');
            $(this).closest('li').addClass('active');         
        }
    });
}