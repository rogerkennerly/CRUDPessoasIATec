// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".addPhoneInput").on("click", function (event) {
    event.preventDefault();
    var teste = $(".phone").last().val();
    var controlNumber = $(".phone").length;
    
    if (teste) {
        $(".returnPhone").append('<input name="Telefone['+controlNumber+']" class="form-control phone" onkeydown="checkPhoneInputIfNumber(event)" type="tel" id="Telefone" value="">');
    }
    else {
        alert("Preencha o campo de telefone vazio!");
    }
});


function checkPhoneInputIfNumber(event)
{
    console.log(event.keyCode);
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39) {//excecao para backspace, delete, arrow right e arrow left

    }
    else if (String.fromCharCode(event.keyCode).match(/[^0-9]/g)) {
        event.preventDefault();
        return false;
    }
    else return false;
}
