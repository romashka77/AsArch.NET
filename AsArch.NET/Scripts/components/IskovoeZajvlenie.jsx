var React = require('react');
var ReactDOM = require('react-dom');
import TabDopIsk from './TabDopIsk.jsx';
import GrafSudZas from './GrafSudZas.jsx';

ReactDOM.render(
    <TabDopIsk
        url={Router.action(`Nodes`, `getdopprediskjson`, { id: id_global })}
    //pollInterval={2000}
    />,
    document.getElementById('tab-dop-isk')
);

ReactDOM.render(
    <GrafSudZas
        url={Router.action(`Nodes`, `GetSudZasJson`, { id: id_global })}
    //pollInterval={2000}
    />,
    document.getElementById('graf-sud-zas')
);
