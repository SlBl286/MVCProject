$(document).ready(function () {
    $("#EditForm").submit(function (event) {
        event.preventDefault();
        var id = parseInt($("#id").val());
        var TenPhongBan = $("#TenPhongBan").val();
        var pageIndex = parseInt($(".active").parent().attr("id").substring(2) -1);
        $("#SubmitBtn").attr("hidden",true);
        $("#SavingtBtn").attr("hidden",false);
        setTimeout(function(){
                $.ajax({
                    type: "Post",
                    url: "/department/edit",
                    data: {id : id, TenPhongBan : TenPhongBan },
                    dataType: "json",
                    success: function (json) {
                        $.ajax({
                            type: "Post",
                            url: "/department/GetPage",
                            data: { pageIndex: pageIndex },
                            dataType: "text",
                            success: function (data) {
                                $(".close").trigger("click");
                                $("#departmentTable").html(data);
                            },
                            error: function (req, status, error) {
                                console.log(error);
        
                            }
                        });
                        var createCurrentPage = $(".active").parent().attr("id");
                            $("#pagenav").empty();    
                            $.ajax({
                                type: "Post",
                                url: "/department/pagenav",
                                data: {currentPage : createCurrentPage },
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
        },800)
        
    });
   
});
