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

