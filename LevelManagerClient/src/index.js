import React from 'react';
import ReactDOM from 'react-dom';

const title = 'Здесь будет менеджер уровней для Quible';

ReactDOM.render(
    <div>{title}</div>,
    document.getElementById('app')
);

module.hot.accept();