export function getLoginFormData() {
    const formData = {
        email: "",
        password: ""
    }

    return formData;
}

export function getRegisterFormData() {
    const formData = {
        firstName: "",
        lastName: "",
        email: "",
        selectedCity: {},
        password: "",
        samePassword: ""
    }

    return formData;
}

export function getAuthCredentialsFormData() {
    const formData = {
        selectedLanguage: {},
        selectedCountry: {}
    }

    return formData;
}