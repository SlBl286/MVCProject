$(document).ready(function(){
    $(".delBtn").click(function () {
        var currentPage = parseInt($(".active").parent().attr("id").substring(2))-1;
        for (var i = parseInt($("#pageNumber").val()+1); i >=1; i--) {
            $("#p-"+i).remove();
        }
        var MaNhanVien = $(this).attr("id");
        
        $.ajax({
            type: "Post",
            url: "/staff/delete",
            data: { MaNhanVien: MaNhanVien },
            dataType: "json",
            success: function (json) {
                console.log(json);
                $("#NV-" + MaNhanVien).modal("hide");
                $("#" + MaNhanVien).closest("tr").remove();
                for (var i = parseInt($("#pageNumber").val()); i >=0; i--) {
                    var li = "<li class='page-item' id='p-" + (i + 1).toString() + "'><button class='page-link'>" + (i + 1).toString() + "</button></li>";
                    $("#startList").after(li);
                }
                console.log("#p-" + (currentPage +1).toString() +" button");
                $("#p-" + (currentPage +1).toString()).children().trigger("click");
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