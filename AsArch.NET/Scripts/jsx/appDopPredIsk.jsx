ReactDOM.render(
    <DopPredIskTable url={Router.action(`Nodes`, `getdopprediskjson`, { id: id_global})}  pollInterval = { 2000} />,
    document.getElementById(`TabDopPredIsk`));
//Router.action('Foo', 'Bar', { id: 123 })