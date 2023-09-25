$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
     dataTable = $('#tableData').DataTable({
        "ajax": { url:'/requestResp/getall'},
        "columns": [
            { data: 'moduleName' },
            { data: 'controlName'},
            { data: 'actionName' },
            { data: 'requestedon' },
            { data: 'responseon' },
            { data: 'totalRequest' },
            { data: 'averageTime'}
    ]});
}
