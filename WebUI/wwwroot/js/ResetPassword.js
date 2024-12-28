document.addEventListener('DOMContentLoaded', function () {
    var form = document.getElementById('resetPasswordForm');
    var formWrapper = document.querySelector('.form-wrapper');

    function showAlerts(event) {
        var errorMessages = [];

        var userNameInput = document.querySelector('[asp-for="UserName"]');
        var nuevaContraseñaInput = document.getElementById('NuevaContraseña');
        var confirmarContraseñaInput = document.getElementById('ConfirmarContraseña');

        if (!userNameInput.value.trim()) {
            errorMessages.push('El campo Nombre de Usuario es obligatorio.');
        }

        if (!nuevaContraseñaInput.value.trim()) {
            errorMessages.push('El campo Nueva Contraseña es obligatorio.');
        }

        if (!confirmarContraseñaInput.value.trim()) {
            errorMessages.push('El campo Confirmar Contraseña es obligatorio.');
        }

        if (errorMessages.length > 0) {
            // Mostrar mensaje de advertencia como alerta del navegador
            alert(errorMessages.join('\n'));
            event.preventDefault(); // Evitar el envío del formulario
        }
    }

    form.addEventListener('submit', showAlerts);

    // Ajusta la altura del formulario al cargar la página
    adjustFormHeight();

    function adjustFormHeight() {
        var hasAlert = document.querySelector('.alerts-container').childElementCount > 0;
        formWrapper.style.height = hasAlert ? 'auto' : '370px';
    }

    // Observa cambios en el contenedor de alertas
    var observer = new MutationObserver(adjustFormHeight);
    observer.observe(document.querySelector('.alerts-container'), {
        childList: true,
        subtree: true
    });
});

function passwordVisibility() {
    const passwordField = document.getElementById("NuevaContraseña");
    const confirmPasswordField = document.getElementById("ConfirmarContraseña");
    const showPass = document.getElementById("showPass");
    const hidePass = document.getElementById("hidePass");

    if (passwordField.type === "password") {
        passwordField.type = "text";
        confirmPasswordField.type = "text";
        showPass.style.display = "none";
        hidePass.style.display = "block";
    } else {
        passwordField.type = "password";
        confirmPasswordField.type = "password";
        showPass.style.display = "block";
        hidePass.style.display = "none";
    }
}
