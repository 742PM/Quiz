import React from "react";
import {LoginForm} from "./login/LoginForm";
import {Welcome} from "./login/Welcom";

export class App extends React.Component {

    constructor(props) {
        super(props)
        // the initial application state
        this.state = {
            user: null
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

    render() {
        // Here we pass relevant state to our child components
        // as props. Note that functions are passed using `bind` to
        // make sure we keep our scope to App
        return (
            <div>
                <h1>Quibble level manager</h1>
                {
                    (this.state.user) ?
                        <Welcome onSignOut={this.signOut.bind(this)} user={this.state.user}/>
                        : <LoginForm onSignIn={this.signIn.bind(this)}/>
                }
            </div>
        )
    }
}