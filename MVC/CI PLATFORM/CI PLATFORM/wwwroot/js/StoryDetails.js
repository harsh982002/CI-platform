function sendMail(id) {

    var emailList = [];

    $('input[name="email"]:checked').each(function () {
        emailList.push(this.id);
    });

    $.ajax({

        url: "Story/StoryDetails/{id}",
        method: "POST",

        data: {
            "emailList": emailList,
            "id": id,

        },
        success: function (data) {
            if (data == success) {

                toastr.options = {
                    "positionClass": "toast-bottom-right"
                }
                toastr.success("Mail Successfully...")




            }
        },
        error: function (request, error) {

            console.log(error);
        }

    })
}
