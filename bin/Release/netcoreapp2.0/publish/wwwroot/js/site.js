$(function(){
    var burger = document.querySelector('.burger');
    var menu = document.querySelector('#'+burger.dataset.target);
    burger.addEventListener('click', function() {
        burger.classList.toggle('is-active');
        menu.classList.toggle('is-active');
    });
    $(".remover-paciente").click(function(){
        if (confirm('Deseja remover o paciente?')) {
            return true;
        } else {
            return false;
        }
    });

    $(".remover-medico").click(function(){
        if (confirm('Deseja remover o médico?')) {
            return true;
        } else {
            return false;
        }
    });
    $(".remover-usuario").click(function(){
        if (confirm('Deseja remover o usuário?')) {
            return true;
        } else {
            return false;
        }
    });
    $(".remover-agendamento").click(function(){
        if (confirm('Deseja remover o agendamento?')) {
            return true;
        } else {
            return false;
        }
    });
});