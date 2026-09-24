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
    veryStrong: { minLength: 16, minTypes: 4 },
    blacklistedChars: "O0l1I"
};

let rules = JSON.parse(localStorage.getItem("pwdRules")) || defaultRules;
if (!rules.blacklistedChars || typeof rules.blacklistedChars !== "string") {
    rules.blacklistedChars = defaultRules.blacklistedChars;
}   

// compte combien des 4 types de caractères sont présents (minuscule, majuscule, chiffre, spécial)
function countCharTypes(password) {
    let types = 0;
    const blacklistRegex = new RegExp(`[${rules.blacklistedChars}]`);
    if (blacklistRegex.test(password)) {
        return -1; // retourne -1 si le mot de passe contient des caractères interdits
    }
    if (/[a-z]/.test(password)) types++;
    if (/[A-Z]/.test(password)) types++;
    if (/[0-9]/.test(password)) types++;
    if (/[!@#$%^&*()_+\[\]{}|;:,.<>?]/.test(password)) types++;
    return types;
}

function checkPasswordStrength(password) {
    const types = countCharTypes(password);
    if (types === -1) {
        return "invalid";
    }
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
    document.getElementById("blacklistedChars").value = rules.blacklistedChars;
}

loadRulesIntoForm();

document.getElementById("saveRules").addEventListener("click", () => {
    levels.forEach((level) => {
        rules[level] = {
            minLength: Number(document.getElementById(`${level}-minLength`).value),
            minTypes: Number(document.getElementById(`${level}-minTypes`).value)
        };
    });
    rules.blacklistedChars = document.getElementById("blacklistedChars").value;
    localStorage.setItem("pwdRules", JSON.stringify(rules));
});

inputPWD.addEventListener("input", () => {
    const password = inputPWD.value;
    const strength = checkPasswordStrength(password);
    if (strength === "invalid") {
        strengthEl.textContent = "Password contains invalid characters";
        strengthEl.className = "invalid";
        return;
    }
    strengthEl.textContent = `Password Strength: ${strength}`;
    strengthEl.className = strength.replace(" ", "-");
});

reveal.addEventListener("mouseover", () => {
    inputPWD.type = "text";
});

reveal.addEventListener("mouseout", () => {
    inputPWD.type = "password";
});