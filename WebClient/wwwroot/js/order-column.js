$(document).ready(function () {
    $('#tableteam').DataTable();
});
new DataTable('#tableteam', {
    responsive: true,
    rowReorder: {
        selector: 'td:nth-child(2)'
    }
});
