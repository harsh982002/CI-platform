let pageindex = 0;
const loadstories = (stories) => {
    $('.stories').empty().append(stories)
}

const pagination = (page_index) => {
    pageindex = page_index - 1;
    $('.pagination li span').each(function (i, item) {
        item.classList.remove('page-active')
    })
    $(`#page-${page_index}`).addClass('page-active')
    $.ajax({
        url: '/Story/Story',
        type: 'POST',
        data: { page_index: page_index - 1 },
        success: function (result) {
            loadstories(result.next_stories.result)
        },
        error: function (e) {
            console.log(e);
        }
    })
}
const prev_page = () => {
    var current_page;
    $('.pagination li span').each(function (i, item) {
        if (item.classList.contains('page-active')) {
            current_page = i - 1;
            if (current_page !== 1) {
                $('.pagination li span').eq(i - 1).addClass('page-active')
                item.classList.remove('page-active')
            }
        }
    })
    if (current_page !== 1) {
        pageindex = current_page - 2
        $.ajax({
            url: '/Story/Story',
            type: 'POST',
            data: { page_index: current_page - 2 },
            success: function (result) {
                loadstories(result.next_stories.result)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}
const next_page = (max_page) => {
    var current_page;
    $('.pagination li span').each(function (i, item) {
        if (item.classList.contains('page-active')) {
            current_page = i - 1;
            if (current_page !== max_page) {
                $('.pagination li span').eq(i + 1).addClass('page-active')
                item.classList.remove('page-active')
                return false
            }
        }
    })
    if (current_page !== max_page) {
        pageindex = current_page
        $.ajax({
            url: '/Story/Story',
            type: 'POST',
            data: { page_index: current_page },
            success: function (result) {
                loadstories(result.next_stories.result)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}

const first_page = () => {
    var current_page;
    $('.pagination li span').each(function (i, item) {
        if (item.classList.contains('page-active')) {
            current_page = i - 1;
            if (current_page !== 1) {
                $('.pagination li span').eq(2).addClass('page-active')
                item.classList.remove('page-active')
            }
        }
    })
    if (current_page !== 1) {
        pageindex = 0
        $.ajax({
            url: '/Story/Story',
            type: 'POST',
            data: { page_index: 0 },
            success: function (result) {
                loadstories(result.next_stories.result)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}
const last_page = (max_page) => {
    var current_page;
    $('.pagination li span').each(function (i, item) {
        if (item.classList.contains('page-active')) {
            current_page = i - 1;
            if (current_page !== max_page) {
                $('.pagination li span').eq(max_page + 1).addClass('page-active')
                item.classList.remove('page-active')
                return false
            }
        }
    })
    if (current_page !== max_page) {
        pageindex = max_page - 1
        $.ajax({
            url: '/Story/Story',
            type: 'POST',
            data: { page_index: max_page - 1 },
            success: function (result) {
                loadstories(result.next_stories.result)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}