//const data = [
//    { Id: 1, Author: "Daniel Lo Nigro", Text: "Hello ReactJS.NET World!" },
//    { Id: 2, Author: "Pete Hunt", Text: "This is one comment" },
//    { Id: 3, Author: "Jordan Walke", Text: "This is *another* comment" }
//];
//- CommentBox
class CommentBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [] };
        this.handleCommentSubmit = this.handleCommentSubmit.bind(this);
    }
    loadCommentsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    handleCommentSubmit(comment) {
        const data = new FormData();
        data.append('Author', comment.Author);
        data.append('Text', comment.Text);

        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = () => this.loadCommentsFromServer();
        xhr.send(data);
    }
    componentDidMount() {
        this.loadCommentsFromServer();
        window.setInterval(() => this.loadCommentsFromServer(), this.props.pollInterval);
    }
    render() {
        //return (<div className="commentBox">Hello, world! I am a CommentBox.</div>);

        //return (
        //    React.createElement('div', { className: "commentBox" },
        //        "Hello, world! I am a CommentBox."
        //    )
        //);
        return (
            <div className="commentBox">
                <h1>Comments</h1>
                <CommentList data={this.state.data} />
                <CommentForm onCommentSubmit={this.handleCommentSubmit} />
            </div>
        );
    }
}
//  - CommentList
class CommentList extends React.Component {
    render() {
        //return (<div className="commentList">Hello, world! I am a CommentList.</div>);
        //return (
        //    <div className="commentList">
        //        <Comment author="Daniel Lo Nigro">Hello ReactJS.NET World!</Comment>
        //        <Comment author="Pete Hunt">This is one comment</Comment>
        //        <Comment author="Jordan Walke">This is *another* comment</Comment>
        //    </div>
        //);
        const commentNodes = this.props.data.map(comment => (
            <Comment author={comment.Author} key={comment.Id}>
                {comment.Text}
            </Comment>
        ));
        return (
            <div className="commentList">
                {commentNodes}
            </div>
        );
    }
}
//      - Comment
class Comment extends React.Component {
    rawMarkup() {
        const md = new Remarkable();
        const rawMarkup = md.render(this.props.children.toString());
        return { __html: rawMarkup };
    }

    render() {
        //return (
        //    <div className="comment">
        //        <h2 className="commentAuthor">
        //            {this.props.author}
        //        </h2>
        //        {this.props.children}
        //    </div>
        //);
        //const md = new Remarkable();
        //return (
        //    <div className="comment">
        //        <h2 className="commentAuthor">
        //            {this.props.author}
        //        </h2>
        //        {md.render(this.props.children.toString())}
        //    </div>
        //);
        return (
            <div className="comment">
                <h2 className="commentAuthor">
                    {this.props.author}
                </h2>
                <span dangerouslySetInnerHTML={this.rawMarkup()} />
            </div>
        );
    }
}
//  - CommentForm
class CommentForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { author: '', text: '' };
        this.handleAuthorChange = this.handleAuthorChange.bind(this);
        this.handleTextChange = this.handleTextChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleAuthorChange(e) {
        this.setState({ author: e.target.value });
    }
    handleTextChange(e) {
        this.setState({ text: e.target.value });
    }
    handleSubmit(e) {
        e.preventDefault();
        const author = this.state.author.trim();
        const text = this.state.text.trim();
        if (!text || !author) {
            return;
        }
        this.props.onCommentSubmit({ Author: author, Text: text });
        this.setState({ author: '', text: '' });
    }

    render() {
        //return (<div className="commentForm">Hello, world! I am a CommentForm.</div>);
        return (
            //<form className="commentForm">
            //    <input type="text" placeholder="Your name" />
            //    <input type="text" placeholder="Say something..." />
            //<form className="commentForm">
            <form className="commentForm" onSubmit={this.handleSubmit}>
                <input
                    type="text"
                    placeholder="Your name"
                    value={this.state.author}
                    onChange={this.handleAuthorChange}
                />
                <input
                    type="text"
                    placeholder="Say something..."
                    value={this.state.text}
                    onChange={this.handleTextChange}
                />
                <input type="submit" value="Post" />
            </form>
        );
    }
}

ReactDOM.render(
    //<CommentBox />,
    //document.getElementById('content')
    //React.createElement(CommentBox, null),
    //<CommentBox data={data} />,
    //<CommentBox url="/comments" />,
    <CommentBox url="/comments" submitUrl="/comments/new" pollInterval={2000} />,
    document.getElementById('content')
);