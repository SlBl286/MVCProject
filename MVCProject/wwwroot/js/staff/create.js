$(document).ready(function () {
    $("#hoTen").blur(function () {
        var maNHanVien = $("#maNhanVien").val();
        var hoTen = $("#hoTen").val();
        var ngaySinh = $("#ngaySinh").val();
        var soDienThoai = $("#soDienThoai").val();
        var diaChi = $("#diaChi").val();
        var chucVu = $("#chucVu").val();
        var soNamCongTac = $("#soNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/IsDuplicatedStaff",
            data: { maNHanVien: maNHanVien, hoTen: hoTen, ngaySinh: ngaySinh, soDienThoai: soDienThoai, diaChi: diaChi, chucVu: chucVu, soNamCongTac: soNamCongTac },
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) {
                    $("#SubmitBtn").prop("disabled", true);
                    $("#ErrorMgs").text("Nhân Viên Đã Tồn Tại");
                    $("#hoTen").css("border-color", "red");
                    $("#ngaySinh").css("border-color", "red");
                }
                if (a == false) {
                    $("#SubmitBtn").prop('disabled', false);
                    $("#ErrorMgs").text("");
                    $("#hoTen").css("border-color", "#ced4da");
                    $("#ngaySinh").css("border-color", "#eff1f3");
                }
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
    $("#ngaySinh").blur(function () {
        var maNHanVien = $("#maNhanVien").val();
        var hoTen = $("#hoTen").val();
        var ngaySinh = $("#ngaySinh").val();
        var soDienThoai = $("#soDienThoai").val();
        var diaChi = $("#diaChi").val();
        var chucVu = $("#chucVu").val();
        var soNamCongTac = $("#soNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/IsDuplicatedStaff",
            data: { maNHanVien: maNHanVien, hoTen: hoTen, ngaySinh: ngaySinh, soDienThoai: soDienThoai, diaChi: diaChi, chucVu: chucVu, soNamCongTac: soNamCongTac },
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) {
                    $("#SubmitBtn").prop("disabled", true);
                    $("#ErrorMgs").text("Nhân Viên Đã Tồn Tại");
                    $("#hoTen").css("border-color", "red");
                    $("#ngaySinh").css("border-color", "red");
                } 
                if (a == false) {
                    $("#SubmitBtn").prop('disabled', false);
                    $("#CreateForm").unbind(event)
                    $("#ErrorMgs").text("");
                    $("#hoTen").css("border-color", "ced4da");
                    $("#ngaySinh").css("border-color", "ced4da");
                }
            },
            error: function (req, status, error) {
                console.log(error);
                
            }
        });
    });

});
