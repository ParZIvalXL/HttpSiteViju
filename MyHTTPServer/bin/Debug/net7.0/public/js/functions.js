document.addEventListener("DOMContentLoaded", function() {
  initButtons();
})

function ScrollTo(element) {
  element.scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
}

function ScrollToTop() {
  document.body.scrollTo({top: 0, behavior: "smooth"});
}

function initButtons() {
  document.querySelectorAll('._slider_1py6y_1').forEach(container => {

    const cardWrapper = container.querySelector('._transition_1py6y_21');
    const leftBtn = container.querySelector('._buttonPrev_1py6y_33');
    const rightBtn = container.querySelector('._buttonNext_1py6y_34');
    const cards = container.querySelectorAll('._slide_1yb9p_1');

    let currentIndex = 0;

    /*
    * 250px 50px 100px
    * 0
    * 100 px (+100px) 0 + 100 <= 250
    * 200 px (+100px) 100 + 100 <= 250
    * 250 px (+ (250 - 200px) 200 + 100 > 250
    * */
    function updateTransform() {
      const cardWidth = cards[0].clientWidth;
      const totalWidth = cardWidth * cards.length;

      //alert(`cardWidth: ${cardWidth} totalWidth: ${totalWidth} x:${cards[0].width}`);
      cardWrapper.style.transform = `translateX(-${currentIndex * cardWidth}px)`;

      // Управляем видимостью кнопок
      leftBtn.style.display = currentIndex > 0 ? 'inline' : 'none';
      rightBtn.style.display = (currentIndex < cards.length - 1 && totalWidth > container.clientWidth) ? 'inline' : 'none';

      // Если ширина блока больше или равна ширине окна
      if (totalWidth <= container.clientWidth) {
        leftBtn.style.display = 'none';
        rightBtn.style.display = 'none';
      }
    }

    leftBtn.addEventListener('click', () => {
      if (currentIndex > 0) {
        currentIndex--;
        updateTransform();
      }
    });

    rightBtn.addEventListener('click', () => {
      if (currentIndex < cards.length - 1) {
        currentIndex++;
        updateTransform();
      }
    });

    // Инициализация видимости кнопок при загрузке
    updateTransform();
  });
}
