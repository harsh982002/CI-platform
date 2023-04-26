var type
var user_id
var mission_id


//user
var falert = document.getElementById(`fname`).value
var lalert = document.getElementById(`lname`).value
var mailalert = document.getElementById(`email`).value
var palert = document.getElementById(`pass`).value
var numberalert = document.getElementById(`phone`).value
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
    validate();
    var fname = document.getElementById(`fname`).value
    var lname = document.getElementById(`lname`).value
    var email = document.getElementById(`email`).value
    var pass = document.getElementById(`pass`).value
    var phone = document.getElementById(`phone`).value
    var role = document.getElementById(`role`).value
    if (falert != "" && lalert != "" && mailalert != "" && palert != "" && numberalert != "") {
        $.ajax({
            url: '/Admin',
            type: 'POST',
            data: { fname: fname, lname: lname, email: email, pass: pass, phone: phone, role: role },
            success: function (result) {
                if (result.success) {
                    document.getElementById("fname").value = ""
                    document.getElementById("lname").value = ""
                    document.getElementById(`email`).value = ""
                    document.getElementById(`pass`).value = ""
                    document.getElementById(`phone`).value = ""
                    document.getElementById(`role`).value = ""
                    location.reload();
                }
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }

}
const Edituser = (id, fname, lname, email, pass, phone, role, city, country, empid, department, status, ptext, typ, img) => {
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
                    location.reload();
                }
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}
const validate = () => {
    falert = document.getElementById(`fname`).value
    lalert = document.getElementById(`lname`).value
    mailalert = document.getElementById(`email`).value
    palert = document.getElementById(`pass`).value
    numberalert = document.getElementById(`phone`).value
    if (falert == "") {
        $("#firstalert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#firstalert").addClass('d-none').removeClass('d-block')
    }
    if (lalert == "") {
        $("#lastalert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#lastalert").addClass('d-none').removeClass('d-block')
    }
    if (mailalert == "") {
        $("#mailalert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#mailalert").addClass('d-none').removeClass('d-block')
    }
    if (palert == "") {
        $("#passalert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#passalert").addClass('d-none').removeClass('d-block')
    }
    if (numberalert == "") {
        $("#phonealert").addClass('d-block').removeClass('d-none')
    }
    else {
        $("#phonealert").addClass('d-none').removeClass('d-block')
    }
}


