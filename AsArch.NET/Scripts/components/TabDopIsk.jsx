//http://allenfang.github.io/react-bootstrap-table/example.html#remote
//http://allenfang.github.io/react-bootstrap-table/example.html#celledit
var React = require('react');
import TabDopIskView from './TabDopIskView.jsx';

export default class TabDopIsk extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], options: [] };
    }

    loadDopPredIskFromServer() {
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
        this.loadDopPredIskFromServer();
    }
    //добавить запись
    onAddRow = (row) => {
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Id', row.Id);
        form.append('Name', row.Name);
        form.append('Comment', row.Comment);
        const xhr = new XMLHttpRequest();
        xhr.open('post', Router.action(`Nodes`, `InsertDopPredIsk`), true);
        xhr.onload = () => this.loadDopPredIskFromServer();
        xhr.send(form);
    }
    //удалить записи
    onDeleteRow = (row) => {
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Ids', row);
        const xhr = new XMLHttpRequest();
        xhr.open('delete', Router.action(`Nodes`, `DeleteDopPredIsk`), true);
        xhr.onload = () => this.loadDopPredIskFromServer();
        xhr.send(form);
    }
    //редактировать ячейку
    onCellEdit = (row, fieldName, value) => {
        row[fieldName] = value;
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Id', row.Id);
        form.append('Name', row.Name);
        form.append('Comment', row.Comment);
        const xhr = new XMLHttpRequest();
        xhr.open('post', Router.action(`Nodes`, `InsertDopPredIsk`), true);
        xhr.onload = () => this.loadDopPredIskFromServer();
        xhr.send(form);
    }

    

    render() {
        return (
            <TabDopIskView
                onCellEdit={this.onCellEdit}
                onAddRow={this.onAddRow}
                onDeleteRow={this.onDeleteRow}
                {...this.state}
            />
        );
    }
}
