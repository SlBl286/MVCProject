// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
showInPopup = (url, title) => {
    $.ajax({
        type: "Get",
        url: url,
        success: function (res) {
            $("#CreateModal .modal-body").html(res);
            $("#CreateModal .modal-title").html(title);
            $("#CreateModal").modal('show');
        },
    });
}
