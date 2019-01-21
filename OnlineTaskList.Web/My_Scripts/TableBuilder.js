$(document).ready(function () {
    $.ajax({
        url: '/TaskItems/TableBuilder',
        method: 'GET',
        success: function (r) {
            $('#table').html(r);
        },
        timeout: 10000,
        error: function () {
            alert("Error retrieving task items");
        }
    });
});