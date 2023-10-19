export function validateSameFields(first, second) {
    if (first === second)
        return { isValid: true, error: null }

    return { isValid: false, error: "These fields must be equal" }
}

export function validateNotEmptyValue(value) {
    if (value)
        return { isValid: true, error: null }

    return { isValid: false, error: "This field is required" }
}

export function validateNotEmptyValues(values) {
    let count = values.length;
    var errors = [];
    let isValid = true;

    for (let i = 0; i < count; i++) {
        if (!values[i]) {
            errors.push(true);
            isValid = false;
        }
        else {
            errors.push(false);
        }
    }

    return { isValid: isValid, errors: errors }
}