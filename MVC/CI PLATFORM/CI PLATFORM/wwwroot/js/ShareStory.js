﻿var mission
var title
var date
var current_date
var comparedate
var mystory
var video_url
var media = []
var count = 0

CKEDITOR.replace('editor', {
    maxLength: 40000,
    toolbar: [
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike'] },
        { name: 'clipboard', items: ['RemoveFormat'] }

    ]
});

function loadimages() {
    var image = document.getElementById('images').files
    var images_count = $('.gallary').find('.main-image').length
    if (images_count + image.length <= 20 && image.length <= 20) {
        if (images_count == 1) {
            var fr = new FileReader()
            const div = document.createElement('div')
            const img = document.createElement('img')
            const close_div = document.createElement('div')
            close_div.className = "bg-black close d-flex justify-content-center align-items-center"
            const close_img = document.createElement('img')
            close_img.src = "/images/cancel.png"
            div.id = `image-${count}`
            div.className = "main-image-div"
            fr.readAsDataURL(image[0])
            fr.onload = () => {
                img.src = fr.result
            }
            img.className = "main-image"
            $('.gallary').append(div)
            $(`#image-${count}`).append(img)
            close_div.append(close_img)
            close_div.onclick = function () { this.parentNode.remove() }
            $(`#image-${count}`).append(close_div)
            count++
        }
        else {
            for (var i = 0; i < image.length; i++) {
                let fr = new FileReader()
                fr.onload = () => {
                    const div = document.createElement('div')
                    const img = document.createElement('img')
                    const close_div = document.createElement('div')
                    close_div.className = "bg-black close d-flex justify-content-center align-items-center"
                    const close_img = document.createElement('img')
                    close_img.src = "/images/cancel.png"
                    div.id = `image-${count}`
                    div.className = "main-image-div"
                    img.src = fr.result
                    img.className = "main-image"
                    $('.gallary').append(div)
                    $(`#image-${count}`).append(img)
                    close_div.append(close_img)
                    close_div.onclick = function () { this.parentNode.remove() }
                    $(`#image-${count}`).append(close_div)
                    count++
                }
                fr.readAsDataURL(image[i])
            }
        }
    }
}

function getdetails(type) {
    validate()
    if (mission != 0 && title.trim().length > 50 && title.trim().length < 255
        && mystory.trim().length > 70 && mystory.trim().length < 40000 && $('.gallary').find('.main-image').length != 0) {
        $.ajax({
            url: '/Story/ShareStory',
            type: 'POST',
            data: { mission_id: mission, title: title, mystory: mystory, media: media, type: type },
            success: function (result) {
                window.location.reload();
                alert("Your story has been published.");
            },
            error: function () {
                console.log("Error updating variable");
            }
        })

    }
}

function savestory() {
    validate()
    if (mission != 0 && title.trim().length > 50 && title.trim().length < 255
        && mystory.trim().length > 70 && mystory.trim().length < 40000 && $('.gallary').find('.main-image').length != 0) {
        $.ajax({
            url: '/Story/ShareStory',
            type: 'POST',
            data: { mission_id: mission, title: title, mystory: mystory, media: media},
            success: function (result) {
                window.location.reload();
                alert("Your story has been saved as draft");
            },
            error: function () {
                console.log("Error updating variable");
            }
        })

    }
}
function validate() {
    mission = parseInt($('.form-select').find(':selected').val())
    title = $('.title').val()
 
    mystory = CKEDITOR.instances.editor.getData();
    video_url = $('.video').val()
    if (video_url.trim().length > 3) {
        media.push(video_url)
    }
    $('.gallary').find('.main-image').each(function (i, item) {
        media.push(item.src)
    }
    )
    if (mission == 0) {
        $('#mission').removeClass('d-none').addClass('d-block')
    }
    else {
        $('#mission').removeClass('d-block').addClass('d-none')
    }
    if (title.trim().length < 50) {
        $('#title').removeClass('d-none').addClass('d-block')
    }
    else {
        $('#title').removeClass('d-block').addClass('d-none')
    }
    if (title.trim().length > 255) {
        $('#title-big').removeClass('d-none').addClass('d-block')
    }
    else {
        $('#title-big').removeClass('d-block').addClass('d-none')
    }

    if (mystory.trim().length < 70) {
        $('#mystory').removeClass('d-none').addClass('d-block')

    }
    else {
        $('#mystory').removeClass('d-block').addClass('d-none')

    }
    if (mystory.trim().length > 40000) {
        $('#mystory-big').removeClass('d-none').addClass('d-block')

    }
    else {
        $('#mystory-big').removeClass('d-block').addClass('d-none')

    }
    if ($('.gallary').find('.main-image').length == 0) {
        $('#image').removeClass('d-none').addClass('d-block')

    }
    else {
        $('#image').removeClass('d-block').addClass('d-none')

    }
}

function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat)
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/')
}