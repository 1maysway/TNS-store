//const { forEach } = require("../lib/fontawesome-free/js/v4-shims");

(function () {

    window.inputNumber = function (el) {

        var min = el.attr('min') || false;
        var max = el.attr('max') || false;

        var els = {};

        els.dec = el.prev();
        els.inc = el.next();

        el.each(function () {
            init($(this));
        });

        function init(el) {

            els.dec.on('click', decrement);
            els.inc.on('click', increment);

            function decrement() {
                var value = el[0].value;
                value--;
                if (!min || value >= min) {
                    el[0].value = value;
                }
            }

            function increment() {
                var value = el[0].value;
                value++;
                if (!max || value <= max) {
                    el[0].value = value++;
                }
            }
        }
    }
})();

//inputNumber($('.input-number'));

var inputNumbers = $('.input-number');
console.log(inputNumbers);

for (var i = 0; i < inputNumbers.length;i++) {

    if ($(inputNumbers[i]).val() == 1) {
        $(inputNumbers[i]).prev().addClass('tns-disabled-a');
    }
    else {
        $(".tns-cart-body-left-item#" + inputNumbers[i].id).find(".originalPrice").css({
            'opacity': '1',
            'user-select': 'auto'
        });
    }
}

$('.input-number-btn').click((el) => {

    var value;

    if (el.target.classList.contains('decrement')) {
        decrement();
    }
    else {
        increment();
    }

    function decrement() {
        value = $(el.target).next().val();
        value--;

        if (value <= 1) {
            $(el.target).addClass('tns-disabled-a');
        }

        if (value >= 1) {
            $(el.target).next().val(value);
        }
    }

    function increment() {
        value = $(el.target).prev().val();
        value++;
        $(el.target).prev().prev().removeClass('tns-disabled-a');
        $(el.target).prev().val(value);
    }

    if (value >= 2) {
        $(".tns-cart-body-left-item#" + el.target.id).find(".originalPrice").css({
            'opacity': '1',
            'user-select': 'auto'
        });
    }
    else {
        $(".tns-cart-body-left-item#" + el.target.id).find(".originalPrice").css({
            'opacity': '0',
            'user-select': 'none'
        });
    }

});