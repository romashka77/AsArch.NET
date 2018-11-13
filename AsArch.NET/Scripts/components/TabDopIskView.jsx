//https://itnext.io/uploading-files-with-react-and-filepond-f8a798308557
var React = require('react');
import { BootstrapTable, TableHeaderColumn, InsertButton, DeleteButton, InsertModalHeader, InsertModalFooter, SearchField, ClearSearchButton } from 'react-bootstrap-table';
export default class TabDopIskView extends React.Component {
    constructor(props) {
        super(props);
    }

    remote(remoteObj) {
        // Только редактирование ячеек, вставка и удаление строк будут обрабатываться удаленным хранилищем
        remoteObj.cellEdit = true;
        remoteObj.insertRow = true;
        remoteObj.dropRow = true;
        return remoteObj;
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

    render() {
        const options = {
            insertBtn: this.createCustomInsertButton,
            deleteBtn: this.createCustomDeleteButton,
            handleConfirmDeleteRow: this.customConfirm,
            insertModalHeader: this.createCustomModalHeader,
            insertModalFooter: this.createCustomModalFooter,
            searchField: this.createCustomSearchField,
            //clearSearch: true,
            clearSearchBtn: this.createCustomClearButton,

            onCellEdit: this.props.onCellEdit,
            onDeleteRow: this.props.onDeleteRow,
            onAddRow: this.props.onAddRow,
            noDataText: 'Таблица пуста'
        };
        return (
            <div>
                <BootstrapTable
                    data={this.props.data}
                    cellEdit={{
                        mode: 'click',
                        blurToSave: true
                        //beforeSaveCell: this.onBeforeSaveCell,
                        //afterSaveCell: this.onAfterSaveCell
                    }}
                    selectRow={{ mode: 'checkbox' }}
                    options={options}
                    insertRow deleteRow search pagination
                    containerClass='table-responsive'
                >
                    <TableHeaderColumn isKey={true} autovalue  hiddenOnInsert={true} dataField='Id'></TableHeaderColumn>
                    <TableHeaderColumn dataField='N'>№</TableHeaderColumn>
                    <TableHeaderColumn dataField='Name' editable={{ type: 'select', options: { values: this.props.options } }}>Сопутствующий предмет иска</TableHeaderColumn>
                    <TableHeaderColumn dataField='Comment'>Примечание</TableHeaderColumn>
                </BootstrapTable>

            </div>
        );
    }
}