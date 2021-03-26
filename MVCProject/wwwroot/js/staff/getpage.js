$(".delBtn").click(function () {

    var MaNhanVien = $(this).attr("id");

    $.ajax({
        type: "Post",
        url: "/staff/delete",
        data: { MaNhanVien: MaNhanVien },
        dataType: "json",
        success: function (json) {
            $("#Huy-" + MaNhanVien).trigger("click");
            $("#" + MaNhanVien).closest("tr").remove();
        },
        error: function (req, status, error) {
            console.log(error);

        }
    });

});