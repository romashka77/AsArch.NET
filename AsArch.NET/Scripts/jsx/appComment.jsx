const data = [
    { Id: 1, Author: 'Daniel Lo Nigro', Text: 'Hello ReactJS.NET World!' },
    { Id: 2, Author: 'Pete Hunt', Text: 'This is one comment' },
    { Id: 3, Author: 'Jordan Walke', Text: 'This is *another* comment' },
];
ReactDOM.render(<CommentBox data={data} />, document.getElementById('content'));
