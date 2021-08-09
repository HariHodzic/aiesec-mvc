$(document).ready(function () {
    var simple_checkbox = function (data, type, full, meta) {
        var is_checked = data == true ? "checked" : "";
        return '<input type="checkbox" class="checkbox" ' +
            is_checked + ' disabled=true />';
    }
    $('#officeDatatable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "ajax": {
            "url": "/Office/GetFilteredItems",
            "type": "POST",
            "datatype":"json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false,
            "orderable":false
        },
        {
            "targets": [6],
            "orderable": false
        }],
        "columns": [
            // For OfficeId
            { "data": "id", "name": "Id" },
            { "data": "address", "name": "Address", "autoWidth": true  },
            { "data": "capacity", "name": "Capacity", "autoWidth": true  },
            // For EstablishmentDate
            {
                "data": "establishmentDate", "name": "EstablishmentDate",
                "render": function (data) {
                    var date = new Date(data);
                    return date.toLocaleString();
                }
            },
            { "data": "city.name", "name": "City Name", "autoWidth": true },
            { "data": "active", "name": "Active", "autoWidth": true ,"render":simple_checkbox},
            {
                'data': null,
                'render': function (data, type, row) {
                    AddAjaxEvents();
                    return '<a class="btn btn-sm btn-outline-info" href=/Office/Edit?id=' + row.id + '>Edit</a> '
                        + " <a class='btn btn-sm btn-outline-info' data-toggle='modal' data-target='#layoutModal' ajax-request='yes' ajax-url='/Office/DetailsPartial?id="+row.id+"' modal-size='sm' ajax-result='modalBody'>Details</a> "
                        + " <a class='btn btn-sm btn-danger' ajax-request='delete' ajax-url='/Office/Remove?id="+row.id+"'>Delete</a>"
                }
            }
        ],
        language: {
            zeroRecords: "No matching records found"
        }
    });
});
