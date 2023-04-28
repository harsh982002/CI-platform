var cms_id
var type
var Talert = document.getElementById(`title`).value
var Salert = document.getElementById(`slug`).value
tinymce.init({
    selector: '#editor',
    menubar: false,
    statusbar: false,
    plugins: 'autoresize anchor autolink charmap code codesample directionality fullpage help hr image imagetools insertdatetime link lists media nonbreaking pagebreak preview print searchreplace table template textpattern toc visualblocks visualchars',
    toolbar: 'h1 h2 bold italic strikethrough blockquote bullist numlist backcolor | link image media | removeformat help fullscreen ',
    skin: 'bootstrap',
    toolbar_drawer: 'floating',
    min_height: 200,
    autoresize_bottom_margin: 16,
    setup: (editor) => {
        editor.on('init', () => {
            editor.getContainer().style.transition = "border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out"
        });
        editor.on('focus', () => {
            editor.getContainer().style.boxShadow = "0 0 0 .2rem rgba(0, 123, 255, .25)",
                editor.getContainer().style.borderColor = "#80bdff"
        });
        editor.on('blur', () => {
            editor.getContainer().style.boxShadow = "",
                editor.getContainer().style.borderColor = ""
        });
    }
});
const getcms = (id) => {
    cms_id = id
}
const deletecms = () => {
    $(`#cms-${cms_id}`).remove()
    $.ajax({
        url: '/Admin/CMS',
        type: 'POST',
        data: { cms_id: parseInt(cms_id), type: "cms-delete" },
        success: function (result) {
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}
const addcms = () => {
    validate();
    var title = document.getElementById("title").value
    var editor = tinymce.get("editor").getContent();
    var slug = document.getElementById("slug").value
    var status = document.getElementById("status").value

    if (type == "edit-cms") {
        if (Talert != "" && Salert != "") {
            $.ajax({
                url: '/Admin/CMS',
                type: 'POST',
                data: { title: title, editor: editor, slug: slug, status: status, type: "edit-cms", cms_id: parseInt(document.getElementById("cms-id").value) },
                success: function (result) {

                    if (result.view) {
                        $(`#cms-${parseInt(document.getElementById("cms-id").value)}`).replaceWith(result.view.result)
                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        }
    }

    else {
        if (Talert != "" && Salert != "") {
            $.ajax({
                url: '/Admin/CMS',
                type: 'POST',
                data: { title: title, editor: editor, status: status, slug: slug },
                success: function (result) {
                    if (result.success) {
                        location.reload();
                    }
                    else {
                        $("#sametitle").addClass('d-block').removeClass('d-none');
                        $("#sameslug").addClass('d-block').removeClass('d-none');
                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        }
}
}
const editcmspage = (id, title, editor, slug, status, typ) => {
    type = typ;
    console.log(id, title, editor, slug, status, typ)
    if (type == "edit-cms") {
        $(`#cms-${cms_id}`).attr("selected", "selected")
        document.getElementById(`title`).value = title
        document.getElementById(`slug`).value = slug
        document.getElementById(`status`).value = status
        document.getElementById("cms-id").value = id
        tinymce.get("editor").setContent(editor)

    }
}
function clearModal() {

    $('#title').val('');
    tinymce.activeEditor.setContent('');
    $('#status').val('');
    $('#slug').val('');
}
const validate = () => {
    Talert = document.getElementById(`title`).value
    Salert = document.getElementById(`slug`).value
    if (Talert== "") {
        $("#titleAlert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#titleAlert").addClass('d-none').removeClass('d-block')
    }
    if (Salert == "") {
        $("#slugAlert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#slugAlert").addClass('d-none').removeClass('d-block')
    }
}




