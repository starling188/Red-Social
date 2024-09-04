document.addEventListener('DOMContentLoaded', function () {
    var form = document.getElementById('loginForm');
    var card = document.querySelector('.card');

    function adjustCardHeight() {
        var hasAlert = document.querySelector('.alert');
        if (hasAlert) {
            card.style.height = 'auto'; // Ajusta la altura según el contenido
        } else {
            card.style.height = '370px'; // Establece una altura fija si no hay alertas
        }
    }

    // Ajusta la altura del card al cargar la página
    adjustCardHeight();

    form.addEventListener('submit', function (event) {
        var errorMessages = [];

        var correoInput = document.getElementById('correoInput');
        var contrasenaInput = document.getElementById('contrasenaInput');

        if (!correoInput.value.trim()) {
            errorMessages.push('El campo Correo es obligatorio.');
        }

        if (!contrasenaInput.value.trim()) {
            errorMessages.push('El campo Contraseña es obligatorio.');
        }

        if (errorMessages.length > 0) {
            event.preventDefault(); // Previene el envío del formulario

            // Mostrar mensaje de advertencia como alerta del navegador
            alert(errorMessages.join('\n'));

            // Limpiar los campos
            correoInput.value = '';
            contrasenaInput.value = '';
        }
    });

    // Ajusta la altura del card después de cualquier actualización de alertas
    var observer = new MutationObserver(adjustCardHeight);
    observer.observe(document.querySelector('.alerts-container'), {
        childList: true,
        subtree: true
    });
});
