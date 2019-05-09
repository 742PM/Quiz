import React from "react";
import {RightsViewer} from "./RightsViewer";

export const Welcome = ({user, onSignOut})=> {
    // This is a dumb "stateless" component
    return (
        <div>
            Welcome <strong>{user.username}</strong>!
            <RightsViewer user={user}/>
            <a href="javascript:;" onClick={onSignOut}>Sign out</a>
        </div>
    )
}