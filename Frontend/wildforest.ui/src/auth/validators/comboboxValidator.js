export function validate(count, values) {
    var errors = [];
    let isValid = true;

    for (let i = 0; i < count; i++) {
        if (!values[i]) {
            errors.push(false);
            isValid = false;
        }
        else {
            errors.push(true);
        }
    }

    return {isValid: isValid, errors: errors}
}