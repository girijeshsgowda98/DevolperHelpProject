$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("topRequest")) {
        loadDataTable("topRequest");
    }
    loadDataTable();
});

function loadDataTable(status) {
     dataTable = $('#tableData').DataTable({
         "ajax": { url: '/requestResp/getall?status' + status },
        "columns": [
            { data: 'moduleName' },
            { data: 'controlName' },
            { data: 'actionName', "width": "15%" },
            { data: 'requestedon' },
            { data: 'responseon' },
            { data: 'totalRequest' },
            { data: 'averageTime' }
        ]
     });
}


