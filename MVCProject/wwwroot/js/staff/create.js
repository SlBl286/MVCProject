$(document).ready(function () {
    $("input[name='addBtn']").click(function () {
        var text = '[{"maNhanVien": "NV-0001","hoTen": "Nguyen Van A","ngaySinh": "2000-05-28T00:00:00","soDienThoai": "0971586931", "diaChi": "Ha Noi","chucVu": "Nhan Vien","soNamCongTac": 2},{"maNhanVien": "NV-0003","hoTen": "Nguyen Van C","ngaySinh": "2000-05-28T00:00:00","soDienThoai": "0971586931","diaChi": "Ha Noi","chucVu": "Nhan Vien","soNamCongTac": 2}]';
;
        var obj[] = JSON.parse(text);
        alert(obj[0].maNhanVien);
    });

});
