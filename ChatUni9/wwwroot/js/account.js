$("#btn-create").click(function () {
    var user = {
        Nome: $("#inputNome").val(),
        Sobrenome: $("#inputSobrenome").val(),
        Email: $("#inputEmail").val(),
        Senha: $("#inputPassword").val(),
        Sexo: $("#inputSexo option:selected").val(),
        tokengoogle: "",
        tokenfacebook: ""
    }

    $.ajax({
        type: "post",
        url: "/account/create",
        data: user,
        datatype: "json",
        success: function () { alert('success'); },
        error: function () {
            alert('error');
        }
    });
});

$("#btn-login").click(function () {
    const parameters = {
        email: $("#inputEmail").val(),
        password: $("#inputPassword").val()
    }

    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: $.param(parameters),
        dataType: "json",
        success: function () { alert('Success'); },
        error: function () {
            alert("error");
        }
    });
}); 