import React from "react";
import '../../styles/EditorForm.css'

export class CreateLevelForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            topicId: '',
            topicValue: '',
            level: '',
            topics: [],
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleChange1 = this.handleChange1.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    componentWillMount() {
        fetch("/proxy/topics")
            .then(response => response.json())
            .then(d => {
                this.setState({topics: d});
                console.log(d)
            })
            .catch(error => console.error(error))
    }

    handleInputChange(event) {
        const target = event.target;
        const name = target.name;
        const value = event.target.value;
        this.setState({
            [name]: value
        });
    }

    handleChange1(event) {
        this.setState({
            topicId: event.target.value,
            topicValue: event.target.label
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        fetch(`proxy/${this.state.topicId}/level`, {
            method: "post",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(
                {
                    "description": this.state.level,
                    "nextLevels": [],
                    "previousLevels": []
                }
            )
        }).catch(resp => {
            console.log("error")
        }).then(() => {
            alert('Был создан пустой Level: ' + this.state.level);
        }).catch(resp => {
            console.log("error")
        })
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Добавление Level</h3>
                <label>
                    <p>Выберите Topic, в который хотите добавить Level:</p>
                    <select name="topic" value={this.state.topic} onChange={this.handleChange1}>
                        {this.state.topics.map(topic =>
                            <option label={topic.name} value={topic.id} name={topic.name}>{topic.name}</option>
                        )};
                    </select>
                </label>
                <br/>
                <label>
                    <p>Имя Level, который хотите добавить:</p>
                    <textarea className="smallTextarea" name="level" value={this.state.level} onChange={this.handleInputChange} />
                </label>
                <br/><br/>
                <input className="button2" type="submit" value="Create Level" />
            </form>
        );
    }
}