$(function () {
    var input = $("#searchField");

    var searchItems = $('#serachItemsContainer');

    var noResultsMessage = $(searchItems).html();

    $(input).on("keyup", function () {
        if (($(this).val() || "").trim()) {
            $.ajax({
                method: "POST",
                url: "/Product/Search",
                data: { query: $(this).val() },
            }).done(function (data) {
                $(searchItems).html(data || '').show();

                if (!data || !data.length) {
                    $(searchItems).append($(noResultsMessage).show());
                }
            });
        }
        else {
            $(serachItemsContainer).hide();
        }
    });
})