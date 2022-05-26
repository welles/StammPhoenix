/*!
* Site Specific JS
*/

$(document).ready(function () {
    const backgroundColor = sessionStorage.getItem('background-color')
        ?? "hsl(" + 360 * Math.random() + ',' + (25 + 70 * Math.random()) + '%,' + (80 + 10 * Math.random()) + '%)';

    sessionStorage.setItem('background-color', backgroundColor);

    $('body').css('background-color', backgroundColor);
});
