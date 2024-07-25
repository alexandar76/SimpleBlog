$(document).ready(function () {
    $("a[data-post]").click(function (e) {
        e.preventDefault();

        var $this = $(this);
        var message = $this.data("post");

        if (message && !confirm(message))
            return;

        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href"))
            .appendTo(document.body)
            .submit();
    });

    $('input[type="checkbox"]').change(function () {
        if ($('input[type="checkbox"]:checked').length > 0) {
            $('#DelButton').prop('disabled', false);
        } else {
            $('#DelButton').prop('disabled', true);
        }
    });
   
    $('.test').on('change', function () {
        $('.test').not(this).prop('checked', false);
    });

    $("[data-slug]").each(function () {
        var $this = $(this);
        var $sendSlugFrom = $($this.data("slug"));

        $sendSlugFrom.keyup(function () {
            var slug = $sendSlugFrom.val();
            slug = slug.replace(/[^a-zA-Z0-9\s]/g, "");
            slug = slug.toLowerCase();
            slug = slug.replace(/\s+/g, "-");

            if (slug.charAt(slug.length - 1) == "-")
                slug = slug.substr(0, slug.length - 1);

            $this.val(slug);
        });
    });



   
});

