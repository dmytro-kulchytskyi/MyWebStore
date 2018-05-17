(function ($) {
    $(function () {
        var productsContainer = $("#productsContainer");

        var loading = false;

        function getData() {
            loading = true;
            $.ajax({
                method: "POST",
                url: "Product/ProductsPage",
                data: {
                    startDate: $(productsContainer).find(".productsListItem").last().attr("date")
                }
            }).done(function (data) {
                loading = false;

                if (!data) {
                    $(window).off("scroll", onScroll);
                }
                else {
                    $(productsContainer).append(data);
                    onScroll();
                }
            });
        }

        function onScroll() {
            if ($(window).scrollTop() + $(window).height() >= $(document).height() - 20) {
                if (!loading)
                    getData();
                
            }
        }

        $(window).scroll(onScroll);
        onScroll();
    });
})(jQuery);