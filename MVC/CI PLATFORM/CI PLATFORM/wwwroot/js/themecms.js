var type
var theme_id
var talert = document.getElementById(`theme`).value
const gettheme = (id) => {
    theme_id = id
}
const deletetheme = () => {
    $(`#theme-${theme_id}`).remove()
    $.ajax({
        url: '/Admin/Theme',
        type: 'POST',
        data: { theme_id: parseInt(theme_id), type: "theme-delete" },
        success: function (result) {
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}
const EditTheme = (id, theme, themestatus, typ) => {
    type = typ;
    if (type == "edit-theme") {
        $(`#theme-${theme_id}`).attr("selected", "selected")
        document.getElementById(`theme`).value = theme
        document.getElementById('themestatus').value = themestatus
        document.getElementById("theme-id").value = id
    }
}
const addtheme = () => {
    themevalidate();
    var theme = document.getElementById("theme").value
    var themestatus = document.getElementById("themestatus").value
    if (type == "edit-theme") {
        if (talert != "") {
            $.ajax({
                url: '/Admin/Theme',
                type: 'POST',
                data: { theme: theme, themestatus: themestatus, type: "edit-theme", theme_id: parseInt(document.getElementById("theme-id").value) },
                success: function (result) {
                    if (result.view) {
                        $(`#theme-${parseInt(document.getElementById("theme-id").value)}`).replaceWith(result.view.result)
                        
                    }
                    location.reload();
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
                url: '/Admin/Theme',
                type: 'POST',
                data: { theme: theme, themestatus: themestatus },
                success: function (result) {
                    if (result.success) {
                        document.getElementById("theme").value = ""
                        document.getElementById("themestatus").value = ""
                        location.reload();
                    }
                    else {
                        $("#sametheme").addClass('d-block').removeClass('d-none');
                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        }
    }

}
const clearThemeModal = () => {

    $('#theme').val('');
    $('#themestatus').val('');
}
const themevalidate = () => {
    talert = document.getElementById(`theme`).value
    if (talert == "") {
        $("#themealert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#themealert").addClass('d-none').removeClass('d-block')
    }
}