import React from "react";

export class DeleteTopicForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {value: 'Сложность алгоритмов'};

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({value: event.target.value});
    }

    handleSubmit(event) {
        alert('Вы удалили Topic: ' + this.state.value);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Выберите Topic, который хотите удалить:
                    <select value={this.state.value} onChange={this.handleChange}>
                        <option value="Сложность алгоритмов">Сложность алгоритмов</option>
                        {/*<option value="lime">Lime</option>*/}
                        {/*<option value="coconut">Coconut</option>*/}
                        {/*<option value="mango">Mango</option>*/}
                    </select>
                </label>
                <input type="submit" value="Submit"/>
            </form>
        );
    }
}