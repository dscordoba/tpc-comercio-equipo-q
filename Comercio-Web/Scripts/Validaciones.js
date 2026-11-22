// Helpers visuales
function marcarInvalido(input) {
    input.classList.add("is-invalid");
    input.classList.remove("is-valid");
}

function marcarValido(input) {
    input.classList.remove("is-invalid");
    input.classList.add("is-valid");
}

// Validar que no esté vacío
function validarRequerido(input) {
    if (input.value.trim() === "") {
        marcarInvalido(input);
        return false;
    }
    marcarValido(input);
    return true;
}

// Validar DNI (7 u 8 dígitos)
function validarDni(input) {
    const regexDni = /^\d{7,8}$/;
    if (!regexDni.test(input.value.trim())) {
        marcarInvalido(input);
        return false;
    }
    marcarValido(input);
    return true;
}

// Validar Email
function validarEmail(input) {
    const regexEmail = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    if (!regexEmail.test(input.value.trim())) {
        marcarInvalido(input);
        return false;
    }
    marcarValido(input);
    return true;
}

// Bloquear letras mientras escribe
function soloNumeros(input) {
    input.value = input.value.replace(/[^0-9]/g, '');
}

//Validar CUIT (11 digitos)
function validarCuit(input) {
    if (!input) return false;
    const limpio = input.value.replace(/[^0-9]/g, "");
    const regexCuit = /^\d{11}$/;
    if (!regexCuit.test(limpio)) {
        marcarInvalido(input);
        return false;
    }
    marcarValido(input);
    return true;
}

//Validar Telefono (10 digitos)
function validarTelefono(input) {
    if (!input) return false;
    const limpio = input.value.replace(/[^0-9]/g, "");
    const regexTel = /^\d{10,15}$/;
    if (!regexTel.test(limpio)) {
        marcarInvalido(input);
        return false;
    }
    marcarValido(input);
    return true;
}