let loadedOrder = 0;

document.addEventListener('DOMContentLoaded', async function () {
  {
    let count = document.getElementsByClassName("_mainBlock_1qh4q_41")
    loadedOrder = count.length;
  }
  sendLoadCategoryAjaxRequest(loadedOrder).then( (res) => {
    sendLoadCategoryAjaxRequest(loadedOrder).then((res) =>{

    });
    }
  );
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

/*window.scroll(function(){
  if  (window.scrollTop() === document.height() - window.height()){
    sendLoadCategoryAjaxRequest(order).then(r => {});
  }
});*/


