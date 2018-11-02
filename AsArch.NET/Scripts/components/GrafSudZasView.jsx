var React = require('react');
import { BootstrapTable, TableHeaderColumn, InsertButton, DeleteButton, InsertModalHeader, InsertModalFooter, SearchField, ClearSearchButton } from 'react-bootstrap-table';

export default class GrafSudZasView extends React.Component {
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
                title='Добавить'
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

    //dateFormatter(cell) {
    //    if (!cell) {
    //        return "";
    //    }
    //    return `${moment(cell).format("DD-MM-YYYY") ? moment(cell).format("DD-MM-YYYY") : moment(cell).format("DD-MM-YYYY")}`;
    //}

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
            onAddRow: this.props.onAddRow
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
                    insertRow deleteRow search //pagination
                    containerClass='table-responsive'
                >
                    <TableHeaderColumn isKey={true} dataField='Id'>№</TableHeaderColumn>
                    <TableHeaderColumn dataField='DateValue' editable={{ type: 'date' }}>Дата</TableHeaderColumn>
                    <TableHeaderColumn dataField='TimeValue'>Время</TableHeaderColumn>
                    <TableHeaderColumn dataField='Comment'>Примечание</TableHeaderColumn>
                    <TableHeaderColumn dataField='Isp' editable={{ type: 'select', options: { values: this.props.isps } }}>Исполнитель</TableHeaderColumn>
                    <TableHeaderColumn dataField='Sud' editable={{ type: 'select', options: { values: this.props.suds } }}>Судья</TableHeaderColumn>
                </BootstrapTable>
            </div>
        );
    }
}