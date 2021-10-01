$("#btn-create").click(function () {
    var user = {
        Nome : $("#inputNome").val(),
        Sobrenome : $("#inputSobrenome").val(),
        Email : $("#inputEmail").val(),
        Senha : $("#inputSenha").val(),
        Sexo : $("#inputSexo").val(),
        TokenGoogle : "",
        TokenFacebook : "" 
    }
    //console.log(JSON.stringify(user));
 
    $.ajax({
        type: "POST",
        url: "/Account/Create",
        data: user,
        dataType: "json",
        success: function () { alert('Success'); },
        error: function () {
            alert("error");
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: "/Account/Create",
    //    contentType: "application/json",
    //    traditional: true,
    //    async: false,
    //    cache: false,
    //    data:JSON.stringify(user),
    //    context: document.body,
    //    success: function (result) {
    //        alert("foi");

    //    },
    //    error: function (XMLHttpRequest, textStatus, errorThrown) {
    //        console.error('erro >>>' + errorThrown + '>> ' + XMLHttpRequest.responseText + '>>>' + textStatus);
    //      alert("erro")
    //    },
    //});
    //var person = {};
    //person.Name = 2;
    //$.ajax({
    //    type: "POST",
    //    url: "/Account/Create",
    //    data:  person,
    //    success: function (response) {
    //        alert("Hello: " + response.Name + ".\nCurrent Date and Time: " + response.DateTime);
    //    },
    //    failure: function (response) {
    //        alert(response.responseText);
    //    },
    //    error: function (response) {
    //        alert(response.responseText);
    //    }
    //});

    //$.ajax({
    //    type: "POST",
    //    url: "/Account/Create",
    //    contentType: "application/json; charset=utf-8",
    //    data: JSON.stringify({ user: user }),
    //    data: '{person: ' + JSON.stringify(person) + '}',
    //    success: function () { alert('Success'); },
    //    error: function () {
    //        alert("erro")
    //    }
    //});


    //var myData = [
    //    {
    //        id: "a",
    //        name: "Name 1"
    //    },
    //    {
    //        id: "b",
    //        name: "Name 2"
    //    }
    //];

    //$.ajax({
    //    type: 'POST',
    //    url: "/Account/Create",
    //    data: { user: myData },
    //    dataType: 'json',
    //    error: function (err) {
    //        alert("error - " + err);
    //    }
    //});
}); 


$("#btn-login").click(function (){
    const parameters = {
        email : $("#inputEmail").val(),
        password : $("#inputPassword").val()
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