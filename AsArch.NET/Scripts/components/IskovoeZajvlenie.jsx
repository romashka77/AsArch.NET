//import 'react-bootstrap-table/dist/react-bootstrap-table.min.css';

var React = require('react');
var ReactDOM = require('react-dom');

//import DopPredIsk from './DopPredIsk.jsx';
import SudZas from './SudZas.jsx';
import DocIsk from './DocIsk.jsx';

//ReactDOM.render(
//    <DopPredIsk
//        url={Router.action(`Nodes`, `GetDopPredIskJson`, { id: id_global })}
//    />,
//    document.getElementById('tab-dop-isk')
//);

ReactDOM.render(
    <SudZas
        url={Router.action(`Nodes`, `GetSudZasJson`, { id: id_global })}
    />,
    document.getElementById('graf-sud-zas')
);

ReactDOM.render(
    <DocIsk
        url={Router.action(`Nodes`, `GetDocIskJson`, { id: id_global })}
    />,
    document.getElementById('doc-isk')
);




