class DopPredIskList extends React.Component {
    render() {
        const dopPredIskNodes = this.props.data.map(isk => (
            <DopPredIsk name={isk.Name} key={isk.Id} id={isk.Id}>
                {isk.Comment}
            </DopPredIsk>
        ));
        return <tbody className="dopPredIskList">{dopPredIskNodes}</tbody>;
    }
}