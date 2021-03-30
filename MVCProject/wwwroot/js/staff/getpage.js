﻿$(document).ready(function(){
    $("#delbtn1").click(function () {
       setTimeout(function(){
        $(".modal-backdrop").remove();  
        $(".modal-backdrop").remove();  
       },200); 
    });
    $(".delBtn").click(function () {
        
        var MaNhanVien = $(this).attr("id");
        $("#NV-" + MaNhanVien).modal('toggle');
        $(".modal-backdrop").remove();  
        console.log(MaNhanVien);
        var pageIndex = parseInt($(".active").parent().attr("id").substring(2));
        $.ajax({
            type: "Post",
            url: "/staff/delete",
            data: { MaNhanVien: MaNhanVien},
            dataType: "json",
            success: function (json) {
                var delCurrentPage = "p-" + pageIndex.toString();
                $("#" + MaNhanVien).closest("tr").remove();
                $("#pagenav").empty();    
                $.ajax({
                    type: "Post",
                    url: "/staff/pagenav",
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