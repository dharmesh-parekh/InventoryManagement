var UserService = (function () {

    var instance;
    var baseUrl = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');

    function InitializeUserDatatable() {
        var columns = [
          { "mData": "FirstName", "sType": "string", "sName": "FirstName", "bSortable": true },
          { "mData": "LastName", "sType": "string", "sName": "LastName", "bSortable": true },
          { "mData": "Email", "sType": "string", "sName": "Email", "bSortable": true },
          { "mData": "MobileNo", "sType": "string", "sName": "MobileNo", "bSortable": true },
          { "mData": "Role", "sType": "string", "sName": "Role", "bSortable": true },
          { "mData": "StrIsActive", "sType": "string", "sName": "StrIsActive", "bSortable": true },
          {
              "mData": "UserId",
              "bSortable": false,
              "defaultContent": "",
              "mRender": function (data, type, row) {
                  var html =
                      "<div class='btn-toolbar'>" +
                          "<button class='btn btn-sm btn-success' onclick='UserJS.LoadUserPopup(\"" + row.UserId + "\")'> Edit </button>" +
                          "<button class='btn btn-sm btn-danger' onclick='UserJS.DeleteUser(\"" + row.UserId + "\")'> Delete </button> </div>";
                  return html;
              }
          }
        ];

        $('#tblUser').dataTable().fnDestroy();
        var url = baseUrl + "/User/GetUserList";
        oTable = $('#tblUser').dataTable({
            "searching": true,
            "bProcessing": true,
            "bServerSide": true,
            "sAjaxSource": url,
            "fnServerParams": function (aoData) {
            },
            "aoColumns": columns,
            "dom": 'T<"clear">lfrtip',
            "bAutoWidth": false,
        });
        $('<a class="btn btn-sm btn-success " style="margin-left:5px;"  onclick="UserJS.LoadUserPopup(\'' + 0 + '\')">  Add New <i class="fa fa-plus"></i> </a>').appendTo('div.dataTables_filter');
    }

    function LoadUserPopup(userId) {
        var url = baseUrl + "/User/UserModel";
        var data = { userId: userId };
        var title = userId > 0 ? 'Edit User' : 'Add User';
        LoadDialogueBox(url, "userPopup", title, data);
    }

    function SaveUser(e, form) {
        var url = baseUrl + "/User/Save";
        if ($(form).validate().form()) {
            $.post(url,
                $(form).serialize(),
                function (data) {
                    if (data != null) {
                        HandleSaveResponseWithValidationSummary(form, data, null);
                        if (data.Success) {
                            CloseModal("userPopup");
                            InitializeUserDatatable();
                        }
                    }
                }
            );
        }
        e.preventDefault();
    }

    function DeleteUser(userId) {
        bootbox.confirm("Are you sure want to delete this user?", function (confirm) {
            if (confirm) {
                var url = baseUrl + "/User/Delete";
                $.ajax({
                    url: url,
                    type: "POST",
                    data: { userId: userId },
                    success: function (result) {
                        HandleResponse(result);
                        if (result.Success)
                            InitializeUserDatatable();
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText);
                    },
                    complete: function () {
                    }
                });
            }
        });
    }



    function Init() {
        return {
            InitializeUserDatatable: function () {
                return InitializeUserDatatable();
            },
            LoadUserPopup: function (userId) {
                return LoadUserPopup(userId);
            },
            SaveUser: function (e, form) {
                return SaveUser(e, form);
            },
            DeleteUser: function (userId) {
                return DeleteUser(userId);
            },

        }
    }

    return {
        // Get the Singleton instance if one exists or create one if it doesn't
        getInstance: function () {
            if (!instance) {
                instance = Init();
            }
            return instance;
        }
    };


})();
var UserJS = UserService.getInstance();