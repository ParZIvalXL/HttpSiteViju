let loadedOrder = 0;

document.addEventListener('DOMContentLoaded', async function () {
  {
    let count = document.getElementsByClassName("_mainBlock_1qh4q_41")
    loadedOrder = count.length;
  }
  sendAjaxRequest(loadedOrder).then( (res) => {
    sendAjaxRequest(loadedOrder).then((res) =>{
      window.addEventListener('scroll', () => {
        console.log("Height: ", window.innerHeight, " ScrollY: ", window.scrollY, " DocumentHeight: ", document.body.offsetHeight);

        if (window.innerHeight + window.scrollY >= document.body.offsetHeight) {
          console.log("Reached bottom!");
          sendAjaxRequest(loadedOrder).then((res) => {
            console.log("Response:", res);
          }).catch(err => {
            console.error("Error:", err);
          });
        }
      });
    });
    }
  );

});



let isAjaxCalled= false;

async function sendAjaxRequest(order) {
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

function ScrollTo(element) {
  element.scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
}

function ScrollToTop() {
  window.scrollTo({top: 0, behavior: "smooth"});
}

$(window).scroll(function(){
  if  ($(window).scrollTop() === $(document).height() - $(window).height()){
    sendAjaxRequest(order).then(r => {});
  }
});


