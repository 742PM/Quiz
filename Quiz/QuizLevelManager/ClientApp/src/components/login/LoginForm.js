import React from 'react';
import '../../styles/LoginForm.css'

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
                <div>
                    <input type="text" ref="username" placeholder="enter you username"/>
                    <br/>
                </div>
                <div>
                    <input type="password" ref="password" placeholder="enter password"/>
                    <br/>
                </div>
                <div>
                    <input type="submit" value="Login"/>
                </div>
            </form>
        )
    }
}