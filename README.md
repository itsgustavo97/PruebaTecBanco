Proyecto de prueba técnica

Este repositorio contiene un proyecto que incluye una API construida con la arquitectura Onion y un cliente web interactivo. La solución está dividida en dos proyectos principales:

API: Una API RESTful desarrollada con ASP.NET Core que utiliza la arquitectura Onion, el patrón Unit of Work, el patrón Repository, CQRS, Entity Framework Core y Dapper para la gestión de datos y el consumo de procedimientos almacenados.
Cliente Web: Una aplicación web construida con HTML5, JavaScript, jQuery, AJAX, Bootstrap 5, DataTables.js, SweetAlert y FontAwesome Icons.
Contenido
Descripción del Proyecto
Requisitos
Configuración del Entorno
Uso
Licencia
Descripción del Proyecto

API
La API está diseñada utilizando la arquitectura Onion para mantener una separación clara entre la lógica de negocio y las capas de infraestructura. A continuación se detallan algunos aspectos clave:

Unit of Work y Repository: Facilita la gestión de la persistencia de datos y promueve un acceso a datos desacoplado.
CQRS (Command Query Responsibility Segregation): Separa las operaciones de lectura y escritura para mejorar el rendimiento y la escalabilidad.
Entity Framework Core y Dapper: Utilizados para la gestión de datos y la ejecución de procedimientos almacenados en la base de datos.

Cliente Web
El cliente web proporciona una interfaz de usuario rica e interactiva con las siguientes tecnologías:

HTML5 y CSS: Para la estructura y el diseño del contenido.
JavaScript y jQuery: Para la interactividad y el manejo de eventos en el navegador.
AJAX: Para realizar solicitudes asincrónicas a la API.
Bootstrap 5: Para el diseño responsivo y los componentes de interfaz de usuario.
DataTables.js: Para la visualización y manipulación de tablas de datos.
SweetAlert: Para la visualización de alertas y mensajes emergentes.
FontAwesome Icons: Para iconos escalables y gráficos vectoriales.

Requisitos
API
.NET Core 6.0 o superior
SQL Server y SSMS
Dapper
Entity Framework Core

Cliente Web
Navegador web moderno (Chrome, Firefox, Edge, etc.)
Conexión a Internet para cargar librerías y recursos externos

Configuración del Entorno
En la carpeta Documentacion he dejado instrucciones específicas para ejecutar este proyecto.

Uso
Inicia la API y asegúrate de que esté en ejecución.
Abre el cliente web en tu navegador iniciando una nueva instancia dentro de Visual Studio.

Licencia
Este proyecto está bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.
