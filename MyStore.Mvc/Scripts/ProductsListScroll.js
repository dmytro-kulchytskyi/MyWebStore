(function ($) {
    $(function () {
        var $productsContainer = $("#productsContainer");
        var pageinfo;
        var loading = false;

        function updatePageInfo() {
            var dataListArr = $($productsContainer).find("datalist");
            var $pageInfo = dataListArr[dataListArr.length - 1];

            console.log($pageInfo);


            if (parseInt($($pageInfo).attr("itemsCount")) > 0) {

                pageinfo = {
                    pageSize: $($pageInfo).attr("pageSize"),
                    pageNumber: parseInt($($pageInfo).attr("pageNumber") || 0) + 1,
                    orderField: $($pageInfo).attr("orderField"),
                    inverseOrder: $($pageInfo).attr("inverseOrder")
                };
                console.log(pageinfo);
                onScroll();
            }
            else $(window).off("scroll", onScroll);
        }

        function getData() {
            loading = true;
            $.ajax({
                method: "POST",
                url: "Product/ProductsPage",
                data: pageinfo
            }).done(function ($data) {
                loading = false;

                $($productsContainer).append($data);

                updatePageInfo();
            });
        }

        function onScroll() {
            if ($(window).scrollTop() + $(window).height() >= $(document).height() - 20) {
                if (!loading)
                    getData();
            }
        }

        $(window).scroll(onScroll);
        updatePageInfo();
    });
})(jQuery);