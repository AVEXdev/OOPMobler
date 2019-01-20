$(function (e) {
    "use strict";
    $(".drop").on("click", function (e) {
        $(".sub").toggleClass("active");
    });
    $(".search").on("click", function (e) {
        $(".search-bar").toggleClass("active");
    });

    $(".banner-wrapper > a").on("click", function (e) {
        var modal = $(".banner-wrapper > a");
        $(".overflow-block").addClass("show");
    });
    $(".close").on("click", function (e) {
        $(".overflow-block").removeClass("show");
    });


    /*   if(!$(event.target).is('#foo')) {
        // hide menu
        $(".search-bar").css("opasity", 0);
        
      } */

    // sticky header
    // run on scroll event
    /*   var navOffset, scrollPos = 0;
      
        $(window).scroll(function() {
        navOffset = $(".header").offset().top;
            scrollPos = $(window).scrollTop();	
            if (scrollPos >= navOffset) {
                $(".header").addClass("fixed");
            } else {
                $(".header").removeClass("fixed");
            }		
        });  */



});





