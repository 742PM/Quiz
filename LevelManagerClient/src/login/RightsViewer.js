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
            {isAllowed(user, ['can_create_topics']) && <p>Может создавать Topic</p>}
            {isAllowed(user, ['can_create_levels']) && <p>Может создавать Level</p>}
        </div>
    </div>
);