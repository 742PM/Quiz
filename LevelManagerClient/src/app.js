import React from "react";
import {RightsViewer} from "./auth/rights-viewer";


export class App extends React.Component {
    constructor(props) {
        super(props)
        this.user = props.user
    }

    render() {
        return (
            <div>
                <h1>Hello "{this.user.name}"</h1>
                <RightsViewer user={this.user}/>
            </div>);
    }
}