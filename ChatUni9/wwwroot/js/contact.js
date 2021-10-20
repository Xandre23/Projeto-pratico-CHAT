$(document).ready(function () {
    $("#btn-add-contact").click(function () {
        $("#header-modal").load("/Contact/Search");
        $("#modal-add-contact").modal("toggle");
    });
});