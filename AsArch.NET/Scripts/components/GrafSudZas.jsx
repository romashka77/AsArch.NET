var React = require('react');
import { BootstrapTable, TableHeaderColumn, InsertButton, DeleteButton, InsertModalHeader, InsertModalFooter, SearchField, ClearSearchButton } from 'react-bootstrap-table';

export default class GrafSudZas extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], options: [] };
    }

    //InsertButton
    createCustomInsertButton = () => {
        return (
            <InsertButton
                btnText='Добавить'
                btnContextual='btn btn-default'
            />
        );
    }
    //добавить запись
    onAfterInsertRow(row) {
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Id', row.Id);
        form.append('DateValue', row.DateValue);
        form.append('TimeValue', row.TimeValue);
        form.append('Comment', row.Comment);
        form.append('Isp', row.Isp);
        form.append('Sud', row.Sud);
        const xhr = new XMLHttpRequest();
        xhr.open('post', Router.action(`Nodes`, `InsertSudZas`), true);
        xhr.onload = () => this.loadDopPredIskFromServer();
        xhr.send(form);
    }
    //CustomModalHeader
    createCustomModalHeader = (closeModal, save) => {
        return (
            <InsertModalHeader
                title='Добавить иск'
            />
        );
    }
    //InsertModalFooter
    createCustomModalFooter = () => {
        return (
            <InsertModalFooter
                saveBtnText='Сохранить'
                closeBtnText='Отмена'
                closeBtnContextual='btn btn-default'
                saveBtnContextual='btn btn-default'
            />
        );
    }
    //SearchField
    createCustomSearchField = () => {
        return (
            <SearchField
                //defaultValue='2000'
                placeholder='Поиск...'
            />
        );
    }
    //ClearSearchButton 
    createCustomClearButton = () => {
        return (
            <ClearSearchButton
                btnText='Очистить'
                btnContextual='btn btn-default'
            />
        );
    }

    //DeleteButton
    createCustomDeleteButton = () => {
        return (
            <DeleteButton
                btnText='Удалить'
                btnContextual='btn btn-default'
            />
        );
    }
    customConfirm(next, dropRowKeys) {
        const dropRowKeysStr = dropRowKeys.join(',');
        if (confirm(`Вы уверены, что хотите удалить ${dropRowKeysStr}?`)) {
            next();
        }
    }
    //удалить записи
    onAfterDeleteRow(rowKeys) {
        //console.log(rowKeys);
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Ids', rowKeys);
        const xhr = new XMLHttpRequest();
        xhr.open('delete', Router.action(`Nodes`, `DeleteSudZas`), true);
        xhr.onload = () => this.loadSudZasFromServer();
        xhr.send(form);
        //alert('The rowkey you drop: ' + rowKeys);
    }
    //редактировать ячейку
    onBeforeSaveCell(row, cellName, cellValue) {
        // Вы можете сделать любую проверку здесь для редактирования значения, вернуть false для отклонения редактирования
        //post ajax
        return true;
    }
    onAfterSaveCell(row, cellName, cellValue) {
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Id', row.Id);
        form.append('DateValue', row.DateValue);
        form.append('TimeValue', row.TimeValue);
        form.append('Comment', row.Comment);
        form.append('Isp', row.Isp);
        form.append('Sud', row.Sud);
        const xhr = new XMLHttpRequest();
        xhr.open('post', Router.action(`Nodes`, `InsertSudZas`), true);
        xhr.onload = () => this.loadSudZasFromServer();
        xhr.send(form);
    }


    //вызывается после рендеринга компонента. Здесь можно выполнять запросы к удаленным ресурсам
    componentDidMount() {
        //this.loadDopPredIskOptionsFromServer();
        this.loadSudZasFromServer();
    }

    //loadDopPredIskOptionsFromServer() {
    //    const xhr = new XMLHttpRequest();
    //    xhr.open('get', this.props.url_options, true);
    //    xhr.onload = () => {
    //        //console.log(`xhr.responseText`, xhr.responseText);
    //        const options = JSON.parse(xhr.responseText);
    //        this.setState({ options: options });
    //    };
    //    xhr.send();
    //}

    loadSudZasFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    dateFormatter(cell) {
        if (!cell) {
            return "";
        }
        return `${moment(cell).format("DD-MM-YYYY") ? moment(cell).format("DD-MM-YYYY") : moment(cell).format("DD-MM-YYYY")}`;
    }
    render() {
        const options = {
            insertBtn: this.createCustomInsertButton,
            deleteBtn: this.createCustomDeleteButton,
            handleConfirmDeleteRow: this.customConfirm,
            insertModalHeader: this.createCustomModalHeader,
            insertModalFooter: this.createCustomModalFooter,
            searchField: this.createCustomSearchField,
            clearSearch: true,
            clearSearchBtn: this.createCustomClearButton,

            afterInsertRow: this.onAfterInsertRow,
            afterDeleteRow: this.onAfterDeleteRow
        };
        return (
            <div>
                <BootstrapTable
                    data={this.state.data}
                    cellEdit={{
                        mode: 'click',
                        blurToSave: true,
                        beforeSaveCell: this.onBeforeSaveCell,
                        afterSaveCell: this.onAfterSaveCell
                    }}
                    selectRow={{ mode: 'checkbox' }}
                    options={options}
                    insertRow
                    deleteRow
                    search
                >
                    <TableHeaderColumn isKey={true} dataField='Id'>№</TableHeaderColumn>
                    <TableHeaderColumn dataField='DateValue' editable={{ type: 'date' }} /*dataFormat={this.dateFormatter}*/>Дата</TableHeaderColumn>
                    <TableHeaderColumn dataField='TimeValue'>Время</TableHeaderColumn>
                    <TableHeaderColumn dataField='Comment'>Примечание</TableHeaderColumn>
                    <TableHeaderColumn dataField='Isp'>Исполнитель</TableHeaderColumn>
                    <TableHeaderColumn dataField='Sud'>Судья</TableHeaderColumn>
                </BootstrapTable>
            </div>
        );
    }
}
//<TableHeaderColumn dataField='Name' editable={{ type: 'select', options: { values: this.state.options } }}>Сопутствующий предмет иска</TableHeaderColumn>