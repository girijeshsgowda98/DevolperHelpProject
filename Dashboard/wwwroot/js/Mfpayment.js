$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblMfpaymentData').DataTable({
       "columnDefs": [{
            "targets": [6, 7, 8, 9, 10,11],
            "searchable": false
        }],
        "ajax": { url:'/Mfpayment/getall'},
        "columns": [
            { data: 'id' },
            { data: 'clientid' },
            { data: 'ucid' },
            { data: 'orderid' },
            { data: 'apiname' },
            { data: 'inserteddate' },
            { data: 'status' },
            { data: 'remark' },
            { data: 'request', "width": "20%"},
            { data: 'response', "width": "20%" },
            { data: 'dionrequest', "width": "20%" },
            { data: 'dionresponse', "width": "20%" }
        ]
    });

    //-------------------------------------------------------------------------------
    // pop window in table
    var table = $('#tblMfpaymentData').DataTable();
    $('#tblMfpaymentData tbody').on('click', 'td', function () {
        if ($(this).text().length > 20) {
            var content = $(this).text();
            var popup = window.open('', '_blank', 'width=400,height=200');
            popup.document.open();
            popup.document.write('<pre>' + content + '</pre>');
            popup.document.close();
        }
    });
    /*$('#tblMfpaymentData tbody').on('click mouseover', 'td', function () {
        var cellData = table.cell(this).data();
        if (cellData.length > 20) { // Customize this threshold
            showPopup(cellData, this);
        }
    });

    function showPopup(content, cell) {
        $('#popup-content').html(content);
        $('#popup').css({
            top: $(cell).offset().top + 'px',
            left: $(cell).offset().left + 'px'
        }).show();
    }

    $('#popup').mouseleave(function () {
        $('#popup').hide();
    });*/

    //--------------------------------------------------------------------------
    // check box creation
    // Create the input elements for filters
    var table = $('#tblMfpaymentData').DataTable()

    $('#filterButton').on('click', function () {
        var inputValue = $('#idFilter').val();
        var lessThanChecked = $('#lessThanCheckbox').is(':checked');
        var greaterThanChecked = $('#greaterThanCheckbox').is(':checked');
        var equalToChecked = $('#equalToCheckbox').is(':checked');
        

        $.fn.dataTable.ext.search.pop(); // Remove previous custom filter

        if (inputValue !== '') {
            $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                var idValue = parseFloat(data[0]);

                var lessThanResult = !lessThanChecked || (lessThanChecked && idValue <= parseFloat(inputValue));
                var greaterThanResult = !greaterThanChecked || (greaterThanChecked && idValue >= parseFloat(inputValue));
                var equalToResult = !equalToChecked || (equalToChecked && idValue === parseFloat(inputValue));

                return lessThanResult && greaterThanResult && equalToResult;
            });
        }

        table.draw();
    });
    //--------------------------------------------------------------------------------------------------
}
   