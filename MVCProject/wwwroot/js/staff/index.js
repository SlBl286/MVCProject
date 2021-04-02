$(document).ready(function () {
    var value = "p-1";
    var PhongBan_Id = $("PhongBanID").val();
    $("option").removeAttr('selected')
    $('option[value ="'+PhongBan_Id+'"]').prop('selected', true)
    $.ajax({
        type: "Post",
        url: "/staff/_table",
        data: {pageNumber: parseInt($("#pageNumberIndex").val()), currentPage : value },
        dataType: "text",
        success: function (data) {
            $("#tablePartial").html(data);
        },
        error: function (req, status, error) {
            console.log(error);

        }
    });
    $("#SearchBox").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $.ajax({
            type: "Post",
            url: "/staff/search",
            data: { key : value },
            dataType: "text",
            success: function (data) {
                $("#tablePartial").html(data);
                
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
        
    });
    $("#excelExport").click(function(){
        $.ajax({
            type: "Post",
            url: "/staff/ExcelExport",
            dataType: "json",
            success: function (json) {
                    console.log(json);
                
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
    $("#chonPhongBan").change(function(){
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
                
            },
            error: function (req, status, error) {
                console.log(error);

            }
        });
    });
});


  
