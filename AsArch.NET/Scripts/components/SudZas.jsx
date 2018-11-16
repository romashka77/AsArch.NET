﻿var React = require('react');
import SudZasView from './SudZasView.jsx';

export default class SudZas extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], isps: [], suds: [] };
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

    loadIspsFromServer(id) {
        const xhr = new XMLHttpRequest();
        xhr.open('get', Router.action(`Nodes`, `GetListDictJson`, { id: id }), true);
        xhr.onload = () => {
            const isps = JSON.parse(xhr.responseText);
            this.setState({ isps: isps });
        };
        xhr.send();
    }
    loadSudsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', Router.action(`Nodes`, `GetListTabConfig`), true);
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
        xhr.open('post', Router.action(`Nodes`, `PostSudZas`), true);
        xhr.onload = () => this.loadFromServer();
        xhr.send(form);
    }
    //удалить записи
    onDeleteRow = (row) => {
        const form = new FormData();
        form.append('Id', id_global);
        form.append('Orders', row);
        const xhr = new XMLHttpRequest();
        xhr.open('delete', Router.action(`Nodes`, `DeleteSudZas`), true);
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
            <SudZasView
                onCellEdit={this.onCellEdit}
                onAddRow={this.onAddRow}
                onDeleteRow={this.onDeleteRow}
                {...this.state}
            />
        );
    }
}