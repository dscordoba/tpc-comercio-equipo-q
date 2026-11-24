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


// Validar coincidencia de contraseñas
function validarPasswordsIguales(inputPass, inputConfirm) {
    if (!inputPass || !inputConfirm) return false;

    if (inputPass.value.trim() === "" || inputPass.value !== inputConfirm.value) {
        inputConfirm.classList.add("is-invalid");
        return false;
    }

    inputConfirm.classList.remove("is-invalid");
    return true;
}

// Manejo visual de contraseñas desde icono
function togglePassword(inputId, btn) {
    const input = document.getElementById(inputId);
    const eye = btn.querySelector(".icon-eye");
    const eyeSlash = btn.querySelector(".icon-eye-slash");

    if (!input || !eye || !eyeSlash) return;

    if (input.type === "password") {
        input.type = "text";
        eye.classList.add("d-none");
        eyeSlash.classList.remove("d-none");
    } else {
        input.type = "password";
        eye.classList.remove("d-none");
        eyeSlash.classList.add("d-none");
    }

    //validar Fecha
    function validarFecha(input) {
        if (!input || input.value.trim() === "") {
            if (input) marcarInvalido(input);
            return false;
        }

        //Para inputs tipo date("yyyy-mm-dd"), parseamos año, mes y dia de manera segura
        const partes = input.value.trim().split("-");
        if (partes.lentgh !== 3) {
            marcarInvalido(input);
            return false;
        }

        const anio = parseInt(partes[0], 10);
        const mes = parseInt(partes[1], 10);
        const dia = parseInt(partes[2], 10);

        const fecha = new Date(anio, mes - 1, dia);
        const esFechaReal =
            fecha.getFullYear() === anio &&
            fecha.getMonth() === mes - 1 &&
            fecha.getDate() === dia;

        if (!esFechaReal) {
            marcarInvalido(input);
            return false;
        }

        //Para evitar fechas futuras
        const hoy = new Date();
        hoy.setHours(0, 0, 0, 0);
        if (fecha > hoy) {
            marcarInvalido(input);
            return false;
        }
        marcarValido(input);
        return true;
    }

//validar Fecha
    function validarFecha(input) {
        if (!input || input.value.trim() === "") {
            if (input) marcarInvalido(input);
            return false;
        }

        //Para inputs tipo date("yyyy-mm-dd"), parseamos año, mes y dia de manera segura
        const partes = input.value.trim().split("-");
        if (partes.lentgh !== 3) {
            marcarInvalido(input);
            return false;
        }

        const anio = parseInt(partes[0], 10);
        const mes = parseInt(partes[1], 10);
        const dia = parseInt(partes[2], 10);

        const fecha = new Date(anio, mes - 1, dia);
        const esFechaReal =
            fecha.getFullYear() === anio &&
            fecha.getMonth() === mes - 1 &&
            fecha.getDate() === dia;

        if (!esFechaReal) {
            marcarInvalido(input);
            return false;
        }

        //Para evitar fechas futuras
        const hoy = new Date();
        hoy.setHours(0, 0, 0, 0);
        if (fecha > hoy) {
            marcarInvalido(input);
            return false;
        }
        marcarValido(input);
        return true;
    }
}