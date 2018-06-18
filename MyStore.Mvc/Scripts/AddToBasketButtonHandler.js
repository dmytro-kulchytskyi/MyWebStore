(function ($) {
    $(function () {
        
        $('.addToBasketForm').submit(function (e) {
            e.preventDefault();

            var btn = $(this).find('.button-container').hide();
            var loader = $(this).find('.loader').show();

            $.ajax({
                method: "POST",
                url: "/Basket/Add",
                data: $(this).serializeArray(),
            }).done(function (data) {
                setTimeout(function () {
                    $(btn).show();
                    $(loader).hide();
                }, 120);
               
                var type = data.Success ? 'success' : 'danger';
                $.notify({
                    message: data.Message
                }, {
                        type: type,
                        allow_dismiss: false,
                        offset: 60,
                        spacing: 10,
                        delay: 1500,
                        placement: {
                            from: "top",
                            align: "center"
                        },
                        mouse_over: 'pause',
                        animate: {
                            enter: 'animated fadeInDown',
                            exit: 'animated fadeOutUp'
                        },
                    });
            });
        });
    });
})(jQuery);