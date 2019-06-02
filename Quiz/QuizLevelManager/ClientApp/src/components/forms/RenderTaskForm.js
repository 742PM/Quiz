import React from "react";
import '../../styles/EditorForm.css'

export class RenderTaskForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            template: "for (int i = {{from1}}; i < {{to1}}; i += {{iter1}})\n    c++",
            possibleAnswers: '["O(n)"]',
            rightAnswer: "O(n)",
            hints: "[]",
            question: "Оцените временную сложность алгоритма"
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
        event.preventDefault();
        fetch("proxy/renderTask", {
            method: "post",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(
                {
                    "text": this.state.template,
                    "question": this.state.question,
                    "possibleAnswers": JSON.parse(this.state.possibleAnswers),
                    "answer": (this.state.rightAnswer),
                    "hints": JSON.parse(this.state.hints)
                })
        }).catch(resp => {
            console.log("error")
        }).then(resp => resp.json()
        ).then((resp) => {
            alert(`Отрендереный генератор: \n\n${resp.text}`);
        }).catch(resp => {
            console.log("error")
        })
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <h3>Параметры с которыми хотите отрендерить Task</h3>
                <br/>
                {/*<label>*/}
                {/*    <p>Question:</p>*/}
                {/*    <textarea className="smallTextarea"*/}
                {/*              name="question"*/}
                {/*              value={this.state.question}*/}
                {/*              onChange={this.handleInputChange}/>*/}
                {/*</label>*/}
                {/*<br/>*/}
                <label>
                    <p>Template:</p>
                    <textarea className="bigTextarea"
                              name="template"
                              value={this.state.template}
                              onChange={this.handleInputChange}/>
                </label>
                {/*<br/>*/}
                {/*<label>*/}
                {/*    <p>Possible Answers:</p>*/}
                {/*    <textarea className="bigTextarea"*/}
                {/*              name="possibleAnswers"*/}
                {/*              value={this.state.possibleAnswers}*/}
                {/*              onChange={this.handleInputChange}/>*/}
                {/*</label>*/}
                {/*<br/>*/}
                {/*<label>*/}
                {/*    <p>Hints:</p>*/}
                {/*    <textarea className="bigTextarea"*/}
                {/*              name="hints"*/}
                {/*              value={this.state.hints}*/}
                {/*              onChange={this.handleInputChange}/>*/}
                {/*</label>*/}
                {/*<br/>*/}
                {/*<label>*/}
                {/*    <p>Right Answer:</p>*/}
                {/*    <textarea className="smallTextarea"*/}
                {/*              name="rightAnswer"*/}
                {/*              value={this.state.rightAnswer}*/}
                {/*              onChange={this.handleInputChange}/>*/}
                {/*</label>*/}
                <br/><br/>
                <input className="button2" type="submit" value="Submit"/>
            </form>
        );
    }
}