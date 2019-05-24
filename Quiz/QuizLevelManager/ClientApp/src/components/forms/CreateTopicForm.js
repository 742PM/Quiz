import React from "react";
import '../../styles/EditorForm.css'

export class CreateTopicForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: '',
            description: ''
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        const target = event.target;
        const name = target.name;
        const value = target.value;
        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        fetch("proxy/topic", {
            method: "post",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(
                {
                    "name": this.state.value,
                    "description": this.state.description
                }
            )
        }).catch(resp => {
            console.log("error")
        }).then(() => {
            alert('Был создан пустой Topic: ' + this.state.value);
        }).catch(resp => {
            console.log("error")
        })
    }


    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Добавление Topic</h3>
                <label>
                    <p>Имя Topic, который хотите добавить:</p>
                    <textarea className="smallTextarea" name="value" value={this.state.value} onChange={this.handleChange}/>
                </label>
                <br/>
                <label>
                    <p>Описание задания для Topic:</p>
                    <textarea className="smallTextarea" name="description" value={this.state.description} onChange={this.handleChange}/>
                </label>
                <br/><br/>
                <input className="button2" type="submit" value="Create Topic"/>
            </form>
        );
    }
}