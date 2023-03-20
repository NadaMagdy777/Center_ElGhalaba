//$(document).ready(function () {
//    $("#studentsDT").dataTable({
//        "serverSide": true,
//        "filter": true,
//        "ajax": {
//            "url": "api/RemoteAdmin/Students",
//            "method": "POST",
//            "dataType": "json"
//        },

//        "columnDefs": [{
//            "targets": [0],
//            "visible": false,
//            "searchable": false
//        }],
//        "columns": [
//            { "data": "id", "name": "Id", "autowidth": true },
//            { "data": "name", "name": "Name", "autowidth": true },
//            { "data": "email", "name": "Email", "autowidth": true },
//            { "data": "joinDate", "name": "JoinDate", "autowidth": true },
//            {
//                "render": function (data, type, row) {
//                    return 
//                    `<div>
//                        <a href="Student/Details/${row.id}" class="btn btn-primary")>Preview</a>
//                        <a href="#" class="btn btn-danger" onclick=deleteCustomer(${row.id})>Delete</a>
//                    </div>`
//                },
//                "orderable": false
//            }
//        ]
//    })
//})