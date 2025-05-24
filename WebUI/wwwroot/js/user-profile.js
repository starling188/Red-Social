// Funcionalidad para la página de perfil de usuario
document.addEventListener("DOMContentLoaded", () => {
    // Funcionalidad para las pestañas
    const tabs = document.querySelectorAll(".profile-tab")
    tabs.forEach((tab) => {
        tab.addEventListener("click", function () {
            // Remover la clase active de todas las pestañas
            tabs.forEach((t) => t.classList.remove("active"))
            // Añadir la clase active a la pestaña clickeada
            this.classList.add("active")

            // Aquí podrías añadir lógica para mostrar diferentes contenidos
            // según la pestaña seleccionada
        })
    })

    // Funcionalidad para el botón de seguir
    const followButton = document.querySelector(".follow-button")
    if (followButton) {
        followButton.addEventListener("click", function () {
            if (this.textContent === "Seguir") {
                this.textContent = "Siguiendo"
                this.classList.add("following")

                // Aquí podrías añadir una llamada AJAX para seguir al usuario
                // Por ejemplo:
                // followUser(username);
            } else {
                this.textContent = "Seguir"
                this.classList.remove("following")

                // Aquí podrías añadir una llamada AJAX para dejar de seguir al usuario
                // Por ejemplo:
                // unfollowUser(username);
            }
        })

        // Efecto hover para el botón cuando está en estado "Siguiendo"
        followButton.addEventListener("mouseenter", function () {
            if (this.textContent === "Siguiendo") {
                this.textContent = "Dejar de seguir"
                this.style.backgroundColor = "#dc3545"
            }
        })

        followButton.addEventListener("mouseleave", function () {
            if (this.textContent === "Dejar de seguir") {
                this.textContent = "Siguiendo"
                this.style.backgroundColor = "#444"
            }
        })
    }

    // Funcionalidad para hacer clic en una publicación
    const posts = document.querySelectorAll(".profile-post")
    posts.forEach((post) => {
        post.addEventListener("click", () => {
            // Aquí podrías abrir un modal con la publicación completa
            // Por ejemplo:
            // openPostModal(postId);
            console.log("Post clicked")
        })
    })
})

// Función para seguir a un usuario (ejemplo)
function followUser(username) {
    fetch(`/Home/FollowUser?username=${encodeURIComponent(username)}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            // Añadir token CSRF si es necesario
        },
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Error al seguir al usuario")
            }
            return response.json()
        })
        .then((data) => {
            console.log("Usuario seguido con éxito", data)
            // Actualizar contador de seguidores
            updateFollowersCount(1)
        })
        .catch((error) => {
            console.error("Error:", error)
            // Revertir cambios en la UI
            const followButton = document.querySelector(".follow-button")
            if (followButton) {
                followButton.textContent = "Seguir"
                followButton.classList.remove("following")
            }
        })
}

// Función para dejar de seguir a un usuario (ejemplo)
function unfollowUser(username) {
    fetch(`/Home/UnfollowUser?username=${encodeURIComponent(username)}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            // Añadir token CSRF si es necesario
        },
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Error al dejar de seguir al usuario")
            }
            return response.json()
        })
        .then((data) => {
            console.log("Usuario dejado de seguir con éxito", data)
            // Actualizar contador de seguidores
            updateFollowersCount(-1)
        })
        .catch((error) => {
            console.error("Error:", error)
            // Revertir cambios en la UI
            const followButton = document.querySelector(".follow-button")
            if (followButton) {
                followButton.textContent = "Siguiendo"
                followButton.classList.add("following")
            }
        })
}

// Función para actualizar el contador de seguidores
function updateFollowersCount(change) {
    const followersCountElement = document.querySelector(".stat-item:nth-child(2) .stat-value")
    if (followersCountElement) {
        const currentCount = Number.parseInt(followersCountElement.textContent)
        followersCountElement.textContent = currentCount + change
    }
}
