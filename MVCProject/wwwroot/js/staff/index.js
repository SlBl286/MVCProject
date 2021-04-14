$(document).ready(function () {
    var value = "p-1";
    var PhongBan_Id = parseInt($("#PhongBanId").val());
    var option = "option[value ="+PhongBan_Id +"]";
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
            $("#ErrorMgsSearch").text("min phải nhỏ hơn max");
            $("#minRange").css("border-color", "red");
            $("#maxRange").css("border-color", "red");
            valid = false;
        }
        else{
            $("#ErrorMgsSearch").text("");
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
    $("#importExcelBtn").click(function(){
        var filepath = $("#excelfile").val();

        if (filepath == null || filepath.length <= 0) alert("Bạn chưa chọn Tập Tin")
        else if (filepath.substring(filepath.lastIndexOf('.') + 1, filepath.length) == "xlsx" || filepath.substring(filepath.lastIndexOf('.') + 1, filepath.length) == "xsl"){
           
            var options = {
                target:  '#divToUpdate',
                url:     '/staff/import',
                dataType: "text",
                success: function(data) {
                    $("#loadingModal").modal('show');
                    $("#loadingModal .modal-body").html(data);
                }
              };
            $("#excelFileForm").ajaxSubmit(options);
              setTimeout(function(){
                $.ajax({
                    type: "Post",
                    url: "/department/dsPhongBan",
                    dataType: "json",
                    success: function (json) {
                        console.log(json);
                        $("#chonPhongBan").empty();
                        $("#chonPhongBan").append('<option value="0" selected>Chọn Phòng Ban</option>');
                        $.each(json,function(key,value){
                                
                                $("#chonPhongBan").append('<option value="'+value.id+'">'+value.TenPhongBan+'</option>');
     
                        });
                        
                        
                        
                    },
                    error: function (req, status, error) {
                        console.log(error);
        
                    }
                });
              },1000);
            

        }    
        else alert("Tệp tin sai định dạng");
    });
    
});  
    
