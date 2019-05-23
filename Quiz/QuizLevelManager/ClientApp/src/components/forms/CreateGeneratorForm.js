import React from "react";
import '../../styles/EditorForm.css'

export class CreateGeneratorForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            topic: 'Cложность алгоритмов',
            level: 'Циклы',
            topicId: '',
            levelId: '',
            topics: [],
            levels: [],
            template: "for (int i = {{from1}}; i < {{to1}}; i += {{iter1}})\\r\\nc++\\r\\n",
            possibleAnswers: '["O(n)", "O(n*log(n)", "O(log(n)"]',
            rightAnswer: "O(1)",
            hints: "[]",
            streak: 1
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleInputChange1 = this.handleInputChange1.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const name = target.name;
        const value = event.target.value;
        this.setState({
            [name]: value
        });
    }

    handleInputChange1(event) {
        const target = event.target;
        const name = target.name;
        const value = event.target.value;
        if (name === 'topic') {
            fetch(`/proxy/${value}/levels/`)
                .then(response2 => response2.json())
                .then(resp2 => {
                    this.setState({levels: resp2});
                })
                .catch(error => console.error(error))
            this.setState({
                topicId: value,
                topic: event.target.label
            });
        } else {
            this.setState({
                levelId: value,
                level: event.target.label
            });
        }
    }

    componentWillMount() {
        fetch("/proxy/topics")
            .then(response => response.json())
            .then(resp => {
                this.setState({topics: resp, topicId: resp[0].id});
                console.log(resp);
                fetch(`/proxy/${resp[0].id}/levels/`)
                    .then(response2 => response2.json())
                    .then(resp2 => {
                        this.setState({levels: resp2, levelId: resp2[0].id});
                    })
                    .catch(error => console.error(error))
            })
            .catch(error => console.error(error));
    }

    handleSubmit(event) {
        fetch(`proxy/${this.state.topicId}/${this.state.levelId}/templategenerator`, {
            method: "post",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(
                {
                    "text": this.state.template,
                    "question": this.state.question,
                    "possibleAnswers": [],
                    // (this.state.possibleAnswers),
                    "answer": (this.state.rightAnswer),
                    "hints": [],
                    "streak": this.state.streak
                    // this.state.hints
                })
        }).catch(resp => {
            console.log("error")
        }).then(resp => resp.json()
        ).then((resp) => {
            alert('Вы создали Generator: ' + this.state.template);
            event.preventDefault();
        }).catch(resp => {
            console.log("error")
        })
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    <p>Выберите Topic, в который вы хотите добавить Generator:</p>
                    <select name="topic" value={this.state.topic} onChange={this.handleInputChange1}>
                        {this.state.topics.map(topic =>
                            <option label={topic.name} value={topic.id} name={topic.name}>{topic.name}</option>
                        )};
                    </select>
                </label>
                <br/>
                <label>
                    <p>Выберите Topic, в который вы хотите добавить Generator:</p>
                    <select name="level" value={this.state.level} onChange={this.handleInputChange1}>
                        {this.state.levels.map(level =>
                            <option label={level.description} value={level.id} name={level.description}>{level.description}</option>
                        )};
                    </select>
                </label>
                <h3>Параметры с которыми хотите создать Generator:</h3>
                <br/>
                <label>
                    <p>Template:</p>
                    <textarea className="bigTextarea"
                              name="template"
                              value={this.state.template}
                              onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    <p>Possible Answers:</p>
                    <textarea className="bigTextarea"
                              name="possibleAnswers"
                              value={this.state.possibleAnswers}
                              onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    <p>Hints:</p>
                    <textarea className="bigTextarea"
                              name="hints"
                              value={this.state.hints}
                              onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    <p>Right Answer:</p>
                    <textarea className="smallTextarea"
                              name="rightAnswer"
                              value={this.state.rightAnswers}
                              onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    <p>Streak:</p>
                    <textarea className="smallTextarea"
                              name="streak"
                              value={this.state.streak}
                              onChange={this.handleInputChange}/>
                </label>
                <br/><br/>
                <input className="button2" type="submit" value="Submit"/>
            </form>
        );
    }
}