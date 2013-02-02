/// <reference path="jquery-1.8.1.min.js" />
/// <reference path="jquery-ui-1.8.23.custom.min.js" />
/// <reference path="jquery.validate.min.js" />

$(document).on('ready', function () {
    $('#showCategoryButton').remove();
    $('#homeButton').remove();
    $('#selectedCategory').css('width', '100%');

    jQuery.validator.addMethod('namePattern', function (value, element) {
        return this.optional(element) || /^([0-9a-zA-ZąęółśćńźżĄĘÓŁŚĆŃŹŻ_+,.$€@*-]+)$/.test(value);
    }, 'Tylko cyfry, litery i znaki: ( _ + - . $ € @ * )');

    jQuery.validator.addMethod('moneyPattern', function (value, element) {
        return this.optional(element) || /^(\d{1,7},\d{})$/.test(value);
    }, 'Tylko cyfry, litery i znaki: ( _ + - . $ € @ * )');

    jQuery.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }

    jQuery.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\,]\d{3})+)(?:[\,]\d+)?$/.test(value);
    }
});

$(document).on('change', '#addImageForm', function () {
    $(this).validate({
        rules: {
            file: {
            }
        },
        messages: {
            file: {
                required: "Nazwa jest wymagana",
                minlength: "Minimum 3 znaki"
            }
        }
    });
});

$(document).on('change', '#createCategoryForm', function () {
    $(this).validate({
        rules: {
            Name: {
                required: true,
                namePattern: true,
                minlength: 3,
                maxlength: 25
            }
        },
        messages: {
            Name: {
                required: "Nazwa jest wymagana",
                minlength: "Minimum 3 znaki"
            }
        }
    });
});

$(document).on('change', '#editCategoryForm', function () {
    $(this).validate({
        rules: {
            Name: {
                required: true,
                namePattern: true,
                minlength: 3,
                maxlength: 25
            }
        },
        messages: {
            Name: {
                required: "Nazwa jest wymagana",
                minlength: "Minimum 3 znaki"
            }
        }
    });
});

$(document).on('change', '#createProductForm', function () {
    $(this).validate({
        rules: {
            Name: {
                required: true,
                namePattern: true,
                minlength: 3,
                maxlength: 25
            },
            Count: {
                digits: true,
                range: [0, 1000]
            },
            Price: {
                number: true,
                range: [0, 1000000]
            }
        },
        messages: {
            Name: {
                required: "Nazwa jest wymagana",
                minlength: "Minimum 3 znaki"
            },
            Count: {
                digits: "Podaj liczbę całkowitą",
                range: "Podaj watrość z zakresu od 0 do 1000"
            },
            Price: {
                number: 'Podaj liczbę oddzieloną przecinkiem ","',
                range: "Podaj liczbę z zakresu od 0 do 1000000"
            }
        }
    });
});

$(document).on('change', '#editProductForm', function () {
    $(this).validate({
        rules: {
            Name: {
                required: true,
                namePattern: true,
                minlength: 3,
                maxlength: 25
            },
            Count: {
                digits: true,
                range: [0, 1000]
            },
            Price: {
                number: true,
                range: [0, 1000000]
            }
        },
        messages: {
            Name: {
                required: "Nazwa jest wymagana",
                minlength: "Minimum 3 znaki"
            },
            Count: {
                digits: "Podaj liczbę całkowitą",
                range: "Podaj watrość z zakresu od 0 do 1000"
            },
            Price: {
                number: 'Podaj liczbę oddzieloną przecinkiem ","',
                range: "Podaj liczbę z zakresu od 0 do 1000000"
            }
        }
    });
});

var upload = function () {
    var file = document.getElementById('file').files[0];
    if (!file) {
        $('#file_validationMessage').attr('class', 'field-validation-error').text('Wybierz jakiś plik');
        return false;
    }
    if (file.size > 100 * 1024) {
        $('#file_validationMessage').attr('class', 'field-validation-error').text('Maksymalny rozmiar pliku to 100 kB');
        return false;
    }
    if (file.type == 'image/jpeg' || file == 'image/png') {
        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (this.readyState == 4) {
                if (this.status == 200) {
                    if (xhr.responseText.search('"success":true') > 0)
                        $('#dialog').load('/Product/Details/' + $('#modelID').val() + ' #productDetailsView');
                    else
                        alert(xhr.responseText);
                } else {
                    alert('Błąd: ' + this.status + '\n' + this.statusText);
                }
            }
        }

        xhr.open('POST', $('#addImageForm').attr('action'), true);
        xhr.setRequestHeader("Cache-Control", "no-cache");
        xhr.setRequestHeader('Content-Type', file.type);
        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
        xhr.setRequestHeader('X-File-Name', file.name);
        xhr.send(file);
        return false;
    }
    $('#file_validationMessage').attr('class', 'field-validation-error').text('Tylko pliki JPEG i PNG');
    return false;
};

