(function (window, $) {
    function collapse(selector, action) {
        $(selector).collapse(action);
    }

    function copyTextToClipboard(text) {
        var $temp = $('<input>');
        $('body').append($temp);
        $temp.val(text).select();
        document.execCommand('copy');
        $temp.remove();
    }

    window.laterlist = {
        collapse: collapse,
        copyTextToClipboard: copyTextToClipboard,
    };

})(window, jQuery);