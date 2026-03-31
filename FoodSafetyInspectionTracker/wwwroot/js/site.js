(() => {
    const html = document.documentElement;
    const toggleButton = document.getElementById("themeToggle");

    if (!toggleButton) {
        return;
    }

    const getTheme = () => html.getAttribute("data-theme") || "light";

    const applyButtonText = () => {
        toggleButton.textContent = getTheme() === "dark" ? "Light mode" : "Dark mode";
    };

    applyButtonText();

    toggleButton.addEventListener("click", () => {
        const nextTheme = getTheme() === "dark" ? "light" : "dark";
        html.setAttribute("data-theme", nextTheme);
        localStorage.setItem("theme", nextTheme);
        applyButtonText();
    });
})();
