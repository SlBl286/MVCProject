$(document).ready(function () {
    $("input[name='key']").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#StaffTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $(".delBtn").click(function (event) {
        
        var MaNhanVien = $(this).attr("id");
        
        $.ajax({
            type: "Post",
            url: "/staff/delete",
            data: { MaNhanVien: MaNhanVien},
            dataType: "json",
            success: function (json) {
                $("#Huy-" + MaNhanVien).trigger("click");
                $("#" + MaNhanVien).closest("tr").remove();
            },
            error: function (req, status, error) {
                console.log(error);
                
            }
        });
        
    });
   
});


  
