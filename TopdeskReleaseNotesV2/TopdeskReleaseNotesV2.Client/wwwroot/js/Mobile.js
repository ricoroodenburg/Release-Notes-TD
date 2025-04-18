function isMobile() {
    return /android|webos|iphone|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

function isAppleWebKit() {

    // Define variable
    const ua = navigator.userAgent;

    const isSafari = ua.includes("Safari") &&
        !ua.includes("Chrome") &&
        !ua.includes("Chromium") &&
        !ua.includes("CriOS") &&
        !ua.includes("Edg"); // ook Edge kan Safari bevatten

    return isSafari;

}