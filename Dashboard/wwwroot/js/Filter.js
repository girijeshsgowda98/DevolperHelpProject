$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
     dataTable = $('#filterData').DataTable({
        "ajax": { url:'/filter/getall'},
        "columns": [
            { data: 'moduleName' },
            { data: 'controlName'},
            { data: 'actionName',"width":"15%" },
            { data: 'requestedon' },
            { data: 'responseon' },
            { data: 'totalRequest' },
            { data: 'averageTime'}
    ]});
}
