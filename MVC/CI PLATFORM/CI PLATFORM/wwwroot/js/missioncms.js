var mission_id
var type
var country
var city
//mission
const getmission = (id) => {
    mission_id = id
}
const deletemission = () => {
    $(`#mission-${mission_id}`).remove()
    $.ajax({
        url: '/Admin/Mission',
        type: 'POST',
        data: { mission_id: parseInt(mission_id), type: "mission-delete" },
        success: function (result) {
        },
        error: function () {
            console.log("Error updating variable");
        }
    });
}
const getcities = () => {
    var country = $('.country').find(":selected").val()
    if (parseInt(country) != 0) {
        $.ajax({
            url: '/Home/Profile',
            type: 'POST',
            data: { country: country },
            success: function (result) {
                $('.city').empty().append(result.cities.result)
            },
            error: function () {
                console.log("Error updating variable");
            }
        })
    }
}
