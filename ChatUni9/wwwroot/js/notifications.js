function notifyUser(user) {
    const parameter = {
        userID: user
    };

    //let idContactOpend = 0;
    //if ($("#id-contact-opend").val()) {
    //    idContactOpend = parseInt($("#id-contact-opend").val());
    //}


    if (parseInt($("#id-contact-opend").val()) != parameter.userID) {
        $.ajax({
            type: "get",
            url: "/Account/Get",
            data: parameter,
            datatype: "json",
            success: function (user) {
                $.notify(`Nova mensagem de ${user.nome}`, "info");
            },
            error: function () {
                console.log("Erro ao criar mensagem")
            }
        });
    }  
}