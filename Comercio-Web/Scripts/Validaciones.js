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

// Bloquear letras, limitar enteros y permitir hasta 2 decimales
function soloDecimales(input, maxEnteros = 10) {
    let valor = input.value.replace(/,/g, '.').replace(/[^0-9.]/g, '');

    const partes = valor.split('.');
    if (partes.length > 2) {
        valor = partes[0] + '.' + partes.slice(1).join('');
    }

    if (partes[0].length > maxEnteros) {
        partes[0] = partes[0].substring(0, maxEnteros);
    }

    if (partes.length === 2) {
        if (partes[1].length > 2) {
            partes[1] = partes[1].substring(0, 2);
        }
        valor = partes[0] + '.' + partes[1];
    } else {
        valor = partes[0];
    }

    input.value = valor;
}


// Limitar cantidad máxima de caracteres
function limitarCaracteres(input, max) {
    if (input.value.length > max) {
        input.value = input.value.substring(0, max);
    }
}

// Valida que un ListBox de selección múltiple tenga al menos un ítem marcado
function validarListaMultiple(elemento) {
    if (!elemento) return false;
    const tieneSeleccion = elemento.selectedOptions.length > 0;

    if (tieneSeleccion) {
        elemento.classList.remove("is-invalid");
        elemento.classList.add("is-valid");
        return true;
    } else {
        elemento.classList.remove("is-valid");
        elemento.classList.add("is-invalid");
        return false;
    }
}

// Valida números >= 0 (acepta decimales con punto o coma - con un tope máximo opcional)
function validarNumeroPositivo(elemento, permiteDecimales, maximo = Infinity) {
    if (!elemento) return false;
    const valor = elemento.value.trim().replace(",", ".");
    const numero = parseFloat(valor);

    let esValido = valor !== "" && !isNaN(numero) && numero >= 0 && numero <= maximo;

    if (esValido && !permiteDecimales && !Number.isInteger(numero)) {
        esValido = false;
    }

    if (esValido) {
        elemento.classList.remove("is-invalid");
        elemento.classList.add("is-valid");
        return true;
    } else {
        elemento.classList.remove("is-valid");
        elemento.classList.add("is-invalid");
        return false;
    }
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