class Theader extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const headNodes = this.props.data.map(h => (
            <th>
                {h}
            </th>
        ));
        return (
            <thead>
                <tr>{headNodes}</tr>
            </thead>
        );
    }
}