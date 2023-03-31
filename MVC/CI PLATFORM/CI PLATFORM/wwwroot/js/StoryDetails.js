function Recommend(StoryId) {

    var emailList = [];

    $('input[name="email"]:checked').each(function () {
        emailList.push(this.id);
    });

    $.ajax({

        url: "/Story/StoryDetails/{id}",
        method: "POST",

        data: {
            "emailList": emailList,
            "StoryId": StoryId,

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