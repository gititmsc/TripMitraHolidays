/* ============================================================
   TripMitra Holidays — Site JavaScript
   Slider | Mobile Nav | Package Filter Tabs | Smooth Scroll
   ============================================================ */

(function () {
    'use strict';

    /* ── HERO SLIDER ── */
    var AUTOPLAY_MS = 5000;
    var sliderTrack = document.getElementById('sliderTrack');

    if (sliderTrack) {
        var slides = document.querySelectorAll('.slide');
        var dotsEl  = document.getElementById('sliderDots');
        var progEl  = document.getElementById('sliderProgress');
        var current = 0;
        var total   = slides.length;
        var timer;

        /* Build dots */
        if (dotsEl) {
            for (var i = 0; i < total; i++) {
                (function (idx) {
                    var d = document.createElement('button');
                    d.className = 'dot' + (idx === 0 ? ' active' : '');
                    d.setAttribute('aria-label', 'Go to slide ' + (idx + 1));
                    d.addEventListener('click', function () { goTo(idx); });
                    dotsEl.appendChild(d);
                })(i);
            }
        }

        function goTo(n) {
            slides[current].classList.remove('active');
            if (dotsEl) dotsEl.children[current].classList.remove('active');
            current = (n + total) % total;
            sliderTrack.style.transform = 'translateX(-' + (current * 100) + '%)';
            slides[current].classList.add('active');
            if (dotsEl) dotsEl.children[current].classList.add('active');
            resetProgress();
        }

        window.changeSlide = function (dir) { goTo(current + dir); };

        /* Progress bar */
        function resetProgress() {
            if (!progEl) return;
            progEl.style.transition = 'none';
            progEl.style.width = '0%';
            clearTimeout(window._progRaf);
            window._progRaf = setTimeout(function () {
                progEl.style.transition = 'width ' + AUTOPLAY_MS + 'ms linear';
                progEl.style.width = '100%';
            }, 20);
        }

        function startAuto() { timer = setInterval(function () { goTo(current + 1); }, AUTOPLAY_MS); }
        function stopAuto()  { clearInterval(timer); }

        var heroSlider = document.getElementById('heroSlider');
        if (heroSlider) {
            heroSlider.addEventListener('mouseenter', stopAuto);
            heroSlider.addEventListener('mouseleave', startAuto);

            /* Touch / swipe */
            var touchStartX = 0;
            heroSlider.addEventListener('touchstart', function (e) {
                touchStartX = e.changedTouches[0].clientX;
            }, { passive: true });
            heroSlider.addEventListener('touchend', function (e) {
                var dx = e.changedTouches[0].clientX - touchStartX;
                if (Math.abs(dx) > 50) goTo(current + (dx < 0 ? 1 : -1));
            });
        }

        /* Keyboard navigation */
        document.addEventListener('keydown', function (e) {
            if (e.key === 'ArrowRight') goTo(current + 1);
            if (e.key === 'ArrowLeft')  goTo(current - 1);
        });

        resetProgress();
        startAuto();
    }


    /* ── MOBILE NAV ── */
    window.toggleMenu = function () {
        var nav = document.getElementById('mobileNav');
        if (nav) nav.classList.toggle('open');
    };
    window.closeMenu = function () {
        var nav = document.getElementById('mobileNav');
        if (nav) nav.classList.remove('open');
    };


    /* ── PACKAGE FILTER TABS (Home page) ── */
    window.filterTab = function (el, cat) {
        document.querySelectorAll('.tab').forEach(function (t) { t.classList.remove('active'); });
        el.classList.add('active');
        document.querySelectorAll('.pkg-card').forEach(function (card) {
            if (cat === 'all') {
                card.style.display = '';
            } else {
                card.style.display = (card.dataset.cat || '').indexOf(cat) !== -1 ? '' : 'none';
            }
        });
    };


    /* ── SMOOTH SCROLL for anchor links ── */
    document.querySelectorAll('a[href^="#"]').forEach(function (a) {
        a.addEventListener('click', function (e) {
            var id = a.getAttribute('href').slice(1);
            if (!id) return;
            var target = document.getElementById(id);
            if (target) {
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth' });
                closeMenu();
            }
        });
    });

})();
