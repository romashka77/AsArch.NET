class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox">
                <label className="control-label col-md-2">Comments</label>
                <div className="col-md-10">
                    <CommentList data={this.props.data} />
                    <CommentForm />
                </div>
            </div>
        );
    }
}
