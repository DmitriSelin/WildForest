export function getItemFromLocalStorage(key) {
    const jsonItem = localStorage.getItem(key);

    if (jsonItem === null)
        throw new Error("No object with the same name was found in localStorage");

    const item = JSON.parse(jsonItem);
    return item;
}

export function setItemInLocalStorage(key, item) {
    if (item === null)
        throw new Error("Can not set nullable object in localStorage");

    localStorage.setItem(key, JSON.stringify(item));
}

export function getItemFromSessionStorage(key) {
    const jsonItem = sessionStorage.getItem(key);

    if (jsonItem === null)
        throw new Error("No object with the same name was found in sessionStorage");

    const item = JSON.parse(jsonItem);
    return item;
}

export function setItemInSessionStorage(key, item) {
    if (item === null)
        throw new Error("Can not set nullable object in sessionStorage");

    sessionStorage.setItem(key, JSON.stringify(item));
}