$(document).ready(function () {
    loadDataTable(type,modulename,control,input);
});

function loadDataTable( type,modulename,control,input) {
     dataTable = $('#filterData').DataTable({
        "ajax": { url:'/filter/getall'},
        "columns": [
            { data: 'moduleName' },
            { data: 'controlName'},
            { data: 'actionName',"width":"15%" },
            { data: 'requestedon' },
            { data: 'responseon' },
            { data: 'uniqueid' },
            { data: 'usermasterid' },
            { data: 'cliendcode' },
            { data: 'uccid' },
    ]});
}
