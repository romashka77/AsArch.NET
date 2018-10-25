var React = require('react');
var ReactDOM = require('react-dom');
import TabDopIsk from './TabDopIsk.jsx';

ReactDOM.render(
    <TabDopIsk
        url={Router.action(`Nodes`, `getdopprediskjson`, { id: id_global })}
        url_options={Router.action(`Nodes`, `GetDopPredIskOptionsJson`)}
        //pollInterval={2000}
    />,
    document.getElementById('tabdopisk')
);
