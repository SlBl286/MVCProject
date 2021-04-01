$(document).ready(function () {
    var value = "p-1";
    
    $.ajax({
        type: "Post",
        url: "/department/pagenav",
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
                url: "/department/search",
                data: { key : value },
                dataType: "text",
                success: function (data) {
                        $("#departmentTable").html(data);
                    
                },
                error: function (req, status, error) {
                    console.log(error);
    
                }
            });
        }
        
    });
});


  
