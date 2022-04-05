/*!
* Downloads Page JS
*/

$(document).ready(function() {
    pdfjsLib.GlobalWorkerOptions.workerSrc = '/js/bundle.pdf.worker.js';

    $('div.card[data-type=pdf-card]').each(function() {
        const me = $(this);
        const url = me.find('a').attr('href');

        pdfjsLib.getDocument(url).promise.then(function(pdf) {
            const pageNumber = 1;

            pdf.getPage(pageNumber).then(function(page) {
                const scale = 1;
                const viewport = page.getViewport({scale: scale});

                const canvas = me.find('canvas')[0];
                const context = canvas.getContext('2d');
                canvas.height = viewport.height;
                canvas.width = viewport.width;

                const renderContext = {
                    canvasContext: context,
                    viewport: viewport
                };
                page.render(renderContext);
            });
        });
    });
});
