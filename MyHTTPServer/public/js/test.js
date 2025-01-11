document.getElementById("spoilerButton").onclick = function() {
  TurnBlock(this);
  alert("test")
}
function TurnBlock(e){
  if(document.querySelector('div._description_1gamc_16').style.display === "flex"){
    HideBlock(e);
  }else{
    ShowBlock(e);
  }
}
function ShowBlock(e){
  e.textContent = "Скрыть"
  document.querySelector('div._description_1gamc_16').style.display = "flex";
}
function HideBlock(e) {
  e.textContent = "Показать больше"
  document.querySelector('div._description_1gamc_16').style.display = "none";
}
