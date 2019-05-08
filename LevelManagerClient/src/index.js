import React from 'react';
import { render } from "react-dom";
import { hasRole, isAllowed } from './auth';


const admin = {
    roles: ['user', 'admin'],
    rights: ['can_create_levels', 'can_create_topics',]
};

const user = {
    roles: ['user'],
    rights: []
};

const App = ({ user }) => (
    <div>
        {hasRole(user, ['user']) && <p>Is User</p>}
        {hasRole(user, ['admin']) && <p>Is Admin</p>}
        {isAllowed(user, ['can_create_topics']) && <p>Может создавать Topic</p>}
        {isAllowed(user, ['can_create_levels']) && <p>Может создавать Level</p>}
    </div>
);

render(
    <App user={admin} />,
    document.getElementById('app')
);