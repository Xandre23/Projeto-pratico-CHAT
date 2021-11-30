$(document).ready(function () {
    $("#btn-add-contact").click(function () {
        
        $.ajax({
            type: "get",
            url: "/Contact/Search",
            datatype: "json",
            success: function (html) {
                $("#header-modal").html(html);
                $("#modal-add-contact").modal("toggle");
            },
            error: function () {
                alert('error');
            }
        });

    });
});
$(document).on('click', '#btn-search', function (event) {
    var filter = {
        name: $("#campo_busca").val()
        
    }

    $.ajax({
        method: "GET",
        url: "/Contact/Search",
        datatype: "json",
        data: filter,
        success: function (html) {
            $("#header-modal").html(html);
            $("#modal-add-contact").modal("toggle");
        }
    });
});

$(document).on('click', '.btn-add', function () {
    $(this).text("Solicitação Enviada!!");
    var user = {
        ID: $(this).data("userid")
    }

    $.ajax({
        method: "POST",
        url: "/Contact/SendSolitation",
        datatype: "json",
        data: user,
        success: function (html) {
            alert('Solicitação enviada com sucesso');
            $("#modal-add-contact").modal("toggle");

        }
    });
});

$(document).on('click', '#btn-requests', function (event) {
    
    $.ajax({
        method: "GET",
        url: "/Contact/ReceiveRequest",
        datatype: "json",
        success: function (html) {
            $("#header-modal").html(html);
            $("#modal-solici").modal("toggle");
        }
    });
});

$(document).on('click', '.btn-aceitar', function () {
    $(this).text("Solicitação Aceita!");
    var solicitation = {
        ID: $(this).data("solicitation")
    }


    $.ajax({
        method: "POST",
        url: "/Contact/Accept",
        datatype: "json",
        data: solicitation,
        success: function () { alert('Solicitação aceita!!'); },
        error: function () {
            alert('error');
        }
    });
});

$(document).on('click', '.btn-recusar', function () {
    $(this).text("Solicitação excluida!");
    var solicitation = {
        ID: $(this).data("solicitation")
    }
   

    $.ajax({
        method: "DELETE",
        url: "/Contact/Refuse",
        datatype: "json",
        data: solicitation,
        success: function () { alert('Solicitação Deletada!'); },
         error: function () {
            alert('error');

        }
    });
});

$(document).on('click', '#proc_teste', function (event) {
    var filter = {
        filter: $("#campo_nome").val()
    }
    if (!filter.filter) {
        return;   
    }
    $.ajax({
        method: "GET",
        url: "/Contact/Get",
        datatype: "json",
        data: filter,
        success: function (html) {
            $("#list-of-contacts").html(html);
        },
        error: function () {

            alert('error');
        }
    });
});
