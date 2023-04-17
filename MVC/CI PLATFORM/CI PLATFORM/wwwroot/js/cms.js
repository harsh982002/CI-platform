var type
var skill_id
const getskill = (id) => {
    skill_id=id
    
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
    var sname = document.getElementById("sname").value
    var status = document.getElementById("Status").value
    if (type == "edit-skill") {
        $.ajax({
            url: '/Admin/Skill',
            type: 'POST',
            data: { sname: sname, status: status, type: "edit-skill", skill_id: parseInt(document.getElementById("skill-id").value)},
            success: function (result) {
                if (result.view) {
                    $(`#skill-${parseInt(document.getElementById("skill-id").value)}`).replaceWith(result.view.result)

                }
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
    else
  {
        $.ajax({
            url: '/Admin/Skill',
            type: 'POST',
            data: { sname: sname, status: status },
            success: function (result) {
                if (result.success) {
                    $("#Add").modal('hide')
                    document.getElementById("sname").value = ""
                    document.getElementById("Status").value = ""

                }
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }

}
const EditSkill = (id, skillname, status, typ) => {
    console.log(id, skillname, status, typ)
    type = typ;
    if (type == "edit-skill") {
        $(`#skill-${skill_id}`).attr("selected", "selected")
        document.getElementById(`sname`).value = skillname
        document.getElementById('Status').value = status
        document.getElementById("skill-id").value = id
    }
}
const getstory = (id) => {
    story_id = id;
}
const deletestory = () => {
    $(`#story-${story_id}`).remove()
    $.ajax({
        url: '/Admin/Story',
        type: 'POST',
        data: { story_id: parseInt(story_id), type: "story-delete" },
        success: function (result) {
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}