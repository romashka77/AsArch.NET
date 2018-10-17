class DopPredIskForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { name: '', comment: '' };
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleCommentChange = this.handleCommentChange.bind(this);
        this.press = this.press.bind(this);
        //this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleNameChange(e) {
        this.setState({ name: e.target.value });
    }
    handleCommentChange(e) {
        this.setState({ comment: e.target.value });
    }
    //handleSubmit(e) {
    press(e) {
        e.preventDefault();
        const name = this.state.name.trim();
        const comment = this.state.comment.trim();
        if (!comment || !name) {
            return;
        }

        this.props.onDopPredIskSubmit({ Name: name, Comment: comment });
        this.setState({ name: '', comment: '' });
    }
    componentWillMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }

    render() {
        return (
            <tr className="dopPredIskForm" /*onSubmit={this.handleSubmit}*/>
                <td>
                    <button onClick={this.press}>Добавить</button>;
                </td>
                <td>
                    <input
                        type="text"
                        placeholder="Сопутствующий предмет иска"
                        value={this.state.name}
                        onChange={this.handleNameChange}
                    />
                </td>
                <td>
                    <input
                        type="text"
                        placeholder="Примечание"
                        value={this.state.comment}
                        onChange={this.handleCommentChange}
                    />
                </td>
            </tr>
        );
    }
}
//<input type="submit" value="Добавить" />