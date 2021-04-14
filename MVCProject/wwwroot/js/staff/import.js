$(document).ready(function () {
        setTimeout(function(){
            $("#loadingIcon").attr("hidden",true);
            $("#doneIcon").attr("hidden",false);
            $("#notiNumberAddAndEdit").attr("hidden",false);
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
        },1500)
});
