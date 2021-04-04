$(document).ready(function () {   
    for (var i = parseInt($("#pageNumber").val()); i >=0; i--) {
        var li = "<li class='page-item' id='p-" + (i + 1).toString() + "'><button class='page-link'>" + (i + 1).toString() + "</button></li>";
        $("#startList").after(li);
    }
    var currentPage = $("#currentPage").val();
    var value = parseInt(currentPage.substring(2)) - 1;
    if(parseInt($("#pageNumber").val()) == 0){
        $("#startList").addClass("disabled");
        $("#endList").addClass("disabled");
    }
    else if(value == 0) {
        $("#startList").addClass("disabled");
    }
    else if(value == parseInt($("#pageNumber").val())){
        $("#endList").addClass("disabled");
    }
    
    $("#" + currentPage + " button").addClass("bg-primary text-white active");
    console.log( parseInt($("#pageNumber").val()), currentPage);
    $("li button").click(function () {
        $("#" + currentPage + " button").removeClass("bg-primary text-white active");
        currentPage = $(this).parent().attr("id");
        var pageIndex = parseInt(currentPage.substring(2)) - 1;
        if (pageIndex == 0) {
            $("#startList").addClass("disabled");
            $("#endList").removeClass("disabled");
        }
        else if (pageIndex == $("#pageNumber").val()) {
            $("#endList").addClass("disabled");
            $("#startList").removeClass("disabled");
        }
        else {
            $("#startList").removeClass("disabled");
            $("#endList").removeClass("disabled");
        }
        $.ajax({
            type: "Post",
            url: "/staff/_table",
            data: {pageNumber:parseInt($("#pageNumber").val()), currentPage:currentPage },
            dataType: "text",
            success: function (data) {
                $("#tablePartial").html(data);
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });


    });
    
    $("#startList a").click(function (event) {

        $("#" + currentPage + " button").removeClass("bg-primary text-white active");
        currentPage = currentPage.substring(0, 2) + (parseInt(currentPage.substring(2)) -1).toString();
        var pageIndex = parseInt(currentPage.substring(2)) - 1;
        if (pageIndex == 0) {
            $("#startList").addClass("disabled");
            $("#endList").removeClass("disabled");
        }
        else if (pageIndex == $("#pageNumber").val()) {
            $("#endList").addClass("disabled");
            $("#startList").removeClass("disabled");
        }
        else {
            $("#startList").removeClass("disabled");
            $("#endList").removeClass("disabled");
        }
        $.ajax({
            type: "Post",
            url: "/staff/_table",
            data: {pageNumber:parseInt($("#pageNumber").val()), currentPage:currentPage },
            dataType: "text",
            success: function (data) {
                $("#tablePartial").html(data);
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });

    });
    $("#endList a").click(function (event) {

        $("#" + currentPage + " button").removeClass("bg-primary text-white active");
        currentPage = currentPage.substring(0, 2) + (parseInt(currentPage.substring(2)) + 1).toString();
        var pageIndex = parseInt(currentPage.substring(2)) - 1;
        if (pageIndex == 0) {
            $("#startList").addClass("disabled");
            $("#endList").removeClass("disabled");
        }
        else if (pageIndex == $("#pageNumber").val()) {
            $("#endList").addClass("disabled");
            $("#startList").removeClass("disabled");
        }
        else {
            $("#startList").removeClass("disabled");
            $("#endList").removeClass("disabled");
        }
        $.ajax({
            type: "Post",
            url: "/staff/_table",
            data: {pageNumber:parseInt($("#pageNumber").val()) , currentPage:currentPage,},
            dataType: "text",
            success: function (data) {
                $("#tablePartial").html(data);
            },
            error: function (req, status, error) {
                console.log(error);
            }
        });

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
                 var delCurrentPage = "p-" + (pageIndex+1).toString();
                 $("#" + MaNhanVien).closest("tr").remove();  
                 $.ajax({
                     type: "Post",
                     url: "/staff/_table",
                     data: {pageNumber:parseInt($("#pageNumber").val()), currentPage : delCurrentPage},
                     dataType: "text",
                     success: function (data) {
                         $("#tablePartial").html(data);
                         var PhongBanId = null;
                                $("#chonPhongBan option:selected").each(function(){
                                    PhongBanId = parseInt($(this).val());
                                });
                                console.log(PhongBanId);
                                $.ajax({
                                    type: "Post",
                                    url: "/staff/DepartmentStaffList",
                                    data: {PhongBanId :PhongBanId},
                                    dataType: "text",
                                    success: function (data) {
                                        $("#tablePartial").html(data);
                                        
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
 
             },
             error: function (req, status, error) {
                 console.log(error);
     
             }
         });
         
     
     });
});
