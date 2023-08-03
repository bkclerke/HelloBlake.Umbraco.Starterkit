// BOOTSTRAP CAROUSEL - ACCESSIBILLITY PLAY PAUSE FEATURE
const button = document.querySelector("button.carousel-media-btn");
if (button != null) {
    button.addEventListener("click", function () {

        var isPaused = this.classList.contains("is-paused");
        var carouselId = this.dataset.carouselId;
        var carouselEl = document.getElementById(carouselId);
        var $carousel = bootstrap.Carousel.getInstance(carouselEl);

        if (isPaused) {
            this.classList.remove("is-paused");
            this.setAttribute("aria-label", "Pause Carousel");
            isPaused = this.classList.contains("is-paused");
            $carousel.cycle();
        }
        else {
            this.classList.add("is-paused");
            this.setAttribute("aria-label", "Play Carousel");
            isPaused = this.classList.contains("is-paused");
            $carousel.pause();
        }

    });
}
const carouselButtons = document.querySelectorAll('.carousel button:not(.carousel-media-btn)');
carouselButtons.forEach(button => {
    button.addEventListener('click', () => {
        const nearestCarouselId = button.closest("div.carousel").getAttribute('id');

        if (nearestCarouselId) {
            const carouselMediaBtn = document.querySelector("#" + nearestCarouselId + " button.carousel-media-btn");

            if (carouselMediaBtn) {
                var carouselPaused = carouselMediaBtn.classList.contains("is-paused");
                if (carouselPaused) {
                    carouselMediaBtn.classList.remove("is-paused");
                    carouselMediaBtn.setAttribute("aria-label", "Pause Carousel");
                }
            }
        }
    });
});
