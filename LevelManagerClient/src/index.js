import React from 'react';
import { render } from "react-dom";
import {App} from "./app";

// <App user={admin} />,
render(
    <App/>,
    document.getElementById('app')
);