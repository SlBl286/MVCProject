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
                    $("#CreateForm").unbind(event)
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
    
});
