import React from "react";
import '../../styles/EditorForm.css'

export class DeleteGeneratorForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            topic: 'Cложность алгоритмов',
            level: 'Циклы',
            generator: 'Generator 1',
            topics: [],
            levels: [],
            templateGenerators: []
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    componentWillMount() {
        fetch("/proxy/topics")
            .then(response => response.json())
            .then(resp => {
                this.setState({topics: resp});
                fetch(`/proxy/${resp[0].id}/levels/`)
                    .then(response2 => response2.json())
                    .then(resp2 => {
                        this.setState({levels: resp2});
                        fetch(`/proxy/${resp[0].id}/${resp2[0].id}/templateGenerators/`)
                            .then(response3 => response3.json())
                            .then(resp3 => {
                                this.setState({templateGenerators: resp3});
                            })
                            .catch(error => console.error(error))
                    })
                    .catch(error => console.error(error))
            })
            .catch(error => console.error(error));
    }

    handleInputChange(event) {
        const target = event.target;
        const name = target.name;
        const value = event.target.value;
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
                    <p>Выберите Topic, из которого хотите удалить Generator:</p>
                    <select name="topic" value={this.state.topic} onChange={this.handleInputChange}>
                        {this.state.topics.map(topic =>
                            <option label={topic.name} value={topic.id} name={topic.name}>{topic.name}</option>
                        )};
                    </select>
                </label>
                <br/>
                <label>
                    <p>Выберите Level, из которого хотите удалить Generator:</p>
                    <select name="level" value={this.state.level} onChange={this.handleInputChange}>
                        {this.state.levels.map(level =>
                            <option label={level.description} value={level.id} name={level.description}>{level.description}</option>
                        )};
                    </select>
                </label>
                <br/>
                <label>
                    <p>Выберите Generator, который хотите удалить:</p>
                    <select name="generator" value={this.state.generator} onChange={this.handleInputChange}>
                        {this.state.templateGenerators.map(generator =>
                            <option label={generator.text} value={generator.id} name={generator.text}>{generator.text}</option>
                        )};
                    </select>
                </label>
                <br/><br/>
                <input className="button2" type="submit" value="Submit" />
            </form>
        );
    }
}