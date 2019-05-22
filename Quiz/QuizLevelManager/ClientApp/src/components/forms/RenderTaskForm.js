import React from "react";
import '../../styles/EditorForm.css'

export class RenderTaskForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            template: "for (int i = {{from1}}; i < {{to1}}; i += {{iter1}})\\r\\nc++\\r\\n",
            possibleAnswers: '["O(n)", "O(n*log(n)", "O(log(n)"]',
            rightAnswers: "O(1)",
            hints: "[]"
        };

        this.handleInputChange = this.handleInputChange.bind(this);
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

    handleSubmit(event) {
        alert('Отрендереный генератор: ' + this.state.template);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Параметры с которыми хотите отрендерить Task</h3>
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
                        name="possibleAnswer"
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
                <br/><br/>
                <input className="button2" type="submit" value="Submit" />
            </form>
        );
    }
}