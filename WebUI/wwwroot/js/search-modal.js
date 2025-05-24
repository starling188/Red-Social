// Funcionalidad de búsqueda
document.addEventListener("DOMContentLoaded", () => {
    const searchButton = document.getElementById("searchButton")
    const searchModal = document.getElementById("searchModal")
    const closeSearch = document.getElementById("closeSearch")
    const searchInput = document.getElementById("searchInput")
    const searchResults = document.getElementById("searchResults")
    const searchSpinner = document.getElementById("searchSpinner")
    const noResults = document.getElementById("noResults")

    let searchTimeout = null

    // Abrir modal de búsqueda
    searchButton.addEventListener("click", (e) => {
        e.preventDefault()
        searchModal.style.display = "block"
        searchModal.classList.add("show")
        searchInput.focus()
        // Limpiar resultados anteriores
        clearSearchResults()
    })

    // Cerrar modal de búsqueda
    closeSearch.addEventListener("click", () => {
        closeModal()
    })

    // Cerrar modal al hacer clic fuera del contenedor
    searchModal.addEventListener("click", (e) => {
        if (e.target === searchModal) {
            closeModal()
        }
    })

    // Cerrar modal con tecla Escape
    document.addEventListener("keydown", (e) => {
        if (e.key === "Escape" && searchModal.style.display === "block") {
            closeModal()
        }
    })

    // Función para cerrar el modal
    function closeModal() {
        searchModal.classList.remove("show")
        setTimeout(() => {
            searchModal.style.display = "none"
        }, 300)
        clearSearchResults()
    }

    // Función para limpiar resultados
    function clearSearchResults() {
        searchInput.value = ""
        searchResults.innerHTML = ""
        noResults.style.display = "none"
        searchSpinner.style.display = "none"
    }

    // Buscar mientras se escribe
    searchInput.addEventListener("input", () => {
        const query = searchInput.value.trim()

        // Limpiar el timeout anterior
        if (searchTimeout) {
            clearTimeout(searchTimeout)
        }

        // Si el campo está vacío, limpiar resultados
        if (query === "") {
            searchResults.innerHTML = ""
            noResults.style.display = "none"
            searchSpinner.style.display = "none"
            return
        }

        // Mostrar spinner
        searchSpinner.style.display = "block"
        noResults.style.display = "none"
        searchResults.innerHTML = ""

        // Esperar 300ms antes de hacer la búsqueda para evitar demasiadas peticiones
        searchTimeout = setTimeout(() => {
            fetchSearchResults(query)
        }, 300)
    })

    // Función para buscar usuarios
    function fetchSearchResults(query) {
        fetch(`/Home/SearchByUserNames?username=${encodeURIComponent(query)}`)
            .then((response) => {
                if (!response.ok) {
                    throw new Error("Error en la respuesta del servidor")
                }
                return response.json()
            })
            .then((data) => {
                // Ocultar spinner
                searchSpinner.style.display = "none"

                // Limpiar resultados anteriores
                searchResults.innerHTML = ""

                // Verificar si hay resultados
                if (!data || data.length === 0) {
                    noResults.style.display = "block"
                    return
                }

                // Mostrar resultados
                data.forEach((user) => {
                    createSearchResultItem(user)
                })
            })
            .catch((error) => {
                console.error("Error en la búsqueda:", error)
                searchSpinner.style.display = "none"
                searchResults.innerHTML = ""
                noResults.style.display = "block"
            })
    }

    // Función para crear un elemento de resultado
    function createSearchResultItem(user) {
        const resultItem = document.createElement("div")
        resultItem.className = "search-result-item"

        // Asegurar que las propiedades existan y mapear según la estructura de User
        const username = user.userName || user.username || "Usuario"
        const name = user.nombre || user.name || ""
        const photo = user.fotoPerfil || user.photo || "https://via.placeholder.com/50"

        resultItem.innerHTML = `
      <img src="${photo}" alt="${username}" onerror="this.src='https://via.placeholder.com/50'">
      <div class="search-result-info">
        <span class="search-result-username">${username}</span>
        <span class="search-result-name">${name}</span>
      </div>
    `

        // Añadir evento de clic para ir al perfil
        resultItem.addEventListener("click", () => {
            // Puedes ajustar esta URL según tu estructura de rutas
            window.location.href = `/Home/UserProfile?username=${encodeURIComponent(username)}`
        })

        searchResults.appendChild(resultItem)
    }
})
