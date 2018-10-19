class DopPredIsk extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectId: null,
            data: []
        };
        this.handleDopPredIskSubmit = this.handleDopPredIskSubmit.bind(this);
        this.selectRow = this.selectRow.bind(this);
    }
    
    componentDidMount() {
        this.loadDopPredIskFromServer();
    }

    selectRow(e, record) {
        let records = this.state.data;
        records.forEach(r => {
            r.edit = false;
            if (r.Id === record.Id) {
                r.Edit = true;
            }
        });
        this.setState({ data: records, selectId: record.id });
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
        return (
            <div className="dopPredIskTable card card-block">
                <div className="table-responsive">
                    <span>ИД:{this.state.selectId}</span>
                    <table className="table table-striped table-hover">
                        <Theader data={[`№`, `Сопутствующий предмет иска`, `Примечание`]} />
                        <tbody className="selectInTable">
                            {this.state.data.map((value, index) => {
                                return (
                                    <GridRow record={value} key={index} onSelectRow={this.selectRow} />
                                );
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        );
    }
}

//<Grid data={this.state.data} />                      
//<DopPredIskForm url={Router.action(`Nodes`, `GetDopPredIskOptionsJson`)} onDopPredIskSubmit={this.handleDopPredIskSubmit} />