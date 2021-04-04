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
        var PhongBan_Id = parseInt($("#PhongBan_Id").val());
        var currentPage = $(".active").parent().attr("id");
        $("#SubmitBtn").attr("hidden",true);
        $("#SavingtBtn").attr("hidden",false);
        setTimeout(function(){
            if(MaNhanVien != null && HoTen != null && NgaySinh != null && ChucVu != null && SoNamCongTac != null  ){
                $.ajax({
                    type: "Post",
                    url: "/staff/edit",
                    data: { MaNhanVien: MaNhanVien, HoTen: HoTen, NgaySinh: NgaySinh, SoDienThoai: SoDienThoai, DiaChi: DiaChi, ChucVu: ChucVu, SoNamCongTac: SoNamCongTac,PhongBan_Id:PhongBan_Id },
                    dataType: "json",
                    success: function (json) {
                        $.ajax({
                            type: "Post",
                            url: "/staff/_table",
                            data: {pageNumber:parseInt($("#pageNumber").val()), currentPage : currentPage },
                            dataType: "text",
                            success: function (data) {
                                $(".close").trigger("click");
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
                                        $("#" +currentPage).children().trigger("click");
                                        
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
            }
        },800)
        
    });
   
});
