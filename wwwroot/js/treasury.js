$(document).ready(function () {

    // CREATE
    $('#createForm').submit(function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            url: '/TreasuryBP/Create',
            type: 'POST',
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    toastr.success('Record added successfully.');
                    var createModal = bootstrap.Modal.getInstance(document.getElementById('createModal'));
                    createModal.hide();
                    refreshTable();
                    form[0].reset();
                } else {
                    $('#createModalContent').html(response);
                }
            },
            error: function () {
                toastr.error('Error adding record.');
            }
        });
    });

    // EDIT - OPEN MODAL
    $(document).on('click', '.editBtn', function () {
        var id = $(this).data('id');
        $.get('/TreasuryBP/Edit/' + id, function (data) {
            $('#editModalContent').html(data);
            var editModal = new bootstrap.Modal(document.getElementById('editModal'));
            editModal.show();
        });
    });

    // EDIT - SUBMIT FORM
    $(document).on('submit', '#editForm', function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            url: '/TreasuryBP/Edit',
            type: 'POST',
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    toastr.success('Record updated successfully.');
                    var modal = bootstrap.Modal.getInstance(document.getElementById('editModal'));
                    modal.hide();
                    refreshTable();
                } else {
                    $('#editModalContent').html(response);
                }
            },
            error: function () {
                toastr.error('Error updating record.');
            }
        });
    });

    // DELETE
    $('#treasuryTable').on('click', '.deleteBtn', function () {
        let id = $(this).data('id');
        const token = $('input[name="__RequestVerificationToken"]').val();

        Swal.fire({
            title: 'Are you sure?',
            text: "This action cannot be undone!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/TreasuryBP/Delete',
                    type: 'POST',
                    data: {
                        __RequestVerificationToken: token,
                        id: id
                    },
                    success: function (response) {
                        if (response.success) {
                            Swal.fire('Deleted!', response.message, 'success');
                            refreshTable();
                        } else {
                            Swal.fire('Error!', response.message, 'error');
                        }
                    },
                    error: function () {
                        Swal.fire('Error!', 'There was a problem deleting the record.', 'error');
                    }
                });
            }
        });
    });

    // REFRESH TABLE
    function refreshTable() {
        $.get('/TreasuryBP/GetAll', function (data) {
            var tbody = '';
            data.data.forEach(function (item) {
                tbody += `<tr data-id="${item.securityId}">
                    <td>${item.maturity}</td>
                    <td>${item.securityId}</td>
                    <td>${item.tenorDays}</td>
                    <td>${item.discountPercentage}</td>
                    <td>
                        <button class="btn btn-success btn-sm mx-1 editBtn" data-id="${item.securityId}">
                            <i class="bi bi-pencil-square"></i> Edit
                        </button>
                        <button class="btn btn-danger btn-sm mx-1 deleteBtn" data-id="${item.securityId}">
                            <i class="bi bi-trash-fill"></i> Delete
                        </button>
                    </td>
                </tr>`;
            });
            $('#treasuryTable tbody').html(tbody);
        });
    }
});
