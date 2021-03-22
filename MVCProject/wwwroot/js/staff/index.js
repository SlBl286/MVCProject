$(document).ready(function () {
    $("input[name='key']").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#StaffTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().includes(value) == true)
        });
    });

    $("a").click(function () {
        $(this).closest("tr").remove();

    });
});