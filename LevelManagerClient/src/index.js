import React from 'react';
import { render } from "react-dom";
import {App} from "./app";

const admin = {
    name: "Admin",
    roles: ['user', 'admin'],
    rights: ['can_create_levels', 'can_create_topics',]
};

const user = {
    name: "User",
    roles: ['user'],
    rights: []
};

render(
    <App user={admin} />,
    document.getElementById('app')
);