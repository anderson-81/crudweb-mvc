$("document").ready(function () {
    $("#lnklogoff").click(function () {
        $("#logoffModal").modal("show");
    });
});

$(".validation-summary-errors").removeClass("validation-summary-errors");
$(".input-validation-error").removeClass("input-validation-error").parent().addClass("has-error");