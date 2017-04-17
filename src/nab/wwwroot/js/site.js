// script loaded in all pages

document.documentElement.className = 'js';

// touch events support
if (!('ontouchstart' in document.documentElement)) {
    document.documentElement.className += ' no-touch';
}

$(function () {
    //console.log('jQuery - DOM ready');
});
