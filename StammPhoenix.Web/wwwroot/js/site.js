/*
* Site Specific JS
*/
$(document).ready(function () {
    const backgroundColors = ['#8D8741', '#659DBD', '#DAAD86', '#BC986A', '#553D67', '#E27D60'];
    const backgroundColor = backgroundColors[Math.floor(Math.random() * backgroundColors.length)];

    $('body').css('background-color', backgroundColor);
});
