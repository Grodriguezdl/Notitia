# Notitia 

Sistema bancario de escritorio desarrollado en **C# con Windows Forms** y **IBM Db2**. Simula la banca en línea de un banco ficticio: los clientes pueden abrir cuentas, transferir dinero, consultar su historial y solicitar préstamos, mientras que administradores y empleados gestionan usuarios y solicitudes. Incluye inicio de sesión con **tarjeta RFID**. Proyecto presentado en **Expotec 2024**.

> *Desktop banking system built in C# (Windows Forms + IBM Db2). Customers can open accounts, transfer money, review their transaction history and apply for loans, while administrators and employees manage users and requests. Includes RFID card login. Presented at Expotec 2024.*

## Funcionalidades

**Acceso y seguridad**
- Inicio de sesión con usuario y contraseña, o acercando una **tarjeta RFID** a un lector conectado por puerto serie.
- Registro de nuevos usuarios en dos pasos (datos personales y dirección).
- Recuperación de contraseña por correo electrónico (SMTP).
- Cambio de contraseña y actualización de datos personales.
- Tres roles con vistas diferentes: **Administrador**, **Empleado** y **Cliente**.

**Operaciones bancarias**
- Resumen con las cuentas del usuario, sus saldos y préstamos activos.
- Apertura de nuevas cuentas, validando DPI y contraseña.
- Afiliación de cuentas de terceros y de otros bancos.
- Transferencias entre cuentas propias, a otras cuentas de Notitia y hacia otros bancos, actualizando los saldos de origen y destino.
- Historial de transacciones.
- Calculadora de cambio de divisas (quetzales y dólares, compra y venta).

**Productos y solicitudes**
- Catálogo de productos: tarjetas de débito, crédito (clásica, gold, platinum, black) y prepago.
- Solicitud de préstamos y de nuevos productos.

**Administración** (Administrador y Empleado)
- Consulta y búsqueda de usuarios.
- Deshabilitación de usuarios junto con sus cuentas.
- Revisión y gestión de solicitudes de préstamos y de productos.

## Tecnologías

- **C#** y **.NET Framework 4.7.2**
- **Windows Forms**, con los controles de **Guna.UI2.WinForms** y **RJCodeAdvance.RJControls**
- **IBM Db2** como base de datos, mediante el proveedor `IBM.Data.DB2`
- **System.IO.Ports** para la lectura de tarjetas RFID por puerto serie
- **System.Net.Mail** para el envío de correos
- Programación Orientada a Objetos

## Estructura del proyecto

```
Notitia/
├── Notitia/                 # Código fuente (formularios, conexión y lógica)
│   ├── Resources/           # Imágenes e íconos de la interfaz
│   ├── Conexion.cs          # Cadenas de conexión a Db2
│   ├── Sesion.cs            # Datos del usuario con sesión activa
│   └── ...
├── database/
│   └── script-base-de-datos.sql   # Creación de tablas y datos de prueba
└── Notitia.sln              # Solución de Visual Studio
```

## Requisitos

- Windows
- Visual Studio 2019 o superior, con la carga de trabajo **Desarrollo de escritorio de .NET**
- **IBM Db2** (por ejemplo, Db2 Community Edition) con el cliente para .NET instalado, que aporta `IBM.Data.DB2`
- Opcional: un lector RFID que envíe el UID de la tarjeta por puerto serie

## Instalación y ejecución

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Grodriguezdl/Notitia.git
   ```
2. Abre la **ventana de comandos de Db2** en la carpeta `database` y crea la base de datos:
   ```bash
   db2 -tvf script-base-de-datos.sql
   ```
3. Abre `Notitia.sln` en Visual Studio. Los paquetes NuGet se restauran automáticamente al compilar.
4. Ejecuta el proyecto con **F5**.

**Configuración opcional**
- **Lector RFID:** el puerto está definido en `Login.cs` (`COM8`, 9600 baudios). Cámbialo al puerto donde esté conectado tu lector.
- **Correo de recuperación:** en `DCorreoSoporte.cs`, coloca una cuenta de correo y su contraseña de aplicación.

### Usuarios de prueba

| Rol | Usuario | Contraseña |
|---|---|---|
| Administrador | `admin` | `12345` |
| Empleado | `empleado` | `12345` |
| Cliente | `cliente` | `12345` |

## Lo que aprendí

- Diseñar una base de datos relacional para operaciones bancarias y conectarla a una aplicación de escritorio con Db2.
- Mantener la consistencia de los saldos al registrar una transferencia y actualizar las cuentas de origen y destino.
- Controlar qué ve cada usuario según su rol.
- Integrar hardware con software, leyendo tarjetas RFID por puerto serie.
- Enviar correos automáticos desde una aplicación mediante SMTP.

## Autores

- **Gabriel Rodríguez** · [GitHub](https://github.com/Grodriguezdl) 
