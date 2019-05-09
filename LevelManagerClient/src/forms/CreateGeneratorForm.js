import React from "react";

export class CreateGeneratorForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            template: "for (int i = {{from1}}; i < {{to1}}; i += {{iter1}})\\r\\nc++\\r\\n",
            possibleAnswers: '["O(n)", "O(n*log(n)", "O(log(n)"]',
            rightAnswers: "O(1)",
            hints: "[]",
            streak: 1
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        alert('Вы создали Generator: ' + this.state.template);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Параметры с которыми хотите создать Generator:</h3>
                <br/>
                <label>
                    Template:
                    <input
                        name="template"
                        type="text"
                        value={this.state.template}
                        onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    Possible Answers:
                    <input
                        name="possibleAnswer"
                        type="text"
                        value={this.state.possibleAnswers}
                        onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    Right Answer:
                    <input
                        name="rightAnswer"
                        type="text"
                        value={this.state.rightAnswers}
                        onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    Hints:
                    <input
                        name="hints"
                        type="text"
                        value={this.state.hints}
                        onChange={this.handleInputChange}/>
                </label>
                <br/>
                <label>
                    Streak:
                    <input
                        name="hints"
                        type="number"
                        value={this.state.streak}
                        onChange={this.handleInputChange}/>
                </label>
                <input type="submit" value="Submit" />
            </form>
        );
    }
}