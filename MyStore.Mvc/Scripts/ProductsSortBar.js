$(function () {
    var $productListSortBar = $("#productListSortBar");

    var $orderFieldSelect = $($productListSortBar).find('select[name=OrderField]').first();

    var $inverseOrderInput = $($productListSortBar).find('select[name=InverseOrder]').first();

    if ($($orderFieldSelect).val()) {
        $($inverseOrderInput).show();
    }
    else {
        $($inverseOrderInput).hide();
    }

    $($orderFieldSelect).on('change', function () {
        if ($(this).val()) {
            $($inverseOrderInput).show();
        }
        else {
            $($inverseOrderInput).hide();
        }
    });

    $($productListSortBar).submit(function (e) {
        e.preventDefault();
        //alert("SUBMIT");
        var params = {};
        $.each($(this).serializeArray(), function (_, field) {
            if (field.value.toString())
                params[field.name] = field.value;
        });

        window.location = $(this).attr('action') + '?' + $.param(params);
    })
});