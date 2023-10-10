$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
     dataTable = $('#tblFilter').DataTable({
        "ajax": { url:'/Filter/getall'},
        "columns": [
            { data: 'Id' },
            { data: 'totalExceptions' },
           
    ]});
}
