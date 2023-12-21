export function getItemFromLocalStorage(name) {
    const jsonItem = localStorage.getItem(name);

    if (jsonItem === null)
        throw new Error("No object with the same name was found in localStorage");

    const item = JSON.parse(jsonItem)._value;
    return item;
}