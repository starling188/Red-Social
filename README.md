Red Social – Backend
Este es el backend oficial del mini proyecto de Red Social, una plataforma que permite a los usuarios crear y compartir publicaciones, interactuar con amigos y gestionar su perfil.

🚀 Tecnologías y Dependencias Principales

ASP.NET Core MVC: Framework principal para la creación de la aplicación web.

C#: Lenguaje de programación.

Entity Framework Core: ORM para la persistencia de datos con un enfoque de Code First.

Automapper: Librería para el mapeo de objetos entre capas.

Bootstrap: Framework de CSS para un diseño visualmente atractivo y responsivo.

JavaScript & CSS: Para la lógica y estilos del frontend.

📁 Estructura del Proyecto

El proyecto sigue la Arquitectura Onion para garantizar una separación de responsabilidades clara y un acoplamiento bajo.

Core/: Contiene la lógica de negocio y las entidades del dominio.

Infrastructure/: Implementación de los repositorios y servicios de datos (Entity Framework).

Application/: Lógica de la aplicación que interactúa con las capas inferiores.

Presentation/: La capa de interfaz de usuario (MVC Views, Controllers, ViewModels).

Shared/: Contiene servicios comunes como el servicio de correo electrónico.

🔐 Autenticación y Seguridad

La autenticación se basa en sesiones gestionadas por ASP.NET Core MVC. Se garantiza que las páginas de Publicaciones, Mi Perfil y Amigos solo sean accesibles para usuarios logueados.

Endpoints de Autenticación

/Login: Muestra el formulario de inicio de sesión y procesa la autenticación.

/Registro: Muestra el formulario de registro y crea nuevos usuarios inactivos.

/MiPerfil: Permite a los usuarios actualizar su información de perfil.

⚙️ Configuración del Entorno

El proyecto se configura a través de los archivos de appsettings.json de ASP.NET Core, donde se define la cadena de conexión a la base de datos y otras configuraciones.

Variables Clave en appsettings.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SocialNetworkDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "SmtpSettings": {
    "Server": "smtp.ejemplo.com",
    "Port": 587,
    "Username": "tu_correo@ejemplo.com",
    "Password": "tu_password"
  }
}
