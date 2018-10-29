var React = require('react');
import { BootstrapTable, TableHeaderColumn, InsertButton, DeleteButton, InsertModalHeader, InsertModalFooter, SearchField, ClearSearchButton} from 'react-bootstrap-table';
import { runInThisContext, runInContext } from 'vm';


export default class TabDopIsk extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], options: [] };
        //this.handleDopPredIskSubmit = this.handleDopPredIskSubmit.bind(this);
        //this.press = this.press.bind(this);
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
        //post ajax
        //let newRowStr = '';
        //for (const prop in row) {
        //    newRowStr += prop + ': ' + row[prop] + ' \n';
        //}
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
        const form = new FormData();
        form.append('IdNode', id_global);
        form.append('Id', rowKeys);
        const xhr = new XMLHttpRequest();
        xhr.open('delete', Router.action(`Nodes`, `DeleteDopPredIsk`), true);
        xhr.onload = () => this.loadDopPredIskFromServer();
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
        alert(`Save cell ${cellName} with value ${cellValue}`);

        let rowStr = '';
        for (const prop in row) {
            rowStr += prop + ': ' + row[prop] + '\n';
        }
        alert('Thw whole row :\n' + rowStr);
    }


    //вызывается после рендеринга компонента. Здесь можно выполнять запросы к удаленным ресурсам
    componentDidMount() {
        this.loadDopPredIskOptionsFromServer();
        this.loadDopPredIskFromServer();
    }

    loadDopPredIskOptionsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url_options, true);
        xhr.onload = () => {
            //console.log(`xhr.responseText`, xhr.responseText);
            const options = JSON.parse(xhr.responseText);
            this.setState({ options: options });
        };
        xhr.send();
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

    //handleDopPredIskSubmit(dopPredIsk) {
    //    const data = new FormData();
    //    data.append('IdNode', id_global);
    //    data.append('Name', dopPredIsk.Name);
    //    data.append('Comment', dopPredIsk.Comment);

    //    const xhr = new XMLHttpRequest();
    //    xhr.open('post', this.props.submitUrl, true);
    //    xhr.onload = () => this.loadDopPredIskFromServer();
    //    xhr.send(data);
    //}
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
                    <TableHeaderColumn dataField='Name' editable={{ type: 'select', options: { values: this.state.options } }}>Сопутствующий предмет иска</TableHeaderColumn>
                    <TableHeaderColumn dataField='Comment' editable={{ type: 'textarea' }}>Примечание</TableHeaderColumn>
                </BootstrapTable>

            </div>
        );
    }
}
//<button onClick={this.press}>Добавить</button>;
//, validator: jobStatusValidator editColumnClassName={this.editingJobStatus} invalidEditColumnClassName={this.invalidJobStatus}
//editColumnClassName='editing-jobsname-class' invalidEditColumnClassName='invalid-jobsname-class'