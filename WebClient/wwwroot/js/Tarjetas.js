let NuevaTarjetaModal = new bootstrap.Modal($('#NuevaTarjetaModal'));
let NuevaCompraModal = new bootstrap.Modal($('#NuevaCompraModal'));
let NuevoPagoModal = new bootstrap.Modal($('#NuevoPagoModal'));
let HistorialTransaccionesModal = new bootstrap.Modal($('#HistorialTransaccionesModal'));
let ListaComprasMesModal = new bootstrap.Modal($('#ListaComprasMesModal'));
let EstadoCuentaModal = new bootstrap.Modal($('#EstadoCuentaModal'));
let PreviewPdfModal = new bootstrap.Modal($('#PreviewPdfModal'));

const PostNuevaTarjeta = () => {
    $('.btnGuardarNuevaTarjeta').prop('disabled', true);
    let titular = $('#TitularTarjeta').val(),
        numero = $('#NumeroTarjeta').val(),
        limite = $('#LimiteTarjeta').val(),
        fechaVencimiento = $('#FechaVencimientoTarjeta').val(),
        porcentajePagoMinimo = $('#PorcentajePagoMinimoTarjeta').val(),
        porcentajeInteres = $('#PorcentajeInteresTarjeta').val();
    //Se valida el form antes de enviar al petición
    if (titular.trim() == '' || numero.trim() == '' || limite.trim() == '' || fechaVencimiento.trim() == '' || porcentajePagoMinimo == '' || porcentajeInteres == '')
        return Utilities.sweetAlertWarning('Hay campos vacios, revisa el formulario');
    //Construye el objeto del request
    const Tarjeta = { titular, numero, limite, fechaVencimiento, porcentajePagoMinimo, porcentajeInteres };
    const jTarjeta = JSON.stringify(Tarjeta);
    PostApiNuevaTarjeta(jTarjeta);
};

const PostApiNuevaTarjeta = (jTarjeta) => {
    HttpRequest.PostAsync(`api/Tarjeta/CrearNuevaTarjetaAsync`, jTarjeta, (resp) => {
        console.log(resp);
        if (resp.textStatus == "success") {
            $('.btnGuardarNuevaTarjeta').prop('disabled', false);
            ClearInputsNuevaTarjeta();
            NuevaTarjetaModal.hide();
            Utilities.sweetAlertSuccess('Se guardó con exito');
            GetAllTarjetas();
        } else {
            Utilities.sweetAlertWarning(`No se pudo guardar: ${resp.xhr.responseJSON.exception.Message}`);
        }
    });
};

const GetAllTarjetas = (route = 'api/Tarjeta/ObtenerTarjetasAsync') => {
    HttpRequest.GetAsync(route, null, (resp) => {
        console.log(resp);
        if (resp.textStatus == 'success') {
            //resp.data = JSON.parse(resp.data);
            Utilities.GenerarTabla({
                idDiv: 'divForTableTarjetas',
                idTabla: 'dtTarjetas',
                cabeceras: ['Titular', 'Numero', 'Limite', 'Saldo actual', 'Saldo disponible', 'Fecha vencimiento', 'Acciones'],
                estilos: 'width:100%',
                claseCss: 'table table-sm table-striped table-hover',
                classTHead: '',
                propiedadesPersonalizadas: ''
            }, {
                data: resp.data,
                language: lenguajeEspanniolDataTable,
                columns: [
                    { data: 'titular' },
                    { data: 'numero' },
                    { data: (data, type, full, meta) => `$${data.limite}` },
                    { data: (data, type, full, meta) => `$${data.saldoActual}` },
                    { data: (data, type, full, meta) => `$${data.saldoDisponible}` },
                    { data: (data, type, full, meta) => Utilities.formatFechaESJS(data.fechaVencimiento) },
                    {
                        data: 'id',
                        render: (data, type, full, meta) => {
                            return `<a class="btn btn-dark btn-sm tooltipp top" onclick="CompraModal(${data});">
                                        <i class="fa-solid fa-basket-shopping"></i><span class="tiptext">Nueva compra</span>
                                    </a>
                                    <a class="btn btn-dark btn-sm tooltipp top" onclick="PagoModal(${data});">
                                        <i class="fa-solid fa-money-bill-1"></i><span class="tiptext">Nuevo pago</span>
                                    </a>
                                    <a class="btn btn-dark btn-sm tooltipp top" onclick="HistorialTransacciones(${data});">
                                        <i class="fa-solid fa-file-lines"></i><span class="tiptext">Historial de transacciones</span>
                                    </a>
                                    <a class="btn btn-dark btn-sm tooltipp top" onclick="EstadoCuenta(${data});">
                                        <i class="fa-solid fa-file-invoice-dollar"></i><span class="tiptext">Estado de cuenta</span>
                                    </a>
                                    <a class="btn btn-dark btn-sm tooltipp top" onclick="ExportarExcelCompras(${data});">
                                        <i class="fa-solid fa-file-excel"></i><span class="tiptext">Exportar excel</span>
                                    </a>`;
                        }
                    },
                ],
                order: [[0, "desc"]]
            });
        } else {
            Utilities.sweetAlertWarning('No se pudo realizar la peticion al servidor');
        }
    });
};

