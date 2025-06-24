$(document).ready(function () {

    // Open Create Modal
    $('#btnShowCreateModal').on('click', function () {
        $.get('/FGNBond/Create', function (data) {
            $('#createModalContent').html(data);
            $('#createModal').modal('show');
        });
    });

    // Submit Create Form
    $('#createModalContent').on('submit', '#createForm', function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            url: '/FGNBond/Create',
            type: 'POST',
            data: form.serialize(),
            success: function (res) {
                if (res.success) {
                    toastr.success('Bond added successfully');
                    var modal = bootstrap.Modal.getInstance(document.getElementById('createModal'));
                    modal.hide();
                    refreshBondTable();
                } else {
                    $('#createModalContent').html(res);
                }
            },
            error: function () {
                toastr.error('Error saving bond');
            }
        });
    });

    // Open Edit Modal
    $(document).on('click', '.editBtn', function () {
        var id = $(this).data('id');
        $.get('/FGNBond/Edit/' + id, function (html) {
            $('#editModalContent').html(html);
            $('#editModal').modal('show');
        });
    });

    // Submit Edit Form
    $('#editModalContent').on('submit', '#editForm', function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            url: '/FGNBond/Edit',
            type: 'POST',
            data: form.serialize(),
            success: function (res) {
                if (res.success) {
                    toastr.success('Bond updated successfully');
                    var modal = bootstrap.Modal.getInstance(document.getElementById('editModal'));
                    modal.hide();
                    refreshBondTable();
                } else {
                    $('#editModalContent').html(res);
                }
            },
            error: function () {
                toastr.error('Error updating bond');
            }
        });
    });

    // Delete Bond
    $(document).on('click', '.deleteBtn', function () {
        var isin = $(this).data('id');
        Swal.fire({
            title: 'Are you sure?',
            text: 'This bond will be permanently deleted.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then(result => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/FGNBond/Delete',
                    type: 'POST',
                    data: { id: isin },
                    success: function (res) {
                        if (res.success) {
                            toastr.success('Bond deleted');
                            refreshBondTable();
                        } else {
                            toastr.error(res.message || 'Delete failed');
                        }
                    },
                    error: function () {
                        toastr.error('An error occurred while deleting');
                    }
                });
            }
        });
    });

    // Refresh Table
    function refreshBondTable() {
        $.get('/FGNBond/GetAll', function (data) {
            let tbody = '';
            data.forEach(item => {
                tbody += `<tr>
                    <td>${item.isin}</td>
                    <td>${item.maturity}</td>
                    <td>${item.yearsToMaturity}</td>
                    <td>${item.price}</td>
                    <td>${item.yieldToMaturity}</td>
                    <td>${item.coupon}</td>
                    <td>${item.security}</td>
                    <td>${item.published ? '<i class="bi bi-check-lg text-success"></i>' : ''}</td>
                    <td>
                        <button class="btn btn-sm btn-secondary editBtn" data-id="${item.isin}">Edit</button>
                        <button class="btn btn-sm btn-danger deleteBtn ms-1" data-id="${item.isin}">Delete</button>
                    </td>
                </tr>`;
            });
            $('#bondTable tbody').html(tbody);
        });
    }
});
