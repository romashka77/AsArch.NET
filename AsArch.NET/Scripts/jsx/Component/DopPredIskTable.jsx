class DopPredIskTable extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [] };
        this.handleDopPredIskSubmit = this.handleDopPredIskSubmit.bind(this);
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
    componentDidMount() {
        this.loadDopPredIskFromServer();
        window.setInterval(
            () => this.loadDopPredIskFromServer(),
            this.props.pollInterval,
        );
    }
    render() {
        return (
            <div className="dopPredIskTable card card-block">
                <div className="table-responsive">
                    <table className="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>№</th>
                                <th>Сопутствующий предмет иска</th>
                                <th>Примечание</th>
                            </tr>
                        </thead>
                        <DopPredIskList data={this.state.data} />
                        <DopPredIskForm onDopPredIskSubmit={this.handleDopPredIskSubmit} />
                    </table>
                </div>
                
            </div>
        );
    }
}
                