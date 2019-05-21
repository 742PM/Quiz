import React from 'react';
import { render } from "react-dom";
import {App} from "./App/components/App";

// <App user={admin} />,
render(
    <App/>,
    document.getElementById('app')
);