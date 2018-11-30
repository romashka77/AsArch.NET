var React = require('react');
import SudZasView from './SudZasView.jsx';

const time_out = 30000;
const e_error = `Ошибка`;
const e_timeout = `Извините, запрос превысил максимальное время ${time_out / 1000}с.`;
export default class SudZas extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], isps: [], suds: [] };
    }

    loadFromServer() {
        const xhr = new XMLHttpRequest();
        //xhr.open('get', this.props.url, true);
        xhr.open('get', Router.action(this.props.controller, this.props.actionGetSudZas, { id: id_global }), true);
        xhr.timeout = time_out; // 30 секунд (в миллисекундах)
        xhr.ontimeout = () => alert(e_timeout);
        xhr.onerror = (e) => alert(`${e_error} : ${e.target.status}`);
        xhr.onreadystatechange = () => {
            if (xhr.readyState !== 4) return;
            // по окончании запроса доступны:
            // status, statusText
            // responseText, responseXML (при content-type: text/xml)
            if (xhr.status !== 200) {
                // status=0 при ошибках сети, иначе status=HTTP-код ошибки
                alert(`${e_error} ${xhr.status}: ${xhr.statusText}: ${xhr.responseText}.`);
                return;
            }

        };
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
            console.log(`Загружено.`);
        };
        xhr.send();
    }

    loadIspsFromServer(id) {
        const xhr = new XMLHttpRequest();
        xhr.open('get', Router.action(this.props.controller, this.props.actionGetListDict, { id: id }), true);
        xhr.timeout = time_out;
        xhr.ontimeout = () => alert(e_timeout);
        xhr.onerror = (e) => alert(`${e_error} : ${e.target.status}`);
        xhr.onreadystatechange = () => {
            if (xhr.readyState !== 4) return;
            if (xhr.status !== 200) {
                alert(`${e_error} ${xhr.status}: ${xhr.statusText}: ${xhr.responseText}.`);
                return;
            }
        };
        xhr.onload = () => {
            const isps = JSON.parse(xhr.responseText);
            this.setState({ isps: isps });
        };
        xhr.send();
    }
    loadSudsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', Router.action(this.props.controller, this.props.actionGetListTabConfig), true);
        xhr.timeout = time_out;
        xhr.ontimeout = () => alert(e_timeout);
        xhr.onerror = (e) => alert(`${e_error} : ${e.target.status}`);
        xhr.onreadystatechange = () => {
            if (xhr.readyState !== 4) return;
            if (xhr.status !== 200) {
                alert(`${e_error} ${xhr.status}: ${xhr.statusText}: ${xhr.responseText}.`);
                return;
            }
        };
        xhr.onload = () => {
            const suds = JSON.parse(xhr.responseText);
            this.setState({ suds: suds });
        };
        xhr.send();
    }
    //вызывается после рендеринга компонента. Здесь можно выполнять запросы к удаленным ресурсам
    componentDidMount() {
        this.loadSudsFromServer(/*2091*/);
        this.loadIspsFromServer(2234);
        this.loadFromServer();
    }

    //добавить запись
    onAddRow = (row) => {
        const form = new FormData();
        form.append('Id', id_global);
        form.append('Order', row.Order);
        form.append('N', row.N);
        form.append('DateValue', row.DateValue);
        form.append('TimeValue', row.TimeValue);
        form.append('Comment', row.Comment);
        form.append('Isp', row.Isp);
        form.append('Sud', row.Sud);
        const xhr = new XMLHttpRequest();
        xhr.open('post', Router.action(this.props.controller, this.props.actionPostSudZas), true);
        xhr.timeout = time_out;
        xhr.ontimeout = () => alert(e_timeout);
        xhr.onerror = (e) => alert(`${e_error} : ${e.target.status}`);
        xhr.onreadystatechange = () => {
            if (xhr.readyState !== 4) return;
            if (xhr.status !== 200) {
                alert(`${e_error} ${xhr.status}: ${xhr.statusText}: ${xhr.responseText}.`);
                return;
            }
        };
        xhr.onload = () => {
            console.log(`${xhr.status}: ${xhr.statusText}: ${xhr.responseText}`);
            //var t = data;
            //t.push(row);
            //this.state({ data: t });
            this.loadFromServer();
        };
        xhr.send(form);
    }
    //удалить записи
    onDeleteRow = (row) => {
        const form = new FormData();
        form.append('Id', id_global);
        form.append('Orders', row);
        const xhr = new XMLHttpRequest();
        xhr.open('delete', Router.action(this.props.controller, this.props.actionDeleteSudZas), true);
        xhr.timeout = time_out;
        xhr.ontimeout = () => alert(e_timeout);
        xhr.onerror = (e) => alert(`${e_error} : ${e.target.status}`);
        xhr.onreadystatechange = () => {
            if (xhr.readyState !== 4) return;
            if (xhr.status !== 200) {
                alert(`${e_error} ${xhr.status}: ${xhr.statusText}: ${xhr.responseText}.`);
                return;
            }
        };
        xhr.onload = () => {
            console.log(`${xhr.status}: ${xhr.statusText}: ${xhr.responseText}`);
            //this.loadFromServer();
        };
        xhr.send(form);
        
    }
    //редактировать ячейку
    onCellEdit = (row, fieldName, value) => {
        if (row[fieldName] !== value) {
            row[fieldName] = value;
            this.onAddRow(row);
        }
    }

    render() {
        return (
            <SudZasView
                onCellEdit={this.onCellEdit}
                onAddRow={this.onAddRow}
                onDeleteRow={this.onDeleteRow}
                {...this.state}
            />
        );
    }
}