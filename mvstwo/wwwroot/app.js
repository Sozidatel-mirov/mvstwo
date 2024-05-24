function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}
        // Получаем модальное окно
        var modalr = document.getElementById('openModalr');
        // Получаем меню
        var sidenav = document.getElementById('mySidenav');
        
        // Закрываем модальное окно при клике вне него
        window.onclick = function(event) {
            if (event.target == modalr) {
                modalr.style.display = "none";
            }
            //закрываем меню
            if (event.target == sidenav) {
                sidenav.style.width = "0";
            }
          }
        function show_hide_password(target){
  var input = document.getElementById('password-input');
  if (input.getAttribute('type') == 'password') {
    target.classList.add('view');
    input.setAttribute('type', 'text');
  } else {
    target.classList.remove('view');
    input.setAttribute('type', 'password');
  }
  return false;
}

