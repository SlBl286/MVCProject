$(document).ready(function () {
    $("#CreateForm #HoTen").blur(function () {
        var MaNhanVien = $("#MaNhanVien").val();
        var HoTen = $("#HoTen").val();
        var NgaySinh = $("#NgaySinh").val();
        var SoDienThoai = $("#SoDienThoai").val();
        var DiaChi = $("#DiaChi").val();
        var ChucVu = $("#ChucVu").val();
        var SoNamCongTac = $("#SoNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/IsDuplicatedStaff",
            data: { MaNhanVien: MaNhanVien, HoTen: HoTen, NgaySinh: NgaySinh, SoDienThoai: SoDienThoai, DiaChi: DiaChi, ChucVu: ChucVu, SoNamCongTac: SoNamCongTac },
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) {
                    $("#SubmitBtn").prop("disabled", true);
                    $("#ErrorMgs").text("Nhân Viên Đã Tồn Tại");
                    $("#HoTen").css("border-color", "red");
                    $("#ngaySinh").css("border-color", "red");
                }
                else {
                    $("#SubmitBtn").prop('disabled', false);
                    $("#ErrorMgs").text("");
                    $("#HoTen").css("border-color", "#ced4da");
                    $("#NgaySinh").css("border-color", "#ced4da");
                }
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
    $("#CreateForm #NgaySinh").blur(function () {
        var MaNhanVien = $("#MaNhanVien").val();
        var HoTen = $("#HoTen").val();
        var NgaySinh = $("#NgaySinh").val();
        var SoDienThoai = $("#SoDienThoai").val();
        var DiaChi = $("#DiaChi").val();
        var ChucVu = $("#ChucVu").val();
        var SoNamCongTac = $("#SoNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/IsDuplicatedStaff",
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
                else {
                    $("#SubmitBtn").prop("disabled", false);
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
    $("#CreateForm").submit(function (event) {
        event.preventDefault();
        var SoDienThoai = "";
        var DiaChi = "";
        var MaNhanVien = $("#MaNhanVien").val();
        var HoTen = $("#HoTen").val();
        var NgaySinh = $("#NgaySinh").val();
        SoDienThoai = $("#SoDienThoai").val();
        DiaChi = $("#DiaChi").val();
        var ChucVu = $("#ChucVu").val();
        var SoNamCongTac = $("#SoNamCongTac").val();
        $("#SubmitBtn").attr("hidden",true);
        $("#SavingtBtn").attr("hidden",false);
        setTimeout(function(){
            if(MaNhanVien != null && HoTen != null && NgaySinh != null && ChucVu != null && SoNamCongTac != null  ){
                $.ajax({
                    type: "Post",
                    url: "/staff/create",
                    data: { MaNhanVien: MaNhanVien, HoTen: HoTen, NgaySinh: NgaySinh, SoDienThoai: SoDienThoai, DiaChi: DiaChi, ChucVu: ChucVu, SoNamCongTac: SoNamCongTac },
                    dataType: "json",
                    success: function (json) {
                        var pageIndex = json;
                        $.ajax({
                            type: "Post",
                            url: "/staff/getpage",
                            data: { pageIndex: pageIndex },
                            success: function (data) {
                                $(".close").trigger("click");
                                $("#p-" + (pageIndex +1).toString() +" button").trigger("click");
                                $("#StaffTable").html(data);
    
                            },
                            error: function (req, status, error) {
                                console.log(error);
    
                            }
                        }); 
                        var createCurrentPage = $("#endList").prev().attr("id");
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
