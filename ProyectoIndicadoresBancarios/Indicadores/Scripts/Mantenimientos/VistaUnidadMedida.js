
function requestDescargar() {
    tableToExcel('myTable', 'Hoja1');
}

//$('#btnCompletar').on('click', requestCompletar);
$('#btnDescargar').on('click', requestDescargar);

