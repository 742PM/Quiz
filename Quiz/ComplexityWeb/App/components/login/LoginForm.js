import React from 'react';
import {isAllowed} from "./auth";

const users = [
    {
        username: "admin",
        password: "pass",
        roles: ['user', 'admin'],
        rights: [
            'can_edit_levels',
            'can_edit_topics',
            'can_edit_generators',
            'can_render_tasks',
            'can_get_levels_tasks_generators']
    },
    {
        username: "user",
        password: "pass",
        roles: ['user'],
        rights: [
            'can_render_tasks',
            'can_get_levels_tasks_generators']
    },
]

export class LoginForm extends React.Component {

    // Using a class based component here because we're accessing DOM refs

    handleSignIn(e) {
        e.preventDefault()
        let username = this.refs.username.value
        let password = this.refs.password.value
        let user = LoginForm.validateUser(username, password)
        if (user)
            this.props.onSignIn(user)
    }

    static validateUser(username, password) {
        // ToDo validate user on backend
        for (let user of users) {
            if (username === user.username &&
                password === user.password) {
                return user
            }
        }
    }

    render() {
        return (
            <form onSubmit={this.handleSignIn.bind(this)}>
                <h3>Sign in</h3>
                <input type="text" ref="username" placeholder="enter you username"/>
                <input type="password" ref="password" placeholder="enter password"/>
                <input type="submit" value="Login"/>
            </form>
        )
    }
}