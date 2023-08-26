var setting = {
    scrollTop: 0,
    screenHeight: 0,
    changeView: false,
    isload: false,
    introName: "intro-cake",
    hotCake: "hot-cake"
}



$(document).ready(function () {
    $(document).scrollTop(0);
    setting.screenHeight = $(window).height();
    setTimeout(() => {
        scrollAnimation();

    }, 1000);
    setting.isload = true;


   

    swiper()
    money()

});

function swiper(){
    var swiper_melody = new Swiper(".swipe-detail-pthump", {
        spaceBetween: 10,
        slidesPerView: 4,
        freeMode: true,


    },
    );
    var swiper = new Swiper(".swipe-detail-product", {
        slidesPerView: 1,
        spaceBetween: 10,

        // pagination: {
        //     el: ".swiper-pagination",
        //     clickable: true,
        // },
        navigation: {
            nextEl: "#next-swpro",
            prevEl: "#prev-swpro",
          },
        thumbs: {
            swiper: swiper_melody
        }
    });
}

function scrollAnimation() {
    let scrollUp = false;
    let scrollDown = false;
    let hotCakeScroll = 0;

    let intro = document.getElementsByClassName(setting.introName);
    let hotCake = document.getElementsByClassName(setting.hotCake);
    let audio = document.getElementById("audio-sound");
    $(hotCake).scroll(function (e) {
        hotCakeScroll = $(hotCake).scrollTop()

    });


    window.onwheel = function (e) {

        if (e.deltaY > 0) {
            scrollUp = false;
            scrollDown = true;
        }
        else {
            scrollDown = false;
            scrollUp = true;
        }

        let hotCake = document.getElementsByClassName(setting.hotCake);

        let audio = document.getElementById("audio-sound");
        console.log("hi", scrollUp, scrollDown, hotCakeScroll);

        if (setting.changeView == false && scrollDown) {
            $(hotCake).css("top", 0)
            audio.play();
            setting.changeView = true;
            audioFade({ audio: audio, state: "on", max: 75, time: 3000 })

            $("#intro-header").css("opacity", "0")
            $("#intro-header").css("top", "-200px")
            $("#sell-cake-header").css("background", "black")
            $("#sell-cake-header").css("top", "0")


            setTimeout(() => {
                $("#special-cake").css("margin-top", "50px")

                $("#sell-cake-header").css("position", "sticky")

            }, 500);



            $(".decore-cakestore img").each(function (index, element) {
                // element == this
                let atr = $(element).attr("anim").split("-");
                setTimeout(() => {
                    $(element).attr("style", "")
                    $(element).css(atr[0], atr[1])
                }, atr[2]);
            });

            setTimeout(() => {
                $(intro).css("display", "none");
                audio.play();
            }, 300);
        }

        if (hotCakeScroll == 0 && setting.changeView == true && scrollUp) {
            $(intro).css("display", "flex");
            $(hotCake).css("top", "100vh")
            audioFade({ audio: audio, state: "off", max: 75, time: 3000 })

            $("#intro-header").css("opacity", "1")
            $("#intro-header").css("top", "0")
            $("#sell-cake-header").css("background", "transparent")
            $("#sell-cake-header").css("position", "absolute")
            $("#sell-cake-header").css("top", "-200px")
            $("#special-cake").css("margin-top", "110px")

            // $(".decore-cakestore img").css("top", "-100vh")

            $(".decore-cakestore img").each(function (index, element) {
                // element == this
                let atr = $(element).attr("anim").split("-");
                $(element).attr("style", "")
                $(element).css(atr[0], "-100vh")
            });

            setting.changeView = false;
            // $(intro).css("height", "0vh");
            // setTimeout(() => {
            //     $(intro).css("display", "none");
            //     setting.changeView = true
            //     $(intro).attr("isHide", "true");
            //     audio.play();
            // }, 400);
        }


    }

    var audioFade = ({ audio, state, max, time }) => {

        max = max / 100;

        if (state === "on") {
            audio.play();
            audio.volume = 0;

            let loop = setInterval(() => {
                let step = max - audio.volume
                if (audio.volume < max && step > 0.05) {
                    audio.volume = audio.volume + 0.01;
                }
                else {
                    audio.volume = audio.volume + step;
                    clearInterval(loop);
                }
            }, time / 100)

        }
        else {
            audio.volume = max;
            let loop = setInterval(() => {
                let step = audio.volume - 0
                if (audio.volume > 0 && step > 0.05) {
                    audio.volume = audio.volume - 0.01;
                }
                else {
                    audio.volume = audio.volume - step;
                    clearInterval(loop);
                    audio.pause();
                    audio.currentTime = 0;
                }
            }, time / 100)
        }
    }


}

// them dau cham vd 1000d => 1.000d

function money() {
    var a = jQuery(".money");
    for (let i = 0; i < a.length; i++) {
        const element = a.eq(i);
        element.html(
            element
                .html()
                .toString()
                .replaceAll(/\B(?=(\d{3})+(?!\d))/g, ".")
        );
    }
}


if ($("#content").attr("current") == "product-content") {
    // let unit = $("#user-detail-input-order").attr("detail")
    // if ($("#user-detail-input-order").val().search(unit) < 0) {
    //     $("#user-detail-input-order").val($("#user-detail-input-order").val() + ` ${unit}`)
    // }

    $("#order-plus-btn").click(function (e) {
        e.preventDefault();
        let current = $("#user-detail-input-order").val()
        let number = parseInt(current)
        
    
        let nexnum = number + 1
        $("#user-detail-input-order").val(nexnum)
        $("#true-number-order").val(nexnum)
    });
    
    $("#order-minus-btn").click(function (e) {
        e.preventDefault();
        let current = $("#user-detail-input-order").val()
        let number = parseInt(current)
    
        if (number > 1) {
            let nexnum = number - 1
            $("#user-detail-input-order").val(nexnum)
            $("#true-number-order").val(nexnum)
        }
    });

}


