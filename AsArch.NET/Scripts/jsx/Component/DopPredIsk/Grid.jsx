class Grid extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectId: null,
            records: []
        };
        console.log(`state`, this.state);
        console.log(`props`, this.props);
        this.selectRow = this.selectRow.bind(this);
    }
    componentDidMount() {
        this.setState({ records: this.props.data });
    }

    selectRow(e, record) {
        let records = this.state.records;
        records.forEach(r => {
            r.edit = false;
            if (r.id === record.id) {
                r.edit = true;
            }
        });
        this.setState({ records: records, selectId: record.id });
    }




    render() {
        return (
            <tbody className="Grid">

            </tbody>
        );
    }

    //    {
    //    this.state.records.map((record, index) => {
    //        return <GridRow record={record}
    //            key={index}
    //            index={index}
    //            onSelectRow={this.selectRow}
    //        />
    //    })
    //}



    //render() {
    //    const dopPredIskNodes = this.props.data.map(isk => (
    //        <DopPredIsk name={isk.Name} key={isk.Id} id={isk.Id}>
    //            {isk.Comment}
    //        </DopPredIsk>
    //    ));
    //    
    //}

}