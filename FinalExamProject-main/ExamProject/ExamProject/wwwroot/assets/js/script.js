

$(document).ready(function () {
    $('.owl-carousel').owlCarousel({
  loop:true,
  margin:10,
        autoplay: true,
autoplayTimeout: 5000,
autoplayHoverPause: false,
  dots: true,
  responsive:{
      0:{
          items:1
      },
      600:{
          items:1
      },
      1000:{
          items:1
      }
  }
  })
  });