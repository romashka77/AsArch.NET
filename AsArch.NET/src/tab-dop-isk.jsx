var React = require('react');
var ReactDOM = require('react-dom');
import TabDopIsk from './components/TabDopIsk.jsx';

ReactDOM.render(
    <TabDopIsk url={Router.action(`Nodes`, `getdopprediskjson`, { id: id_global })} pollInterval={2000}/>,
    document.getElementById('tabdopisk')
);
