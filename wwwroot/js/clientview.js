document.addEventListener("DOMContentLoaded", function () {
    const pages = [
        "/Publish/ClientTreasuries",
        "/Publish/ClientForex",
        "/Publish/ClientBonds"
    ];

    let currentIndex = pages.findIndex(p => window.location.pathname.toLowerCase().includes(p.toLowerCase()));
    if (currentIndex === -1) currentIndex = 0;

    const progressBar = document.getElementById("progressBar");

    let timePassed = 0;
    const duration = 60; // seconds

    function updateProgressBar() {
        timePassed++;
        const percent = (timePassed / duration) * 100;
        if (progressBar) progressBar.style.width = percent + "%";

        if (timePassed >= duration) {
            const nextIndex = (currentIndex + 1) % pages.length;
            window.location.href = pages[nextIndex];
        }
    }

    setInterval(updateProgressBar, 1000);
});
