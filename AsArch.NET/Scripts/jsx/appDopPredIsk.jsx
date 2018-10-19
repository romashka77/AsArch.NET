ReactDOM.render(
    <DopPredIsk
        url={Router.action(`Nodes`, `GetDopPredIskJson`, { id: id_global })}
        submitUrl={Router.action(`Nodes`, `AddDopPredIsk`)}
        pollInterval={2000}
    />,
    document.getElementById(`TabDopPredIsk`));