import React from "react";
import '../../styles/EditorForm.css'

export class DeleteLevelForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            topic: 'Cложность алгоритмов',
            level: 'Циклы',
            topics: [],
            levels: []
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentWillMount() {
        fetch("/proxy/topics")
            .then(response => response.json())
            .then(resp => {
                this.setState({topics: resp});
                console.log(resp);
                fetch(`/proxy/${resp[0].id}/levels/`)
                    .then(response2 => response2.json())
                    .then(resp2 => {
                        this.setState({levels: resp2});
                        console.log(resp2)
                        console.log(this.state)
                    })
                    .catch(error => console.error(error))
            })
            .catch(error => console.error(error));
    }

    handleInputChange(event) {
        const target = event.target;
        const name = target.name;
        const value = event.target.value;
        if (name === 'topic')
        {
            fetch(`/proxy/${value}/levels/`)
                .then(response2 => response2.json())
                .then(resp2 => {
                    this.setState({levels: resp2});
                    console.log(resp2)
                    console.log(this.state)
                })
                .catch(error => console.error(error))
        }
        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        alert('Вы удалили Level: ' + this.state.level);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Удаление Level</h3>
                <label>
                    <p>Выберите Topic, из которого хотите удалить Level:</p>
                    <select name="topic" value={this.state.topic} onChange={this.handleInputChange}>
                        {this.state.topics.map(topic =>
                            <option label={topic.name} value={topic.id} name={topic.name}>{topic.name}</option>
                        )};
                    </select>
                </label>
                <br/>
                <label>
                    <p>Выберите Level, который хотите удалить:</p>
                    <select name="level" value={this.state.level} onChange={this.handleInputChange}>
                        {this.state.levels.map(level =>
                            <option label={level.description} value={level.id} name={level.description}>{level.description}</option>
                        )};
                    </select>
                </label>
                <br/><br/>
                <input className="button2" type="submit" value="Submit"/>
            </form>
        );
    }
}