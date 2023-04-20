var type
var skill_id
var theme_id
var user_id

//user

const getuser = (id) => {
    user_id = id
}
const deleteuser = () => {
    $(`#user-${user_id}`).remove()
    $.ajax({
        url: '/Admin',
        type: 'POST',
        data: { user_id: parseInt(user_id), type: "user-delete" },
        success: function (result) {
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}
const adduser = () => {

    var fname = document.getElementById(`fname`).value
    var lname = document.getElementById(`lname`).value
    var email = document.getElementById(`email`).value
    var pass = document.getElementById(`pass`).value
    var phone = document.getElementById(`phone`).value
    var role = document.getElementById(`role`).value

        $.ajax({
            url: '/Admin',
            type: 'POST',
            data: { fname: fname, lname: lname, email: email, pass: pass, phone: phone, role: role },
            success: function (result) {
                if (result.success) {
                    $("#Add").modal('hide')
                    document.getElementById("fname").value = ""
                    document.getElementById("lname").value = ""
                    document.getElementById(`email`).value = ""
                    document.getElementById(`pass`).value = ""
                    document.getElementById(`phone`).value = ""
                    document.getElementById(`role`).value = ""
                }
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    
}
const Edituser = (id, fname, lname, email, pass, phone, role, city, country, empid, department,status, ptext, typ, img) => {

    type = typ;
    if (type == "edit-user") {
        $(`#user-${user_id}`).attr("selected", "selected")
        document.getElementById(`firstname`).value = fname
        document.getElementById('lastname').value = lname
        document.getElementById(`emailadd`).value = email
        document.getElementById(`password`).value = pass
        document.getElementById(`roles`).value = role
        document.getElementById(`phonenumber`).value = phone
        document.getElementById(`city`).value = city
        document.getElementById(`country`).value = country
        document.getElementById(`empid`).value = empid
        document.getElementById(`department`).value = department
        document.getElementById(`ustatus`).value = status
        document.getElementById(`bio`).value = ptext
        if (img == '') {
            document.getElementById(`img`).src = '/images/volunteer1.png'
        }
        else {
            document.getElementById(`img`).src = img
        }
        document.getElementById("user-id").value = id
    }
    
}
const EditUserDetails = () => {
    var role = document.getElementById(`roles`).value
    var department = document.getElementById(`department`).value
    var empid = document.getElementById(`empid`).value
    var status = document.getElementById(`ustatus`).value
    if (type == "edit-user") {
        $.ajax({
            url: '/Admin',
            type: 'POST',
            data: { role: role, department: department, empid: empid, status: status, type: "edit-user", user_id: parseInt(document.getElementById("user-id").value) },
            success: function (result) {
                if (result.view) {
                    $(`#user-${parseInt(document.getElementById("user-id").value)}`).replaceWith(result.view.result)

                }
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}


//SkillCMS
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

    var sname = document.getElementById("sname").value
    var status = document.getElementById("Status").value
    if (type == "edit-skill") {
       
            $.ajax({

                url: '/Admin/Skill',
                type: 'POST',
                data: { sname: sname, status: status, type: "edit-skill", skill_id: parseInt(document.getElementById("skill-id").value) },
                success: function (result) {
                    if (result.view) {
                        $(`#skill-${parseInt(document.getElementById("skill-id").value)}`).replaceWith(result.view.result)
                        window.location.reload();
                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        
    }
    else {
        
            $.ajax({
                url: '/Admin/Skill',
                type: 'POST',
                data: { sname: sname, status: status },
                success: function (result) {
                    if (result.success) {
                        $("#Add").modal('hide')
                        document.getElementById("sname").value = ""
                        document.getElementById("Status").value = ""
                        window.location.reload();
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
const clearSkillModal = () => {

    $('#sname').val('');
    $('#Status').val('');
}


//StoryCMS
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
            location.reload();
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}
const storyvalidate = (status) => {
    $.ajax({
        url: '/Admin/storyvalidate',
        type: 'POST',
        data: { story_id: parseInt(story_id), status: status },
        success: function (result) {
            window.location.reload();
        },
        error: function () {
            console.log("Error updating variable");
        }
    })
}

//ThemeCMS
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

    var theme = document.getElementById("theme").value
    var themestatus = document.getElementById("themestatus").value
    if (type == "edit-theme") {

            $.ajax({
                url: '/Admin/Theme',
                type: 'POST',
                data: { theme: theme, themestatus: themestatus, type: "edit-theme", theme_id: parseInt(document.getElementById("theme-id").value) },
                success: function (result) {
                    if (result.view) {
                        $(`#theme-${parseInt(document.getElementById("theme-id").value)}`).replaceWith(result.view.result)

                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        
    }
    else {
        
            $.ajax({
                url: '/Admin/Theme',
                type: 'POST',
                data: { theme: theme, themestatus: themestatus },
                success: function (result) {
                    if (result.success) {
                        $("#Add").modal('hide')
                        document.getElementById("theme").value = ""
                        document.getElementById("themestatus").value = ""

                    }
                },
                error: function () {
                    console.log("Error updating variable");
                }
            })
        
    }

}
const clearThemeModal = () => {

    $('#theme').val('');
    $('#themestatus').val('');
}


//missionapplicationCMS
var ma_id = document.getElementById("ma_id").value
const getma = (id) => {
    ma_id = id
    console.log(id, ma_id)
}
const validation = (status) => {
    console.log(ma_id)

    $.ajax({
        url: '/Admin/MAValidate',
        type: 'POST',
        data: { ma_id: parseInt(ma_id), status: status },
        success: function (result) {
            window.location.reload();
        },
        error: function () {
            console.log("Error updating variable");
        }
    })
}

