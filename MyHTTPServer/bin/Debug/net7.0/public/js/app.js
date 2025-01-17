let loadedOrder = 0;

document.addEventListener('DOMContentLoaded', async function () {
  {
    let count = document.getElementsByClassName("_mainBlock_1qh4q_41")
    loadedOrder = count.length;
  }
  initButtons();
  document.documentElement.style = `--app-width: ${window.innerWidth}px; --app-height: ${window.innerHeight}px;`;
  sendLoadCategoryAjaxRequest(loadedOrder).then( (res) => {
    sendLoadCategoryAjaxRequest(loadedOrder).then((res) =>{

    });
    }
  );
});

window.addEventListener("resize", function () {
  document.documentElement.style = `--app-width: ${window.innerWidth}px; --app-height: ${window.innerHeight}px;`;
});
//alert("Height: " + window.innerHeight + " ScrollY: " + window.scrollY + " DocumentHeight: " + document.body.offsetHeight);
window.addEventListener('scroll', function () {
  alert("Height: " + window.innerHeight + " ScrollY: " + window.scrollY + " DocumentHeight: " + document.body.offsetHeight);

  if (window.innerHeight + window.scrollY >= document.body.offsetHeight) {
    console.log("Reached bottom!");
    sendLoadCategoryAjaxRequest(loadedOrder).then((res) => {
      console.log("Response:", res);
    }).catch(err => {
      console.error("Error:", err);
    });
  }
}, false);

document.addEventListener('scroll', function () {
  alert("Height: " + window.innerHeight + " ScrollY: " + window.scrollY + " DocumentHeight: " + document.body.offsetHeight);

  if (window.innerHeight + window.scrollY >= document.body.offsetHeight) {
    console.log("Reached bottom!");
    sendLoadCategoryAjaxRequest(loadedOrder).then((res) => {
      console.log("Response:", res);
    }).catch(err => {
      console.error("Error:", err);
    });
  }
}, false);

let isAjaxCalled= false;

async function sendLoadCategoryAjaxRequest(order) {
  return new Promise(function (resolve, reject) {
    let xhr = new XMLHttpRequest();
    // Используем метод GET для запроса
    xhr.open('GET', 'filmCategory?order=' + encodeURIComponent(loadedOrder), true);
    xhr.onreadystatechange = function() {
      if (xhr.readyState === 4 && xhr.status === 200) {
        let response = xhr.responseText;
        document.querySelector('article._mainBlock_1qh4q_41').insertAdjacentHTML('afterend', response);
        loadedOrder++;
        isAjaxCalled = false;
        initButtons();
        return resolve(response);
      }else if(xhr.status !== 200){
        document.getElementsByClassName("_loader_1slcp_1").item(0).style.display = "none";
        let response = xhr.status;
        return reject(response);
      }
    };
    xhr.send();
  })

}
function initButtons() {
  document.querySelectorAll('._mainBlock_1qh4q_41').forEach(container => {
    const cardWrapper = container.querySelector('._transition_1py6y_21');
    const leftBtn = container.querySelector('._buttonPrev_1py6y_33');
    const rightBtn = container.querySelector('._buttonNext_1py6y_34');
    const cards = container.querySelectorAll('._slide_1yb9p_1');

    let currentIndex = 0;

    function updateTransform() {
      const cardWidth = cards[0].clientWidth;
      const totalWidth = cardWidth * cards.length;

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

/*window.scroll(function(){
  if  (window.scrollTop() === document.height() - window.height()){
    sendLoadCategoryAjaxRequest(order).then(r => {});
  }
});*/