$(document).on('click', '#aboutButton', function () {
    $('#dialog').dialog('destroy').dialog({
        autoOpen: false,
        resizable: false,
        height: 'auto',
        width: 'auto'
    }).load($(this).attr('href') + ' #aboutC').dialog('open').dialog('widget').position({
        my: 'right',
        at: 'left',
        of: $(this)
    });
    return false;
});

$(document).on('click', '#addImageButton', function () {
    $('#dialog').load($(this).attr('href') + ' #addImageForm');
    return false;
});

$(document).on('click', '#deleteImageButton', function () {
    var url = $(this).attr('href') + '?ajax=true';
    var id = $('#modelID').val();
    $('#dialog').html('Na pewno ?').dialog({
        buttons: {
            'Tak': function () {
                $.ajax({
                    cache: false,
                    type: 'GET',
                    dataType: 'json',
                    url: url,
                    success: function (response) {
                        if (response.success) {
                            $('#dialog').dialog('destroy').dialog({
                                autoOpen: false,
                                resizable: false,
                                height: 'auto',
                                width: 'auto'
                            }).load('/Product/Details/' + id + ' #productDetailsView').dialog('open');
                        } else {
                            alert(response.error);
                        }
                    },
                    error: function () {
                        alert('Błąd Ajax!\n' + url);
                    }
                });
            },
            'Nie': function () {
                $(this).dialog('destroy').dialog({
                    autoOpen: false,
                    resizable: false,
                    height: 'auto',
                    width: 'auto'
                }).load('/Product/Details/' + id + ' #productDetailsView').dialog('open');
            }
        }
    });
    return false;
});

$(document).on('click', '.productDetailsLink', function () {
    $('#dialog').dialog('destroy').dialog({
        autoOpen: false,
        resizable: false,
        height: 'auto',
        width: 'auto'
    }).load($(this).attr('href') + ' #productDetailsView').dialog('open').dialog('widget').position({
        my: 'left',
        at: 'right',
        of: $(this)
    });
    return false;
});

$(document).on('click', '#addProductButton', function () {
    $('#dialog').dialog('destroy').dialog({
        autoOpen: false,
        resizable: false,
        height: 'auto',
        width: 'auto'
    }).load($(this).attr('href') + ' #createProductForm').dialog('open').dialog('widget').position({
        my: 'left',
        at: 'right',
        of: $(this)
    });
    return false;
});

$(document).on('click', '#editProductButton', function () {
    $('#dialog').load($(this).attr('href') + ' #editProductForm');
    return false;
});

$(document).on('click', '#deleteProductButton', function () {
    var url = $(this).attr('href') + '?ajax=true';
    $('#dialog').html('Na pewno ?').dialog({
        buttons: {
            'Tak': function () {
                $.ajax({
                    cache: false,
                    type: 'GET',
                    dataType: 'json',
                    url: url,
                    success: function (response) {
                        if (response.success) {
                            $('#categoryContent').load('/Product', { id: $('#selectedCategory').val() });
                            $('#dialog').dialog('close');
                        } else {
                            alert(response.error);
                        }
                    },
                    error: function () {
                        alert('Błąd Ajax!\n' + url);
                    }
                });
            },
            'Nie': function () { $(this).dialog('close'); }
        }
    });
    return false;
});

$(document).on('click', '#createCategoryButton', function () {
    var url = $(this).attr('href') + " #createCategoryForm";
    $('#dialog').dialog('destroy').dialog({
        autoOpen: false,
        resizable: false,
        height: 'auto',
        width: 'auto'
    }).load(url).dialog('open').dialog('widget').position({
        my: 'left',
        at: 'right',
        of: $(this)
    });
    return false;
});

$(document).on('click', '#editCategoryButton', function () {
    var id = $('#selectedCategory').val();
    if (id > 0) {
        $('#dialog').dialog('destroy').dialog({
            autoOpen: false,
            resizable: false,
            height: 'auto',
            width: 'auto'
        }).load('/Category/Edit/' + id + " #editCategoryForm").dialog('open').dialog('widget').position({
            my: 'left',
            at: 'right',
            of: $(this)
        });
    }
    return false;
});

