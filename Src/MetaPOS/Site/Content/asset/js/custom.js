(function ($) {
    "use strict"; // Start of use strict
    // jQuery for page scrolling feature - requires jQuery Easing plugin
    $('a.page-scroll').bind('click', function (event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top - 50)
        }, 1250, 'easeInOutExpo');
        event.preventDefault();
    });
    // Highlight the top nav as scrolling occurs
    $('body').scrollspy({
        target: '.navbar-fixed-top',
        offset: 100
    });
    // Closes the Responsive Menu on Menu Item Click
    $('.navbar-collapse ul li a').click(function () {
        $('.navbar-toggle:visible').click();
    });
    // Offset for Main Navigation
    $('#mainNav').affix({
        offset: {
            top: 50
        }
    })
    // Change cursor on hover
    $('.link').hover(function () {
        $(this).css('cursor', 'pointer');
    })
    //active tools tip
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
    // Onclick events handaler
    $('.add_new').hover(function () {
        $(this).fadeOut(function () {
            $(this).text("সংযোজন ফিচার গুলো দেখুন").fadeIn(function () {
                $(this).click(function () {
                    window.location.href = 'features';
                })
            })
        })
    })
    $('.updated').hover(function() {
        $(this).fadeOut(function() {
            $(this).text("আপডেট ফিচার গুলো দেখুন").fadeIn(function() {
                $(this).click(function() {
                    window.location.href = 'features';
                })
            })
        })
    })


    //$(".go_features").click(function () {
    //    window.location.href = 'features.html';
    //});
    $(".pro_header").click(function () {
        window.location.href = 'features';
    });
    $(".customization-benefit").click(function () {
        window.location.href = 'http://demo.metaposbd.com/login/';
    });
    $(".company-name").click(function () {
        window.open('http://metakave.com/', '_blank');
    });
    $(".order-now").click(function () {
        window.location.href = 'http://demo.metaposbd.com/login/';
    });
    $(".btn-enterprise-contact").click(function () {
        window.location.href = '/contact';
    });
    $(".btn-basic-demo").click(function () {
        window.location.href = 'http://demo.metaposbd.com/login/';
    });




})(jQuery); // End of use strict

(function () {
    $('#itemslider').carousel({
        interval: 4000
    });
}());
(function () {
    $('#itemslider2').carousel({
        interval: 0
    });
}());
(function () {
    $('.carousel-showmanymoveone .item').each(function () {
        var itemToClone = $(this);
        for (var i = 1; i < 6; i++) {
            itemToClone = itemToClone.next();
            if (!itemToClone.length) {
                itemToClone = $(this).siblings(':first');
            }
            itemToClone.children(':first-child').clone().addClass("cloneditem-" + (i)).appendTo($(this));
        }
    });
}());
