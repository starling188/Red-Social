# 🌐 Social Network — Backend

Este es el backend oficial del proyecto **Social Network**, una red social desarrollada en **ASP.NET Core MVC (6/7/8)** con arquitectura **Clean**.  
La aplicación permite a los usuarios registrarse, autenticarse, crear publicaciones, interactuar con amigos y administrar su perfil, manteniendo altos estándares de seguridad y buenas prácticas de desarrollo.

---

## 🚀 Tecnologías y dependencias principales

- [ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core) — Framework principal para el desarrollo de la aplicación web.
- [Entity Framework Core](https://learn.microsoft.com/ef/core) — ORM con enfoque **Code First** para la persistencia de datos.
- [SQL Server](https://www.microsoft.com/sql-server) — Base de datos relacional.
- [AutoMapper](https://automapper.org/) — Mapeo automático entre entidades y ViewModels/DTOs.
- [Bootstrap](https://getbootstrap.com/) — Framework CSS para diseño responsivo y visualmente entendible.
- [MailKit](https://github.com/jstedfast/MailKit) — Servicio de correo electrónico (activación de cuenta, recuperación de contraseña).
- [Repositorio y servicio genéricos] — Patrón para desacoplar la lógica de acceso a datos y negocio.
- [Clean Architecture] — Patrón de arquitectura aplicada al 100% para separación de responsabilidades.

---

## 📁 Estructura del proyecto

- `Application/` — Servicios de aplicación, DTOs, validaciones, reglas de negocio.
- `Domain/` — Entidades principales y contratos.
- `Infrastructure/` — Persistencia con EF Core, repositorios, servicio de correo.
- `WebUI/` — Controladores MVC, vistas (Razor), filtros y middlewares.
- `Shared/` — Servicios transversales como correo electrónico.
- `Program.cs` — Punto de entrada principal, configuración de servicios y middleware.

---

## 🔐 Autenticación y Seguridad

- Sistema de autenticación con **cookies**.  
- Registro y login con validaciones de usuario único y estado de activación.  
- Contraseñas seguras almacenadas con **hashing** (`bcrypt`/ASP.NET Identity).  
- Seguridad adicional:
  - Usuarios inactivos deben activar su cuenta vía **correo electrónico con token de 5 minutos**.
  - Se restringe acceso a rutas privadas (Publicaciones, Amigos, Mi Perfil) si no se está logueado.

### Endpoints y funcionalidades clave

#### 👤 Autenticación
- `GET /login` — Vista de inicio de sesión.
- `POST /login` — Validación de credenciales.
- `GET /register` — Vista de registro de usuario.
- `POST /register` — Registro de nuevos usuarios (inactivos hasta activación vía correo).
- `POST /forgot-password` — Restablecer contraseña con envío de nueva clave al correo.
- `GET /activate?token=xxxx` — Activación de cuenta vía token de correo.

#### 🏠 Publicaciones (Home)
- Crear, listar, editar y eliminar publicaciones propias.
- Agregar **comentarios** y **replies** a publicaciones.
- Soporte para imágenes y videos de YouTube.

#### 👥 Amigos
- Ver publicaciones de amigos.
- Agregar/eliminar amigos.
- Listado de amigos con foto, nombre, apellido y usuario.

#### 📝 Mi Perfil
- Editar datos del usuario logueado (nombre, apellido, teléfono, correo, contraseña, foto).
- Validaciones en formularios (contraseña opcional, formato de teléfono RD, etc.).

---

## ⚙️ Configuración del entorno

Crea un archivo `.env` o configura `appsettings.json` con las siguientes variables:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SocialNetwork;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "SocialNetwork",
    "SenderEmail": "tu_correo@gmail.com",
    "Password": "clave_de_aplicacion"
  }
}
```

---

## ✅ Consideraciones generales

- Uso obligatorio de **ViewModels** con validaciones.
- Arquitectura **Clean** implementada correctamente.
- Uso de **repositorio y servicio genéricos**.
- Uso de **AutoMapper** para conversión entre entidades y DTOs.
- Diseño visual con **Bootstrap** para vistas limpias y entendibles.
- Seguridad: acceso restringido a secciones privadas si no se está autenticado.
