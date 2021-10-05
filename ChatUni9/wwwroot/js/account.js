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
    //console.log(json.stringify(user));

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

    //$.ajax({
    //    type: "post",
    //    url: "/account/create",
    //    contenttype: "application/json",
    //    traditional: true,
    //    async: false,
    //    cache: false,
    //    data:json.stringify(user),
    //    context: document.body,
    //    success: function (result) {
    //        alert("foi");

    //    },
    //    error: function (xmlhttprequest, textstatus, errorthrown) {
    //        console.error('erro >>>' + errorthrown + '>> ' + xmlhttprequest.responsetext + '>>>' + textstatus);
    //      alert("erro")
    //    },
    //});
    //var person = {};
    //person.name = 2;
    //$.ajax({
    //    type: "post",
    //    url: "/account/create",
    //    data:  person,
    //    success: function (response) {
    //        alert("hello: " + response.name + ".\ncurrent date and time: " + response.datetime);
    //    },
    //    failure: function (response) {
    //        alert(response.responsetext);
    //    },
    //    error: function (response) {
    //        alert(response.responsetext);
    //    }
    //});

    //$.ajax({
    //    type: "post",
    //    url: "/account/create",
    //    contenttype: "application/json; charset=utf-8",
    //    data: json.stringify({ user: user }),
    //    data: '{person: ' + json.stringify(person) + '}',
    //    success: function () { alert('success'); },
    //    error: function () {
    //        alert("erro")
    //    }
    //});


    //var mydata = [
    //    {
    //        id: "a",
    //        name: "name 1"
    //    },
    //    {
    //        id: "b",
    //        name: "name 2"
    //    }
    //];

    //$.ajax({
    //    type: 'post',
    //    url: "/account/create",
    //    data: { user: mydata },
    //    datatype: 'json',
    //    error: function (err) {
    //        alert("error - " + err);
    //    }
    //});
});