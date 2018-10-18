class DopPredIskSelect extends React.Component {
    constructor(props) {
        super(props);
        this.state = { value: `` };
        this.handleChange = this.handleChange.bind(this);
    }
    handleChange(e) {
        this.setState({ value: e.target.value });
        this.props.onValueChange(e);
    }
    render() {
        const optionNodes = this.props.data.map(o =>
            (
                <option value={o.Text}>
                    {o.Text}
                </option>
            )
        );
        return (
            <select value={this.state.value} onChange={this.handleChange} >
                {optionNodes}
            </select >
        );
    }
}