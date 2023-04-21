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


//missionapplicationCMS
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