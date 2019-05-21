import React from "react";

export class DeleteGeneratorForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            topic: 'Cложность алгоритмов',
            level: 'Циклы',
            generator: 'Generator 1',
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleInputChange(event) {
        const target = event.target
        const name = target.name
        const value = event.target.value
        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        alert('Вы удалили Generator: ' + this.state.generator);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Удаление Generator</h3>
                <label>
                    Выберите Topic, из которого хотите удалить Generator:
                    <select name="topic" value={this.state.topic} onChange={this.handleInputChange}>
                        <option value="Сложность алгоритмов">Сложность алгоритмов</option>
                    </select>
                </label>
                <br/>
                <label>
                    Выберите Level, из которого хотите удалить Generator:
                    <select name="level" value={this.state.level} onChange={this.handleInputChange}>
                        <option value="Циклы">Циклы</option>
                        <option value="Двойные циклы">Двойные циклы</option>
                    </select>
                </label>
                <br/>
                <label>
                    Выберите Generator, который хотите удалить:
                    <select name="generator" value={this.state.generator} onChange={this.handleInputChange}>
                        <option value="generator_1">Generator 1</option>
                    </select>
                </label>
                <input type="submit" value="Submit" />
            </form>
        );
    }
}