const CompraModal = (idTarjeta) => {
    $('#IdTarjetaCompra').val(idTarjeta);
    NuevaCompraModal.show();
}

const PostNuevaCompra = () => {
    let idTarjeta = $('#IdTarjetaCompra').val(),
        fecha = $('#FechaCompra').val(),
        descripcion = $('#DescripcionCompra').val(),
        monto = $('#MontoCompra').val();
    //Se valida el form antes de enviar al petición
    if (descripcion.trim() == '' || fecha.trim() == '' || monto.trim() == '')
        return Utilities.sweetAlertWarning('Hay campos vacios, revisa el formulario');
    //Construye el objeto del request
    const Compra = { idTarjeta, fecha, descripcion, monto };
    const jCompra = JSON.stringify(Compra);
    HttpRequest.PostAsync(`api/Movimiento/CrearNuevaCompraAsync`, jCompra, (resp) => {
        console.log(resp);
        if (resp.textStatus == "success") {
            ClearInputsNuevaCompra();
            NuevaCompraModal.hide();
            Utilities.sweetAlertSuccess('Se guardó con exito');
            GetAllTarjetas();
        } else {
            Utilities.sweetAlertWarning(`No se pudo guardar: ${resp.xhr.responseJSON.exception.Message}`);
        }
    });
}

const PagoModal = (idTarjeta) => {
    $('#IdTarjetaPago').val(idTarjeta);
    NuevoPagoModal.show();
}

const PostNuevoPago = () => {
    let idTarjeta = $('#IdTarjetaPago').val(),
        fecha = $('#FechaPago').val(),
        monto = $('#MontoPago').val();
    //Se valida el form antes de enviar al petición
    if (fecha.trim() == '' || monto.trim() == '')
        return Utilities.sweetAlertWarning('Hay campos vacios, revisa el formulario');
    //Construye el objeto del request
    const Compra = { idTarjeta, fecha, monto };
    const jCompra = JSON.stringify(Compra);
    HttpRequest.PostAsync(`api/Movimiento/CrearNuevoPagoAsync`, jCompra, (resp) => {
        console.log(resp);
        if (resp.textStatus == "success") {
            ClearInputsNuevoPago();
            NuevoPagoModal.hide();
            Utilities.sweetAlertSuccess('Se guardó con exito');
            GetAllTarjetas();
        } else {
            Utilities.sweetAlertWarning(`No se pudo guardar: ${resp.xhr.responseJSON.exception.Message}`);
        }
    });
}

