import React from "react";
import '../../styles/EditorForm.css'

export class DeleteTopicForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: 'Сложность алгоритмов',
            id: '0',
            data: []
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentWillMount() {
        fetch("/proxy/topics")
            .then(response => response.json())
            .then(d => {
                this.setState({data: d});
                console.log(d)
            })
            .catch(error => console.error(error))
    }

    handleChange(event) {
        this.setState({
            id: event.target.value,
            value: event.target.label
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        fetch("./proxy/topic/" + this.state.id,
            {
                mode: "same-origin",
                method: "delete"
            })
            .then(() => {
                alert('Вы удалили Topic: ' + this.state.value + ' с Id: ' + this.state.id);
            })
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Удаление Topic</h3>
                <label>
                    <p>Выберите Topic, который хотите удалить:</p>
                    <select value={this.state.id} onChange={this.handleChange}>
                        {this.state.data.map(topic =>
                            <option label={topic.name} value={topic.id} name={topic.name}>{topic.name}</option>
                        )};
                    </select>
                </label>
                <br/><br/>
                <input className="button2" type="submit" value="Submit"/>
            </form>
        );
    }
}