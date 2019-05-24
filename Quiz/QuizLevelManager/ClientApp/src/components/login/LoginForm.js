import React from 'react';
import '../../styles/Login.css'

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
];

export class LoginForm extends React.Component {

    // Using a class based component here because we're accessing DOM refs

    handleSignIn(e) {
        e.preventDefault();
        let username = this.refs.username.value;
        let password = this.refs.password.value;
        let user = LoginForm.validateUser(username, password);
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
            <div id="login">
                <form name='form-login' onSubmit={this.handleSignIn.bind(this)}>
                    <span className="fontawesome-user"></span>
                    <input type="text" ref="username" id="user" placeholder="Username"/>

                    <span className="fontawesome-lock"></span>
                    <input type="password" ref="password" id="pass" placeholder="Password"/>

                    <input type="submit" value="Login"/>
                </form>
            </div>)
    }
}