$(document).ready(function () {
    $("input[name='key']").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#StaffTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("a").click(function () {
        $.ajax({
            type: "Post",
            url: "/staff/delete",
            data: { maNHanVien: maNHanVien},
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) {

                }
            },
            error: function (req, status, error) {
                alert(error);

            }
     });
 });
  
