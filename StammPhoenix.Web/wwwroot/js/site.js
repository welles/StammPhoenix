/*!
* Site Specific JS
*/

$(document).ready(function () {
    updateBackgroundColor(sessionStorage.getItem('background-color'));

    $('.navbar-brand').click(_ => sessionStorage.removeItem('background-color'));
});

function updateBackgroundColor(cacheColor) {
    const backgroundColor = cacheColor ?? "hsl(" + 360 * Math.random() + ',' + (25 + 70 * Math.random()) + '%,' + (80 + 10 * Math.random()) + '%)';

    sessionStorage.setItem('background-color', backgroundColor);

    $('body').css('background-color', backgroundColor);
}
