function ScrollTo(element) {
  element.scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
}

function ScrollToTop() {
  alert("ScrollToTop");
  $(window).scrollTo({top: 0, behavior: "smooth"});
}
