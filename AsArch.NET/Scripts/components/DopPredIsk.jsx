//http://allenfang.github.io/react-bootstrap-table/example.html#remote
//http://allenfang.github.io/react-bootstrap-table/example.html#celledit
var React = require('react');
import DopPredIskView from './DopPredIskView.jsx';

export default class DopPredIsk extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], options: [] };
    }

    loadFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    loadListDictJsonFromServer(id) {
        const xhr = new XMLHttpRequest();
        xhr.open('get', Router.action(`Nodes`, `GetListDictJson`, { id: id }), true);
        xhr.onload = () => {
            const options = JSON.parse(xhr.responseText);
            this.setState({ options: options });
        };
        xhr.send();
    }
    //вызывается после рендеринга компонента. Здесь можно выполнять запросы к удаленным ресурсам
    componentDidMount() {
        this.loadListDictJsonFromServer(1964);
        this.loadFromServer();
    }

    //добавить запись
    onAddRow = (row) => {
        const form = new FormData();
        form.append('Id', id_global);
        form.append('Order', row.Order);
        form.append('N', row.N);
        form.append('Name', row.Name);
        form.append('Comment', row.Comment);
        const xhr = new XMLHttpRequest();
        xhr.open('post', Router.action(`Nodes`, `PostDopPredIsk`), true);
        xhr.onload = () => this.loadFromServer();
        xhr.send(form);
    }

    //удалить записи
    onDeleteRow = (row) => {
        const form = new FormData();
        form.append('Id', id_global);
        form.append('Orders', row);
        const xhr = new XMLHttpRequest();
        xhr.open('delete', Router.action(`Nodes`, `DeleteDopPredIsk`), true);
        xhr.onload = () => this.loadFromServer();
        xhr.send(form);
    }

    //редактировать ячейку
    onCellEdit = (row, fieldName, value) => {
        if (row[fieldName] !== value) {
            row[fieldName] = value;
            this.onAddRow(row);
        } else {
            this.loadFromServer();
        }
    }

    render() {
        return (
            <DopPredIskView
                onCellEdit={this.onCellEdit}
                onAddRow={this.onAddRow}
                onDeleteRow={this.onDeleteRow}
                {...this.state}
            />
        );
    }
}
