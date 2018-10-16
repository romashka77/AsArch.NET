//class CommentList extends React.Component {
//    render() {
//        return (
//            <div className="commentList">
//                <Comment author="Daniel Lo Nigro">
//                    Hello ReactJS.NET World!
//                </Comment>
//                <Comment author="Pete Hunt">This is one comment</Comment>
//                <Comment author="Jordan Walke">
//                    This is *another* comment
//                </Comment>
//            </div>
//        );
//    }
//}
class CommentList extends React.Component {
    render() {
        const commentNodes = this.props.data.map(comment => (
            <Comment author={comment.Author} key={comment.Id}>
                {comment.Text}
            </Comment>
        ));
        return <div className="commentList">{commentNodes}</div>;
    }
}