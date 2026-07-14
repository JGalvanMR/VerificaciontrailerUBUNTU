Now let's look at GuardarFotos.aspx.cs, the largest and likely core file.Let me continue reading the rest of the save logic, the update path, and the percentage recalculation.Now let's check AdminManto.aspx.cs, VerFoto.aspx.cs, Zip.aspx.cs, webservicefotos.asmx.cs, and the DBML schema for context.Now VerFoto, Zip, and webservicefotos.This gives enough schema context. Now let's check the csproj/packages for tech stack confirmation and check the `verificaciontrailer` folder (appears to be a duplicate/published copy).# 📦 Nombre del Módulo: VerificaciontrailerUBUNTU (Sistema Tickets2 — Verificación Fotográfica de Embarques)

## 🧭 Propósito

Este sistema es una aplicación web ASP.NET Web Forms (.NET Framework, proyecto `Tickets2` / `Verificacion.csproj`) que documenta fotográfica y en video el proceso de carga de trailers en el centro de distribución de Mr. Lucky (GAB Irapuato). Su objetivo de negocio es dejar evidencia auditable —por cada embarque— de que se cumplieron los puntos de control de calidad y frío durante la carga (set point del termo, número de caja, difusor, piso, producto completo, temperaturas, y video de encendido), además de habilitar la consulta administrativa de ese avance y la descarga masiva de la evidencia en ZIP. El repositorio incluye dos proyectos (`Tickets2` y `Datos`) y una carpeta paralela `verificaciontrailer` que parece ser una copia de despliegue (solo marcado, sin código C#) para el entorno Ubuntu que da nombre al repositorio.

## ⚙️ Responsabilidades

- Autenticar tres tipos de usuario contra tres orígenes de datos distintos: trabajador interno (`Usuario`/`Persona`/`Trabajador`), operador de embarques (`Tb_Autoriza_OdeP`) y usuario de consulta/mantenimiento (`tb_cat_usuarios`).
- Listar los trailers pendientes de verificación fotográfica (`SeleccionarTrailer.aspx`), calculados por rango de fechas y estado de avance.
- Capturar y almacenar en disco (con conversión y redimensionado) las fotografías de los puntos de control de un trailer, e insertar/actualizar el registro de revisión correspondiente (`GuardarFotos.aspx`).
- Subir y convertir a `.mp4` (vía `ffmpeg`) el video de encendido del sistema de frío ("Ryan").
- Recalcular el porcentaje de avance de verificación de cada trailer mediante una actualización SQL directa.
- Ofrecer un panel administrativo (`AdminManto.aspx`) para filtrar embarques por fecha y visualizar el porcentaje de avance por destino/departamento.
- Mostrar la galería de fotos y video de un embarque específico (`VerFoto.aspx`) y permitir su descarga comprimida (`Zip.aspx`).
- Exponer un servicio web SOAP (`webservicefotos.asmx`) que recibe fotografías adicionales ("posiciones" de pallet) desde un cliente externo (probablemente una app móvil) y las asocia al registro de revisión vía consecutivo y fecha.
- Registrar en bitácora (`tb_registro_vertrai`) los eventos de inicio de sesión, alta y modificación de fotos.

## 🔄 Flujo de Funcionamiento

1. **Login (`PaginaLogin.aspx`)**: el usuario selecciona un rol (`user`, `emb`, `consulta`) y el sistema valida credenciales contra la tabla correspondiente. Si el rol es `emb`, se guarda el objeto de sesión `objAdmin` y se redirige a `SeleccionarTrailer.aspx`; también se intenta insertar un registro de bitácora de ingreso, pero esa inserción está **después** del `Response.Redirect`, por lo que el bloqueo de hilo generado por el redirect impide que ese código se ejecute (ver Riesgos Técnicos).
2. **Selección de trailer (`SeleccionarTrailer.aspx`)**: se valida la sesión (`Session["Iniciada"] == "1"`), y se listan los trailers cuya fecha esté entre "hoy - 2 días" y "mañana", que tengan hora de entrada capturada y consecutivo distinto de cero, y cuyo porcentaje de revisión no sea 100%. Al seleccionar una fila se guardan en sesión la placa, el consecutivo (`conse`), la fecha y el porcentaje, y se redirige a `GuardarFotos.aspx`.
3. **Captura de fotos (`GuardarFotos.aspx`)**: al cargar, se traen los datos del trailer y se marcan como "ya cargadas" las fotos/video que ya existan en el registro de revisión (bloqueando los campos ya capturados). Al presionar Guardar (`btnGuardar_Click`):
   - Si es la primera carga, se exige seleccionar un andén; se valida que el andén no esté ocupado por otro trailer activo y se actualiza mediante SQL directo.
   - Se recorre cada archivo subido (`Request.Files`): las imágenes se validan por `ContentType` (o extensión `.heic`), se redimensionan a 405px de alto y se guardan como `.jpg` en una carpeta por mes/año; el video se valida por extensión (`.mp4`/`.mov`) y tamaño (máx. 100MB), se guarda temporalmente y se convierte a `.mp4` vía `ffmpeg` (ruta de binario distinta según sea Windows o Linux), borrando el archivo original tras la conversión.
   - Según si ya existía un registro de revisión (`Session["actualizar"]`), se hace `INSERT` o `UPDATE` en `tb_det_revision_trailer`.
   - Se recalcula el porcentaje de avance con una sentencia `UPDATE` en SQL puro (`RecalcularPorcentajeSQL`) sobre 11 campos de evidencia.
   - Se inserta un registro de bitácora (alta o modificación) y se redirige de vuelta a `SeleccionarTrailer.aspx`.
4. **Consulta administrativa (`AdminManto.aspx`)**: usuario del rol `consulta` filtra por rango de fechas; el sistema arma manualmente una tabla HTML (concatenación de strings) con el avance porcentual y filtra adicionalmente por departamento del usuario (`CEDIS CANCUN` ve solo destino Cancún; `TRANSPORTES GAB` exige que exista número de trailer).
5. **Visualización de fotos (`VerFoto.aspx`) y descarga (`Zip.aspx`)**: reciben un parámetro `folio` en query string con formato `ddMMyyyy_consecutivo`, lo parsean, consultan las 28 posiciones de pallet más las fotos de control y arman una galería o un ZIP con todo el contenido.
6. **Servicio web (`webservicefotos.asmx`)**: método `BajarRecibo` recibe una foto en binario, un nombre de archivo, fecha, consecutivo y posición (2 a 28, pares); guarda la imagen en disco y actualiza el campo `pos*` correspondiente en `tb_det_revision_trailer` mediante SQL concatenado directamente (sin parámetros).

## 📐 Reglas de Negocio

### 🔒 Restricciones
- Un trailer solo puede seleccionarse para carga de fotos si su fecha está entre "hoy - 2 días" y "mañana", tiene hora de entrada registrada y su consecutivo (`conse`) es distinto de cero.
- Un trailer con porcentaje de avance de 100% no puede volver a abrirse para captura ("Las fotos del trailer ya fueron cubiertas al 100%").
- No se puede asignar un andén que ya esté ocupado por otro trailer activo (sin hora de fin, no guardado, y cuyo responsable no sea "J CONCEPCION RAZO PIZANO", que actúa como excepción explícita a esta regla).
- Los videos solo se aceptan en extensiones `.mpg`, `.wmv`, `.avi`, `.mp4` o `.mov`, y no pueden exceder 100 MB.
- Cada campo de foto/video, una vez cargado, queda bloqueado para edición en la misma sesión de captura (los controles se deshabilitan visualmente).
- El login de usuario `emb` exige además que la clave de autorización de la operación de datos (`Tb_Autoriza_OdeP.clave`) sea exactamente `"TRAIL"`.
- El login de usuario `consulta` exige que el estatus del usuario sea `"A"` (activo).

### ✅ Validaciones
- Las imágenes se validan por tipo MIME (`image/\S+`) o como HEIC (por `ContentType` o extensión `.heic`); si no cumplen, se rechaza el archivo individual con mensaje de error.
- Las fechas provenientes de sesión o de parámetros de URL se validan estrictamente contra el formato `dd/MM/yyyy` (o `ddMMyyyy` para folios), rechazando el flujo si el formato no es válido.
- El parámetro `folio` recibido en `VerFoto.aspx` y `Zip.aspx` debe tener el formato `fecha_consecutivo` (separado por guion bajo) y la fecha debe tener 8 caracteres.
- Antes de guardar, se valida que exista sesión de usuario administrador (`objAdmin`) y que la fecha/consecutivo de sesión sean válidos.
- En `AdminManto.aspx`, las fechas de filtro son obligatorias y la fecha inicial no puede ser mayor a la final.

### 🔁 Agrupaciones
- El avance de un trailer se agrupa por el par `(conse, fecha)`, que es la llave primaria de `tb_det_revision_trailer`.
- El listado administrativo se agrupa/filtra por `usu_departamento` del usuario en sesión: `CEDIS CANCUN` limita a destino Cancún; `TRANSPORTES GAB` exige trailer asignado; cualquier otro departamento ve el listado sin filtro adicional.
- Las 28 fotografías de "posición" de pallet (`posunodos` a `posveintisieteveintiocho`) se agrupan de dos en dos (posiciones impares/pares) y se cargan mediante un canal distinto (el servicio web SOAP) al resto de las fotos de control.

### ⚙️ Reglas Operativas
- El porcentaje de avance de verificación se calcula como el conteo de 11 campos de evidencia no vacíos (`setpointini`, `numcaja`, `difusor`, `piso`, `cajacompleta`, `temprod1`, `temprod2`, `temprod3`, `setpointfin`, `termino_carga`, `fotoryan`) dividido entre 11 y multiplicado por 100, redondeado a 2 decimales. Nótese que este cálculo en `RecalcularPorcentajeSQL` **no incluye** `temprod4`, `temprod5`, `temprod6`, `anden`, `vidrayan` ni las 14 fotos de posición de pallet, aunque sí se solicitan y almacenan en la interfaz.
- Cada operación de guardado (alta o modificación) genera un registro en `tb_registro_vertrai` con tipo de movimiento `"A"` (alta) o `"M"` (modificación), sistema `"EMBWEB"` y clave de operación fija `"7.9"`.
- Las imágenes se redimensionan siempre a una altura máxima de 405 píxeles antes de guardarse.
- Los archivos de fotos se organizan en carpetas físicas por mes/año (`MMyyyy`) dentro de `~/FotoRevisionTrailer/`.
- Si falla la carga de una foto individual, el sistema envía automáticamente un correo de notificación de error a una dirección fija de soporte, sin detener el resto del proceso.
- El archivo ZIP de evidencia se genera con un nombre fijo (`Archivos Trailer.zip`), sobrescribiéndose en cada descarga.

## 🔗 Dependencias

- **Framework**: ASP.NET Web Forms sobre .NET Framework (proyecto `Verificacion.csproj`, referencia a `Microsoft.EntityFrameworkCore.Tools` 9.0.6 y `Ionic.Zip` 1.9.1.8 según `packages.config`).
- **Acceso a datos**: LINQ to SQL sobre dos contextos (`DataVerificacionDataContext`, `dcTicketsDataContext`) definidos en el proyecto `Datos` (archivos `.dbml`), apuntando a la base de datos `GAB_Irapuato`.
- **Base de datos**: SQL Server, accedida tanto por LINQ to SQL como por `SqlConnection`/`SqlCommand` directos en varios puntos.
- **Procesamiento de video**: binario externo `ffmpeg` (ejecutable `.exe` embebido en `js/ffmpeg.exe` para Windows, o comando global `ffmpeg` en Linux).
- **Compresión**: librería `Ionic.Zip` (DotNetZip) para generación de archivos ZIP.
- **Correo saliente**: `System.Net.Mail.SmtpClient` contra el servidor `mail1.mrlucky.com.mx`.
- **Servicio web**: `webservicefotos.asmx` (SOAP/ASMX), consumido presumiblemente por un cliente externo (app móvil) para el envío de fotos de posición de pallet.
- **Componentes internos**: clases `MessageBox`, `MessageBoxError`, `MessageBoxSuccess` para mensajería en pantalla; `ZipArchive.cs`.
- **Recurso de red compartido**: ruta UNC `\\192.168.123.4\FotosRevisionTrailer\` referenciada como destino de video convertido (aunque el código que la usa parece no completarse — ver Riesgos Técnicos).

## ⚠️ Riesgos Técnicos

- **Credenciales de SQL Server e IP pública en texto plano en el código fuente**: la cadena de conexión (`user id=sa; password=Gabira2026$; ... server=tcp:189.206.160.206,2352`) está hardcodeada tanto en `GuardarFotos.aspx.cs` como en `webservicefotos.asmx.cs`, con el usuario `sa` (administrador de SQL Server) expuesto directamente. Este es el mismo patrón de riesgo detectado previamente en otros sistemas de mrlucky (RFIDTrackBin, GABRFIDLabeler, Sistema de Quejas).
- **Inyección SQL en el servicio web**: `webservicefotos.asmx.cs` (`BajarRecibo`) construye la sentencia `UPDATE` concatenando directamente `fechatrailer`, `conse` y `nombre_archivo` sin parametrizar, permitiendo inyección SQL desde un cliente que invoque el servicio.
- **Servicio web sin autenticación aparente**: no se observa mecanismo de autenticación en `webservicefotos.asmx`; cualquier cliente que conozca el endpoint podría escribir en `tb_det_revision_trailer` para cualquier consecutivo/fecha.
- **Contraseñas de usuario en texto plano**: las tablas `Usuario`, `Tb_Autoriza_OdeP` y `tb_cat_usuarios` almacenan contraseñas en columnas de texto plano (`password`, `usu_password`) sin evidencia de hashing.
- **Credenciales SMTP en texto plano** (`sistemas`/`sisgab`) embebidas en `EnviarErrorPorCorreo`.
- **`Response.Redirect` antes de código de negocio en `PaginaLogin.aspx.cs`**: en el flujo de login `emb`, el `Response.Redirect("SeleccionarTrailer.aspx")` se ejecuta antes del bloque que inserta el registro de bitácora de ingreso; dado que `Response.Redirect` por defecto aborta el hilo de ejecución, ese `INSERT` nunca se ejecuta, por lo que los inicios de sesión de este rol probablemente no quedan auditados.
- **Cálculo de porcentaje inconsistente con la interfaz**: `RecalcularPorcentajeSQL` solo considera 11 de los aproximadamente 25+ campos de evidencia capturables en pantalla (excluye `temprod4-6`, `anden`, `vidrayan` y las 14 fotos de posición de pallet cargadas vía servicio web), lo que puede generar un porcentaje mostrado al usuario que no refleja la completitud real de la evidencia.
- **Condición de carrera en generación de ZIP**: `Zip.aspx.cs` usa un nombre de archivo fijo (`Archivos Trailer.zip`) compartido por todos los usuarios; solicitudes concurrentes de distintos folios podrían sobrescribirse mutuamente antes de la descarga.
- **Doble ruta de código (métodos "2" y "LEGACY")**: el archivo `GuardarFotos.aspx.cs` conserva métodos duplicados o alternativos (`CargarInfotrailer2`, `CargarFotosTrailer2`, `btnGuardarOG_Click`, `btnGuardarLEGACY_Click`, `EncodingVideoOG`) junto a las versiones activas, lo que incrementa el riesgo de mantenimiento y de que se invoque accidentalmente la ruta equivocada.
- **Manejo de imágenes vía `System.Drawing.Bitmap`**: la conversión y redimensionado de imágenes usa `System.Drawing`, una API con soporte limitado y advertido como no recomendado para aplicaciones de servidor por Microsoft, especialmente relevante si el despliegue objetivo es Linux/Ubuntu (nombre del repositorio).
- **Construcción manual de HTML por concatenación de strings** en `AdminManto.aspx.cs` para renderizar la tabla de resultados, sin codificación HTML de los valores (riesgo de XSS si algún campo de datos contiene marcado HTML) y de difícil mantenimiento.
- **Referencia a ruta UNC de red (`\\192.168.123.4\FotosRevisionTrailer\`) no utilizada de forma completa**: las variables `originFile`, `sourceFile` y `destFile` se calculan pero no se observa una operación de copia/movimiento hacia esa ruta en el código mostrado, sugiriendo funcionalidad incompleta o removida.
- **Doble copia del sitio en el repositorio** (`Tickets2` con código fuente y `verificaciontrailer` solo con marcado/assets, sin `.cs`): no determinable con la información disponible si esta segunda carpeta es un artefacto de publicación, un entorno de respaldo, o código muerto; representa riesgo de desincronización entre ambas copias.

## 🧪 Casos Edge

- Un usuario intenta guardar fotos de un trailer cuyo registro de revisión fue eliminado o modificado por otro proceso entre la carga de la página y el guardado (no se observa control de concurrencia optimista).
- Carga simultánea del mismo trailer por dos sesiones distintas: ambas podrían competir por el mismo andén o por el mismo registro de revisión.
- Un archivo de imagen con `ContentType` válido pero contenido corrupto podría fallar al construirse el `Bitmap`, disparando el envío de correo de error pero dejando el registro en un estado parcialmente actualizado.
- Video subido cuya conversión con `ffmpeg` falla (`ExitCode != 0`): el archivo original ya fue guardado en disco antes de intentar la conversión, y solo se registra el error en un log de texto (`ffmpeg_error.log`), sin notificación visible clara al usuario final ni limpieza garantizada del archivo temporal.
- Descarga de ZIP (`Zip.aspx`) para un folio sin ninguna foto asociada: el código retorna el mensaje "No se encontró información" y no genera archivo.
- Parámetro `folio` malformado en `VerFoto.aspx` o `Zip.aspx` (longitud de fecha distinta de 8, o menos de dos segmentos separados por `_`): se maneja con mensajes de error y redirección, pero no hay registro de auditoría de estos intentos.

## 🧱 Suposiciones Detectadas

- Se asume que el servidor de aplicación tiene acceso de red directo (con IP pública) al servidor SQL Server (`189.206.160.206:2352`) desde cualquier entorno de despliegue.
- Se asume que el binario `ffmpeg` está disponible en el `PATH` del sistema operativo cuando el despliegue es Linux/Ubuntu, y en `js/ffmpeg.exe` cuando es Windows.
- Se asume que el consumidor del servicio web `webservicefotos.asmx` (probablemente una app móvil de captura de fotos de pallet) siempre envía valores válidos de `pos` (2, 4, 6... 28) y no requiere autenticación adicional.
- Se asume que solo existe un archivo ZIP de evidencia en descarga a la vez (no hay previsión de acceso concurrente al mismo nombre de archivo).
- Se asume que el usuario administrador de "Transportes GAB" y "CEDIS Cancún" son los únicos departamentos con reglas de filtrado especiales; cualquier otro valor de `usu_departamento` no aplica filtro adicional.

## 📈 Recomendaciones Técnicas

- Migrar las credenciales de SQL Server, SMTP y cualquier secreto embebido a un mecanismo seguro de configuración (variables de entorno, Azure Key Vault, o al menos `Web.config` con sección protegida), eliminando el usuario `sa` de uso en producción y sustituyéndolo por una cuenta de aplicación con privilegios mínimos.
- Parametrizar completamente la sentencia `UPDATE` en `webservicefotos.asmx.cs` (`BajarRecibo`) para eliminar la inyección SQL, y añadir autenticación/autorización al servicio web (token, API key, o certificado de cliente).
- Alinear el cálculo de `RecalcularPorcentajeSQL` con todos los campos de evidencia realmente solicitados en la interfaz (incluyendo `temprod4-6`, `anden`, `vidrayan` y las fotos de posición de pallet), o documentar explícitamente por qué se excluyen si es una decisión de negocio intencional.
- Revisar y corregir el orden de ejecución en `PaginaLogin.aspx.cs` para que el registro de bitácora de ingreso se ejecute antes del `Response.Redirect` (o usar `Response.Redirect(url, false)` seguido de `CompleteRequest()`).
- Eliminar o migrar el código duplicado/legacy (`btnGuardarLEGACY_Click`, `CargarFotosTrailer2`, `EncodingVideoOG`, etc.) una vez confirmado que las versiones activas cubren todos los casos, para reducir superficie de mantenimiento y riesgo de ejecución accidental de rutas obsoletas.
- Sustituir el hasheo/almacenamiento de contraseñas en texto plano por un algoritmo de hash con sal (BCrypt o similar), como ya se hizo en otros sistemas internos (ATU).
- Reemplazar la generación de HTML por concatenación de strings en `AdminManto.aspx.cs` por controles de datos nativos (Repeater/GridView) con codificación automática, para mitigar XSS y mejorar mantenibilidad.
- Aclarar el propósito de la carpeta `verificaciontrailer` (¿copia de despliegue, respaldo, o remanente?) y, si no está en uso, retirarla del repositorio para evitar confusión.
- Nombrar el archivo ZIP de descarga de forma única por folio/consecutivo (o usar un directorio temporal por solicitud) para eliminar la condición de carrera entre descargas concurrentes.
- Evaluar la migración de `System.Drawing` a una librería de procesamiento de imágenes multiplataforma (p. ej. `ImageSharp`) dado que el nombre del repositorio sugiere despliegue en Ubuntu, donde `System.Drawing` tiene soporte limitado y no oficial en Linux para .NET moderno.

## 🧾 Resumen Ejecutivo

Este sistema es la "libreta de evidencias" digital que usa el equipo de embarques para comprobar, con fotos y video, que cada trailer que sale del centro de distribución cumplió los puntos de control de calidad y de frío antes de cerrarse. Un operador elige el trailer, sube las fotos requeridas (set point del termo, número de caja, temperatura del producto, piso, etc.) y el sistema calcula automáticamente qué tan completo está ese expediente fotográfico; el área administrativa puede luego consultar, ver la galería completa o descargar todo en un ZIP para auditorías o reclamaciones. El sistema funciona y automatiza un proceso que antes probablemente era manual, pero arrastra riesgos de seguridad importantes heredados de su desarrollo original —contraseñas y credenciales de base de datos visibles en el código, y una vía de entrada (el servicio que recibe fotos desde el celular) vulnerable a manipulación—, además de una inconsistencia entre lo que la pantalla le muestra al usuario como "porcentaje de avance" y lo que realmente se está contando por dentro. Ninguno de estos riesgos impide que el sistema opere hoy, pero si se planea modernizarlo o exponerlo a más usuarios, conviene primero cerrar esos puntos de seguridad y aclarar cuál código (activo vs. "legacy") es el que realmente se debe mantener.