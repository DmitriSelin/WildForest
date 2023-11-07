export const isFirstLoaded = (name) => {
    let page = sessionStorage.getItem(name);

    if (page === null) {
        sessionStorage.setItem(name, name);
        return true;
    }

    return false;
}