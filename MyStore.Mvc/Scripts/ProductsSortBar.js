$(function () {
    var $productListSortBar = $("#productListSortBar");

    var $orderFieldSelect = $($productListSortBar).find('select[name=OrderField]').first();

    var $inverseOrderInput = $($productListSortBar).find('select[name=InverseOrder]').first();

    var $inverseOrderFieldConatiner = $('#inverseOrderField');

    if ($($orderFieldSelect).val()) {
        $($inverseOrderFieldConatiner).show();
    }
    else {
        $($inverseOrderFieldConatiner).hide();
    }

    $($orderFieldSelect).on('change', function () {
        if ($(this).val()) {
            $($inverseOrderFieldConatiner).show();
        }
        else {
            $($inverseOrderFieldConatiner).hide();
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

        params.pageNumber = 1;

        window.location = $(this).attr('action') + '?' + $.param(params);
    })
});