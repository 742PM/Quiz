import React from "react";

export class DeleteLevelForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {value: 'Циклы'};

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({value: event.target.value});
    }

    handleSubmit(event) {
        alert('Вы удалили Level: ' + this.state.value);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Выберите Level, которую хотите удалить:
                    <select value={this.state.value} onChange={this.handleChange}>
                        <option value="Циклы">Циклы</option>
                        <option value="Двойные циклы">Двойные циклы</option>
                        {/*<option value="coconut">Coconut</option>*/}
                        {/*<option value="mango">Mango</option>*/}
                    </select>
                </label>
                <input type="submit" value="Submit" />
            </form>
        );
    }
}