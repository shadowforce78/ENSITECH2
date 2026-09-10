/*
    weak
        = < 8 chars
        = Only letters or only numbers
        = no special characters
    medium
        = >= 8 chars
        = at least two types of characters (lower or upper letters, numbers, special characters)
    strong
        = >= 12 chars
        = at least three types of characters (lower or upper letters, numbers, special characters)
    very strong
        = >= 16 chars
        = all four types of characters (lower or upper letters, numbers, special characters)
*/
const reveal = document.querySelector(".reveal");
const inputPWD = document.getElementById("passwordInput");
const strengthEl = document.getElementById("passwordStrength");

const levels = ["medium", "strong", "veryStrong"];

const defaultRules = {
    medium: { minLength: 8, minTypes: 2 },
    strong: { minLength: 12, minTypes: 3 },
    veryStrong: { minLength: 16, minTypes: 4 }
};

let rules = JSON.parse(localStorage.getItem("pwdRules")) || defaultRules;

// compte combien des 4 types de caractères sont présents (minuscule, majuscule, chiffre, spécial)
function countCharTypes(password) {
    let types = 0;
    if (/[a-z]/.test(password)) types++;
    if (/[A-Z]/.test(password)) types++;
    if (/[0-9]/.test(password)) types++;
    if (/[!@#$%^&*()_+\[\]{}|;:,.<>?]/.test(password)) types++;
    return types;
}

function checkPasswordStrength(password) {
    const types = countCharTypes(password);

    if (password.length >= rules.veryStrong.minLength && types >= rules.veryStrong.minTypes) {
        return "very strong";
    } else if (password.length >= rules.strong.minLength && types >= rules.strong.minTypes) {
        return "strong";
    } else if (password.length >= rules.medium.minLength && types >= rules.medium.minTypes) {
        return "medium";
    } else {
        return "weak";
    }
}

// remplit le formulaire de reglages avec les regles actuelles
function loadRulesIntoForm() {
    levels.forEach((level) => {
        document.getElementById(`${level}-minLength`).value = rules[level].minLength;
        document.getElementById(`${level}-minTypes`).value = rules[level].minTypes;
    });
}

loadRulesIntoForm();

document.getElementById("saveRules").addEventListener("click", () => {
    levels.forEach((level) => {
        rules[level] = {
            minLength: Number(document.getElementById(`${level}-minLength`).value),
            minTypes: Number(document.getElementById(`${level}-minTypes`).value)
        };
    });
    localStorage.setItem("pwdRules", JSON.stringify(rules));
});

inputPWD.addEventListener("input", () => {
    const password = inputPWD.value;
    const strength = checkPasswordStrength(password);
    strengthEl.textContent = `Password Strength: ${strength}`;
    strengthEl.className = strength.replace(" ", "-");
});

reveal.addEventListener("mouseover", () => {
    inputPWD.type = "text";
});

reveal.addEventListener("mouseout", () => {
    inputPWD.type = "password";
});