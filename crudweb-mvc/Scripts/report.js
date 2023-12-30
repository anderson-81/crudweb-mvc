$(document).ready(function () {

    function Clear() {
        $("#divOpcSalRanger").removeClass("divOpcClick").addClass("divOpc");
        $("#divOpcAVGWage").removeClass("divOpcClick").addClass("divOpc");
        $("#divOpcSalary").removeClass("divOpcClick").addClass("divOpc");
        $("#divOpcBornMonth").removeClass("divOpcClick").addClass("divOpc");
        $("#divOpcAll").removeClass("divOpcClick").addClass("divOpc");
        $("#divOpcGender").removeClass("divOpcClick").addClass("divOpc");
        $("InitialSal").val("");
        $("FinalSal").val("");
    }

    function Hide() {
        $("#divSalRanger").hide();
        $("#divAVGWage").hide();
        $("#divSalary").hide();
        $("#divBornMonth").hide();
        $("#divAll").hide();
        $("#divGender").hide();
    }

    Clear();

    Hide();

    $("#divOpcSalRanger").addClass("divOpcClick");

    $("#divSalRanger").fadeIn("slow", function () {
    });

    $("#opt").val(1);

    $("#divOpcSalRanger").click(function () {

        Hide();

        Clear();

        $(this).addClass("divOpcClick");

        $("#divSalRanger").fadeIn("slow");

        $("#opt").val(1);
    });

    $("#divOpcAVGWage").click(function () {

        Hide();

        Clear();

        $(this).addClass("divOpcClick");

        $("#divAVGWage").fadeIn("slow");

        $("#opt").val(3);
    });

    $("#divOpcSalary").click(function () {

        Hide();

        Clear();

        $(this).addClass("divOpcClick");

        $("#divSalary").fadeIn("slow");

        $("#opt").val(2);
    });

    $("#divOpcBornMonth").click(function () {

        Hide();

        Clear();

        $(this).addClass("divOpcClick");

        $("#divBornMonth").fadeIn("slow");

        $("#opt").val(4);
    });

    $("#divOpcAll").click(function () {

        Hide();

        Clear();

        $(this).addClass("divOpcClick");

        $("#divAll").fadeIn("slow");

        $("#opt").val(6);
    });

    $("#divOpcGender").click(function () {

        Hide();

        Clear();

        $(this).addClass("divOpcClick");

        $("#divOpcGender").fadeIn("slow");

        $("#opt").val(5);
    });
});


$(function () {
    $('#InitialSal').maskMoney({ thousands: '', decimal: '.' });
})

$(function () {
    $('#FinalSal').maskMoney({ thousands: '', decimal: '.' });
})


