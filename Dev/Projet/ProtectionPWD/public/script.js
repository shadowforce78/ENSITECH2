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

function checkPasswordStrength(password) {
    let strength = 0;
    if (password.length >= 8) strength++;
    if (/[a-z]/.test(password) && /[A-Z]/.test(password)) strength++;
    if (/[0-9]/.test(password)) strength++;
    if (/[!@#$%^&*()_+\[\]{}|;:,.<>?]/.test(password)) strength++;

    if (password.length >= 16 && strength === 4) {
        return "very strong";
    } else if (password.length >= 12 && strength >= 3) {
        return "strong";
    } else if (password.length >= 8 && strength >= 2) {
        return "medium";
    } else {
        return "weak";
    }
}

// console.log every time the user types in the input field
inputPWD.addEventListener("input", () => {
    const password = inputPWD.value;
    const strength = checkPasswordStrength(password);
    const strengthEl = document.getElementById("passwordStrength");
    strengthEl.textContent = `Password Strength: ${strength}`;
    strengthEl.className = strength.replace(" ", "-");
    console.log(password)
});

reveal.addEventListener("mouseover", () => {
    inputPWD.type = "text";
});

reveal.addEventListener("mouseout", () => {
    inputPWD.type = "password";
});