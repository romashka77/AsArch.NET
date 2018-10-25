//https://allenfang.github.io/react-bootstrap-table/custom.html

var React = require('react');
import { InsertModalFooter } from 'react-bootstrap-table';

export default class TabDopIsk extends React.Component {
    constructor(props) {
        super(props);
    }

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
    };
}