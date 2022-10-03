// const firstCarousel = document.querySelector('#myCarousel');
// const carousel = new bootstrap.Carousel(firstCarousel, {
//     touch: true
// });

//const { get } = require("jquery");

showCartCount();

function showCartCount(plus=null) {
    var CartButtonCount = document.getElementsByClassName("tns-cartButton-count")[0];

    if (parseInt(CartButtonCount.innerText) > 0 || plus != null) {
        //if (parseInt(CartButtonCount.innerText) > 9 || CartButtonCount.innerText=="9+")
        //    CartButtonCount.innerText = "9+";
        //else
        if (plus != null)
            CartButtonCount.innerText = parseInt(CartButtonCount.innerText) + plus;

        $(CartButtonCount).show();
    }
    if (plus == 0 || parseInt(CartButtonCount.innerText)==0) {
        $(CartButtonCount).hide();
    }
}

$('.addToCart').click(function () {
    showCartCount(1);
});


try {
    CartProductsCount();
} catch (e) {
    console.log('not cart');
}

function CartProductsCount(plus=0) {
    var productsCountElement = document.getElementsByClassName('products_sum_value')[0].children[0];
    var productsCount = parseInt(productsCountElement.innerText.split(' ')[0])+plus;


    if ((productsCount < 10 && productsCount % 10 == 1) || (productsCount > 20 && productsCount % 10 == 1 && !(productsCount - 11) % 10 == 0)) {
        productsCountElement.innerText = productsCount + " Товар";
    } else if ((productsCount < 10 && productsCount % 10 > 1 && productsCount % 10 < 5) || (productsCount > 20 && productsCount % 10 > 1 && productsCount % 10 < 5 && !(productsCount - 11) % 10 == 0)) {
        productsCountElement.innerText = productsCount + " Товара";
    } else {
        productsCountElement.innerText = productsCount + " Товаров";
    }
}

function TotalPrice(plus = 0) {
    var TotalPriceElement = document.getElementsByClassName('total_price_value')[0].children[0];
    console.log(parseInt(plus));
    TotalPriceElement.innerText = parseInt(TotalPriceElement.innerText.split(' ')[0]) + parseInt(plus) + " ₽";
}

$('.input-number-btn').click((el) => {
    let id = parseInt(el.target.id);
    console.log(id);

    let product = $(".tns-cart-body-left-item#" + id);
    var countPlus;
    var totalPricePlus = $(product).find('#Price_'+id).val();


    if (el.target.classList.contains('decrement')) {
        countPlus = -1;
    }
    else {
        countPlus = 1;
    }

    $(product).find(".tns-cart-item-totalPrice").text(parseInt((product).find(".tns-cart-item-totalPrice").text()) + parseInt(totalPricePlus) * countPlus + ' ₽');

    showCartCount(countPlus);
    CartProductsCount(countPlus);
    TotalPrice(parseInt(totalPricePlus) *countPlus);
});



$('.carousel').hover(function () {
    $(this).find('.carousel-control-prev').show();
    $(this).find('.carousel-control-next').show();
},
    function () {
        $(this).find('.carousel-control-prev').hide();
        $(this).find('.carousel-control-next').hide();
    });



$('.tns-drop').hover(function () {
    console.log('hover');

    //$(this).find('.dropdown-menu').show();

    // show dropdown menu to the right of this card

    const btn = $(this);

    setTimeout(function () {
        if (btn.is(':hover')) {

            if ($(btn).hasClass('tns-category-list-item')) {
                $(btn).find('.dropdown-menu').css('top', $('#tns-category-card').position().top - $(btn).position().top - $('#tns-category-card').position().top - 10);
            }

            $(btn).find('.dropdown-menu').css('display', 'flex');
            $('.tns-drop').not(btn).find('.dropdown-menu').hide();
        }
    }, 300);

    // hide other dropdown menus

},
    function () {
        console.log('hover out');

        let btn = $(this);

        setTimeout(function () {
            if (!btn.find('.dropdown-menu:hover').length && !btn.is(':hover')) {
                btn.find('.dropdown-menu').hide();
            }
        }, 300);

    });

$('.dropdown-menu').hover(function () {
    console.log('hover');
}, function () {
    console.log('hover out');
    setTimeout(() => {
        // find the parent tns-drop element
        let btn = $(this).closest('.tns-drop');
        if (!btn.is(':hover')) {
            $(this).hide();
        }
    }, 300);
});

// change background color of the 'tns-list-item' when mouse is over it
$('.tns-list-item').hover(function () {
    $(this).css('background-color', 'rgb(230, 230, 230)');
}, function () {
    $(this).css('background-color', 'white');
});

// hide dropdown menu when mouse is over the 'tns-list-item'

$('.tns-actual-category-btn').click(function () {
    event.preventDefault();

    $('.tns-actual-category-btn').removeClass('selected');
    $(this).addClass('selected');
});

$('.tns-close-button').click(function () {
    // remove the element that contains this
    $(this).parent().remove();

    if ($('.tns-history-item').length == 0) {
        $('#tns-history').hide();
    }
});




console.log('AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA');

$(".tns-btn-ajax").click(function (e) {
    e.preventDefault();
    //alert("text");
    $.ajax({

        url: $(this).attr("href"), // comma here instead of semicolon   
        success: function () {
            //alert("Value Added");  // or any other indication if you want to show
        }
    });
});

$(".tns-addToCartBtn").click(function (e) {
    $(this).addClass('adedToCart');
    $(this).addClass('tns-disabled-a');
});

$(".removeFromCart").click(function (e) {
    e.preventDefault();

    let id = $(this).attr('id');

    let product = $(".tns-cart-body-left-item#" + id);

    var CartButtonCount = document.getElementsByClassName("tns-cartButton-count")[0];

    var CartItemCount = parseInt($(".tns-cart-body-left-item#" + id).find('.input-number').val());
    showCartCount(-CartItemCount);

    CartProductsCount(-CartItemCount)

    TotalPrice(-parseInt($(product).find(".tns-cart-item-totalPrice").text()));
    
    product.remove();
});

//function TnsButtonAjax(e) {
//    e.preventDefault();
//    alert("text");
//    console.log('click');
//    $.ajax({

//        url: $(this).attr("href"), // comma here instead of semicolon   
//        success: function () {
//            //alert("Value Added");  // or any other indication if you want to show
//        }
//    });
//}





////////////////////////////////////////////


// get cookie
function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}

// set cookie
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

//array to json function
function arrayToJson(arr) {
    let json = {};
    for (let i = 0; i < arr.length; i++) {
        json[i] = arr[i];
    }
    return json;
}

// save array in cookie
function setCookieArray(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

// string to int array
function stringToIntArray(str) {
    let arr = str.split(',');
    let intArr = [];

    for (let i = 0; i < arr.length; i++) {
        intArr.push(parseInt(arr[i]));
    }

    return intArr;
}



const toastTrigger = document.getElementById('liveToastBtn')
const toastLiveExample = document.getElementById('liveToast')
if (toastTrigger) {
    toastTrigger.addEventListener('click', () => {
        const toast = new bootstrap.Toast(toastLiveExample)
        // change toast delay
        toast._config.delay = 10000;
        toast.show()
    })
}