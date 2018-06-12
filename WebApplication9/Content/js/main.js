// STICKY HEADER
$(document).ready(function() {
    "use strict";

    $("header").sticky({
        topSpacing: 0
    });

    // ONSCROLL PROGRESS
    $(window).scroll(function() {
        var s = $(window).scrollTop(),
            d = $(document).height(),
            c = $(window).height();
        scrollPercent = (s / (d - c)) * 100;
        var position = scrollPercent;

        $("#progressbar").attr('value', position);
    });

    // SEARCH
    $(".social-trigger").on("click", function() {
        $(this).addClass("st-hide");
        $(".social-top a").removeClass("hide-social");
        $(".social-top").addClass("show-social");
        return false;
    });

    $(".search-trigger img").on("click", function(e) {
        e.stopPropagation();
        $(".search-content").toggleClass("sc-active");
    });

    $("body").on("click", function(e) {
        e.stopPropagation();
        $(".search-content").removeClass("sc-active");
       
    });

    $(".search-content").on("click", function(e) {
        e.stopPropagation();
        $(".search-content").addClass("sc-active");
     
    });

    // MOBILE NAV ACTIVE
    $(".navbar-toggle").on("click", function() {
        $(".freebie-btn").toggleClass("mob-hide");
        $(".social-top").toggleClass("mob-hide");
       
    });

});
