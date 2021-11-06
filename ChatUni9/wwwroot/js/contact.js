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
