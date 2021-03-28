$(document).ready(function () {
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
    
});


  
