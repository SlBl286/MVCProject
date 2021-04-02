$(document).ready(function(){
    $("#delbtn1").click(function () {
       setTimeout(function(){
        $(".modal-backdrop").remove();  
        $(".modal-backdrop").remove();  
       },200); 
    });
    $(".delBtn").click(function () {
        
        var MaPhongBan = parseInt($(this).attr("id"));
        $("#NV-" + MaPhongBan).modal('toggle');
        $(".modal-backdrop").remove();  
        console.log(MaPhongBan);
        var pageIndex = parseInt($(".active").parent().attr("id").substring(2));
        $.ajax({
            type: "Post",
            url: "/department/delete",
            data: { id: MaPhongBan},
            dataType: "json",
            success: function (json) {
                var delCurrentPage = "p-" + pageIndex.toString();
                $("#" + MaPhongBan).closest("tr").remove();
                $("#pagenav").empty();    
                $.ajax({
                    type: "Post",
                    url: "/department/pagenav",
                    data: {currentPage : delCurrentPage },
                    dataType: "text",
                    success: function (data) {
                        $("#pagenav").html(data);
                    },
                    error: function (req, status, error) {
                        console.log(error);
            
                    }
                });

            },
            error: function (req, status, error) {
                console.log(error);
    
            }
        });
        
    
    });
    
       
});