function AddAjaxEvents() {
    $("a[ajax-request='yes']").click(function (event) {
        $(this).attr("ajax-request", "added");
        event.preventDefault();
        var requestUrl = $(this).attr("ajax-url");
        var resultDiv = $(this).attr("ajax-result");
        var modalSize = $(this).attr("modal-size");
        if (modalSize == "lg") {
            if (!$("#modelDialog").hasClass('modal-lg'))
                $("#modelDialog").toggleClass('modal-lg')
        }
        else {
            if ($("#modelDialog").hasClass('modal-lg'))
                $("#modelDialog").removeClass('modal-lg')
        }
        $.get(requestUrl, function (data, status) {
            $("#" + resultDiv).html(data);
        });
    });

    $("a[ajax-request='delete']").click(function (event) {
        $(this).attr("ajax-request", "added");
        event.preventDefault();
        var requestUrl = $(this).attr("ajax-url");
        Swal.fire({
            title: 'Warning!',
            text: 'Do you want to delete this item?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: `Delete`
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "GET",
                    url: requestUrl,
                    success: function (data) {
                        Swal.fire('Deleted!', '', 'success').then(() => {
                            window.location.reload();
                        })
                    }
                })
            }
        });
    });
}
$(document).ready(function () {
    AddAjaxEvents();
});

