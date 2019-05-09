import React from "react";
import {isAllowed} from "./login/auth";
import {CreateTopicForm} from "./forms/CreateTopicForm"
import {CreateLevelForm} from "./forms/CreateLevelForm";
import {DeleteTopicForm} from "./forms/DeleteTopicForm";
import {DeleteLevelForm} from "./forms/DeleteLevelFoerm";

export const Editor = ({user}) => {
    return (
        <div>
            {(isAllowed(user, ['can_edit_topics'])) ? <CreateTopicForm/> : undefined}
            {(isAllowed(user, ['can_edit_levels'])) ? <CreateLevelForm/> : undefined}
            {(isAllowed(user, ['can_edit_topics'])) ? <DeleteTopicForm/> : undefined}
            {(isAllowed(user, ['can_edit_levels'])) ? <DeleteLevelForm/> : undefined}
        </div>
    )
}