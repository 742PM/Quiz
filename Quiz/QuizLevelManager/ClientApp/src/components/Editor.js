import React from "react";
import {isAllowed} from "./login/auth";
import {CreateTopicForm} from "./forms/CreateTopicForm"
import {CreateLevelForm} from "./forms/CreateLevelForm";
import {DeleteTopicForm} from "./forms/DeleteTopicForm";
import {DeleteLevelForm} from "./forms/DeleteLevelForm";
import {RenderTaskForm} from "./forms/RenderTaskForm";
import {DeleteGeneratorForm} from "./forms/DeleteGeneratorForm";
import {CreateGeneratorForm} from "./forms/CreateGeneratorForm";
import '../styles/Editor.css'

export class Editor extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            user: props.user,
            createTopic: false,
            deleteTopic: false,
            createLevel: false,
            deleteLevel: false,
            createGenerator: false,
            deleteGenerator: false,
            renderTask: false,
        }
    }

    render() {
        return (
            <div>
                {(isAllowed(this.state.user, ['can_edit_topics'])) ?
                    <div className="center">
                        <button className="button1" onClick={() => this.setState({createTopic: !this.state.createTopic})}>Создание Topic</button>
                        {(this.state.createTopic) ? < CreateTopicForm/> : undefined}
                        <br/>
                    </div> : undefined}
                {(isAllowed(this.state.user, ['can_edit_topics'])) ?
                    <div className="center">
                        <button className="button1" onClick={() => this.setState({deleteTopic: !this.state.deleteTopic})}>Удаление Topic</button>
                        {(this.state.deleteTopic) ? < DeleteTopicForm/> : undefined}
                        <br/>
                    </div> : undefined}
                {(isAllowed(this.state.user, ['can_edit_levels'])) ?
                    <div className="center">
                        <button className="button1" onClick={() => this.setState({createLevel: !this.state.createLevel})}>Создание Level</button>
                        {(this.state.createLevel) ? < CreateLevelForm/> : undefined}
                        <br/>
                    </div> : undefined}
                {(isAllowed(this.state.user, ['can_edit_levels'])) ?
                    <div className="center">
                        <button className="button1" onClick={() => this.setState({deleteLevel: !this.state.deleteLevel})}>Удаление Level</button>
                        {(this.state.deleteLevel) ? < DeleteLevelForm/> : undefined}
                        <br/>
                    </div> : undefined}
                {(isAllowed(this.state.user, ['can_edit_generators'])) ?
                    <div className="center">
                        <button className="button1" onClick={() => this.setState({createGenerator: !this.state.createGenerator})}>Создание Generator</button>
                        {(this.state.createGenerator) ? < CreateGeneratorForm/> : undefined}
                        <br/>
                    </div> : undefined}
                {(isAllowed(this.state.user, ['can_edit_generators'])) ?
                    <div className="center">
                        <button className="button1" onClick={() => this.setState({deleteGenerator: !this.state.deleteGenerator})}>Удаление Generator</button>
                        {(this.state.deleteGenerator) ? < DeleteGeneratorForm/> : undefined}
                        <br/>
                    </div> : undefined}
                {(isAllowed(this.state.user, ['can_render_tasks'])) ?
                    <div className="center">
                        <button className="button1" onClick={() => this.setState({renderTask: !this.state.renderTask})}>Отрисовка Task</button>
                        {(this.state.renderTask) ? < RenderTaskForm/> : undefined}
                        <br/>
                    </div> : undefined}
            </div>
        )
    }
}