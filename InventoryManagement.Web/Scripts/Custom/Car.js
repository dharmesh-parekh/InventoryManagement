var CarService = (function () {

    var instance;
    var baseUrl = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');

    function InitializeCarDatatable() {
        var columns = [
          { "mData": "Brand", "sType": "string", "sName": "Brand", "bSortable": true },
          { "mData": "Model", "sType": "string", "sName": "Model", "bSortable": true },
          { "mData": "Year", "sType": "string", "sName": "Year", "bSortable": true },
          { "mData": "Price", "sType": "string", "sName": "Price", "bSortable": true },
          { "mData": "StrNew", "sType": "string", "sName": "StrNew", "bSortable": true },
          {
              "mData": "CarId",
              "bSortable": false,
              "defaultContent": "",
              "mRender": function (data, type, row) {
                  var html =
                      "<div class='btn-toolbar'>" +
                          "<button class='btn btn-sm btn-success' onclick='CarJS.LoadCarPopup(\"" + row.CarId + "\")'> Edit </button>" +
                          "<button class='btn btn-sm btn-danger' onclick='CarJS.DeleteCar(\"" + row.CarId + "\")'> Delete </button> </div>";
                  return html;
              }
          }
        ];

        $('#tblCar').dataTable().fnDestroy();
        var url = baseUrl + "/Car/GetCarList";
        oTable = $('#tblCar').dataTable({
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
        $('<a class="btn btn-sm btn-success " style="margin-left:5px;"  onclick="CarJS.LoadCarPopup(\'' + 0 + '\')">  Add New <i class="fa fa-plus"></i> </a>').appendTo('div.dataTables_filter');
    }

    function LoadCarPopup(carId) {
        var url = baseUrl + "/Car/CarModel";
        var data = { carId: carId };
        var title = carId > 0 ? 'Edit Car' : 'Add Car';
        LoadDialogueBox(url, "CarPopup", title, data);
    }

    function SaveCar(e, form) {      
        var url = baseUrl + "/Car/Save";
        if ($(form).validate().form()) {
            $.post(url,
                $(form).serialize(),
                function (data) {
                    if (data != null) {
                        HandleSaveResponseWithValidationSummary(form, data, null);
                        if (data.Success) {
                            CloseModal("CarPopup");
                            InitializeCarDatatable();
                        }
                    }
                }
            );
        }
        e.preventDefault();
    }

    function DeleteCar(carId) {
        bootbox.confirm("Are you sure want to delete this Car?", function (confirm) {
            if (confirm) {
                var url = baseUrl + "/Car/Delete";
                $.ajax({
                    url: url,
                    type: "POST",
                    data: { carId: carId },
                    success: function (result) {
                        HandleResponse(result);
                        if (result.Success)
                            InitializeCarDatatable();
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
            InitializeCarDatatable: function () {
                return InitializeCarDatatable();
            },
            LoadCarPopup: function (carId) {
                return LoadCarPopup(carId);
            },
            SaveCar: function (e, form) {
                return SaveCar(e, form);
            },
            DeleteCar: function (carId) {
                return DeleteCar(carId);
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
var CarJS = CarService.getInstance();