$(document).ready(function () {
    $("#frm-create-account").validate({
        rules: {
            inputNome: {
                required: true,
                maxlength: 20,
                minlength: 5

            },
            inputSobrenome: {
                required: true,
                maxlength: 80,
                minlength: 4
            },

            inputEmail: {
                required: true,
                email: true

            },
            inputPassword: {
                required: true

            }
        },
        messages: {
            inputNome: {
                required: 'Por favor, Insira seu nome',
                minlength: 'Nome deve ter no mínimo 5 caracteres',
                maxlength: 'Nome é muito grande'
            },
            inputSobrenome: {
                required: 'Por favor, Insira seu sobrenome.',
                minlength: 'Sobrenome deve ter no mínimo 10 caracteres',
                maxlength: 'Sobrenome é muito grande'
            },
            inputEmail: {
                required: 'Por favor, Insira seu email.',
                email: "Preencha com um E-mail válido",
            },
            inputPassword: {
                required: 'Por favor, Insira sua senha.'
            }
        },
        submitHandler: function () {
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
                        alert("Cadastro realizado com sucesso")
                        window.location.href = "/Account/Index";
                    }
                },
                error: function () {
                    alert('error');
                }
            });
        },
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




