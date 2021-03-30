$(document).ready(function () {
    $("#HoTen").blur(function () {
        var MaNhanVien = $("#MaNhanVien").val();
        var HoTen = $("#HoTen").val();
        var NgaySinh = $("#NgaySinh").val();
        var SoDienThoai = $("#SoDienThoai").val();
        var DiaChi = $("#DiaChi").val();
        var ChucVu = $("#ChucVu").val();
        var SoNamCongTac = $("#SoNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/EditValidate",
            data: { MaNhanVien: MaNhanVien, HoTen: HoTen, NgaySinh: NgaySinh, SoDienThoai: SoDienThoai, DiaChi: DiaChi, ChucVu: ChucVu, SoNamCongTac: SoNamCongTac },
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) {
                    $("#SubmitBtn").prop("disabled", true);
                    $("#ErrorMgs").text("Nhân Viên Đã Tồn Tại");
                    $("#HoTen").css("border-color", "red");
                    $("#NgaySinh").css("border-color", "red");
                }
                if (a == false) {
                    $("#SubmitBtn").prop('disabled', false);
                    $("#ErrorMgs").text("");
                    $("#HoTen").css("border-color", "#ced4da");
                    $("#NgaySinh").css("border-color", "#eff1f3");
                }
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
    $("#NgaySinh").blur(function () {
        var MaNhanVien = $("#MaNhanVien").val();
        var HoTen = $("#HoTen").val();
        var NgaySinh = $("#NgaySinh").val();
        var SoDienThoai = $("#SoDienThoai").val();
        var DiaChi = $("#DiaChi").val();
        var ChucVu = $("#ChucVu").val();
        var SoNamCongTac = $("#SoNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/EditValidate",
            data: { MaNhanVien: MaNhanVien, HoTen: HoTen, NgaySinh: NgaySinh, SoDienThoai: SoDienThoai, DiaChi: DiaChi, ChucVu: ChucVu, SoNamCongTac: SoNamCongTac },
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) {
                    $("#SubmitBtn").prop("disabled", true);
                    $("#ErrorMgs").text("Nhân Viên Đã Tồn Tại");
                    $("#HoTen").css("border-color", "red");
                    $("#NgaySinh").css("border-color", "red");
                }
                if (a == false) {
                    $("#SubmitBtn").prop('disabled', false);
                    $("#ErrorMgs").text("");
                    $("#HoTen").css("border-color", "ced4da");
                    $("#NgaySinh").css("border-color", "ced4da");
                }
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
        var pageIndex = parseInt($(".active").parent().attr("id").substring(2) -1);
        $("#SubmitBtn").attr("hidden",true);
        $("#SavingtBtn").attr("hidden",false);
        setTimeout(function(){
            if(MaNhanVien != null && HoTen != null && NgaySinh != null && ChucVu != null && SoNamCongTac != null  ){
                $.ajax({
                    type: "Post",
                    url: "/staff/edit",
                    data: { MaNhanVien: MaNhanVien, HoTen: HoTen, NgaySinh: NgaySinh, SoDienThoai: SoDienThoai, DiaChi: DiaChi, ChucVu: ChucVu, SoNamCongTac: SoNamCongTac },
                    dataType: "json",
                    success: function (json) {
                        $.ajax({
                            type: "Post",
                            url: "/staff/GetPage",
                            data: { pageIndex: pageIndex },
                            dataType: "text",
                            success: function (data) {
                                console.log(pageIndex);
                                $(".close").trigger("click");
                                $("#StaffTable").html(data);
                            },
                            error: function (req, status, error) {
                                console.log(error);
        
                            }
                        });
                        var createCurrentPage = $(".active").parent().attr("id");
                            $("#pagenav").empty();    
                            $.ajax({
                                type: "Post",
                                url: "/staff/pagenav",
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
