import React from "react";
import {RightsViewer} from "./RightsViewer";

export const Welcome = ({user, rights, onRights, onSignOut})=> {
    // This is a dumb "stateless" component
    return (
        <div>
            Welcome <strong>{user.username}</strong>!
            <br/>
            <a href="javascript:" onClick={onRights}>{
                (rights)?
                    "Скрыть права пользователя":
                    "Посмотреть права пользователя"}</a>
            <br/>
            <a href="javascript:" onClick={onSignOut}>Sign out</a>
        </div>
    )
};