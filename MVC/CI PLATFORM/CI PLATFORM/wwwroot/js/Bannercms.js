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

