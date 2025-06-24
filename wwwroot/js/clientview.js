// wwwroot/js/clientview.js

document.addEventListener("DOMContentLoaded", function () {
    const routes = [
        "/Publish/ClientTreasuries",
        "/Publish/ClientForex",
        "/Publish/ClientBonds"
    ];

    const currentPath = window.location.pathname;
    const currentIndex = routes.indexOf(currentPath);

    // Default to first route if current page not in list
    let nextIndex = (currentIndex + 1) % routes.length;
    let nextRoute = routes[nextIndex];

    setTimeout(function () {
        // Optional fade-out effect
        document.body.style.transition = "opacity 1s";
        document.body.style.opacity = 0;

        setTimeout(function () {
            window.location.href = nextRoute;
        }, 1000); // Redirect after fade
    }, 59000); // Start fade at 59s
});
