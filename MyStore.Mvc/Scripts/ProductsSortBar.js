$(function () {
    var $productListSortBar = $("#productListSortBar");
    
    $($productListSortBar).submit(function (e) {
        e.preventDefault();
    
        var params = {};
        $.each($(this).serializeArray(), function (_, field) {
            if (field.value.toString())
                params[field.name] = field.value;
        });

        params.pageNumber = 1;

        window.location = $(this).attr('action') + '?' + $.param(params);
    })
});