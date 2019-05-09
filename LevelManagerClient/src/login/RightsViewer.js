import {hasRole, isAllowed} from "./auth";
import React from "react";

export const RightsViewer = ({user}) => (
    <div>
        <div>
            <h4> Role of {user.username}:</h4>
            {hasRole(user, ['user']) && <p>Is User</p>}
            {hasRole(user, ['admin']) && <p>Is Admin</p>}
        </div>
        <div>
            <h4> Rights of {user.name}:</h4>
            {isAllowed(user, ['can_edit_topics']) && <p>Может создавать и удалять Topic</p>}
            {isAllowed(user, ['can_edit_levels']) && <p>Может создавать и удалять Level</p>}
            {isAllowed(user, ['can_edit_generators']) && <p>Может создавать и удалять Generator</p>}
            {isAllowed(user, ['can_render_tasks']) && <p>Может рендерить Task</p>}
            {isAllowed(user, ['can_get_levels_tasks_generators']) && <p>Может смотреть Topics, Levels, Generators</p>}
        </div>
    </div>
);