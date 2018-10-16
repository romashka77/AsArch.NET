class DopPredIskTable extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [] };
    }
    loadCommentsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    componentDidMount() {
        this.loadCommentsFromServer();
        window.setInterval(
            () => this.loadCommentsFromServer(),
            this.props.pollInterval,
        );
    }
    render() {
        return (
            <div className="dopPredIskTable">
                <h3>Таблица</h3>
                <DopPredIskList data={this.state.data} />
                <DopPredIskForm />
            </div>
        );
    }
}
