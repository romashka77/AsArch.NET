class DopPredIsk extends React.Component {
    rawMarkup() {
        const md = new Remarkable();
        const rawMarkup = md.render(this.props.children.toString());
        return { __html: rawMarkup };
    }
    render() {
        return (
            <tr className="dopPredIsk">
                <td>
                    <div className="dopPredIskKey">{this.props.id}</div>
                </td>
                <td>
                    <div className="dopPredIskName">{this.props.name}</div>
                </td>
                <td>
                    <div>
                        <span dangerouslySetInnerHTML={this.rawMarkup()} />
                    </div>
                </td>
                <td>
                    <button className="btn btn-default" /*onClick={this.press}*/>Редактировать</button>
                
                    <button className="btn btn-default" /*onClick={this.press}*/>Удалить</button>
                </td>
            </tr>
        );
    }
}