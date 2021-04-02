$(document).ready(function () {
    $("#CreateForm #TenPhongBan").blur(function () {
        var TenPhongBan = $("#TenPhongBan").val();
        $.ajax({
            type: "Post",
            url: "/department/IsDuplicated",
            data: { TenPhongBan:TenPhongBan },
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) {
                    $("#SubmitBtn").prop("disabled", true);
                    $("#ErrorMgs").text("Phòng Ban Đã Tồn Tại");
                    $("#TenPhongBan").css("border-color", "red");
                }
                else {
                    $("#SubmitBtn").prop('disabled', false);
                    $("#ErrorMgs").text("");
                    $("#TenPhongBan").css("border-color", "#ced4da");
                }
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
    
    $("#SubmitBtn").click(function (event) {
        event.preventDefault();
        var TenPhongBan = $("#TenPhongBan").val();
        console.log(TenPhongBan);
        $("#SubmitBtn").attr("hidden",true);
        $("#SavingtBtn").attr("hidden",false);
        setTimeout(function(){
            if(TenPhongBan != null  ){
                $.ajax({
                    type: "Post",
                    url: "/department/create",
                    data: { TenPhongBan: TenPhongBan},
                    dataType: "json",
                    success: function (json) {
                        var pageIndex = json;
                        $.ajax({
                            type: "Post",
                            url: "/department/getpage",
                            data: { pageIndex: pageIndex},
                            success: function (data) {
                                $(".close").trigger("click");
                                $("#p-" + (pageIndex +1).toString() +" button").trigger("click");
                                $("#departmentTable").html(data);
                            },
                            error: function (req, status, error) {
                                console.log(error);
    
                            }
                        }); 
                        var createCurrentPage = $("#endList").prev().attr("id");
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
            }
        },800)
  
    });
});
