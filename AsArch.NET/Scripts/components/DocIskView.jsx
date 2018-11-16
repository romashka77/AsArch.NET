var React = require('react');
import { BootstrapTable, TableHeaderColumn, InsertButton, DeleteButton, InsertModalHeader, InsertModalFooter, SearchField, ClearSearchButton } from 'react-bootstrap-table';

import { FilePond } from 'react-filepond';


export default class DocIskView extends React.Component {
    constructor(props) {
        super(props);
        //console.log(`this.props.scanRequest`,this.props.scanRequest);
        //this.state = {
        //    scanRequest: this.props.scanRequest
        //};
        //console.log(`this.state.scanRequest`, this.state.scanRequest);
    }

    remote(remoteObj) {
        remoteObj.cellEdit = true;
        remoteObj.onClickDocIskScan = true;
        //remoteObj.scan = true;
        //remoteObj.insertRow = true;
        //remoteObj.dropRow = true;
        return remoteObj;
    }
    //InsertButton
    //createCustomInsertButton = () => {
    //    return (
    //        <InsertButton
    //            btnText='Добавить'
    //            btnContextual='btn btn-default'
    //        />
    //    );
    //}
    //CustomModalHeader
    //createCustomModalHeader = (closeModal, save) => {
    //    return (
    //        <InsertModalHeader
    //            title='Добавить иск'
    //        />
    //    );
    //}
    //InsertModalFooter
    //createCustomModalFooter = () => {
    //    return (
    //        <InsertModalFooter
    //            saveBtnText='Сохранить'
    //            closeBtnText='Отмена'
    //            closeBtnContextual='btn btn-default'
    //            saveBtnContextual='btn btn-default'
    //        />
    //    );
    //}
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
    //createCustomDeleteButton = () => {
    //    return (
    //        <DeleteButton
    //            btnText='Удалить'
    //            btnContextual='btn btn-default'
    //        />
    //    );
    //}
    //customConfirm(next, dropRowKeys) {
    //    const dropRowKeysStr = dropRowKeys.join(',');
    //    if (confirm(`Вы уверены, что хотите удалить ${dropRowKeysStr}?`)) {
    //        next();
    //    }
    //}

    //onClickDocIskSelected(Id) {
    //    console.log('Id #', Id);
    //}

    cellScanButton(cell, row, enumObject, rowIndex) {
        return (
            <button
                type="button"
                onClick={() =>
                    this.props.onClickDocIskScan(row.Order)}
            >Сканировать - {row.Order}</button>
        );
    }

    cellUploadButton(cell, row, enumObject, rowIndex) {
        return (
            <FilePond
                server={Router.action(`Nodes`, `DocIskUpload`, { Id: id_global, Order: row.Id })}
                labelIdle='Перенесите файлы или нажмите <span class="filepond--label-action">Обзор</span>'
                labelFileWaitingForSize='Получение размера'
                labelFileSizeNotAvailable='Размер не определен'
                labelFileLoading='Загрузка'
                labelFileLoadError='Ошибка во время загрузки'
                labelFileProcessing='Передача'
                labelFileProcessingComplete='Передача завершена'
                labelFileProcessingAborted='Передача отменена'
                labelFileProcessingError='Ошибка во время передачи'
                labelTapToCancel='Нажмите, чтобы отменить'
                labelTapToRetry='Нажмите, чтобы повторить'
                labelTapToUndo='Нажмите, чтобы отменить'
                labelButtonRemoveItem='Удалить'
                labelButtonAbortItemLoad='Прервать загрузку'
                labelButtonRetryItemLoad='Повтор'
                labelButtonAbortItemProcessing='Отмена'
                labelButtonUndoItemProcessing='Отменить'
                labelButtonRetryItemProcessing='Повтор'
                labelButtonProcessItem='Передать'
                //allowMultiple={true}
            />
            //<button
            //    type="button"
            //    onClick={() =>
            //        this.props.onClickDocIskUpload(row.Id)}
            //>Загрузить - {row.Id}</button>
        );
    }

    render() {
        const options = {
            //insertBtn: this.createCustomInsertButton,
            //deleteBtn: this.createCustomDeleteButton,
            //handleConfirmDeleteRow: this.customConfirm,
            //insertModalHeader: this.createCustomModalHeader,
            //insertModalFooter: this.createCustomModalFooter,
            searchField: this.createCustomSearchField,
            //clearSearch: true,
            clearSearchBtn: this.createCustomClearButton,
            onCellEdit: this.props.onCellEdit,
            //onDeleteRow: this.props.onDeleteRow,
            //onAddRow: this.props.onAddRow,
            noDataText: 'Таблица пуста'
        };
        return (
            <div>
                <BootstrapTable
                    data={this.props.data}
                    //cellEdit={{
                    //    mode: 'click',
                    //    blurToSave: true
                    //    //beforeSaveCell: this.onBeforeSaveCell,
                    //    //afterSaveCell: this.onAfterSaveCell
                    //}}
                    //selectRow={{ mode: 'checkbox' }}
                    options={options}
                    //insertRow deleteRow
                    search pagination
                    containerClass='table-responsive'
                >
                    <TableHeaderColumn isKey={true} /*hidden*/ dataField='Order'>№</TableHeaderColumn>
                    <TableHeaderColumn dataField='Name'>Тип</TableHeaderColumn>
                    <TableHeaderColumn dataField='Filter'>Фильтр</TableHeaderColumn>
                    <TableHeaderColumn dataField='DocFile'>Файл</TableHeaderColumn>
                    <TableHeaderColumn dataField='button' dataFormat={this.cellUploadButton.bind(this)}></TableHeaderColumn>
                </BootstrapTable>
            </div>
        );
    }
}
//<TableHeaderColumn dataField='button' dataFormat={this.cellScanButton.bind(this)}></TableHeaderColumn>