$(document).ready(function () {
    var currentPage = "p-1";
    var value = parseInt(currentPage.substring(2)) - 1;
    $("#startList").addClass("disabled");
    for (var i = parseInt($("#pageNumber").val()); i >=0; i--) {
        var li = "<li class='page-item' id='p-" + (i + 1).toString() + "'><button class='page-link'>" + (i + 1).toString() + "</button></li>";
        $("#startList").after(li);
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
    $("#EditForm").submit(function (event) {
        event.preventDefault();
        var MaNhanVien = $("#MaNhanVien").val();
        var HoTen = $("#HoTen").val();
        var NgaySinh = $("#NgaySinh").val();
        var SoDienThoai = $("#SoDienThoai").val();
        var DiaChi = $("#DiaChi").val();
        var ChucVu = $("#ChucVu").val();
        var SoNamCongTac = $("#SoNamCongTac").val();
        var pageIndex = parseInt($(".active").text().substring(2));
        $.ajax({
            type: "Post",
            url: "/staff/edit",
            data: { MaNhanVien: MaNhanVien, HoTen: HoTen, NgaySinh: NgaySinh, SoDienThoai: SoDienThoai, DiaChi: DiaChi, ChucVu: ChucVu, SoNamCongTac: SoNamCongTac, pageIndex: pageIndex },
            dataType: "json",
            success: function (json) {
                var pageIndex2 = json;
                $.ajax({
                    type: "Post",
                    url: "/staff/GetPage",
                    data: { pageIndex: parseInt(pageIndex2) },
                    dataType: "text",
                    success: function (data) {
                        console.log(pageIndex2);
                        $(".close").trigger("click");
                        $("#StaffTable").html(data);
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


  
