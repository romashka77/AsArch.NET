class DopPredIskList extends React.Component {
    render() {
        const dopPredIskNodes = this.props.data.map(isk => (
            <DopPredIsk name={isk.Name} key={isk.Id}>
                {isk.Comment}
            </DopPredIsk>
        ));
        return <div className="dopPredIskList">{dopPredIskNodes}</div>;
    }
}