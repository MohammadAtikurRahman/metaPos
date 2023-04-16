// Initialize
$("#pnlForgotPassword").hide();

// Click on forgot password link
$("#btnForgotPassword").click(function() {
    $("#pnlForgotPassword").show();
    $("#pnlLogin").hide();
});

// Click on back to login section link
$("#btnBack").click(function() {
    $("#pnlLogin").show();
    $("#pnlForgotPassword").hide();
});

// Initialize Background Video Assets
var vid = document.getElementById("bgvid");
var pauseButton = document.querySelector("#loginSection button");

if (window.matchMedia('(prefers-reduced-motion)').matches) {
    vid.removeAttribute("autoplay");
    vid.pause();
    pauseButton.innerHTML = "Paused";
}


function vidFade() {
    vid.classList.add("stopfade");
}


vid.addEventListener('ended', function() {
    // only functional if "loop" is removed 
    vid.pause();
    // to capture IE10
    vidFade();
});


//pauseButton.addEventListener("click", function () {
//    vid.classList.toggle("stopfade");
//    if (vid.paused) {
//        vid.play();
//        pauseButton.innerHTML = "Pause";
//    } else {
//        vid.pause();
//        pauseButton.innerHTML = "Paused";
//    }
//});