﻿var React = require('react');
import { BootstrapTable, TableHeaderColumn, InsertButton, InsertModalFooter} from 'react-bootstrap-table';

const cellEditProp = {
    mode: 'click',
    blurToSave: true
};
// validator function pass the user input value and should return true|false.
function jobNameValidator(value) {
    const response = { isValid: true, notification: { type: 'success', msg: '', title: '' } };
    if (!value) {
        response.isValid = false;
        response.notification.type = 'error';
        response.notification.msg = 'Value must be inserted';
        response.notification.title = 'Requested Value';
    } else if (value.length < 10) {
        response.isValid = false;
        response.notification.type = 'error';
        response.notification.msg = 'Value must have 10+ characters';
        response.notification.title = 'Invalid Value';
    }
    return response;
}

function jobStatusValidator(value) {
    const nan = isNaN(parseInt(value, 10));
    if (nan) {
        return 'Job Status must be a integer!';
    }
    return true;
}

export default class TabDopIsk extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], options: [] };
        this.handleDopPredIskSubmit = this.handleDopPredIskSubmit.bind(this);
    }

    //invalidJobStatus = (cell, row) => {
    //    //console.log(`${cell} at row id: ${row.id} fails on editing`);
    //    return 'invalid-jobstatus-class';
    //}
    //editingJobStatus = (cell, row) => {
    //    //console.log(`${cell} at row id: ${row.id} in current editing`);
    //    return 'editing-jobstatus-class';
    //}
    //кнопка Добавить
    handleInsertButtonClick = (onClick) => {
        // Пользовательское событие onClick здесь, нет необходимости реализовывать эту функцию, если у вас нет никакого процесса перед onClick
        //console.log('This is my custom function for InserButton click event');
        onClick();
    }
    createCustomInsertButton = (onClick) => {
        return (
            <InsertButton
                btnText='Добавить'
                btnContextual='btn btn-default'
                className='my-custom-class'
                btnGlyphicon='glyphicon-edit'
                onClick={() => this.handleInsertButtonClick(onClick)}
            />
        );
    }
    //InsertModalFooter
    beforeClose(e) {
        alert(`[Custom Event]: Modal close event triggered!`);
    }

    beforeSave(e) {
        alert(`[Custom Event]: Modal save event triggered!`);
    }

    handleModalClose(closeModal) {
        // Custom your onCloseModal event here,
        // it's not necessary to implement this function if you have no any process before modal close
        console.log('This is my custom function for modal close event');
        closeModal();
    }

    handleSave(save) {
        // Custom your onSave event here,
        // it's not necessary to implement this function if you have no any process before save
        console.log('This is my custom function for save event');
        save();
    }

    createCustomModalFooter = (closeModal, save) => {
        return (
            <InsertModalFooter
                className='my-custom-class'
                saveBtnText='Сохранить'
                closeBtnText='Отмена'
                closeBtnContextual='btn btn-default'
                saveBtnContextual='btn btn-default'
                closeBtnClass='my-close-btn-class'
                saveBtnClass='my-save-btn-class'
                beforeClose={this.beforeClose}
                beforeSave={this.beforeSave}
                onModalClose={() => this.handleModalClose(closeModal)}
                onSave={() => this.handleSave(save)}
            />
        );
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
            console.log(`xhr.responseText`, xhr.responseText);
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

    handleDopPredIskSubmit(dopPredIsk) {
        const data = new FormData();
        data.append('IdNode', id_global);
        data.append('Name', dopPredIsk.Name);
        data.append('Comment', dopPredIsk.Comment);

        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = () => this.loadDopPredIskFromServer();
        xhr.send(data);
    }
    render() {
        const options = {
            insertBtn: this.createCustomInsertButton,
            insertModalFooter: this.createCustomModalFooter
        };
        return (
            <BootstrapTable data={this.state.data} cellEdit={cellEditProp} options={options} insertRow>
                <TableHeaderColumn isKey={true} dataField='Id'>№</TableHeaderColumn>
                <TableHeaderColumn dataField='Name' editable={{ type: 'select', options: { values: this.state.options } }}>Сопутствующий предмет иска</TableHeaderColumn>
                <TableHeaderColumn dataField='Comment' editable={{ type: 'textarea' }}>Примечание</TableHeaderColumn>
            </BootstrapTable>
        );
    }
}
//, validator: jobStatusValidator editColumnClassName={this.editingJobStatus} invalidEditColumnClassName={this.invalidJobStatus}
//editColumnClassName='editing-jobsname-class' invalidEditColumnClassName='invalid-jobsname-class'