const HistorialTransacciones = (IdTarjeta) => {
    Swal.showLoading();
    HttpRequest.GetAsync(`api/Movimiento/ObtenerHistorialTransaccionesPorTarjetaAsync/${IdTarjeta}`, null, (resp) => {
        console.log(resp);
        if (resp.textStatus == 'success') {
            //resp.data = JSON.parse(resp.data);
            Utilities.GenerarTabla({
                idDiv: 'divForTableHistorialTransaccionesDelMes',
                idTabla: 'dtTransaccionesDelMel',
                cabeceras: ['Fecha', 'Descripcion', 'Monto', 'Tipo'],
                estilos: 'width:100%',
                claseCss: 'table table-sm table-striped table-hover',
                classTHead: '',
                propiedadesPersonalizadas: ''
            }, {
                data: resp.data,
                language: lenguajeEspanniolDataTable,
                columns: [
                    { data: (data, type, full, meta) => Utilities.mostrarFechaHoraESJS(data.fecha) },
                    { data: 'descripcion' },
                    { data: (data, type, full, meta) => `$${data.monto}` },
                    { data: 'tipo' }
                ],
                order: [[0, "desc"]]
            });
            Swal.close();
            HistorialTransaccionesModal.show();
        } else {
            Utilities.sweetAlertWarning('No se pudo realizar la peticion al servidor');
        }
    });
}

const ListaComprasDelMes = (IdTarjeta) => {
    Swal.showLoading();
    HttpRequest.GetAsync(`api/Movimiento/ObtenerComprasDelMesPorTarjetaAsync/${IdTarjeta}`, null, (resp) => {
        console.log(resp);
        if (resp.textStatus == 'success') {
            //resp.data = JSON.parse(resp.data);
            Utilities.GenerarTabla({
                idDiv: 'divForTableListaComprasDelMes',
                idTabla: 'dtComprasDelMes',
                cabeceras: ['Fecha', 'Descripcion', 'Monto', 'Tipo'],
                estilos: 'width:100%',
                claseCss: 'table table-sm table-striped table-hover',
                classTHead: '',
                propiedadesPersonalizadas: ''
            }, {
                data: resp.data,
                language: lenguajeEspanniolDataTable,
                columns: [
                    { data: (data, type, full, meta) => Utilities.mostrarFechaHoraESJS(data.fecha) },
                    { data: 'descripcion' },
                    { data: (data, type, full, meta) => `$${data.monto}` },
                    { data: 'tipo' }
                ],
                order: [[0, "desc"]]
            });
            Swal.close();
            ListaComprasMesModal.show();
        } else {
            Utilities.sweetAlertWarning('No se pudo realizar la peticion al servidor');
        }
    });
}

const EstadoCuenta = (IdTarjeta) => {
    $('#IdTarjetaEstadoCuenta').val(IdTarjeta);
    Swal.showLoading();
    HttpRequest.GetAsync(`api/Movimiento/GenerarEstadoDeCuentaPorTarjetaAsync/${IdTarjeta}`, null, (resp) => {
        if (resp.textStatus == 'success') {
            const { titular, numero, fechaVencimiento, limite, saldoActual, saldoDisponible,
                porcentajePagoMinimo, porcentajeInteres, listaComprasMes, saldoTotalComprasMes,
                interesBonificable, cuotaMinima, totalContadoAPagar, totalContadoMasInteresBonificable } = resp.data;
            //console.log(resp);
            $('#TitularEstadoCuenta').html(`Titular: ${titular}`);
            $('#NumeroEstadoCuenta').html(`Numero: ${numero}`);
            $('#FechaVencimientoEstadoCuenta').html(`Fecha vencimiento: ${Utilities.formatFechaESJS(fechaVencimiento)}`);
            $('#LimiteEstadoCuenta').html(`Limite: $${limite}`);
            $('#SaldoActualEstadoCuenta').html(`Saldo actual: $${saldoActual}`);
            $('#SaldoDispoEstadoCuenta').html(`Saldo disponible: $${saldoDisponible}`);
            $('#PorcentajePagoMinimoEstadoCuenta').html(`Porcentaje pago minimo: ${porcentajePagoMinimo}%`);
            $('#PorcentajeInteresEstadoCuenta').html(`Porcentaje interes: ${porcentajeInteres}%`);
            $('#TotalComprasMes').html(`Saldo total de compras del mes: $${saldoTotalComprasMes}`);
            $('#InteresBonificable').html(`Interes bonificable: $${interesBonificable}`);
            $('#CuotaMinimaEstadoCuenta').html(`Cuota minima: $${cuotaMinima}`);
            $('#TotalPagarEstadoCuenta').html(`Total de contado: $${totalContadoAPagar}`);
            $('#TotalMasInteresBonificable').html(`Total de contado + interes bonificable: $${totalContadoMasInteresBonificable}`);

            //resp.data = JSON.parse(resp.data);
            Utilities.GenerarTabla({
                idDiv: 'divForTableListaComprasDelMesEstadoCuenta',
                idTabla: 'dtComprasDelMesEstadoCuenta',
                cabeceras: ['Fecha', 'Descripcion', 'Monto', 'Tipo'],
                estilos: 'width:100%',
                claseCss: 'table table-sm table-striped table-hover',
                classTHead: '',
                propiedadesPersonalizadas: ''
            }, {
                data: listaComprasMes,
                language: lenguajeEspanniolDataTable,
                columns: [
                    { data: (data, type, full, meta) => Utilities.mostrarFechaHoraESJS(data.fecha) },
                    { data: 'descripcion' },
                    { data: (data, type, full, meta) => `$${data.monto}` },
                    { data: 'tipo' }
                ],
                order: [[0, "desc"]]
            });
            Swal.close();
            EstadoCuentaModal.show();
        } else {
            Swal.close();
            Utilities.sweetAlertWarning('No se pudo realizar la peticion al servidor');
        }
    });
}

