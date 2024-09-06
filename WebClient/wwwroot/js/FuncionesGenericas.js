const client = "http://localhost:5112/";
/**
 * Este elemento contiene funciones para realizar peticiones HTTP
 * Las cuales son GetAsync, PostAsync, PostFormAsync(archivos) */
const HttpRequest = {
    /**
     * Esta función realiza una petición GET ajax por url 
     * @param ruta es la url del endpoint
     * @param payload es la data de la petición
     * @param callBack es una función para manipular la respuesta */
    GetAsync: (ruta, payload, callBack) => {
        $.ajax({
            url: `${client}${ruta}`,
            crossDomain: true,
            type: "GET",
            data: payload,
            async: true,
            statusCode: {
                400: () => {
                    console.log("Bad request: 400");
                },
                401: () => {
                    console.log("Anauthorized request: 401");
                },
                403: () => {
                    console.log("Forbidden request: 403");
                },
                404: () => {
                    console.log("Not found request: 404");
                },
                405: () => {
                    console.log("Method not allowed: 405");
                },
                200: () => {
                    console.log("Ok request: 200");
                }
            },
            success: (data, textStatus) => {
                callBack({ data, textStatus });
            },
            error: (xhr, textStatus, error) => {
                callBack({ xhr, textStatus, error });
            }
        });

    },


    /**
     * Esta función realiza una petición POST ajax por url 
     * @param ruta es la url del endpoint
     * @param payload es la data de la petición
     * @param callBack es una función para manipular la respuesta */
    PostAsync: (ruta, payload, callBack) => {
        $.ajax({
            url: `${client}${ruta}`,
            type: "POST",
            contentType: "application/json",
            data: payload,
            async: true,
            dataType: "json",
            statusCode: {
                400: () => {
                    console.log("Bad request: 400");
                },
                401: () => {
                    console.log("Anauthorized request: 401");
                },
                403: () => {
                    console.log("Forbidden request: 403");
                },
                404: () => {
                    console.log("Not found request: 404");
                },
                405: () => {
                    console.log("Method not allowed: 405");
                },
                200: () => {
                    console.log("Ok request: 200");
                }
            },
            success: (data, textStatus) => {
                callBack({ data, textStatus });
            },
            error: (xhr, textStatus, error) => {
                callBack({ xhr, textStatus, error });
            }
        });

    },

    /**
     * Esta función realiza una petición PUT ajax por url 
     * @param ruta es la url del endpoint
     * @param payload es la data de la petición
     * @param callBack es una función para manipular la respuesta */
    PutAsync: (ruta, payload, callBack) => {
        $.ajax({
            url: `${client}${ruta}`,
            accepts: "application/json",
            crossDomain: true,
            type: "PUT",
            contentType: "application/json",
            data: payload,
            async: true,
            dataType: "json",
            statusCode: {
                400: () => {
                    console.log("Bad request: 400");
                },
                401: () => {
                    console.log("Anauthorized request: 401");
                },
                403: () => {
                    console.log("Forbidden request: 403");
                },
                404: () => {
                    console.log("Not found request: 404");
                },
                405: () => {
                    console.log("Method not allowed: 405");
                },
                200: () => {
                    console.log("Ok request: 200");
                }
            },
            success: (data, textStatus) => {
                callBack({ data, textStatus });
            },
            error: (xhr, textStatus, error) => {
                callBack({ xhr, textStatus, error });
            }
        });

    },

    /**
     * Esta función realiza una petición DELETE ajax por url 
     * @param ruta es la url del endpoint
     * @param payload es la data de la petición
     * @param callBack es una función para manipular la respuesta */
    DeleteAsync: (ruta, payload, callBack) => {
        $.ajax({
            url: `${client}${ruta}`,
            type: "DELETE",
            data: payload,
            async: true,
            dataType: "json",
            statusCode: {
                400: () => {
                    console.log("Bad request: 400");
                },
                401: () => {
                    console.log("Anauthorized request: 401");
                },
                403: () => {
                    console.log("Forbidden request: 403");
                },
                404: () => {
                    console.log("Not found request: 404");
                },
                405: () => {
                    console.log("Method not allowed: 405");
                },
                200: () => {
                    console.log("Ok request: 200");
                }
            },
            success: (data, textStatus) => {
                callBack({ data, textStatus });
            },
            error: (xhr, textStatus, error) => {
                callBack({ xhr, textStatus, error });
            }
        });
    },

    /**
     * Función para exportar un reporte excel con Fetch
     * @param {any} ruta
     * @param {any} payload
     * @param {any} callBack
     */
    GetFileExcelFetchAsync: async (ruta, payload, callBack) => {
        
        // Construye la cadena de consulta con los parámetros
        if (payload != undefined || payload != null) {
            var queryString = Object.keys(payload)
                .map(key => encodeURIComponent(key) + "=" + encodeURIComponent(payload[key]))
                .join("&");

            const response = await fetch(`${client}${ruta}?${queryString}`,
                {
                    method: "GET",
                });
            if (!response.ok) {
                callBack(response);
                throw new Error(`Error al descargar el informe: ${response.status} - ${response.statusText}`);
            }
            const blob = await response.blob();

            // Crea objeto URL para el blob
            const url = window.URL.createObjectURL(blob);

            // Crea un enlace invisible en el documento
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;

            // Agregar el elemento al documento
            document.body.appendChild(a);
            a.click();

            // Limpiar y liberar el objeto URL y el enlace
            window.URL.revokeObjectURL(url);
            document.body.removeChild(a);
            callBack(response);
        } else {

            const response = await fetch(`${client}${ruta}`,
                {
                    method: "GET",
                });
            if (!response.ok) {
                callBack(response);
                throw new Error(`Error al descargar el informe: ${response.status} - ${response.statusText}`);
            }
            const blob = await response.blob();

            // Crea objeto URL para el blob
            const url = window.URL.createObjectURL(blob);

            // Crea un enlace invisible en el documento
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;

            // Agregar el elemento al documento
            document.body.appendChild(a);
            a.click();

            // Limpiar y liberar el objeto URL y el enlace
            window.URL.revokeObjectURL(url);
            document.body.removeChild(a);
            callBack(response);
        }
    },

    VerifyTextStatusRequest: (statusCode) => {
        let msj = "";
        switch (statusCode) {
            case 200:
                msj = "Enhorabuena";
                return msj;
                break;
            case 400:
                msj = "Hubo un problema con la solicitud al servidor";
                return msj;
                break;
            case 401:
                msj = "Recurso no autorizado";
                return msj;
                break;
            case 404:
                msj = "No se encontró información relacionada";
                return msj;
                break;
            case 405:
                msj = "Metodo no permitido";
                return msj;
                break;
            case 500:
                msj = "El servidor encontró errores en la solicitud";
                return msj;
                break;
            default:
                msj = "Ocurrió algo inesperado";
                return msj;
                break;
        }
    },

};

