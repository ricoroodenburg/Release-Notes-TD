function isMobile() {
    return /android|webos|iphone|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

function isAppleWebKit() {
    return /^((?!chrome|android).)*safari/i.test(navigator.userAgent);
}