/**
 * Created by Yavuz on 5.1.2017.
 */

$(document).ready(function(){
    var bx = $('body').find('.bxslider').bxSlider({
        easing:'ease',
        captions:false,
        responsive:true,
        controls:true,
        auto: true,
        autoHover: true,
        pause:10000,
        touchEnabled:true,
        swipeThreshold:50,
        oneToOneTouch:true
    });
/*

    var index;
    $(".minislider .item").hover(
        function() {
            index = $(this).index();
            $('.minislider .item').eq(index).addClass('miniActive');
            $('.minislider .item').addClass('pointerNone');
            $('.minislider .item').eq(index).removeClass('pointerNone');
            if(index==0 || index==3){
                $('.minislider').css('background-image', 'url("images/car-bg.jpg")');
            }
            if(index==3){
                $('.default-info').css('background-image', 'url("images/lcv-bg.jpg")');
            }
            if(index!=3){
                $('.minislider .item').eq(3).addClass('itemCustomSize');
            }
            $('.title', this).addClass('animated fadeInRight');
            $('.default-info').css('background-image', 'url("images/lcv-bg.jpg")');
        }/*,
        function() {
            $('.minislider .item').eq(index).removeClass('miniActive');
            $('.minislider .item').removeClass('pointerNone');
            if(index!=3) {
                $('.minislider .item').eq(3).removeClass('itemCustomSize');
            }
            if(index==3){
                $('.default-info').css('background-image', '');
            }
            if(index==0 || index==3){
                //$('.minislider').css('background-image', 'url("images/lcv-bg.jpg")');
            }
            //$('.title span', this).addClass('animated fadeInRight');
        }
    );*/





    /*
    $(".minislider .item").hover(
        function() {
            var index = $(this).index();
            if(index==0 || index==2){
                $('.minislider').css('background-image', 'url("images/car-bg.jpg")');
            }
            if(index==1 || index==3){
                $('.minislider').css('background-image', 'url("images/lcv-bg.jpg")');
            }
            $('.default-info').fadeOut(400);
            $('.title', this).fadeIn(400);
            $(this).removeClass('pointerNone');
            $(this).animate({
                marginLeft  : '0',
                width       : '+=26.7%'
            }, 500, function(){

            });
        },
        function() {
            $(this).animate({
                marginLeft  : '-3%',
                width       : '-=26.7%'
            }, 200, function(){
                $('.minislider').css('background-image', '');
            });
            $('.default-info').fadeIn(400);
            $('.title', this).fadeOut(50);
        }
    );
    */
    /*
    $('.minislider .item.car').hover(function(){
        var index = $(this).index();
        if(index!=4) {
            $('.default-info').fadeOut(400);
            $('.title', this).fadeIn(400);
            $(this).animate({
                marginLeft  : '0',
                width       : '+=26.7%'
            }, 1000);
        }
    });
    $('.minislider .item.car').mouseleave(function(){
        var index = $(this).index();
        if(index!=4) {
            $(this).animate({
                marginLeft  : '-3%',
                width       : '-=26.7%'
            }, 500);
            $('.default-info').fadeIn(400);
            $('.title', this).fadeOut(400);
        }
    });

    $('.minislider .car').hover(function(){
        var index = $(this).index();
        if(index!=4) {
            $('.default-info').fadeOut(400);
            $('.title', this).fadeIn(400);
            $(this).animate({
                marginLeft  : '0',
                width       : '46.8%'
            }, 1000);
        }
    });
    $('.minislider .car').mouseleave(function(){
        var index = $(this).index();
        if(index!=4) {
            $(this).animate({
                marginLeft  : '-3%',
                width       : '-=26.7%'
            }, 500);
            $('.default-info').fadeIn(400);
            $('.title', this).fadeOut(400);
        }
    });
    $('.minislider .lcv').hover(function(){
        var index = $(this).index();
        if(index!=4) {

            $('.default-info').fadeOut(400);
            $('.title', this).fadeIn(400);
            $(this).animate({
                marginLeft  : '0',
                width       : '46.7%'
            }, 1000);
        }
    });
    $('.minislider .lcv').mouseleave(function(){
        var index = $(this).index();
        if(index!=4) {


            $(this).animate({
                marginLeft  : '-3%',
                width       : '-=30%'
            }, 500);
            $('.default-info').fadeIn(400);
            $('.title', this).fadeOut(400);
        }
    });
    $('.minislider .truck').hover(function(){
        var index = $(this).index();
        if(index!=4) {
            $('.default-info').fadeOut(400);
            $('.title', this).fadeIn(400);
            $(this).animate({
                marginLeft  : '0',
                width       : '50%'
            }, 1000);
        }
    });
    $('.minislider .truck').mouseleave(function(){
        var index = $(this).index();
        if(index!=4) {
            $(this).animate({
                marginLeft  : '-3%',
                width       : '-=30%'
            }, 500);
            $('.default-info').fadeIn(400);
            $('.title', this).fadeOut(400);
        }
    });
    $('.minislider .bus').hover(function(){
        var index = $(this).index();
        if(index!=4) {
            $('.default-info').fadeOut(400);
            $('.title', this).fadeIn(400);
            $(this).animate({
                marginLeft  : '0',
                width       : '50%'
            }, 1000);
        }
    });
    $('.minislider .bus').mouseleave(function(){
        var index = $(this).index();
        if(index!=4) {
            $(this).animate({
                marginLeft  : '-3%',
                width       : '-=30%'
            }, 1000);
            $('.default-info').fadeIn(400);
            $('.title', this).fadeOut(400);
        }
    });
    */






    $('.minislider .item').hover(
        function () {
            var index = $(this).index();
            $(this).addClass('miniActive');
            if ($(this).hasClass('miniActive')) {
                if(index!=1) {
                    $(".title", this).css('right', '150px');
                }else{
                    $(".title", this).css('right', '79px');
                }
                $(".title", this).animate({
                    opacity: 1
                }, 250);
                // $(".info").remove();
            }

        },
        function () {
            $(this).removeClass('miniActive');
            if (!$(this).hasClass('miniActive')) {
                $(".title", this).animate({
                    opacity:0
                }, 10);
                $(".title", this).css('right', '-250px');


                //  $(".minislider").append("<div class='info'>Ýlgilendiðiniz modeller arasýnda hýzlý ve kolayca geçiþ yapabilirsiniz.</div>")
            }
        }
    );


    $('.campaigns .title h2').click(function(){
      $('.campaigns .title h2').removeClass('active');
      $('.tabInner').removeClass('active');
       var id = $(this).data('tab');
        $('#'+id).addClass('active');
        $(this).addClass('active');
    });

    $('.tabs h3').click(function(){
      $('.tabs h3').removeClass('active');
      $('.tabContainer>div').removeClass('active');
       var id = $(this).data('tab');
        $('#'+id).addClass('active');
        $(this).addClass('active');
    });

    $('.menuIcon').click(function(){
        //$('body').toggleClass('overflow', '');
        $('.mobileMenu').fadeIn(400);

        $('body').css({
            height      : $(window).height(),
            overflow    : 'hidden'
        });
    });
    $('.mobileMenu .close').click(function(){
       // $('body').toggleClass('overflow', '');
        $('.mobileMenu').fadeOut(400);
		
		$('body').css({
            height      : 'auto',
            overflow    : 'auto'
        });
    });
});
