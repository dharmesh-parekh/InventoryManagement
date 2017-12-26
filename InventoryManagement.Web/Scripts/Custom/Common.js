var baseUrl = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');

$(function () { 

    $(document).ajaxError(function (event, jqxhr, settings, exception) {
        if (jqxhr.status == 401) {
            window.location.href = window.location.href;
        }
    });

    $('.modal-content').keypress(function (e) {
        if (e.which == 13) {
            //dosomething
            $(this).find("button[type=submit]").click();
        }
    });
    
    $(".modal").on('shown.bs.modal', function () {
        $(this).find(".form-control:visible:first").focus();
    });

});

function CloseModal(id) {
    if ($("#" + id).length > 0)
        $('#' + id).modal('hide');
}

function NavigateTo(link) {
    window.location.href = link;
}

function PreventExecution(e) {
    e.stopPropagation();
}

function DisableButton(form) {
    var button = $(form).find("button[type=submit]");
    if (button != null) {
        var icon = button.find("i");
        if ($(icon) != null)
            $(icon).removeClass().addClass("fa fa-spinner fa-spin");

        $(button).attr('disabled', 'disabled');
    }
}

function EnableButton(form) {
    var button = $(form).find("button[type=submit]");
    if (button != null) {
        var icon = button.find("i");
        if ($(icon) != null)
            $(icon).removeClass().addClass("fa fa-save");
        button.removeAttr('disabled');
    }
}

///General function to open DialogueBox
function LoadDialogueBox(contentURL, divID, dialogtitle, data) {

    var dialogInstance;
    $.ajax({
        url: contentURL,
        cache: false,
        async: false,
        data: data,
        success: function (result) {
            dialogInstance = BootstrapDialog.show({
                id: divID,
                size: BootstrapDialog.SIZE_WIDE,
                title: dialogtitle,
                type: BootstrapDialog.TYPE_SUCCESS,
                closeByBackdrop: false,
                closeByKeyboard: false,
                draggable: true,
                message: $('<div id="modal-dialog"></div>').html(result)
            });

        },
        complete: function () {

        }
    });
    return dialogInstance;
}
function LoadDialogueBoxSmall(contentURL, divID, dialogtitle, data) {

    var dialogInstance;
    $.ajax({
        url: contentURL,
        cache: false,
        async: false,
        data: data,
        success: function (result) {
            dialogInstance = BootstrapDialog.show({
                id: divID,
                size: BootstrapDialog.SIZE_SMALL,
                title: dialogtitle,
                type: BootstrapDialog.TYPE_SUCCESS,
                closeByBackdrop: false,
                closeByKeyboard: false,
                draggable: true,
                message: $('<div id="modelWindow"></div>').html(result)
            });

        },
        complete: function () {

        }
    });
    return dialogInstance;
}

function HandleSaveResponseWithValidationSummary(form, data, parentContainerSelector) {  
    if (data.Success) {
        HideValidation();
    }
    else {
        ShowCustomValidationSummary(form, data.Message, parentContainerSelector);
    }
    RefreshMessages();
    EnableButton(form);
}

function HandleSaveResponseWithRedirectandValidationSummary(form, data, parentContainerSelector, url) {
    if (data.Success) {
        NavigateTo(url);
    }
    else {
        ShowCustomValidationSummary(form, data.Message, parentContainerSelector);
    }
    EnableButton(form);
}

function ShowCustomValidationSummary(form, message, parentContainerSelector) {  
    var validationSummary = null;
    if (parentContainerSelector != null) {
        validationSummary = parentContainerSelector.find($(form).find(".validation-summary-errors"));
    }
    else {
        validationSummary = $($(form).find(".validation-summary-errors"));
    }
    if ($(".alert") != null) {
        validationSummary.closest(".alert").show();
    }
    validationSummary.html(message);
    $(validationSummary).show();
}


function HandleResponse(data) {
    RefreshMessages();
}

function HandleRedirectResponse(data, url) {
    if (data) {
        NavigateTo(url);
    }
    EnableButton(form);
    RefreshMessages();
}

function RefreshMessages() {   
    $.ajax({
        url: baseUrl + '/Message/Messages',
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var message = data[i];
                FireJGrowl(message.Text, message.TypeText);
            }
        }
    });
}

function FireJGrowl(messageText, messageType) {    
    $.jGrowl(messageText, { theme: messageType, life: 3000, position: 'top-right' });
}

function HideValidation() {
    if ($(".alert") != null) {
        $(".alert").hide();
    }
}