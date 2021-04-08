$(document).ready(function () {
    var value = "p-1";
    var PhongBan_Id = parseInt($("#PhongBanId").val());
    var option = "option[value ="+PhongBan_Id +"]";
    console.log(PhongBan_Id);
    $("option").removeAttr('selected')
    $(option).prop('selected', true)
    var PhongBanId = null;
        $("#chonPhongBan option:selected").each(function(){
            PhongBanId = parseInt($(this).val());
        });
        $.ajax({
            type: "Post",
            url: "/staff/DepartmentStaffList",
            data: {PhongBanId :$("#PhongBanId").val()},
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
        if(parseInt(value.length) >=2 || parseInt(value.length) == 0){       
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
        }
        
        
    });
    $('#advandSearch').submit(function(event) {
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
    $("#resetSearch").click(function (e) { 
        setTimeout(function(){
            $.ajax({
                type: "Post",
                url: "/staff/search",
                data: { key : null },
                dataType: "text",
                success: function (data) {
                    $("#tablePartial").html(data);
                    
                },
                error: function (req, status, error) {
                    console.log(error);
    
                }
            });
        },200);
    });
});
