//https://itnext.io/uploading-files-with-react-and-filepond-f8a798308557
var React = require('react');
import DocIskView from './DocIskView.jsx';

const scanRequest = {
    "use_asprise_dialog": true, // Использовать ли диалог сканирования Asprise
    "show_scanner_ui": false, // Должен ли отображаться пользовательский интерфейс сканера
    "twain_cap_setting": { // Дополнительные параметры сканирования
        "ICAP_PIXELTYPE": "TWPT_RGB" // Цвет
    },
    "output_settings": [{
        "type": "return-base64",
        "format": "jpg"
    }]
};

//const scanRequest2 = {
//    "output_settings": [
//        {
//            "type": "upload",
//            "format": "pdf",
//            "upload_target": {
//                "url": "https://asprise.com/scan/applet/upload.php?action=dump",
//                "post_fields": {
//                    "sample-field": "Test scan"
//                },
//                "cookies": document.cookie,
//                "headers": [
//                    "Referer: " + window.location.href,
//                    "User-Agent: " + navigator.userAgent
//                ]
//            }
//        }
//    ]
//};



//Запускает сканирование
function scan(Id) {
    scanner.scan(displayImagesOnPage, scanRequest);
}

//Обрабатывает результат сканирования
function displayImagesOnPage(successful, mesg, response, Id) {
    if (!successful) { // При ошибке
        console.error('Ошибка: ' + mesg);
        return;
    }
    if (successful && mesg !== null && mesg.toLowerCase().indexOf('user cancel') >= 0) { // User cancelled.
        console.info('Отмена');
        return;
    }
    var scannedImages = scanner.getScannedImages(response, true, false); // возвращает массив отсканированного изображения
    for (var i = 0;
        (scannedImages instanceof Array) && i < scannedImages.length; i++) {
        var scannedImage = scannedImages[i];
        var elementImg = scanner.createDomElementFromModel({
            'name': 'img',
            'attributes': {
                'class': 'scanned',
                'src': scannedImage.src
            }
        });
        (document.getElementById('images') ? document.getElementById('images') : document.body).appendChild(elementImg);



    }
}

export default class DocIsk extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [] };
    }

    loadDocIskFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    //вызывается после рендеринга компонента. Здесь можно выполнять запросы к удаленным ресурсам
    componentDidMount() {
        this.loadDocIskFromServer();
    }
    //добавить запись
    //onAddRow = (row) => {
    //    const form = new FormData();
    //    form.append('IdNode', id_global);
    //    form.append('Id', row.Id);
    //    form.append('DateValue', row.DateValue);
    //    form.append('TimeValue', row.TimeValue);
    //    form.append('Comment', row.Comment);
    //    form.append('Isp', row.Isp);
    //    form.append('Sud', row.Sud);
    //    const xhr = new XMLHttpRequest();
    //    xhr.open('post', Router.action(`Nodes`, `InsertSudZas`), true);
    //    xhr.onload = () => this.loadSudZasFromServer();
    //    xhr.send(form);
    //}
    //удалить записи
    //onDeleteRow = (row) => {
    //    const form = new FormData();
    //    form.append('IdNode', id_global);
    //    form.append('Ids', row);
    //    const xhr = new XMLHttpRequest();
    //    xhr.open('delete', Router.action(`Nodes`, `DeleteSudZas`), true);
    //    xhr.onload = () => this.loadSudZasFromServer();
    //    xhr.send(form);
    //}
    //редактировать ячейку
    onCellEdit = (row, fieldName, value) => {
        row[fieldName] = value;
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Id', row.Id);
        form.append('Name', row.Name);
        form.append('Filter', row.Filter);
        form.append('DocFile', row.DocFile);
        const xhr = new XMLHttpRequest();
        xhr.open('post', Router.action(`Nodes`, `EditDocIsk`), true);
        xhr.onload = () => this.loadDocIskFromServer();
        xhr.send(form);
    }
    onClickDocIskScan(Id) {
        console.log('Scan Id #', Id);
        scan();
    }

    render() {
        return (
            <DocIskView
                onCellEdit={this.onCellEdit}
                onClickDocIskScan={this.onClickDocIskScan}
                //onAddRow={this.onAddRow}
                //onDeleteRow={this.onDeleteRow}
                {...this.state}
            />
        );
    }
}