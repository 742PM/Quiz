import React from "react";
import {LoginForm} from "./login/LoginForm";
import {Welcome} from "./login/Welcom";
import {Editor} from "./Editor";
import {RightsViewer} from "./login/RightsViewer";

import '../styles/App.css';

export class App extends React.Component {

    constructor(props) {
        super(props)
        // the initial application state
        this.state = {
            user: null,
            rights: false
        }
    }


    // App "actions" (functions that modify state)
    signIn(user) {
        // This is where you would call Firebase, an API etc...
        // calling setState will re-render the entire app (efficiently!)
        this.setState({
            user
        })
    }

    signOut() {
        // clear out user from state
        this.setState({user: null})
    }

    showRights() {
        this.setState({rights: true})
    }

    hideRights() {
        this.setState({rights: false})
    }

    render() {
        // Here we pass relevant state to our child components
        // as props. Note that functions are passed using `bind` to
        // make sure we keep our scope to App
        return (
            <div>
                <h1>Quibble level manager</h1>
                <div class="main">
                {
                    (this.state.user) ?
                        <div>
                            <Welcome
                                onSignOut={this.signOut.bind(this)}
                                rights={this.state.rights}
                                user={this.state.user}
                                onRights={
                                    (this.state.rights) ?
                                        this.hideRights.bind(this) :
                                        this.showRights.bind(this)}/>
                            <Editor user={this.state.user}/>
                        </div>
                        : <LoginForm onSignIn={this.signIn.bind(this)}/>
                }
                </div>
                {(this.state.rights) ?
                    <RightsViewer user={this.state.user}/> :
                    undefined}
            </div>
        )
    }
}