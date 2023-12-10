jQuery(document).ready(function ($) {
    $(".scroll").click(function (event) {
        //event.preventDefault();
        $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1200);
    });
});
 

$(window).load(function () {
    // $('.flexslider').flexslider({
    //     animation: "slide",
    //     controlNav: "thumbnails"
    // });
});


$(document).ready(function () {
    $("#slideshow > div:gt(0)").hide();
    setInterval(function () {
        $('#slideshow > div:first')
          .fadeOut(3000)
          .next()
          .fadeIn(3000)
          .end()
          .appendTo('#slideshow');
    }, 5000);
})
// $(document).ready(function () {
//     $("#slideshow1 > div:gt(0)").hide();
//     setInterval(function () {
//         $('#slideshow1 > div:first')
//           .fadeOut(3000)
//           .next()
//           .fadeIn(3000)
//           .end()
//           .appendTo('#slideshow1');
//     }, 5000);
// })









