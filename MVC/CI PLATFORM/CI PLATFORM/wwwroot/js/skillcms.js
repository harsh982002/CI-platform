var salert = document.getElementById(`sname`).value
var skill_id
var type
const getskill = (id) => {
    skill_id = id
}
const deleteskill = () => {
    $(`#skill-${skill_id}`).remove()
    $.ajax({
        url: '/Admin/Skill',
        type: 'POST',
        data: { skill_id: parseInt(skill_id), type: "skill-delete" },
        success: function (result) {
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}
const addskill = () => {
    skillvalidate()
    var sname = document.getElementById("sname").value
    var status = document.getElementById("Status").value
    salert = sname
    if (type == "edit-skill") {
        if (salert != "") {
            $.ajax({
                url: '/Admin/Skill',
                type: 'POST',
                data: { sname: sname, status: status, type: "edit-skill", skill_id: parseInt(document.getElementById("skill-id").value) },
                success: function (result) {
                    if (result.view) {
                        $(`#skill-${parseInt(document.getElementById("skill-id").value)}`).replaceWith(result.view.result)
                        
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
        if (salert != "") {
            $.ajax({
                url: '/Admin/Skill',
                type: 'POST',
                data: { sname: sname, status: status },
                success: function (result) {
                    if (result.success) {
                        document.getElementById("sname").value = ""
                        document.getElementById("Status").value = ""
                        location.reload();
                    }
                    else {
                        $("#sameskill").addClass('d-block').removeClass('d-none')
                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        }
    }

}
const EditSkill = (id, skillname, status, typ) => {
    
    type = typ;
    if (type == "edit-skill") {
        $(`#skill-${skill_id}`).attr("selected", "selected")
        document.getElementById(`sname`).value = skillname
        document.getElementById('Status').value = status
        document.getElementById("skill-id").value = id
    }
}
const clearSkillModal = () => {

    $('#sname').val('');
    $('#Status').val('');
}
const skillvalidate = () => {
    salert = document.getElementById(`sname`).value
    if (salert == "") {
        $("#skillalert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#skillalert").addClass('d-none').removeClass('d-block')
    }
}

