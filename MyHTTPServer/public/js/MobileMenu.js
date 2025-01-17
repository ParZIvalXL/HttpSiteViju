document.getElementsByClassName("_burgerMenuButton_zslx3_1")
  .item(0).addEventListener("click", OpenMobileMenu);
document.getElementsByClassName("_closeMenuButton_1ms0r_26")
  .item(0).addEventListener("click", CloseMobileMenu);
//document.getElementById("mobile-login-button").addEventListener("click", () => fetch("/login").then(() => {}).then(() => location.reload()));
document.addEventListener("DOMContentLoaded", function() {

})

function OpenMobileMenu() {
  let wrapper = document.getElementsByClassName("_menuMobileWrapper_1ssj7_145");
  if(wrapper.length > 0) {
    wrapper[0].style.display = "block";
    let menu = document.getElementsByClassName("_menuMobile_1ms0r_1");
    if(menu.length > 0) {
      menu[0].classList.add("_menuMobileOpened_1ms0r_22");
    }
  }
}

function CloseMobileMenu() {
  let wrapper = document.getElementsByClassName("_menuMobileWrapper_1ssj7_145");
  if(wrapper.length > 0) {
    wrapper[0].style.display = "none";
    let menu = document.getElementsByClassName("_menuMobile_1ms0r_1");
    if(menu.length > 0 && menu[0].classList.contains("_menuMobileOpened_1ms0r_22")) {
      menu[0].classList.remove("_menuMobileOpened_1ms0r_22");
    }
  }
}

