var count = 1;
function display(item) {
    let image = document.getElementById("image-area");
    image.src = item.src;
}

function Add(MissionId,UserId) {
    
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
               /* $('#addToFav').addClass("bi-heart-fill");*/
                $('#addToFav').css("color", "red");
                $('#addToFav').html("Remove From Favourites");
            }
            else {
                $('#addToFav').css("color", "black");
                //$('#addToFav').removeClass();
              /*  $('#addToFav').addClass("bi-heart");*/
                $('#addToFav').html("Add to Favourites");

            }
        }
    })
}

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
               
                $('#user-comment').val('');
                
            }
        })
    }
}

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
            if (data == true) {
                console.log("success");
                $("#volunteer-status").html("Request sent for approvel");
                $("#volunteer-status").css("color", "green");
                alert("Your request has been sent.");
            }
            else {
                $("#volunteer-status").html("Already Applied");
                $("#volunteer-status").css("color", "red");
            }

            
        }
    });


  
}

function sendMail(MissionId) {
    
    var emailList = [];

    $('input[name="email"]:checked').each(function () {
        emailList.push(this.id);
    });

    $.ajax({

        url: "/Home/RecommendCoWorker",
        method: "POST",

        data: {
            "emailList": emailList,
            "MissionId": MissionId,
            
        },
        success: function (data) {
            if (data) {
/*
                toastr.options = {
                    "positionClass": "toast-bottom-right"
                }
                toastr.success("Mail Successfully...")*/

                //// Loop through each ID in the emailList array
                //for (var i = 0; i < emailList.length; i++) {
                //    // Get the checkbox element with the current ID
                //    var checkbox = $('#' + emailList[i]);

                //    // Set the checked property of the checkbox to false
                //    checkbox.prop('checked', false);
                //}


            }
        },
        error: function (request, error) {

            console.log(error);
        }

    })
}

const rating = (rating, user_id, mission_id) => {
    if (rating == 1) {
        $('.rating').find('img').each(function (i, item) {
            if (i == (rating - 1)) {
                if (item.id == `${i + 1}-star-empty`) {
                    item.src = '/images/selected-star.png'
                    item.id = `${i + 1}-star`
                    $.ajax({
                        url: `/Home/Volunteermission/${mission_id}`,
                        type: 'POST',
                        data: {mission_id: mission_id, user_id: user_id, rating: rating },
                        success: function (result) {
                        },
                        error: function () {
                            console.log("Error updating variable");
                        }
                    })
                }
                else {
                    item.src = '/images/star-empty.png'
                    item.id = `${i + 1}-star-empty`
                    $.ajax({
                        url: `/Home/Volunteermission/${mission_id}`,
                        type: 'POST',
                        data: {mission_id: mission_id, user_id: user_id, rating: 0 },
                        success: function (result) {
                        },
                        error: function () {
                            console.log("Error updating variable");
                        }
                    })
                }
            }
            else {
                item.src = '/images/star-empty.png'
                item.id = `${i + 1}-star-empty`
            }
        })
    }
    else {
        $('.rating').find('img').each(function (i, item) {
            if (i <= (rating - 1)) {
                if (item.id == `${i + 1}-star-empty` || i <= (rating - 1)) {
                    item.src = '/images/selected-star.png'
                    item.id = `${i + 1}-star`
                }
                else {
                    item.src = '/images/star-empty.png'
                    item.id = `${i + 1}-star-empty`
                }
            }
            else {
                item.src = '/images/star-empty.png'
                item.id = `${i + 1}-star-empty`
            }
        })
        $.ajax({
            url: `/Home/Volunteermission/${mission_id}`,
            type: 'POST',
            data: {mission_id: mission_id, user_id: user_id, rating: rating },
            success: function (result) {
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }

}

const prev_volunteers = (UserId, MissionId) => {
    var one_page_volunteers = 3
    console.log(MissionId)
    if (count > 1) {
        count--;
        $.ajax({
            url: `/Home/RecentVolunteer`,
            type: 'POST',
            data: { count: count - 1, UserId: UserId, MissionId: MissionId },
            success: function (result) {
                $('.volunteers').empty().append(result)
                $('.current_volunteers').html(`${one_page_volunteers * (count - 1) == 0 ? 1 : one_page_volunteers * (count - 1)}-${one_page_volunteers * count} of recent ${result.Total_volunteers} volunteers`)
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}

const next_volunteers = (max_page, UserId, MissionId) => {
    var one_page_volunteers = 3
    if (count < max_page) {
        count++;
        $.ajax({
            url: `/Home/RecentVolunteer`,
            type: 'POST',
            data: { count: count - 1, UserId: UserId, MissionId: MissionId },
            success: function (result) {
                $('.volunteers').empty().append(result)
                $('.current_volunteers').html(`${one_page_volunteers * (count - 1) + 1}-${one_page_volunteers * count >= result.Total_volunteers ? result.Total_volunteers : one_page_volunteers * count} of recent ${result.Total_volunteers} volunteers`)
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}

