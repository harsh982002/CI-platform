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
const add_comments = (MissionId, UserId) => {
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
}

const apply_for_mission = (UserId, MissionId) => {
    $.ajax({
        url: `/Home/Volunteermission/${mission_id}`,
        type: 'POST',
        data: { UserId: UserId, MissionId: MissionId, request_for: "mission" },
        success: function (result) {
            if (result.success) {
                $('.apply-button').empty().append('<button  class="applyButton btn" disabled>Applied<img src="images/right-arrow.png" alt="">' + '</button >')
                $('.validate-recommend').removeClass('d-none').addClass('d-flex')
            }
        },
        error: function () {
            console.log("Error updating variable");
        }
    })
}