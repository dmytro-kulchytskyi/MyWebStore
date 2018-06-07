$(function () {
    var input = $("#searchField");

    var searchItems = $('#serachItemsContainer');

    var noResultsMessage = $(searchItems).html();

    if ($(input).val()) {
        loadDataToContainer(false);
    }

    function loadDataToContainer(showContainerAfterLoad) {
        $.ajax({
            method: "POST",
            url: "/Product/Search",
            data: { query: $(input).val() },
        }).done(function (data) {
            if (($(input).val() || "").trim()) {
                $(searchItems).html(data || '');
                if (showContainerAfterLoad) {
                    $(searchItems).show();
                }

                if (!data || !data.length) {
                    if (showContainerAfterLoad)
                        $(searchItems).append($(noResultsMessage).show());
                }
            }
        });
    }

    $(input).on("propertychange keyup paste input", function () {
        if (($(this).val() || "").trim()) {
            loadDataToContainer(true);
        }
        else {
            $(searchItems).hide();
        }
    });

    $(input).on("keydown", function (e) {
        if (e.keyCode === 13) {
            var inputVal = ($(this).val() || "").trim();
            if (inputVal)
                window.location = '/SearchPage?' + $.param({ query: inputVal });
        }
    });

    $(input).focusin(function () {
        if (($(this).val() || "").trim())
            $(searchItems).show();
    });

    $(document).on("click", function () {
        if ($('.searchField:hover').length === 0)
            $(searchItems).hide();
    });
})