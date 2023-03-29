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
                $("#comment-status").html("comment sent for approvel");
                $("#comment-status").css("color", "green");
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

                toastr.options = {
                    "positionClass": "toast-bottom-right"
                }
                toastr.success("Mail Successfully...")


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
