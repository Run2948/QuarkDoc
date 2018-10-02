jQuery.sw = function (type, title, callback) {
    if (type == 'success-message') {
        swal({
            title: title,
            buttonsStyling: false,
            confirmButtonClass: "btn btn-info",
            type: "success"
        }).then((value) => {
            if (callback != null) {
                callback();
            }
        })
    } else if (type == 'error-message') {
        swal({
            title: title,
            buttonsStyling: false,
            confirmButtonClass: "btn btn-info",
            type: "error"
        }).then(function () {
            if (callback != null) {
                callback();
            }
        })
    }

}

jQuery.sn = function (e, a) {
    type = ["", "info", "danger", "success", "warning", "rose", "primary"],
        color = Math.floor(6 * Math.random() + 1),
        $.notify({
            icon: "add_alert",
            message: "成功"
        }, {
                type: type[color],
                timer: 3e3,
                placement: { from: e, align: a }
            }
        )
}