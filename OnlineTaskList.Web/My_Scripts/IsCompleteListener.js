$(document).ready(function () {
    $('.TaskItemCompleted').change(function () {
        var t = $(this);
        var id = t.attr('id');
        var val = t.prop('checked');
        $.ajax({
            url: '/TaskItems/AjaxEditExisting',
            type: 'POST',
            data: {
                id: id,
                isComplete: val
            },
            success: function (r) {
                $('#table').html(r);
            },
            timeout: 10000,
            error: function () {
                alert("Error updating task items");
            }
        });
    });
});