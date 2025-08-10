// Sidebar Controller para ASP.NET MVC
; (() => {
    class SidebarController {
        constructor() {
            this.sidebar = null
            this.toggleBtn = null
            this.mainContent = null
            this.overlay = null
            this.isOpen = true
            this.isMobile = window.innerWidth <= 768

            this.init()
            this.bindEvents()
        }

        init() {
            // Obtener elementos existentes
            this.sidebar = document.querySelector(".sidebar")
            this.mainContent = document.querySelector(".main-content")

            // Crear botón toggle
            this.createToggleButton()
            this.toggleBtn = document.querySelector(".sidebar-toggle")

            // Crear overlay para móvil
            this.createOverlay()
            this.overlay = document.querySelector(".sidebar-overlay")

            // Configuración inicial
            this.updateLayout()
        }

        createToggleButton() {
            const toggleBtn = document.createElement("button")
            toggleBtn.className = "sidebar-toggle"
            toggleBtn.innerHTML = '<i class="fas fa-bars"></i>'
            toggleBtn.setAttribute("aria-label", "Toggle sidebar")
            toggleBtn.type = "button"
            document.body.appendChild(toggleBtn)
        }

        createOverlay() {
            const overlay = document.createElement("div")
            overlay.className = "sidebar-overlay"
            document.body.appendChild(overlay)
        }

        bindEvents() {


            // Toggle button click
            if (this.toggleBtn) {
                this.toggleBtn.addEventListener("click", (e) => {
                    e.preventDefault()
                    this.toggleSidebar()
                })
            }

            // Overlay click (mobile)
            document.addEventListener("click", (e) => {
                if (e.target && e.target.classList.contains("sidebar-overlay")) {
                    this.closeSidebar()
                }
            })

            // Escape key
            document.addEventListener("keydown", (e) => {
                if (e.key === "Escape" && this.isOpen && this.isMobile) {
                    this.closeSidebar()
                }
            })

            // Window resize
            window.addEventListener("resize", () => {
                const wasMobile = this.isMobile
                this.isMobile = window.innerWidth <= 768

                if (wasMobile !== this.isMobile) {
                    this.updateLayout()
                }
            })
        }

        toggleSidebar() {
            if (this.isOpen) {
                this.closeSidebar()
            } else {
                this.openSidebar()
            }
        }

        openSidebar() {
            this.isOpen = true
            this.updateLayout()
        }

        closeSidebar() {
            this.isOpen = false
            this.updateLayout()
        }

        updateLayout() {
            if (!this.sidebar || !this.mainContent || !this.toggleBtn) return

            if (this.isMobile) {
                // Mobile layout
                if (this.isOpen) {
                    this.sidebar.classList.add("visible")
                    this.sidebar.classList.remove("hidden")
                    if (this.overlay) this.overlay.classList.add("active")
                    this.toggleBtn.classList.add("sidebar-open")
                } else {
                    this.sidebar.classList.remove("visible")
                    this.sidebar.classList.add("hidden")
                    if (this.overlay) this.overlay.classList.remove("active")
                    this.toggleBtn.classList.remove("sidebar-open")
                }
                this.mainContent.classList.remove("sidebar-hidden")
            } else {
                // Desktop layout
                if (this.overlay) this.overlay.classList.remove("active")

                if (this.isOpen) {
                    this.sidebar.classList.remove("hidden")
                    this.sidebar.classList.remove("visible")
                    this.mainContent.classList.remove("sidebar-hidden")
                    this.toggleBtn.classList.add("sidebar-open")
                } else {
                    this.sidebar.classList.add("hidden")
                    this.sidebar.classList.remove("visible")
                    this.mainContent.classList.add("sidebar-hidden")
                    this.toggleBtn.classList.remove("sidebar-open")
                }
            }

            // Update toggle button icon
            const icon = this.toggleBtn.querySelector("i")
            if (icon) {
                icon.className = this.isOpen ? "fas fa-times" : "fas fa-bars"
            }
        }
    }

    // Initialize when DOM is loaded
    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", () => {
            new SidebarController()
        })
    } else {
        new SidebarController()
    }

    // Export for global access if needed
    window.SidebarController = SidebarController
})()
