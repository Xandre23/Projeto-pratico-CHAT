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
        success: function (response) {
            if (response.code != 200) {
                alert(response.message);
            } else {
                alert("Cadastro realizado com sucesso!");
            }
        },
        error: function () {
            alert('error');
        }
    });
});

$("#btn-login").click(function () {

    const parameters = {
        email: $("#loginEmail").val(),
        password: $("#loginPassword").val()        
    }

    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: $.param(parameters),
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.code != 200) {
                alert(response.message);
            } else {
                window.location.href = "/Talk";
            }
        },
        error: function () {
            alert("error");
        }
    });
});




