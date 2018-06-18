(function ($) {
    $(function () {
        var $form = $("#productListSortBar");

        var $sortFieldSelect = $($form).find('#sortFieldSelect');

        var $inverseSortType = $('#inverseSortType');

        $($sortFieldSelect).on('change', function () {
            
            $($inverseSortType).val($('option:selected', this).attr('inverse'));

            var params = {};
            
            $.each($($form).serializeArray(), function (_, field) {
                if (field.value && field.value.toLowerCase() !== 'false')
                    params[field.name] = field.value;
            });

            //params.pageNumber = 1;

            window.location = $($form).attr('action') + '?' + $.param(params);
        });

        function submitForm() {
            var params = {};

            $.each($($form).serializeArray(), function (_, field) {
                if (field.value.toString())
                    params[field.name] = field.value;
            });

            params.pageNumber = 1;

            window.location = $($form).attr('action') + '?' + $.param(params);
        }
    });
})(jQuery); 