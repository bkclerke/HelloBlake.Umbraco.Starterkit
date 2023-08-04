// BOOTSTRAP CAROUSEL - ACCESSIBILLITY PLAY PAUSE FEATURE
function playBtn(button) {
    button.classList.remove("is-paused");
    button.setAttribute("aria-label", "Pause Carousel");
}

function pauseBtn(button) {
    button.classList.add("is-paused");
    button.setAttribute("aria-label", "Play Carousel");
}

const carouselBtns = document.querySelectorAll('.carousel button');
carouselBtns.forEach(button => {
    button.addEventListener('click', () => {
        // determine if media button was clicked
        const mediaBtnClick = button.classList.contains('carousel-media-btn');
        // get nearest carousel id
        const nearestCarouselId = button.closest("div.carousel").getAttribute('id');
        // get nearest carousel media button
        const carouselMediaBtn = document.querySelector("#" + nearestCarouselId + " button.carousel-media-btn");
        // check if slider has autoplay enabled
        if (carouselMediaBtn) {
            // get carousel instance
            var carouselEl = document.getElementById(nearestCarouselId);
            var $carousel = bootstrap.Carousel.getInstance(carouselEl);
            // determine carousel paused state
            var isPaused = carouselMediaBtn.classList.contains("is-paused");

            if (mediaBtnClick) {
                if (isPaused) {
                    // play
                    playBtn(carouselMediaBtn);
                    $carousel.cycle();
                }
                else {
                    // pause
                    pauseBtn(carouselMediaBtn);
                    $carousel.pause();
                }
                
            } else {
                if (isPaused) {
                    // update button to reflect play state
                    playBtn(carouselMediaBtn);
                }
            }
        }
    })
});