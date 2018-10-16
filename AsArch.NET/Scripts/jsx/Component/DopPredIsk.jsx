class DopPredIsk extends React.Component {
    //render() {
    //    return (
    //        <div className="DopPredIsk">
    //            <div className="DopPredIskItem">{this.props.NameIsk}</div>
    //            {this.props.children}
    //        </div>
    //    );
    //}
    rawMarkup() {
        const md = new Remarkable();
        const rawMarkup = md.render(this.props.children.toString());
        return { __html: rawMarkup };
    }
    render() {
        return (
            <div className="dopPredIsk">
                    <div className="dopPredIskKey">{this.props.key}</div>
                    <div className="dopPredIskName">{this.props.name}</div>
                    <div>
                        <span dangerouslySetInnerHTML={this.rawMarkup()} />
                    </div>
            </div>
        );
    }
}