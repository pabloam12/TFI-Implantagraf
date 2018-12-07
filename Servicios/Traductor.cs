using AccesoDatos;
using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class Traductor
    {

        public Hashtable Traducir(string idioma)
        {

            //var accDatosIdioma = new IdiomaDAC();


            //return accDatosIdioma.Traducir(idioma);

            var traducciones = new Hashtable();

            if (idioma == "Esp")
            {
                traducciones.Add("BITACORA_FILTROS_BOTON_FILTRAR", "FILTRAR");
                traducciones.Add("BITACORA_TITULO", "BITÁCORA DEL SISTEMA");
                traducciones.Add("BITACORA_TITULO_FILTROS", "FILTROS DE BÚSQUEDA");
                traducciones.Add("BITACORA_WARNING_FECHAS_MAL", "LA FECHA DE INICIO SIEMPRE DEBE SER MENOR QUE LA DE FIN");
                traducciones.Add("BITACORA_WARNING_SIN_FECHA_INICIO", "NO PUEDE INGRESAR FECHA DE FIN SIN FECHA DE INICIO");
                traducciones.Add("BOTON_ACTUALIZAR_DATOS_CUENTA", "ACTUALIZAR DATOS");
                traducciones.Add("BOTON_ACTUALIZAR_PSW", "ACTUALIZAR CLAVE");
                traducciones.Add("BOTON_AGREGAR_CARRITO", "AGREGAR CARRITO");
                traducciones.Add("BOTON_ALTA_USR_ADMINISTRATIVO", "ALTA USUARIO ADMINISTRATIVO");
                traducciones.Add("BOTON_BLOQUEAR_CUENTA", "BLOQUEAR");
                traducciones.Add("BOTON_CATALOGO", "CATÁLOGO");
                traducciones.Add("BOTON_COMPRAR", "COMPRAR");
                traducciones.Add("BOTON_DESBLOQUEAR_CUENTA", "DESBLOQUEAR");
                traducciones.Add("BOTON_ELIMINAR_CUENTA", "ELIMINAR");
                traducciones.Add("BOTON_ENVIAR", "ENVIAR");
                traducciones.Add("BOTON_FINALIZAR", "FINALIZAR");
                traducciones.Add("BOTON_PERMISOS", "VER PERMISOS");
                traducciones.Add("BOTON_QUITAR_PRODUCTO", "QUITAR");
                traducciones.Add("BOTON_RECUPERAR_PSW", "RECUPERAR CLAVE");
                traducciones.Add("BOTON_REGISTRAR", "REGISTRARME");
                traducciones.Add("BOTON_SUSCRIPCION", "SUSCRIBIRME");
                traducciones.Add("BOTON_VER", "DETALLE");
                traducciones.Add("BOTON_VOLVER", "VOLVER");
                traducciones.Add("CATALOGO_SELECTOR_AUTO", "AUTOMÁTICAS");
                traducciones.Add("CATALOGO_SELECTOR_MANUALES", "MANUALES");
                traducciones.Add("CATALOGO_SELECTOR_OTRAS", "OTRAS");
                traducciones.Add("CATALOGO_SELECTOR_SEMIAUTO", "SEMI - AUTOMÁTICAS");
                traducciones.Add("CATALOGO_SELECTOR_TAMPO", "TAMPOGRÁFICAS");
                traducciones.Add("CATALOGO_SELECTOR_TODAS", "TODAS");
                traducciones.Add("CUENTA_ACTUALIZAR_CLAVE_LEYENDA", "La nueva contraseña ha sido actualizada correctamente. Muchas gracias.");
                traducciones.Add("CUENTA_ACTUALIZARDATOS_BOTON_GUARDAR", "GUARDAR");
                traducciones.Add("CUENTA_ACTUALIZARDATOS_TITULO", "ACTUALIZAR DATOS DE CUENTA");
                traducciones.Add("CUENTA_BLOQUEARCUENTA_LEYENDA", "La cuenta de usuario se ha bloqueado con éxito.");
                traducciones.Add("CUENTA_CAMBIAR_CLAVE_TITULO", "INGRESE LA NUEVA CONTRASEÑA");
                traducciones.Add("CUENTA_DESBLOQUEARCUENTA_LEYENDA", "La cuenta de usuario se ha desbloqueado con éxito.");
                traducciones.Add("CUENTA_DETALLE_DATOSCONTACTO", "Datos de Contacto");
                traducciones.Add("CUENTA_DETALLE_DATOSPERSONALES", "Datos Personales");
                traducciones.Add("CUENTA_DETALLE_IDIOMA", "Mi Idioma");
                traducciones.Add("CUENTA_DETALLE_TITULO", "MI CUENTA");
                traducciones.Add("CUENTA_ENVIO_CLAVE_LEYENDA", "La nueva clave se ha enviado a su correo de usuario. Muchas gracias.");
                traducciones.Add("CUENTA_LISTADO_USUARIOS", "LISTADO DE USUARIOS");
                traducciones.Add("CUENTA_RECUPERAR_PSW_TITULO", "RECUPERAR CONTRASEÑA");
                traducciones.Add("CUENTA_VERPERMISOS_TITULO", "PERMISOS DE USUARIO");
                traducciones.Add("ENTIDAD_ACCION", "Acción");
                traducciones.Add("ENTIDAD_ANIO_V", "Año Vencimiento");
                traducciones.Add("ENTIDAD_APELLIDO", "Apellido");
                traducciones.Add("ENTIDAD_CANTIDAD", "Cantidad");
                traducciones.Add("ENTIDAD_CODIGO", "Código");
                traducciones.Add("ENTIDAD_CODIGO_SEG", "Código de Seguridad");
                traducciones.Add("ENTIDAD_CONFIRMACION_PSW", "Confirmación de Contraseña");
                traducciones.Add("ENTIDAD_CRITICIDAD", "Criticidad");
                traducciones.Add("ENTIDAD_CUIL", "CUIL");
                traducciones.Add("ENTIDAD_CUOTAS", "COMPRA EN CÓMODAS CUOTAS");
                traducciones.Add("ENTIDAD_DESCRIPCION", "Descripción");
                traducciones.Add("ENTIDAD_DETALLE", "Detalle");
                traducciones.Add("ENTIDAD_DIRECCION", "Dirección");
                traducciones.Add("ENTIDAD_EMAIL", "Email");
                traducciones.Add("ENTIDAD_EMPRESA_TC", "Empresa");
                traducciones.Add("ENTIDAD_FECHA", "Fecha");
                traducciones.Add("ENTIDAD_FECHA_ALTA", "Fecha de Alta");
                traducciones.Add("ENTIDAD_FECHA_FIN", "Fecha Fin");
                traducciones.Add("ENTIDAD_FECHA_INICIO", "Fecha Inicio");
                traducciones.Add("ENTIDAD_FECHAHORA", "Fecha / Hora");
                traducciones.Add("ENTIDAD_IDIOMA", "Idioma");
                traducciones.Add("ENTIDAD_IMPORTE_TOTAL_PAGAR", "TOTAL A PAGAR");
                traducciones.Add("ENTIDAD_LOCALIDAD", "Localidad");
                traducciones.Add("ENTIDAD_MES_V", "Mes Vencimiento");
                traducciones.Add("ENTIDAD_NOMBRE", "Nombre");
                traducciones.Add("ENTIDAD_NUEVA_PSW", "Nueva Contraseña");
                traducciones.Add("ENTIDAD_NUMERO_TC", "Número de Tarjeta");
                traducciones.Add("ENTIDAD_PRECIO_UNITARIO", "Precio Unitario");
                traducciones.Add("ENTIDAD_PSW", "Contraseña");
                traducciones.Add("ENTIDAD_RAZON_SOCIAL", "Razón Social");
                traducciones.Add("ENTIDAD_SUBTOTAL", "Sub-Total");
                traducciones.Add("ENTIDAD_TELEFONO", "Teléfono");
                traducciones.Add("ENTIDAD_TITULAR", "Titular");
                traducciones.Add("ENTIDAD_USUARIO", "Usuario");
                traducciones.Add("ENTIDAD_USUARIO_MAIL", "Usuario / Mail");
                traducciones.Add("ERROR_CUIT", "EL CUIL INGRESADO NO ES VÁLIDO");
                traducciones.Add("ERROR_DATOS_TC_INVALIDOS", "LOS DATOS DE LA TARJETA SON INVÁLIDOS");
                traducciones.Add("ERROR_LIMITE_SALDO", "LIMITE DE SALDO DE COMPRA INSUFICIENTE");
                traducciones.Add("ERROR_LOGIN_CUENTA_BLOQUEADA", "Cuenta Bloqueada");
                traducciones.Add("ERROR_LOGIN_SESION_ACTIVA", "Usted ya tiene una Sesión Activa");
                traducciones.Add("ERROR_LOGIN_USUARIO_PSW_INVALIDOS", "Usuario o contraseña inválidos");
                traducciones.Add("ERROR_REGISTRO_USUARIO", "ERROR AL REGISTRAR EL USUARIO");
                traducciones.Add("ERROR_USUARIO_EXISTENTE", "EL USUARIO YA EXISTE");
                traducciones.Add("EXCEPCIONES_INDEX_TITULO", "DISCULPE, TUVIMOS UN PROBLEMA. POR FAVOR CONTACTE UN ADMINISTRADOR.");
                traducciones.Add("HOME_CONTACTO_FORMULARIO_LEYENDA", "Llená el formulario de abajo y te responderemos a la brevedad.");
                traducciones.Add("HOME_CONTACTO_LEYENDA", "Hay muchas maneras de contactarnos, envianos tu opinión o conoce nuestras oficinas comerciales.Encontrá la forma que mejor se adapte a tus tiempos.");
                traducciones.Add("HOME_CONTACTO_TITULO", "CONTACTO");
                traducciones.Add("HOME_CONTACTO_TITULO_2", "CONTANOS TU OPINIÓN!");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS", "HORARIOS");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_CERRADO", "Cerrado");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_DOMINGOS_FERIADOS", "Domingos y Feriados");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_SABADO", "Sábados");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_SEMANA", "Lun - Vie");
                traducciones.Add("HOME_CONTACTO_TITULO_MAPA", "ESTAMOS EN RAMOS MEJÍA");
                traducciones.Add("HOME_LEYENDA_FOLLETO", "Suscribite a nuestro boletín mensual y enterate de las últimas novedades.");
                traducciones.Add("HOME_LEYENDA_OFERTA_DIAS", "Días");
                traducciones.Add("HOME_LEYENDA_OFERTA_HORAS", "Horas");
                traducciones.Add("HOME_LEYENDA_OFERTA_MINUTOS", "Min");
                traducciones.Add("HOME_LEYENDA_OFERTA_SEGUNDOS", "Seg");
                traducciones.Add("HOME_LEYENDA_PRINCIPAL_1", "IMPRIMIENDO CONFIANZA");
                traducciones.Add("HOME_LEYENDA_PRINCIPAL_2", "EXPERTOS EN SERIGRAFÍA");
                traducciones.Add("HOME_SERVICIO_ECOLOGIA_LEYENDA_1", "TECNOLOGÍA SUSTENTABLE");
                traducciones.Add("HOME_SERVICIO_ECOLOGIA_LEYENDA_2", "ECOFRIENDLY");
                traducciones.Add("HOME_SERVICIO_ENVIO_LEYENDA_1", "SERVICIO DE ENVÍOS GRATIS");
                traducciones.Add("HOME_SERVICIO_ENVIO_LEYENDA_2", "logística especializada");
                traducciones.Add("HOME_SERVICIO_IMPORTADOS_LEYENDA_1", "PRODUCTOS IMPORTADOS");
                traducciones.Add("HOME_SERVICIO_IMPORTADOS_LEYENDA_2", "todo el año");
                traducciones.Add("HOME_SERVICIO_LUGAR_LEYENDA_1", "COMPRA DESDE DONDE ESTÉS");
                traducciones.Add("HOME_SERVICIO_LUGAR_LEYENDA_2", "servicio 24 x 7");
                traducciones.Add("HOME_SERVICIO_MATERIALES_LEYENDA_1", "MATERIA PRIMA DE PRIMERA CALIDAD");
                traducciones.Add("HOME_SERVICIO_MATERIALES_LEYENDA_2", "garantía de fábrica");
                traducciones.Add("HOME_SERVICIO_TECNICO_LEYENDA_1", "SERVICIO TÉCNICO ESPECIALIZADO");
                traducciones.Add("HOME_SERVICIO_TECNICO_LEYENDA_2", "Mantenimiento preventivo");
                traducciones.Add("HOME_TITULO_BENEFICIOS", "CONOCÉ NUESTROS SERVICIOS");
                traducciones.Add("HOME_TITULO_FOLLETO", "BOLETÍN DE NOTICIAS VIRTUAL");
                traducciones.Add("HOME_TITULO_OFERTA_SEMANAL", "OFERTA SEMANAL");
                traducciones.Add("LOGIN_OLVIDO_PSW", "¿Olvido su Contraseña?");
                traducciones.Add("LOGIN_TITULO_TENGO_CUENTA", "TENGO UNA CUENTA");
                traducciones.Add("LOGIN_TITULO_USUARIO_NUEVO", "SOY USUARIO NUEVO");
                traducciones.Add("ENTIDAD_PERFIL_USR", "Perfil Usuario"); 

                //LOYOUT TRADUCIDA EN SITIO

                traducciones.Add("MENSAJE_MAIL_COMPRA", "MUCHAS GRACIAS POR SU COMPRA. MUCHAS GRACIAS.");
                traducciones.Add("PRODUCTO_CARRITO_LEYENDA_SIN_PRODUCTOS", "Usted no tiene Productos en el Carrito.");
                traducciones.Add("PRODUCTO_CARRITO_TITULO", "PRODUCTOS EN CARRITO");
                traducciones.Add("PRODUCTO_CATALOGO_TITULO", "CATÁLOGO DE MÁQUINAS");
                traducciones.Add("PRODUCTO_DETALLE_TITULO", "FICHA DEL PRODUCTO");
                traducciones.Add("PRODUCTO_FINALIZAR_COMPRA_LEYENDA", "MUCHAS GRACIAS POR SU COMPRA, RECUERDE QUE HEMOS ENVIADO SU FACTURA POR CORREO. MUCHAS GRACIAS.");
                traducciones.Add("PRODUCTO_IMPRESORA_AUTOMATICA", "Impresora Automática");
                traducciones.Add("PRODUCTO_IMPRESORA_MANUAL", "Impresora Manual");
                traducciones.Add("PRODUCTO_IMPRESORA_SEMIAUTOMATICA", "Impresora Semi-Automática");
                traducciones.Add("PRODUCTO_IMPRESORA_TAMPOGRAFICA", "Impresora Tampográfica");
                traducciones.Add("PRODUCTO_LAMPARA_UV", "Lámpara UV");
                traducciones.Add("PRODUCTO_LEYENDA_ENVIO", "Envío Gratis");
                traducciones.Add("PRODUCTO_PAGO_CONTADO_LEYENDA", "EL PAGO AL CONTADO DEBERÁ REALIZARSE EN LAS OFICINAS COMERCIALES DE LA EMPRESA, HASTA ENTONCES LA FACTURA QUEDARÁ COMO PENDIENTE Y NO SE ENTREGARÁ LA MERCADERIA HASTA QUE EL PAGO NO ESTE REALIZADO. MUCHAS GRACIAS.");
                traducciones.Add("PRODUCTO_PAGO_CONTADO_TITULO", "PAGO CONTADO");
                traducciones.Add("PRODUCTO_PAGO_TC_TITULO", "PAGO CON TARJETA DE CRÉDITO");
                traducciones.Add("PRODUCTO_TUNEL_SECADO", "Túnel de Secado");
                traducciones.Add("REGISTRO_CLIENTE_TITULO", "FORMULARIO DE REGISTRO");
                traducciones.Add("VALIDACION_USURIO_OBLIGATORIO", "El Usuario es Obligatorio.");
                traducciones.Add("PRODUCTO_LEYENDA_NUEVO", "NUEVA");
                traducciones.Add("LOYOUT_BARRA_CUENTA_COMPRAS", "Mis Operaciones");
                traducciones.Add("CUENTA_VERPERMISOS_SACAR", "SACAR");
                traducciones.Add("CUENTA_VERPERMISOS_DAR", "DAR");
                traducciones.Add("CUENTA_VERPERMISOS_LEYENDA_OTORGADOS", "PERMISOS OTORGADOS");
                traducciones.Add("CUENTA_VERPERMISOS_LEYENDA_RESTANTES", "PERMISOS SIN OTORGAR");
                traducciones.Add("BOTON_EXPORTAR_XML", "EXPORTAR XML");
                traducciones.Add("CLIENTE_TITULO", "LISTADO DE CLIENTES");

                traducciones.Add("VENTAS_TITULO", "LISTADO DE VENTAS");
                traducciones.Add("ENTIDAD_IMPORTE_TOTAL", "Importe Total");

                traducciones.Add("BOTON_BITACORA_HIST", "CONSULTAR HISTÓRICO");

                traducciones.Add("TITULO_COMPRAS", "MIS COMPRAS");
                traducciones.Add("BOTON_REENVIAR_FACTURA", "ENVIAR FACTURA");

                traducciones.Add("BOTON_CANCELAR_OPERACION", "CANCELAR COMPRA");


            }

            if (idioma == "Eng")
            {
                traducciones.Add("BOTON_CANCELAR_OPERACION", "CANCEL PURCHASE");
                traducciones.Add("ENTIDAD_PERFIL_USR", "User Role");
                traducciones.Add("TITULO_COMPRAS", "MY PURCHASES");
                traducciones.Add("BOTON_REENVIAR_FACTURA", "SEND RECEIPT");
                traducciones.Add("BOTON_BITACORA_HIST", "CONSULT HISTORY");
                traducciones.Add("VENTAS_TITULO", "SALES LIST");
                traducciones.Add("ENTIDAD_IMPORTE_TOTAL", "Total Amount");
                traducciones.Add("CLIENTE_TITULO", "CUSTOMER LIST");
                traducciones.Add("BOTON_EXPORTAR_XML", "EXPORT XML");
                traducciones.Add("CUENTA_VERPERMISOS_LEYENDA_OTORGADOS", "PERMITS GRANTED");
                traducciones.Add("CUENTA_VERPERMISOS_LEYENDA_RESTANTES", "PERMITS NOT GRANTED");
                traducciones.Add("CUENTA_VERPERMISOS_DAR", "GIVE");
                traducciones.Add("CUENTA_VERPERMISOS_SACAR", "TAKE AWAY");
                traducciones.Add("LOYOUT_BARRA_CUENTA_COMPRAS", "My Operations");
                traducciones.Add("PRODUCTO_LEYENDA_NUEVO", "NEW");
                traducciones.Add("BITACORA_FILTROS_BOTON_FILTRAR", "FILTER");
                traducciones.Add("BITACORA_TITULO", "SYSTEM LOG");
                traducciones.Add("BITACORA_TITULO_FILTROS", "SEARCH FILTERS");
                traducciones.Add("BITACORA_WARNING_FECHAS_MAL", "THE START DATE MUST ALWAYS BE LESS THAN THE END");
                traducciones.Add("BITACORA_WARNING_SIN_FECHA_INICIO", "YOU CAN NOT ENTER END DATE WITHOUT START DATE");
                traducciones.Add("BOTON_ACTUALIZAR_DATOS_CUENTA", "UPDATE DATA");
                traducciones.Add("BOTON_ACTUALIZAR_PSW", "UPDATE PSW");
                traducciones.Add("BOTON_AGREGAR_CARRITO", "ADD TO CART");
                traducciones.Add("BOTON_ALTA_USR_ADMINISTRATIVO", "NEW ADMINISTRATIVE USR");
                traducciones.Add("BOTON_BLOQUEAR_CUENTA", "BLOCK");
                traducciones.Add("BOTON_CATALOGO", "CATALOG");
                traducciones.Add("BOTON_COMPRAR", "BUY");
                traducciones.Add("BOTON_DESBLOQUEAR_CUENTA", "UNLOCK");
                traducciones.Add("BOTON_ELIMINAR_CUENTA", "DELETE");
                traducciones.Add("BOTON_ENVIAR", "SEND");
                traducciones.Add("BOTON_FINALIZAR", "FINALIZE");
                traducciones.Add("BOTON_PERMISOS", "SEE PERMITS");
                traducciones.Add("BOTON_QUITAR_PRODUCTO", "REMOVE");
                traducciones.Add("BOTON_RECUPERAR_PSW", "RECOVER PSW");
                traducciones.Add("BOTON_REGISTRAR", "REGISTER");
                traducciones.Add("BOTON_SUSCRIPCION", "SUSCRIBE");
                traducciones.Add("BOTON_VER", "DETAIL");
                traducciones.Add("BOTON_VOLVER", "BACK");
                traducciones.Add("CATALOGO_SELECTOR_AUTO", "AUTOMATIC");
                traducciones.Add("CATALOGO_SELECTOR_MANUALES", "MANUALS");
                traducciones.Add("CATALOGO_SELECTOR_OTRAS", "OTHER");
                traducciones.Add("CATALOGO_SELECTOR_SEMIAUTO", "SEMI-AUTOMATIC");
                traducciones.Add("CATALOGO_SELECTOR_TAMPO", "TAMPOGRAPHIC");
                traducciones.Add("CATALOGO_SELECTOR_TODAS", "ALL");
                traducciones.Add("CUENTA_ACTUALIZAR_CLAVE_LEYENDA", "The new password has been updated correctly. Thank you.");
                traducciones.Add("CUENTA_ACTUALIZARDATOS_BOTON_GUARDAR", "SAVE");
                traducciones.Add("CUENTA_ACTUALIZARDATOS_TITULO", "UPDATE ACCOUNT DATA");
                traducciones.Add("CUENTA_BLOQUEARCUENTA_LEYENDA", "The user account has been successfully blocked.");
                traducciones.Add("CUENTA_CAMBIAR_CLAVE_TITULO", "ENTER THE NEW PASSWORD");
                traducciones.Add("CUENTA_DESBLOQUEARCUENTA_LEYENDA", "The user account has been unlocked successfully.");
                traducciones.Add("CUENTA_DETALLE_DATOSCONTACTO", "Contact information");
                traducciones.Add("CUENTA_DETALLE_DATOSPERSONALES", "Personal information");
                traducciones.Add("CUENTA_DETALLE_IDIOMA", "My language");
                traducciones.Add("CUENTA_DETALLE_TITULO", "MY ACCOUNT INFO");
                traducciones.Add("CUENTA_ENVIO_CLAVE_LEYENDA", "The new password has been sent to your user mail. Thank you.");
                traducciones.Add("CUENTA_LISTADO_USUARIOS", "LIST OF USERS");
                traducciones.Add("CUENTA_RECUPERAR_PSW_TITULO", "RECOVER PSW");
                traducciones.Add("CUENTA_VERPERMISOS_TITULO", "USER PERMITS");
                traducciones.Add("ENTIDAD_ACCION", "Action");
                traducciones.Add("ENTIDAD_ANIO_V", "Expiration Year");
                traducciones.Add("ENTIDAD_APELLIDO", "Surname");
                traducciones.Add("ENTIDAD_CANTIDAD", "Quantity");
                traducciones.Add("ENTIDAD_CODIGO", "Code");
                traducciones.Add("ENTIDAD_CODIGO_SEG", "Security Code");
                traducciones.Add("ENTIDAD_CONFIRMACION_PSW", "Confirm Password");
                traducciones.Add("ENTIDAD_CRITICIDAD", "Criticity");
                traducciones.Add("ENTIDAD_CUIL", "CUIL");
                traducciones.Add("ENTIDAD_CUOTAS", "FINANTIAL");
                traducciones.Add("ENTIDAD_DESCRIPCION", "Description");
                traducciones.Add("ENTIDAD_DETALLE", "Detail");
                traducciones.Add("ENTIDAD_DIRECCION", "Address");
                traducciones.Add("ENTIDAD_EMAIL", "Email");
                traducciones.Add("ENTIDAD_EMPRESA_TC", "Company");
                traducciones.Add("ENTIDAD_FECHA", "Date");
                traducciones.Add("ENTIDAD_FECHA_ALTA", "Start Date");
                traducciones.Add("ENTIDAD_FECHA_FIN", "End Date");
                traducciones.Add("ENTIDAD_FECHA_INICIO", "Start Date");
                traducciones.Add("ENTIDAD_FECHAHORA", "Date/Hr");
                traducciones.Add("ENTIDAD_IDIOMA", "Language");
                traducciones.Add("ENTIDAD_IMPORTE_TOTAL_PAGAR", "TOTAL AMOUNT");
                traducciones.Add("ENTIDAD_LOCALIDAD", "Location");
                traducciones.Add("ENTIDAD_MES_V", "Expiration Month");
                traducciones.Add("ENTIDAD_NOMBRE", "Name");
                traducciones.Add("ENTIDAD_NUEVA_PSW", "New Psw");
                traducciones.Add("ENTIDAD_NUMERO_TC", "Credit Number");
                traducciones.Add("ENTIDAD_PRECIO_UNITARIO", "Price");
                traducciones.Add("ENTIDAD_PSW", "Psw");
                traducciones.Add("ENTIDAD_RAZON_SOCIAL", "Company Name");
                traducciones.Add("ENTIDAD_SUBTOTAL", "Sub-Total");
                traducciones.Add("ENTIDAD_TELEFONO", "Number");
                traducciones.Add("ENTIDAD_TITULAR", "Full Name");
                traducciones.Add("ENTIDAD_USUARIO", "User");
                traducciones.Add("ENTIDAD_USUARIO_MAIL", "User / Mail Account");
                traducciones.Add("ERROR_CUIT", "Invalid CUIL");
                traducciones.Add("ERROR_DATOS_TC_INVALIDOS", "THE DATA ON THE CARD IS INVALID");
                traducciones.Add("ERROR_LIMITE_SALDO", "LIMIT OF INSUFFICIENT PURCHASE BALANCE");
                traducciones.Add("ERROR_LOGIN_CUENTA_BLOQUEADA", "Blocked account");
                traducciones.Add("ERROR_LOGIN_SESION_ACTIVA", "You already have an Active Session");
                traducciones.Add("ERROR_LOGIN_USUARIO_PSW_INVALIDOS", "Invalid username or password");
                traducciones.Add("ERROR_REGISTRO_USUARIO", "ERROR WHEN REGISTERING THE USER");
                traducciones.Add("ERROR_USUARIO_EXISTENTE", "USER ALREADY EXISTS");
                traducciones.Add("EXCEPCIONES_INDEX_TITULO", "EXCUSE US, WE HAD A PROBLEM HERE. PLEASE CONTACT AN ADMIN. THANK YOU VERY MUCH");
                traducciones.Add("HOME_CONTACTO_FORMULARIO_LEYENDA", "Fill out the form below and we will respond as soon as possible.");
                traducciones.Add("HOME_CONTACTO_LEYENDA", "There are many ways to contact us, send us your opinion or visit our commercial offices.");
                traducciones.Add("HOME_CONTACTO_TITULO", "CONTACT US");
                traducciones.Add("HOME_CONTACTO_TITULO_2", "TELL US WHAT YOU THINK!");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS", "TIMETABLE");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_CERRADO", "Closed");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_DOMINGOS_FERIADOS", "Sunday and Hollydays");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_SABADO", "Saturday");
                traducciones.Add("HOME_CONTACTO_TITULO_HORARIOS_SEMANA", "Mon - Fri");
                traducciones.Add("HOME_CONTACTO_TITULO_MAPA", "WE ARE AT RAMOS MEJIA");
                traducciones.Add("HOME_LEYENDA_FOLLETO", "Subscribe to our monthly newsletter and find out about the latest news.");
                traducciones.Add("HOME_LEYENDA_OFERTA_DIAS", "Days");
                traducciones.Add("HOME_LEYENDA_OFERTA_HORAS", "Hours");
                traducciones.Add("HOME_LEYENDA_OFERTA_MINUTOS", "Min");
                traducciones.Add("HOME_LEYENDA_OFERTA_SEGUNDOS", "Sec");
                traducciones.Add("HOME_LEYENDA_PRINCIPAL_1", "PRINTING TRUST");
                traducciones.Add("HOME_LEYENDA_PRINCIPAL_2", "EXPERTS IN SERIGRAPH");
                traducciones.Add("HOME_SERVICIO_ECOLOGIA_LEYENDA_1", "SUSTAINABLE TECHNOLOGY");
                traducciones.Add("HOME_SERVICIO_ECOLOGIA_LEYENDA_2", "ECOFRIENDLY");
                traducciones.Add("HOME_SERVICIO_ENVIO_LEYENDA_1", "FREE SHIPPING SERVICE");
                traducciones.Add("HOME_SERVICIO_ENVIO_LEYENDA_2", "specialized logistics");
                traducciones.Add("HOME_SERVICIO_IMPORTADOS_LEYENDA_1", "IMPORTED PRODUCTS");
                traducciones.Add("HOME_SERVICIO_IMPORTADOS_LEYENDA_2", "all year");
                traducciones.Add("HOME_SERVICIO_LUGAR_LEYENDA_1", "PURCHASE FROM WHERE YOU ARE");
                traducciones.Add("HOME_SERVICIO_LUGAR_LEYENDA_2", "24 x 7 service");
                traducciones.Add("HOME_SERVICIO_MATERIALES_LEYENDA_1", "RAW MATERIAL OF FIRST QUALITY");
                traducciones.Add("HOME_SERVICIO_MATERIALES_LEYENDA_2", "Factory guarantee");
                traducciones.Add("HOME_SERVICIO_TECNICO_LEYENDA_1", "SPECIALIZED TECHNICAL SERVICE");
                traducciones.Add("HOME_SERVICIO_TECNICO_LEYENDA_2", "Preventive Maintenance");
                traducciones.Add("HOME_TITULO_BENEFICIOS", "KNOW OUR SERVICES");
                traducciones.Add("HOME_TITULO_FOLLETO", "VIRTUAL NEWS");
                traducciones.Add("HOME_TITULO_OFERTA_SEMANAL", "WEEKLY OFFER");
                traducciones.Add("LOGIN_OLVIDO_PSW", "Forgot Password?");
                traducciones.Add("LOGIN_TITULO_TENGO_CUENTA", "I HAVE AN ACCOUNT");
                traducciones.Add("LOGIN_TITULO_USUARIO_NUEVO", "NEW USER");

                traducciones.Add("MENSAJE_MAIL_COMPRA", "THANK YOU SO MUCH FOR YOUR PURCHASE. THANK YOU.");
                traducciones.Add("PRODUCTO_CARRITO_LEYENDA_SIN_PRODUCTOS", "You do not have Products in the Cart.");
                traducciones.Add("PRODUCTO_CARRITO_TITULO", "PRODUCTS IN CART");
                traducciones.Add("PRODUCTO_CATALOGO_TITULO", "MACHINE CATALOG");
                traducciones.Add("PRODUCTO_DETALLE_TITULO", "PRODUCT SHEET");
                traducciones.Add("PRODUCTO_FINALIZAR_COMPRA_LEYENDA", "THANK YOU SO MUCH FOR YOUR PURCHASE, REMEMBER THAT WE HAVE SENT YOUR INVOICE BY MAIL.THANK YOU.");
                traducciones.Add("PRODUCTO_IMPRESORA_AUTOMATICA", "Automatic Printer");
                traducciones.Add("PRODUCTO_IMPRESORA_MANUAL", "Manual Printer");
                traducciones.Add("PRODUCTO_IMPRESORA_SEMIAUTOMATICA", "Semi-Automatic Printer");
                traducciones.Add("PRODUCTO_IMPRESORA_TAMPOGRAFICA", "Tampographic Printer");
                traducciones.Add("PRODUCTO_LAMPARA_UV", "Uv Lamp");
                traducciones.Add("PRODUCTO_LEYENDA_ENVIO", "Free shipping");
                traducciones.Add("PRODUCTO_PAGO_CONTADO_LEYENDA", "THE PAYMENT TO THE COUNTED SHOULD BE MADE IN THE COMMERCIAL OFFICES OF THE COMPANY, THEN THE INVOICE WILL BE PENDING AND THE MERCHANDISE WILL NOT BE DELIVERED UNTIL THE PAYMENT IS NOT MADE. THANK YOU.");
                traducciones.Add("PRODUCTO_PAGO_CONTADO_TITULO", "CASH PAYMENT");
                traducciones.Add("PRODUCTO_PAGO_TC_TITULO", "CREDIT CARD PAYMENT");
                traducciones.Add("PRODUCTO_TUNEL_SECADO", "Drying tunnel");
                traducciones.Add("REGISTRO_CLIENTE_TITULO", "REGISTER FORM");
                traducciones.Add("VALIDACION_USURIO_OBLIGATORIO", "Usr is Obligatory");
            }


            return traducciones;


        }

    }
}