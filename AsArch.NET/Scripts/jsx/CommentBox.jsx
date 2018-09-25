//import React from 'react';
//import CommentList from './CommentList';
//import CommentForm from './CommentForm';

class CommentBox extends React.Component {
    //constructor(props) {
    //    super(props);
    //    this.state = { data: [] };
    //    this.handleCommentSubmit = this.handleCommentSubmit.bind(this);
    //}

    //componentDidMount() {
    //    this.loadCommentsFromServer();
    //    window.setInterval(() => this.loadCommentsFromServer(), this.props.pollInterval);
    //}

    //handleCommentSubmit(comment) {
    //    const data = new FormData();
    //    data.append('Author', comment.Author);
    //    data.append('Text', comment.Text);

    //    const xhr = new XMLHttpRequest();
    //    xhr.open('post', this.props.submitUrl, true);
    //    xhr.onload = () => this.loadCommentsFromServer();
    //    xhr.send(data);
    //}

    //loadCommentsFromServer() {
    //    const xhr = new XMLHttpRequest();
    //    xhr.open('get', this.props.url, true);
    //    xhr.onload = () => {
    //        const data = JSON.parse(xhr.responseText);
    //        this.setState({ data: data });
    //    };
    //    xhr.send();
    //}
    render() {
        return (
            <div className="commentBox">
                Hello, world! I am a CommentBox.
            </div>
        );
        //return (
        //    <div className="commentBox">
        //        <h1>Comments</h1>
        //        <CommentList data={this.state.data} />
        //        <CommentForm onCommentSubmit={this.handleCommentSubmit} />
        //    </div>
        //);
    }
}
export default CommentBox;