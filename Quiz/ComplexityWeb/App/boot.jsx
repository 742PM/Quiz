import React from 'react';
import ReactDOM from 'react-dom';
import 'whatwg-fetch';
import {AppContainer} from 'react-hot-loader';
import {App} from './components/App';


function renderApp() {
    // This code starts up the React app when it runs in a browser. It sets up the routing
    // configuration and injects the app into a DOM element.
    ReactDOM.render(
        <AppContainer>
            <App/>
        </AppContainer>,
        document.getElementById('react-app')
    );
}

renderApp();

// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept(App, () => {
        renderApp();
    });
}
