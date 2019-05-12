import React from "react";
import {isAllowed} from "./login/auth";
import {CreateTopicForm} from "./forms/CreateTopicForm"
import {CreateLevelForm} from "./forms/CreateLevelForm";
import {DeleteTopicForm} from "./forms/DeleteTopicForm";
import {DeleteLevelForm} from "./forms/DeleteLevelForm";
import {RenderTaskForm} from "./forms/RenderTaskForm";
import {DeleteGeneratorForm} from "./forms/DeleteGeneratorForm";
import {CreateGeneratorForm} from "./forms/CreateGeneratorForm";

export const Editor = ({user}) => {
    return (
        <div>
            {(isAllowed(user, ['can_edit_topics'])) ? <CreateTopicForm/> : undefined}
            {(isAllowed(user, ['can_edit_levels'])) ? <CreateLevelForm/> : undefined}
            {(isAllowed(user, ['can_edit_topics'])) ? <DeleteTopicForm/> : undefined}
            {(isAllowed(user, ['can_edit_levels'])) ? <DeleteLevelForm/> : undefined}
            {(isAllowed(user, ['can_edit_generators'])) ? <DeleteGeneratorForm/> : undefined}
            {(isAllowed(user, ['can_edit_generators'])) ? <CreateGeneratorForm/>: undefined}
            {(isAllowed(user, ['can_render_tasks'])) ? <RenderTaskForm/> : undefined}
        </div>
    )
}