$(document).ready(function () {
    for (var i = parseInt($("#pageNumber").val()); i >=0; i--) {
        var li = "<li class='page-item' id='p-" + (i + 1).toString() + "'><button class='page-link'>" + (i + 1).toString() + "</button></li>";
        $("#startList").after(li);
    }
    var currentPage = $("#currentPage").val();
    var value = parseInt(currentPage.substring(2)) - 1;
    if(parseInt($("#pageNumber").val()) ==0){
        $("#startList").addClass("disabled");
        $("#endList").addClass("disabled");
    }
    else if(value == 0) {
        $("#startList").addClass("disabled");
    }
    else if(value == parseInt($("#pageNumber").val())){
        $("#endList").addClass("disabled");
    }
    $.ajax({
        type: "Post",
        url: "/staff/GetPage",
        data: { pageIndex: parseInt(value) },
        dataType: "text",
        success: function (data) {
            $("#StaffTable").html(data);
        },
        error: function (req, status, error) {
            console.log(error);

        }
    });
    
    $("#" + currentPage + " button").addClass("bg-primary text-white active");
    
    $("li button").click(function () {
        $("#" + currentPage + " button").removeClass("bg-primary text-white active");
        currentPage = $(this).parent().attr("id");
        $(this).addClass("bg-primary text-white active");
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
            url: "/staff/GetPage",
            data: { pageIndex: parseInt(pageIndex) },
            dataType: "text",
            success: function (data) {
                $("#StaffTable").html(data);
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });


    });
    
    $("#startList a").click(function (event) {

        $("#" + currentPage + " button").removeClass("bg-primary text-white active");
        currentPage = currentPage.substring(0, 2) + (parseInt(currentPage.substring(2)) -1).toString();
        $("#" + currentPage + " button").addClass("bg-primary text-white active");
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
            url: "/staff/GetPage",
            data: { pageIndex: parseInt(pageIndex) },
            dataType: "text",
            success: function (data) {
                $("#StaffTable").html(data);
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });

    });
    $("#endList a").click(function (event) {

        $("#" + currentPage + " button").removeClass("bg-primary text-white active");
        currentPage = currentPage.substring(0, 2) + (parseInt(currentPage.substring(2)) + 1).toString();
        $("#" + currentPage + " button").addClass("bg-primary text-white active");
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
            url: "/staff/GetPage",
            data: { pageIndex: parseInt(pageIndex) },
            dataType: "text",
            success: function (data) {
                $("#StaffTable").html(data);
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });

    });
});


  