const ExportarEstadoCuentaPdf = () => {
    Swal.showLoading();
    let IdTarjeta = $('#IdTarjetaEstadoCuenta').val();
    HttpRequest.GetAsync(`api/Movimiento/ExportarPdfEstadoDeCuentaPorTarjetaAsync/${IdTarjeta}`, null, (resp) => {
        debugger;
        if (resp.textStatus == "success") {
            Swal.close();
            $('#btnExportPdf').prop('disabled', true);
            PreviewPdf(resp.data.file);
        } else {
            Swal.close();
            Utilities.sweetAlertWarning(`Ocurrió un problema al intentar exportar archivo`);
        }
    });
}

const PreviewPdf = (fileData) => {
    if (fileData) {
        $('#embedPdfEvidencia').show();
        $('#pdfPreviewEvidencia').attr('type', `application/pdf`);
        $('#pdfPreviewEvidencia').attr('src', `data:application/pdf;base64,${fileData}`);
        PreviewPdfModal.show();
        $('#btnExportPdf').prop('disabled', false);
    } else {
        console.log('La imagen no se pudo mostrar');
        Utilities.sweetAlertError('Ocurrió un problema al intentar cargar el archivo');
    }
}

const ExportarExcelCompras = (IdTarjeta) => {
    Swal.showLoading();
    HttpRequest.GetFileExcelFetchAsync(`api/Movimiento/ExportarExcelComprasDelMesPorTarjetaAsync/${IdTarjeta}`, null, (response) => {
        //console.log(response);
        if (response.status == 200 || response.ok) {
            Swal.close();
            Utilities.sweetAlertSuccess('Reporte generado exitosamente');
        } else {
            Swal.close();
            Utilities.sweetAlertWarning(HttpRequest.VerifyTextStatusRequest(response.status));
        }
    })
}

//Clear modal inputs
const ClearInputsNuevaTarjeta = () => {
    $('#IdTarjeta').val('0');
    $('#TitularTarjeta').val('');
    $('#NumeroTarjeta').val('');
    $('#LimiteTarjeta').val('');
    $('#FechaVencimientoTarjeta').val('');
    $('#PorcentajePagoMinimoTarjeta').val('');
    $('#PorcentajeInteresTarjeta').val('');
};

const ClearInputsNuevaCompra = () => {
    $('#IdTarjetaCompra').val('0');
    $('#FechaCompra').val('');
    $('#DescripcionCompra').val('');
    $('#MontoCompra').val('');
};

const ClearInputsNuevoPago = () => {
    $('#IdTarjetaPago').val('0');
    $('#FechaoPago').val('');
    $('#DescripcionPago').val('');
    $('#MontoPago').val('');
};

$(() => {
    GetAllTarjetas();
});