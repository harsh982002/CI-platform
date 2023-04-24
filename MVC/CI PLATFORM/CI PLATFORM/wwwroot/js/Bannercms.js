var type
var banner_id

const getbanner = (id) => {
    banner_id = id
}
const deletebanner = () => {
    $(`#banner-${banner_id}`).remove()
    $.ajax({
        url: '/Admin/Banner',
        type: 'POST',
        data: { banner_id: parseInt(banner_id), type: "banner-delete" },
        success: function (result) {
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}

const EditBanner = (id, text, sortorder, typ) => {
    type = typ;
    if (type == "edit-banner") {
        $(`#banner-${banner_id}`).attr("selected", "selected")
        document.getElementById("banner-id").value = id
    }
}
const addbanner = () => {

    if (type == "edit-banner") {
        if (talert != "") {
            $.ajax({
                url: '/Admin/Banner',
                type: 'POST',
                data: { theme: theme, themestatus: themestatus, type: "edit-Banner", banner_id: parseInt(document.getElementById("banner-id").value) },
                success: function (result) {
                    if (result.view) {
                        $(`#banner-${parseInt(document.getElementById("banner-id").value)}`).replaceWith(result.view.result)

                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        }
    }
    else {
        if (talert != "") {
            $.ajax({
                url: '/Admin/banner',
                type: 'POST',
                data: { theme: theme, themestatus: themestatus },
                success: function (result) {
                    if (result.success) {
                        $("#Add").modal('hide')
                        document.getElementById("banner").value = ""
                        document.getElementById("themestatus").value = ""

                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        }
    }

}