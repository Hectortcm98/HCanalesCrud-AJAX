



$(document).ready(function () {

    EmpleadoGetAll();
    EstadoGetAll();

    $(document).ready(function () {
        EmpleadoGetAll();
        EstadoGetAll();

        $('#btnForm').on("click", function () {
            if (ValidateForm()) {
                $.ajax({
                    url: "https://localhost:7213/api/Empleado/Empleado/Add",
                    type: "POST",
                    data: JSON.stringify({
                        NumeroNomina: $("#txtNomina").val(),
                        Nombre: $("#txtNombre").val(),
                        ApellidoPaterno: $("#txtApellidoPaterno").val(),
                        ApellidoMaterno: $("#txtApellidoMaterno").val(),
                        EntidadFederativa: {
                            IdEstado: parseInt($("#ddlEntidadFede").val()),
                            Estado: $("#ddlEntidadFede option:selected").text() // Agregar el nombre del estado
                        }
                    }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: "JSON",
                    success: function (result) {
                        $("#alertForm").hide(); // Ocultar la alerta del formulario
                        if (result.success) {
                            $("#closeModalForm").modal("hide"); // Ocultar el modal
                            $("#alertInfo").show().removeClass("alert-danger").addClass("alert-success").text("Nuevo empleado agregado");
                            EmpleadoGetAll(); // Actualizar la lista de empleados
                        } else {
                            $("#alertInfo").show().removeClass("alert-success").addClass("alert-danger").text("Error al guardar nuevo empleado");
                        }

                        setTimeout(function () {
                            $("#alertInfo").hide();
                        }, 6000);
                    },
                    error: function (error) {
                        console.log(error);
                        $("#alertForm").hide(); // Ocultar la alerta del formulario
                        $("#alertInfo").show().removeClass("alert-success").addClass("alert-danger").text("Error al conectar con el servidor");
                        setTimeout(function () {
                            $("#alertInfo").hide();
                        }, 6000);
                    }
                });
            }
        });

        $('#cancelForm').on("click", function () {
            $("#modalForm").modal("hide"); // Ocultar el modal
            setTimeout(function () {
                normalForm();
            }, 1000);
        });
    });



    $('#btnFormUpdate').on("click", function () {
        if (ValidateForm()) {
            $.ajax({
                url: "https://localhost:7213/api/Empleado/Empleado/Update",
                type: "PUT",
                data: JSON.stringify({
                    idEmpleado: $("#txtIdEmpleado").val(),
                   NumeroNomina: $("#txtNomina").val(),
                    Nombre: $("#txtNombre").val(),
                    ApellidoPaterno: $("#txtApellidoPaterno").val(),
                    ApellidoMaterno: $("#txtApellidoMaterno").val(),
                    EntidadFederativa: {
                        IdEstado: parseInt($("#ddlEntidadFede").val()),
                        Estado: $("#ddlEntidadFede option:selected").text() 
                    }
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: "JSON",
                success: function (result) {
                    $("#alertInfo").show();
                    if (result.success) {
                        $("#alertInfo").attr("class", "alert alert-success text-center");
                        $("#alertInfo").text("El empleado se ha actualizado");
                        EmpleadoGetAll();
                    } else {
                        $("#alertInfo").attr("class", "alert alert-danger text-center");
                        $("#alertInfo").text("Error al actualizar el empleado");
                    }
                    $("#cancelForm").click();
                    setTimeout(function () {
                        $("#alertInfo").hide();
                    }, 6000);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });
});

function EmpleadoGetAll() {
    $.ajax({
        url: "https://localhost:7213/api/Empleado/Empleado/GetAll",
        type: "GET",
        crossDomain: true,
        dataType: "JSON",
        success: function (result) {
            $("#showEmpleados").empty(); // Limpiar la tabla antes de agregar nuevos registros
            if (result.success) {
                $.each(result.data, function (i, data) {
                    $("#showEmpleados").append('<tr>' +
                        '<td>' + data.numeroNomina + '</td>' +
                        '<td>' + data.nombre + '</td>' +
                        '<td>' + data.apellidoPaterno + '</td>' +
                        '<td>' + data.apellidoMaterno + '</td>' +
                        '<td>' + data.entidadFederativa.estado + '</td>' +
                        '<td><button type="button" class="btn btn-outline-secondary" style="" onclick="Update(' + data.idEmpleado + ')">Editar</button></td>' +
                        '<td><button type="button" class="btn btn-outline-danger" onclick="Delete(' + data.idEmpleado + ')">Eliminar</button></td>' +
                        '</tr>'
                    );
                });
              
            } else {
                $("#showEmpleados").html('<tr class="text-center"><td colspan="7">' +
                    '<p> NO HAY EMPLEADOS</p >' +
                    '<img src="https://c0.klipartz.com/pngpicture/81/490/gratis-png-humano-al-lado-del-signo-de-interrogacion-icono-de-graficos-escalables-archivo-de-computadora-signo-de-interrogacion-thumbnail.png" width="150" height="150" />' +
                    '</td ></tr>');
            }
        },
       
    
        error: function (error) {
            console.log(error);
        }
    });
}

function EstadoGetAll() {
    $("#ddlEntidadFede").empty();
    $("#ddlEntidadFede").append("<option value='' selected>Selecciona una opción</option>");
    $.ajax({
        url: "https://localhost:7213/api/EntidadFederativa/Estado/GetAll",
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.success) {
                $.each(result.data, function (i, data) {
                    $("#ddlEntidadFede").append("<option value='" + data.idEstado + "'>" + data.estado + "</option>");
                });
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function Update(id) {
    $("#btnModal").click();
    $("#modalLabel").text("Editar Empleado");

    $.ajax({
        url: "https://localhost:7213/api/Empleado/Empleado/GetByid/" + id,
        type: "GET",
        success: function (result) {
            if (result.success) {
                $("#txtNomina").val(result.data.numeroNomina);
                $("#txtNombre").val(result.data.nombre);
                $("#txtApellidoPaterno").val(result.data.apellidoPaterno);
                $("#txtApellidoMaterno").val(result.data.apellidoMaterno);

                $($("#ddlEntidadFede")[0][0]).val(result.data.entidadFederativa.idEstado);
                $($("#ddlEntidadFede")[0][0]).text(result.data.entidadFederativa.nombre);

                $("#txtIdEmpleado").val(result.data.idEmpleado);

                $("#btnForm").hide();
                $("#btnFormUpdate").show();
            } else {

            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function Delete(id) {
    if (confirm("¿Estás seguro de eliminar el empleado de forma permanente?")) {
        $.ajax({
            url: "https://localhost:7213/api/Empleado/Empleado/Delete/" + id,
            type: "Delete",
            success: function (result) {
                if (result.success) {
                    EmpleadoGetAll();
                    Alert("success", "El empleado ha sido eliminado de forma permanente");
                } else {
                    Alert("danger", "Error al eliminar empleado");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}

///////// Validación del formulario campos requeridos.
function ValidateForm() {
    var check = true;
    //if ($("#txtNomina").val().length == 0) {
    //    $("#lblNomina").text("El nombre es requerido");
    //    $("#lblNomina").show();
    //    $("#txtNomina").addClass("is-invalid");
    //    check = false;
    //}
    if ($("#txtNombre").val().length == 0) {
        $("#lblNombre").text("El nombre es requerido");
        $("#lblNombre").show();
        $("#txtNombre").addClass("is-invalid");
        check = false;
    }
    if ($("#txtApellidoPaterno").val().length == 0) {
        $("#lblApellidoPaterno").text("El apellido es requerido");
        $("#lblApellidoPaterno").show();
        $("#txtApellidoPaterno").addClass("is-invalid");
        check = false;
    }
    if ($("#txtApellidoMaterno").val().length == 0) {
        $("#txtApellidoMaterno").text("El apellido es requerido");
        $("#txtApellidoMaterno").show();
        $("#txtApellidoMaterno").addClass("is-invalid");
        check = false;
    }
    if ($('#ddlEntidadFede').val().trim() === '') {
        $("#lblEntidadFede").text("Selecciona una opción, es requerido");
        $("#lblEntidadFede").show();
        $("#ddlEntidadFede").addClass("is-invalid");
        check = false;
    }
    if (!check) { $("#alertForm").show(); }

    return check;
}
function normalForm() {
    $("#modalLabel").text("Agregar Empleado");
    $("#alertForm").hide();

    $("#txtNombre").val('');
    $("#txtNombre").removeClass("is-invalid");
    $("#lblNombre").hide();

    $("#txtApellidoPaterno").val('');
    $("#txtApellidoPaterno").removeClass("is-invalid");
    $("#lblApellidoPaterno").hide();


    $("#txtApellidoMaterno").val('');
    $("#txtApellidoMaterno").removeClass("is-invalid");
   /* $("#txtApellidoMaterno").hide();*/

    $("#ddlEntidadFede").removeClass("is-invalid");
    $("#lblEntidadFede").hide();

    $("#txtNomina").val('');
    $("#txtNomina").removeClass("is-invalid");
    $("#lblNomina").hide();

    $("#btnForm").show();
    $("#btnFormUpdate").hide();

    $("#txtIdEmpleado").val('');

    EstadoGetAll();
}

function Alert(type, message) {
    $("#alertInfo").show();
    if (type === "success") {
        $("#alertInfo").attr("class", "alert alert-success text-center");
        $("#alertInfo").text(message);
    } else {
        $("#alertInfo").attr("class", "alert alert-danger text-center");
        $("#alertInfo").text(message);
    }
    setTimeout(function () {
        $("#alertInfo").hide();
    }, 6000);
}