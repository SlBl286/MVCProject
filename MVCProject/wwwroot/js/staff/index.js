$(document).ready(function () {
    var value = "p-1";
    var PhongBan_Id = $("#PhongBanID").val();
    console.log(PhongBan_Id);
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
    $('#advandSearch').on('submit',function(event) {
        event.preventDefault();
        var key = $("#SearchBox").val();
        var chucVu = $("#chucVuSearch").val();
        var min = $("#minRange").val();
        var max = $("#maxRange").val();
        var valid = false;
        if(parseInt(min) > parseInt(max)){
            $("#ErrorMgs").text("min phải nhỏ hơn max");
            $("#minRange").css("border-color", "red");
            $("#maxRange").css("border-color", "red");
            valid = false;
        }
        else{
            $("#ErrorMgs").text("");
            $("#minRange").css("border-color", "gray");
            $("#maxRange").css("border-color", "gray");
            valid = true;
        }
        if(valid == true){
            $.ajax({
                type: "Post",
                url: "/staff/advandSearch",
                data: {key:key,chucVu:chucVu,min: parseInt(min),max: parseInt(max) },
                dataType: "text",
                success: function (data) {
                    $("#tablePartial").html(data);
                },
                error: function (req, status, error) {
                    console.log(error);
        
                }
            });
        }
        
    });
    $("#chonPhongBan").change(function(){
        var PhongBanId = null;
        $("#chonPhongBan option:selected").each(function(){
            PhongBanId = parseInt($(this).val());
        });
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


  