const lenguajeEspanniolDataTable =
{
    // https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
};

const Utilities = {

    /**
            * Genera la tabla con la configuracion que tu definas
            * @param {any} configuracionTable es un objeto json con las siguientes propiedades: idDiv, idTabla, cabeceras, estilos, claseCss, propiedadesPersonalizadas
            * @param {any} configuracionDataTable
            */
    GenerarTabla: (configuracionTable, configuracionDataTable = null) => {
        const { idDiv, idTabla, cabeceras, estilos = 'width:100%;', claseCss = 'table table-hover', propiedadesPersonalizadas = '', claseCssTh = '', classTHead = '' } = configuracionTable;
        let contenedor = document.getElementById(idDiv);
        if (contenedor != undefined) {
            contenedor.innerHTML = '';
            let contentHtml = `<table class="${claseCss}" id="${idTabla}" ${estilos != '' ? `style="${estilos}"` : ''} ${propiedadesPersonalizadas}><thead class="${classTHead}"><tr>`;
            for (let i = 0; i < cabeceras.length; i++) {
                contentHtml += `<th  class="${claseCssTh}">${cabeceras[i].toUpperCase()}</th>`;
            }
            contentHtml += `</tr></thead><tbody></tbody></table>`;
            contenedor.innerHTML = contentHtml;
            configuracionDataTable != null ? $(`#${idTabla}`).DataTable({ ...configuracionDataTable, language: lenguajeEspanniolDataTable }) : $(`#${idTabla}`).DataTable({ 'language': lenguajeEspanniolDataTable });
        } else {
            Utilities.sweetAlertWarning('No se encontró él contenedor donde se tiene que pintar la tabla');
        }
    },

    /**
        * Recibe una fecha en formato ISO(SQL) y devuelve el formato El Salvador: dd-MM-yyyy
        * @param {any} fecha parametro que contiene la hora
        */
    formatFechaESJS: (fecha) => {
        let nuevafecha = "";
        if (fecha != null && fecha != '') {
            //debugger;
            fecha = fecha.split('T');
            let dia = '', mes = '', anio = '';
            let soloFecha = fecha[0].split('-');
            dia = soloFecha[2];
            mes = soloFecha[1];
            anio = soloFecha[0];
            nuevafecha = `${dia}-${mes}-${anio}`;
        }
        return nuevafecha;
    },

    mostrarFechaHoraESJS: (fechaHora) => {
        let nuevafechaHora = "", arrFechaHora, soloFechaFormat = "", soloHoraFormat = "", soloHora = "";
        if (fechaHora != null && fechaHora != '') {
            let dia = '', mes = '', anio = '';
            let horas = '', minutos = '';
            arrFechaHora = fechaHora.split('T');
            let arrSoloFecha = arrFechaHora[0].split('-');
            dia = arrSoloFecha[2];
            mes = arrSoloFecha[1];
            anio = arrSoloFecha[0];
            soloFechaFormat = `${dia}-${mes}-${anio}`;
            let soloHora = arrFechaHora[1].split('.');
            soloHora = soloHora[0].split(':');
            horas = soloHora[0];
            minutos = soloHora[1];
            soloHoraFormat = `${horas}:${minutos}`
            nuevafechaHora = `${soloFechaFormat} ${soloHoraFormat}`;
        }
        return nuevafechaHora;
    },

    /**
        * muestra un mensaje de confirmacion
        * @param {any} mensaje mensaje que tendra la notificacion
        */
    sweetAlertSuccess: (mensaje) => Swal.fire('¡Exito!', mensaje, 'success'),
    /**
        * muestra un mensaje de error
        * @param {any} mensaje mensaje que tendra la notificacion
        */
    sweetAlertError: (mensaje) => Swal.fire('¡Error!', mensaje, 'error'),
    sweetAlertInfo: (mensaje) => Swal.fire('¡Información!', mensaje, 'info'),
    /**
        * muestra un mensaje de advertencia
        * @param {any} mensaje mensaje que tendra la notificacion
        */
    sweetAlertWarning: (mensaje) => Swal.fire('¡Advertencia!', mensaje, 'warning'),
};