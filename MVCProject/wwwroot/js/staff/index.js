$(document).ready(function () {
    var value = "p-1";
    
    $.ajax({
        type: "Post",
        url: "/staff/pagenav",
        data: { currentPage : value },
        dataType: "text",
        success: function (data) {
            $("#pagenav").html(data);
        },
        error: function (req, status, error) {
            console.log(error);

        }
    });
    $("#SearchBox").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        if(value == ""){
            $("#pagenav").show();
            $(".active").trigger("click");
        }
        else{
            $("#pagenav").hide();  
            $.ajax({
                type: "Post",
                url: "/staff/search",
                data: { key : value },
                dataType: "text",
                success: function (data) {
                        $("#StaffTable").html(data);
                    
                },
                error: function (req, status, error) {
                    console.log(error);
    
                }
            });
        }
        
    });
    $("#excelExport").click(function(){
        $.ajax({
            type: "Post",
            url: "/staff/ExcelExport",
            dataType: "json",
            success: function (json) {
                    console.log(json);
                
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
});


  
