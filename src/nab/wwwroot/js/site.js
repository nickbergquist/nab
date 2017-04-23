// script loaded in all pages

document.documentElement.className += ' js';

// touch events support
if (!('ontouchstart' in document.documentElement)) {
    document.documentElement.className += ' no-touch';
}

$(function () {
    enquire.register('screen and (max-width: 479px)', {
        match: function () {
            siteNavBehaviour.navCondensed();
        }
    }).register('screen and (min-width: 480px)', {
        match: function () {
            siteNavBehaviour.navExpanded();
        }
    }).listen(50); // milliseconds
});


// METHODS

var siteNavBehaviour = {
    navCondensed: function () {
        if (!$('header .nav-button-show').length) {
            var $header = $('header');
            var $headerNav = $('header nav');
            var $buttonNavVisibility = $('<button type="button" class="nav-button-show">Show/Hide</button>');

            $buttonNavVisibility.prependTo($header);

            $buttonNavVisibility.click(function () {
                if ($headerNav.hasClass('show')) {
                    $headerNav.removeClass('show');
                } else {
                    $headerNav.addClass('show');
                }
            });
        }
    },
    navExpanded: function () {
        $('header nav').removeClass('show');
        if ($('header .nav-button-show').length) {
            $('header .nav-button-show').remove();
        }
    }
};