$(document).on('click', '#deleteCategoryButton', function () {
    var id = $('#selectedCategory').val();
    if (id > 0) {
        var cat = $("#selectedCategory option[value='" + $('#selectedCategory').val() + "']").text();
        $('#dialog').dialog('destroy').html('Usunąć ' + cat + ' ?').dialog({
            autoOpen: true,
            modal: true,
            resizable: false,
            height: 'auto',
            width: 'auto',
            buttons: {
                "Tak": function () {
                    $.ajax({
                        cache: false,
                        type: 'GET',
                        dataType: 'json',
                        traditional: true,
                        url: '/Category/Delete/' + id + '?ajax=true',
                        success: function (response) {
                            if (response.success) {
                                $('#selectedCategory option').each(function () {
                                    if ($(this).val() == id) {
                                        $(this).remove();
                                    }
                                });
                                id = $('#selectedCategory').val();
                                if (id > 0) {
                                    $('#categoryContent').load('/Product', { id: id });
                                    $('#categoryDesc').load('/Category/GetCategoryDescription/', { id: id });
                                }
                                else {
                                    $('#categoryContent').html('');
                                    $('#editCategoryButton').hide();
                                    $('#deleteCategoryButton').hide();
                                }
                                $('#dialog').dialog('close');
                            }
                            else {
                                alert(response.error);
                            }
                        },
                        error: function () {
                            alert('Błąd ajax.');
                        }
                    });
                },
                "Nie": function () { $(this).dialog('close') }
            }
        }).dialog('widget').position({
            my: 'left',
            at: 'right',
            of: $(this)
        });
    }
    return false;
});

$(document).on('click', '#cancelForm', function () {
    $('#dialog').dialog('close');
    return false;
});

$(document).on('submit', '#createCategoryForm', function () {
    $.ajax({
        cache: false,
        url: $(this).attr('action'),
        type: 'POST',
        data: $(this).serialize(),
        dataType: 'json',
        traditional: true,
        success: function (response) {
            if (response.success) {
                $('#selectedCategory').append($('<option></option>').val(response.id).text(response.name).attr('selected', 'selected'));
                $('#categoryContent').load('/Product', { id: response.id });
                $('#editCategoryButton').show();
                $('#deleteCategoryButton').show();
                $('#categoryDesc').load('/Category/GetCategoryDescription/' + response.id);
                $('#dialog').dialog('close');
            } else {
                if (response.errors.length > 0) {
                    var msg = "";
                    for (i = 0; i < response.errors.length; ++i) {
                        msg += response.errors[i].ErrorMessage + '\n';
                    }
                    alert(msg);
                } else {
                    alert(response.error)
                }
            }
        },
        error: function () {
            alert('Błąd Ajax');
        }
    });
    return false;
});

$(document).on('submit', '#editCategoryForm', function () {
    $.ajax({
        cache: false,
        url: $(this).attr('action'),
        type: 'POST',
        data: $(this).serialize(),
        dataType: 'json',
        traditional: true,
        success: function (response) {
            if (response.success) {
                $('#selectedCategory option').each(function () {
                    if ($(this).attr('value') == response.id) {
                        $(this).text(response.name);
                    }
                });
                $('#categoryDesc').load('/Category/GetCategoryDescription/' + response.id);
                $('#dialog').dialog('close');
            } else {
                alert(response.errors);
            }
        }
    });
    return false;
});

$(document).on('submit', '#createProductForm', function () {
    $.ajax({
        cache: false,
        url: $(this).attr('action'),
        type: 'POST',
        data: $(this).serialize(),
        dataType: 'json',
        traditional: true,
        success: function (response) {
            if (response.success) {
                $('#categoryContent').load('/Product', { id: $('#selectedCategory').val() });
                $('#dialog').dialog('close');
            } else {
                var msg = "";
                for(i = 0; i < response.errors.length; ++i)
                {
                    msg += response.errors[i].ErrorMessage + '\n';
                }
                alert(msg);
            }
        },
        error: function () {
            alert('Błąd Ajax!');
        }
    });
    return false;
});

$(document).on('submit', '#editProductForm', function () {
    $.ajax({
        cache: false,
        url: $(this).attr('action'),
        type: 'POST',
        data: $(this).serialize(),
        dataType: 'json',
        traditional: true,
        success: function (response) {
            if (response.success) {
                $('#categoryContent').load('/Product', { id: $('#selectedCategory').val() });
                $('#dialog').dialog('close');
            } else {
                alert(response.msg);
            }
        },
        error: function () {
            alert('Błąd Ajax!');
        }
    });
    return false;
});

$(document).on('submit', '#addImageForm', function () {
    return false;
});

$(document).on('submit', '#categoryIndexForm', function () {
    return false;
});

$(document).on('change', '#selectedCategory', function () {
    var val = $('#selectedCategory').val();
    $('#categoryContent').load('/Product', { id: val });
    $('#categoryDesc').load('/Category/GetCategoryDescription/', { id: val });
    return false;
});
