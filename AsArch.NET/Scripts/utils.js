//переделать Graf Tab
function loadListDictJsonFromServer(id, callback) {
    const xhr = new XMLHttpRequest();
    xhr.open('get', Router.action(`Nodes`, `GetListDictJson`, { id: id }), true);
    xhr.onload = () => {
        const options = JSON.parse(xhr.responseText);
        callback.apply({ options: options });
    };
    xhr.send();
}