class GridRow extends React.Component {
    constructor(props) {
        super(props);
        this.clickEvent = this.clickEvent.bind(this);
    }

    clickEvent(e) {
        this.props.onSelectRow(e, this.props.record);
    }

    render() {
        let record = this.props.record;
        return (
            <tr onClick={this.clickEvent} className={record.Edit ? "selectRow" : ""}>
                <td>{record.Id}</td>
                <td>{record.Name}</td>
                <td>{record.Comment}</td>
                <td>{record.Edit}</td>
            </tr>
        )
    }
}
//class GridRow extends React.Component {
//    rawMarkup() {
//        const md = new Remarkable();
//        const rawMarkup = md.render(this.props.children.toString());
//        return { __html: rawMarkup };
//    }
//    render() {
//        return (
//            <tr className="dopPredIsk">
//                <td>
//                    <div className="dopPredIskKey">{this.props.id}</div>
//                </td>
//                <td>
//                    <div className="dopPredIskName">{this.props.name}</div>
//                </td>
//                <td>
//                    <div>
//                        <span dangerouslySetInnerHTML={this.rawMarkup()} />
//                    </div>
//                </td>
//                <td>
//                    <button className="btn btn-default" /*onClick={this.press}*/>Редактировать</button>
                
//                    <button className="btn btn-default" /*onClick={this.press}*/>Удалить</button>
//                </td>
//            </tr>
//        );
//    }
//}