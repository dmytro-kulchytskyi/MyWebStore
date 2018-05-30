$(function () {
    var input = $("#searchField");

    var searchItems = $('#serachItemsContainer');

    var noResultsMessage = $(searchItems).html();

    $(input).on("propertychange keyup paste input", function () {
        if (($(this).val() || "").trim()) {
            $.ajax({
                method: "POST",
                url: "/Product/Search",
                data: { query: $(this).val() },
            }).done(function (data) {
                if (($(input).val() || "").trim()) {
                    $(searchItems).html(data || '').show();

                    if (!data || !data.length) {
                        $(searchItems).append($(noResultsMessage).show());
                    }
                }
            });
        }
        else {
            $(searchItems).hide();
        }
    });

    $(input).on("keydown", function (e) {
        if (e.keyCode === 13) {
            var inputVal = $(this).val();
            if (inputVal)
                window.location = '/SearchPage?' + $.param({ "Query": inputVal });
        }
    });

    $(input).focusin(function () {
        console.log("ficus")
        if (($(this).val() || "").trim())
            $(searchItems).show();
    });

    $(document).on("click", function () {
        if ($('.searchField:hover').length === 0)
            $(searchItems).hide();
    });
})