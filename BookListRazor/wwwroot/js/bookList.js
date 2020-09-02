var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": 'api/Book',
            "type": "Get",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "name","width": "20%"
            },
            {
                "data": "author", "width": "20%"
            },
            {
                "data": "isbn", "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/BookList/Edit?id=${data}" class='btn btn-success' style='cursor:pointer; width: 100px'>Edit</a>
                                &nbsp
                                <a class='btn btn-danger' style='cursor:pointer; width: 100px' onClick=Delete('/api/Book?id=${data}')>Delete</a>
                            </div>
                        `
                },
                "width": "40%"

            }
        ]
        ,
        "language": {
            "emptyTable": "No Data Found",
           

        },

         "width": "100%"

    })
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                        
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}