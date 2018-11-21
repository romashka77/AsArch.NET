var React = require('react');
import { BootstrapTable, TableHeaderColumn, InsertButton, DeleteButton, InsertModalHeader, InsertModalFooter, SearchField, ClearSearchButton } from 'react-bootstrap-table';

import { FilePond, File, registerPlugin } from 'react-filepond';


export default class DocIskView extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            // Set initial files

        };
        //console.log(`files: [this.props.files]`, files);
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
    handleInit() {
        //this.pond.add(row.DocFile);
        //this.pond.files this.props.onGetFiles(row));
        //console.log('FilePond instance has initialised', this.pond);
    }

    cellUploadButton(cell, row, enumObject, rowIndex) {
        return (
            <FilePond ref={ref => this.pond = ref}
                server={
                    process = Router.action(`Nodes`, `DocIskUpload`, {
                        Id: id_global,
                        Order: row.Order
                    })
                    
                }
                
                oninit={() => this.handleInit()}
                onupdatefiles={(fileItems) => {
                    // Set current file objects to this.state
                    this.setState({
                        files: fileItems.map(fileItem => fileItem.file)
                    });
                }}
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
            >

                <File key={row.DocFile} src={row.DocFile} origin="local" />


            </FilePond >
        );
    }

    render() {
        const options = {
            searchField: this.createCustomSearchField,
            //clearSearch: true,
            clearSearchBtn: this.createCustomClearButton,
            onCellEdit: this.props.onCellEdit,
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
                    search pagination
                    containerClass='table-responsive'
                >
                    <TableHeaderColumn isKey={true} dataField='Order'>№</TableHeaderColumn>
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