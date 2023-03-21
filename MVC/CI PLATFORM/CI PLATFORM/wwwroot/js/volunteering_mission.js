function display(item) {
    let image = document.getElementById("image-area");
    image.src = item.src;
}

function Add(MissionId,UserId) {
    debugger
    $.ajax({

        url: "/Home/AddToFavourite",
        method: "POST",

        data:
        {
            MissionId: MissionId,
            UserId: UserId

        },
        success: function (data) {

            if (data == true) {
                //$('#addToFav').removeClass();
                //$('#addToFav').addClass("bi bi-heart-fill");
                $('#addToFav').css("color", "red");
            }
            else {
                $('#addToFav').css("color", "black");
                //$('#addToFav').removeClass();
                //$('#addToFav').addClass("bi bi-heart");
            }
        }
    })
}
/*const add_comments = (MissionId, UserId) => {
    var comment = document.getElementById('usercomment').value
    var length = $('.user-comments').find('.usercomment-image').length
    if (comment.length > 3) {
        $.ajax({
            url: `/Home//Home/AddToFavourite/${mission_id}`,
            type: 'POST',
            data: { UserId: UserId, MissionId: MissionId, comment: comment, length: length },
            success: function (result) {
                load_comments(result.comments.result)
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}*/

function AddComments(missionId, UserId) {

    var comment = $("#user-comment").val();

    if (comment == null || comment == "") {
        $("#comment-status").html("please enter the comment");
        $("#comment-status").css("color", "red");
    }
    else {
        
        $.ajax({

            url: "/Home/AddComment",
            method: "POST",

            data:
            {
                comment: comment,
                UserId: UserId,
                missionId: missionId
            },
            success: function (data) {
                $("#comment-status").html("comment sent for approvel");
                $("#comment-status").css("color", "green");
                $('#user-comment').val('');
            }
        })
    }
}
/*
function applymission = (MissionId, UserId) => {
    debugger
    $.ajax({
        url: "/Home/ApplyMission",
        type: "POST",
        data: { MissionId: MissionId, UserId: UserId },
        success: function (data) {
            if (result.success) {
                $('.apply-button').empty().append('<button  class="applyButton btn" disabled>Applied<img src="images/right-arrow.png" alt="">' + '</button >')
                $('.validate-recommend').removeClass('d-none').addClass('d-flex')
            }
        },
        error: function () {
            console.log("Error updating variable");
        }
    })
}*/


function ApplyMission(MissionId, UserId) {

    $.ajax({

        url: "/Home/ApplyMission",
        method: "POST",

        data:
        {

            UserId: UserId,
            MissionId: MissionId
        },
        success: function (data) {
            console.log("success");
            $("#volunteer-status").html("Request sent for approvel");
            $("#volunteer-status").css("color", "green");
        }
    });



}