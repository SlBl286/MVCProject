$(document).ready(function () {
    for (var i = parseInt($("#pageNumber").val()); i >=0; i--) {
        var li = "<li class='page-item' id='p-" + (i + 1).toString() + "'><button class='page-link'>" + (i + 1).toString() + "</button></li>";
        $("#startList").after(li);
    }
    $("button").click(function () {
        var a = parseInt($(this).parent().attr("id").substring(2));
        $(this).addClass("bg-primary text-white");

    });
    
    
    $("#SearchBox").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $.ajax({
            type: "Post",
            url: "/staff/search",
            data: { key : value },
            dataType: "text",
            success: function (data) {
                console.log(data)
                $("#StaffTable").html(data);
            },
            error: function (req, status, error) {
                console.log(error);

            }
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


  
