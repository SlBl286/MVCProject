$(document).ready(function () {
    $("#HoTen").mouseout(function () {
        var maNHanVien = $("#MaNhanVien").val();
        var hoTen = $("#HoTen").val();
        var ngaySinh = $("#NgaySinh").val();
        var soDienThoai = $("#SoDienThoai").val();
        var diaChi = $("#DiaChi").val();
        var chucVu = $("#ChucVu").val();
        var soNamCongTac = $("#SoNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/EditValidate",
            data: { maNHanVien: maNHanVien, hoTen: hoTen, ngaySinh: ngaySinh, soDienThoai: soDienThoai, diaChi: diaChi, chucVu: chucVu, soNamCongTac: soNamCongTac },
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
                    $("#ngaySinh").css("border-color", "#eff1f3");
                }
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
    $("#NgaySinh").mouseout(function () {
        var maNHanVien = $("#MaNhanVien").val();
        var hoTen = $("#HoTen").val();
        var ngaySinh = $("#NgaySinh").val();
        var soDienThoai = $("#SoDienThoai").val();
        var diaChi = $("#DiaChi").val();
        var chucVu = $("#ChucVu").val();
        var soNamCongTac = $("#SoNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/EditValidate",
            data: { maNHanVien: maNHanVien, hoTen: hoTen, ngaySinh: ngaySinh, soDienThoai: soDienThoai, diaChi: diaChi, chucVu: chucVu, soNamCongTac: soNamCongTac },
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
