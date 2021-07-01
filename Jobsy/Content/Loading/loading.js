          
var loader = document.querySelector(".preloader")
var logo = document.querySelector(".logo")
var background = document.querySelector(".background")

//window.addEventListener("load", vanish);

setTimeout(function vanish() {
    loader.classList.add("disppear");
    logo.classList.add("disppear");
    background.classList.add("disppear");
}, 2000);

/*function vanish() {
    loader.classList.add("disppear");
    logo.classList.add("disppear");
    background.classList.add("disppear");
}*/

