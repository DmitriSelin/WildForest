export function validate(count, values) {
    var errors = [];

    for (let i = 0; i < count; i++) {
        if (!values[i])
            errors.push(false);
        else
            errors.push(true);
    }
}