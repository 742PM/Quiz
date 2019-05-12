import React from "react";

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
        fetch("./service/topics")
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
        fetch("./service/deleteTopic/" + this.state.id, {method: "delete"})
            .then(() => {
                alert('Вы удалили Topic: ' + this.state.value + ' с Id: ' + this.state.id);
                event.preventDefault();
            })
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Удаление Topic</h3>
                <label>
                    Выберите Topic, который хотите удалить:
                    <select value={this.state.id} onChange={this.handleChange}>
                        {this.state.data.map(fbb =>
                            <option label={fbb.name} value={fbb.id} name={fbb.name}>{fbb.name}</option>
                        )};
                    </select>
                </label>
                <input type="submit" value="Submit"/>
            </form>
        );
    }
}