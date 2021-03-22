$(document).ready(function () {
    
    $("#Edit").submit(function (event) {
        event.preventDefault();
        var maNHanVien = $("#maNhanVien").val();
        var hoTen = $("#hoTen").val();
        var ngaySinh = $("#ngaySinh").val();
        var soDienThoai = $("#soDienThoai").val();
        var diaChi = $("#diaChi").val();
        var chucVu = $("#chucVu").val();
        var soNamCongTac = $("#soNamCongTac").val();
        $.ajax({
            type: "Post",
            url: "/staff/EditValidate",
            data: { maNHanVien: maNHanVien, hoTen: hoTen, ngaySinh: ngaySinh, soDienThoai: soDienThoai, diaChi: diaChi, chucVu: chucVu, soNamCongTac: soNamCongTac },
            dataType: "json",
            success: function (json) {
                var a = json;
                if (a == true) $("#ErrorMgs").text("Nhân Viên Đã Tồn Tại");
                if (a == false) {
                    $("#ErrorMgs").text("");
                    $(this).unbind(event);
                    $('#SubmitBtn').trigger('click');
                }
            },
            error: function (req, status, error) {
                alert(error);
                
            }
        });
    });

});
