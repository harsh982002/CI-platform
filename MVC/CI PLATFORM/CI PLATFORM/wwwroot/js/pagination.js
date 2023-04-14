const pagination = (page_index) => {
    var selected = $('#sort').find(':selected').text();
    pageindex = page_index - 1;
    $('.pagination li span').each(function (i, item) {
        item.classList.remove('page-active')
    })
    $(`#page-${page_index}`).addClass('page-active')
    $.ajax({
        url: '/Admin/Mission',
        type: 'POST',
        data: { page_index: page_index - 1, countries: countries, cities: cities, themes: themes, skills: skills, sort_by: selected.toLowerCase() },
        success: function (result) {
            loadmissions(result.mission.result, result.length)
        },
        error: function (e) {
            console.log(e);
        }
    })
}
const prev = () => {
    var selected = $('#sort').find(':selected').text();
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
            url: '/Admin/Mission',
            type: 'POST',
            data: { page_index: current_page - 2, countries: countries, cities: cities, themes: themes, skills: skills, sort_by: selected.toLowerCase() },
            success: function (result) {
                loadmissions(result.mission.result, result.length)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}
const next = (max_page) => {
    var selected = $('#sort').find(':selected').text();
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
            url: '/Admin/Mission',
            type: 'POST',
            data: { page_index: current_page, countries: countries, cities: cities, themes: themes, skills: skills, sort_by: selected.toLowerCase() },
            success: function (result) {
                loadmissions(result.mission.result, result.length)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}

const first_page = () => {
    var selected = $('#sort').find(':selected').text();
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
            url: '/Admin/Mission',
            type: 'POST',
            data: { page_index: 0, countries: countries, cities: cities, themes: themes, skills: skills, sort_by: selected.toLowerCase() },
            success: function (result) {
                loadmissions(result.mission.result, result.length)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}
const last_page = (max_page) => {
    var selected = $('#sort').find(':selected').text();
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
            url: '/Admin/Mission',
            type: 'POST',
            data: { page_index: max_page - 1, countries: countries, cities: cities, themes: themes, skills: skills, sort_by: selected.toLowerCase() },
            success: function (result) {
                loadmissions(result.mission.result, result.length)
            },
            error: function (e) {
                console.log(e);
            }
        })
    }
}
