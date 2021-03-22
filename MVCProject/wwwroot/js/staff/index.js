$(document).ready(function () {
    $("input[name='key']").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#StaffTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $(".delBtn").click(function () {
        var maNhanVien = $(this).text();
        $.ajax({
            type: "Post",
            url: "/staff/delete",
            data: { maNhanVien: maNhanVien},
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) alert(maNhanVien);
            },
            error: function (req, status, error) {
                alert(error);
                
            }
        });
        
    });
 });
  
