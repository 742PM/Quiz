/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// identity function for calling harmony imports with the correct context
/******/ 	__webpack_require__.i = function(value) { return value; };
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "dist/";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 8);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(2))(82);

/***/ }),
/* 1 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.hasRole = exports.isAllowed = exports.isAuthenticated = void 0;

var isAuthenticated = function isAuthenticated(user) {
  return !!user;
};

exports.isAuthenticated = isAuthenticated;

var isAllowed = function isAllowed(user, rights) {
  return rights.some(function (right) {
    return user.rights.includes(right);
  });
};

exports.isAllowed = isAllowed;

var hasRole = function hasRole(user, roles) {
  return roles.some(function (role) {
    return user.roles.includes(role);
  });
};

exports.hasRole = hasRole;

/***/ }),
/* 2 */
/***/ (function(module, exports) {

module.exports = vendor_83bf5dddc7dc1e9df2f7;

/***/ }),
/* 3 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.RightsViewer = void 0;

var _auth = __webpack_require__(1);

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

var RightsViewer = function RightsViewer(_ref) {
  var user = _ref.user;
  return _react["default"].createElement("div", null, _react["default"].createElement("div", null, _react["default"].createElement("h4", null, " Role of ", user.username, ":"), (0, _auth.hasRole)(user, ['user']) && _react["default"].createElement("p", null, "Is User"), (0, _auth.hasRole)(user, ['admin']) && _react["default"].createElement("p", null, "Is Admin")), _react["default"].createElement("div", null, _react["default"].createElement("h4", null, " Rights of ", user.name, ":"), (0, _auth.isAllowed)(user, ['can_edit_topics']) && _react["default"].createElement("p", null, "\u041C\u043E\u0436\u0435\u0442 \u0441\u043E\u0437\u0434\u0430\u0432\u0430\u0442\u044C \u0438 \u0443\u0434\u0430\u043B\u044F\u0442\u044C Topic"), (0, _auth.isAllowed)(user, ['can_edit_levels']) && _react["default"].createElement("p", null, "\u041C\u043E\u0436\u0435\u0442 \u0441\u043E\u0437\u0434\u0430\u0432\u0430\u0442\u044C \u0438 \u0443\u0434\u0430\u043B\u044F\u0442\u044C Level"), (0, _auth.isAllowed)(user, ['can_edit_generators']) && _react["default"].createElement("p", null, "\u041C\u043E\u0436\u0435\u0442 \u0441\u043E\u0437\u0434\u0430\u0432\u0430\u0442\u044C \u0438 \u0443\u0434\u0430\u043B\u044F\u0442\u044C Generator"), (0, _auth.isAllowed)(user, ['can_render_tasks']) && _react["default"].createElement("p", null, "\u041C\u043E\u0436\u0435\u0442 \u0440\u0435\u043D\u0434\u0435\u0440\u0438\u0442\u044C Task"), (0, _auth.isAllowed)(user, ['can_get_levels_tasks_generators']) && _react["default"].createElement("p", null, "\u041C\u043E\u0436\u0435\u0442 \u0441\u043C\u043E\u0442\u0440\u0435\u0442\u044C Topics, Levels, Generators")));
};

exports.RightsViewer = RightsViewer;

/***/ }),
/* 4 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.App = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

var _LoginForm = __webpack_require__(17);

var _Welcom = __webpack_require__(18);

var _Editor = __webpack_require__(9);

var _RightsViewer = __webpack_require__(3);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var App =
/*#__PURE__*/
function (_React$Component) {
  _inherits(App, _React$Component);

  function App(props) {
    var _this;

    _classCallCheck(this, App);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(App).call(this, props)); // the initial application state

    _this.state = {
      user: null,
      rights: false
    };
    return _this;
  } // App "actions" (functions that modify state)


  _createClass(App, [{
    key: "signIn",
    value: function signIn(user) {
      // This is where you would call Firebase, an API etc...
      // calling setState will re-render the entire app (efficiently!)
      this.setState({
        user: user
      });
    }
  }, {
    key: "signOut",
    value: function signOut() {
      // clear out user from state
      this.setState({
        user: null
      });
    }
  }, {
    key: "showRights",
    value: function showRights() {
      this.setState({
        rights: true
      });
    }
  }, {
    key: "hideRights",
    value: function hideRights() {
      this.setState({
        rights: false
      });
    }
  }, {
    key: "render",
    value: function render() {
      // Here we pass relevant state to our child components
      // as props. Note that functions are passed using `bind` to
      // make sure we keep our scope to App
      return _react["default"].createElement("div", null, _react["default"].createElement("h1", null, "Quibble level manager"), this.state.user ? _react["default"].createElement("div", null, _react["default"].createElement(_Welcom.Welcome, {
        onSignOut: this.signOut.bind(this),
        rights: this.state.rights,
        user: this.state.user,
        onRights: this.state.rights ? this.hideRights.bind(this) : this.showRights.bind(this)
      }), _react["default"].createElement(_Editor.Editor, {
        user: this.state.user
      })) : _react["default"].createElement(_LoginForm.LoginForm, {
        onSignIn: this.signIn.bind(this)
      }), this.state.rights ? _react["default"].createElement(_RightsViewer.RightsViewer, {
        user: this.state.user
      }) : undefined);
    }
  }]);

  return App;
}(_react["default"].Component);

exports.App = App;

/***/ }),
/* 5 */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(21);


/***/ }),
/* 6 */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(2))(81);

/***/ }),
/* 7 */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(2))(83);

/***/ }),
/* 8 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var _react = _interopRequireDefault(__webpack_require__(0));

var _reactDom = _interopRequireDefault(__webpack_require__(6));

__webpack_require__(7);

var _reactHotLoader = __webpack_require__(5);

var _App = __webpack_require__(4);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function renderApp() {
  // This code starts up the React app when it runs in a browser. It sets up the routing
  // configuration and injects the app into a DOM element.
  _reactDom["default"].render(_react["default"].createElement(_reactHotLoader.AppContainer, null, _react["default"].createElement(_App.App, null)), document.getElementById('react-app'));
}

renderApp(); // Allow Hot Module Replacement

if (false) {
  module.hot.accept(_App.App, function () {
    renderApp();
  });
}

/***/ }),
/* 9 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.Editor = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

var _auth = __webpack_require__(1);

var _CreateTopicForm = __webpack_require__(12);

var _CreateLevelForm = __webpack_require__(11);

var _DeleteTopicForm = __webpack_require__(15);

var _DeleteLevelForm = __webpack_require__(14);

var _RenderTaskForm = __webpack_require__(16);

var _DeleteGeneratorForm = __webpack_require__(13);

var _CreateGeneratorForm = __webpack_require__(10);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

var Editor = function Editor(_ref) {
  var user = _ref.user;
  return _react["default"].createElement("div", null, (0, _auth.isAllowed)(user, ['can_edit_topics']) ? _react["default"].createElement(_CreateTopicForm.CreateTopicForm, null) : undefined, (0, _auth.isAllowed)(user, ['can_edit_levels']) ? _react["default"].createElement(_CreateLevelForm.CreateLevelForm, null) : undefined, (0, _auth.isAllowed)(user, ['can_edit_topics']) ? _react["default"].createElement(_DeleteTopicForm.DeleteTopicForm, null) : undefined, (0, _auth.isAllowed)(user, ['can_edit_levels']) ? _react["default"].createElement(_DeleteLevelForm.DeleteLevelForm, null) : undefined, (0, _auth.isAllowed)(user, ['can_edit_generators']) ? _react["default"].createElement(_DeleteGeneratorForm.DeleteGeneratorForm, null) : undefined, (0, _auth.isAllowed)(user, ['can_edit_generators']) ? _react["default"].createElement(_CreateGeneratorForm.CreateGeneratorForm, null) : undefined, (0, _auth.isAllowed)(user, ['can_render_tasks']) ? _react["default"].createElement(_RenderTaskForm.RenderTaskForm, null) : undefined);
};

exports.Editor = Editor;

/***/ }),
/* 10 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.CreateGeneratorForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var CreateGeneratorForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(CreateGeneratorForm, _React$Component);

  function CreateGeneratorForm(props) {
    var _this;

    _classCallCheck(this, CreateGeneratorForm);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(CreateGeneratorForm).call(this, props));
    _this.state = {
      template: "for (int i = {{from1}}; i < {{to1}}; i += {{iter1}})\\r\\nc++\\r\\n",
      possibleAnswers: '["O(n)", "O(n*log(n)", "O(log(n)"]',
      rightAnswers: "O(1)",
      hints: "[]",
      streak: 1
    };
    _this.handleInputChange = _this.handleInputChange.bind(_assertThisInitialized(_this));
    _this.handleSubmit = _this.handleSubmit.bind(_assertThisInitialized(_this));
    return _this;
  }

  _createClass(CreateGeneratorForm, [{
    key: "handleInputChange",
    value: function handleInputChange(event) {
      var target = event.target;
      var name = target.name;
      var value = event.target.value;
      this.setState(_defineProperty({}, name, value));
    }
  }, {
    key: "handleSubmit",
    value: function handleSubmit(event) {
      alert('Вы создали Generator: ' + this.state.template);
      event.preventDefault();
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSubmit
      }, _react["default"].createElement("h3", null, "\u041F\u0430\u0440\u0430\u043C\u0435\u0442\u0440\u044B \u0441 \u043A\u043E\u0442\u043E\u0440\u044B\u043C\u0438 \u0445\u043E\u0442\u0438\u0442\u0435 \u0441\u043E\u0437\u0434\u0430\u0442\u044C Generator:"), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Template:", _react["default"].createElement("input", {
        name: "template",
        type: "text",
        value: this.state.template,
        onChange: this.handleInputChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Possible Answers:", _react["default"].createElement("input", {
        name: "possibleAnswer",
        type: "text",
        value: this.state.possibleAnswers,
        onChange: this.handleInputChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Right Answer:", _react["default"].createElement("input", {
        name: "rightAnswer",
        type: "text",
        value: this.state.rightAnswers,
        onChange: this.handleInputChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Hints:", _react["default"].createElement("input", {
        name: "hints",
        type: "text",
        value: this.state.hints,
        onChange: this.handleInputChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Streak:", _react["default"].createElement("input", {
        name: "hints",
        type: "number",
        value: this.state.streak,
        onChange: this.handleInputChange
      })), _react["default"].createElement("input", {
        type: "submit",
        value: "Submit"
      }));
    }
  }]);

  return CreateGeneratorForm;
}(_react["default"].Component);

exports.CreateGeneratorForm = CreateGeneratorForm;

/***/ }),
/* 11 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.CreateLevelForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var CreateLevelForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(CreateLevelForm, _React$Component);

  function CreateLevelForm(props) {
    var _this;

    _classCallCheck(this, CreateLevelForm);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(CreateLevelForm).call(this, props));
    _this.state = {
      topic: 'Сложность алгоритмов',
      level: ''
    };
    _this.handleInputChange = _this.handleInputChange.bind(_assertThisInitialized(_this));
    _this.handleSubmit = _this.handleSubmit.bind(_assertThisInitialized(_this));
    return _this;
  }

  _createClass(CreateLevelForm, [{
    key: "handleInputChange",
    value: function handleInputChange(event) {
      var target = event.target;
      var name = target.name;
      var value = event.target.value;
      this.setState(_defineProperty({}, name, value));
    }
  }, {
    key: "handleSubmit",
    value: function handleSubmit(event) {
      alert('Был создан пустой Level: ' + this.state.kevek);
      event.preventDefault();
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSubmit
      }, _react["default"].createElement("h3", null, "\u0414\u043E\u0431\u0430\u0432\u043B\u0435\u043D\u0438\u0435 Level"), _react["default"].createElement("label", null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 Topic, \u0432 \u043A\u043E\u0442\u043E\u0440\u044B\u0439 \u0445\u043E\u0442\u0438\u0442\u0435 \u0434\u043E\u0431\u0430\u0432\u0438\u0442\u044C Level:", _react["default"].createElement("select", {
        name: "topic",
        value: this.state.topic,
        onChange: this.handleInputChange
      }, _react["default"].createElement("option", {
        value: "\u0421\u043B\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0430\u043B\u0433\u043E\u0440\u0438\u0442\u043C\u043E\u0432"
      }, "\u0421\u043B\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0430\u043B\u0433\u043E\u0440\u0438\u0442\u043C\u043E\u0432"))), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "\u0418\u043C\u044F Level, \u043A\u043E\u0442\u043E\u0440\u044B\u0439 \u0445\u043E\u0442\u0438\u0442\u0435 \u0434\u043E\u0431\u0430\u0432\u0438\u0442\u044C:", _react["default"].createElement("textarea", {
        name: "level",
        value: this.state.level,
        onChange: this.handleInputChange
      })), _react["default"].createElement("input", {
        type: "submit",
        value: "Create Level"
      }));
    }
  }]);

  return CreateLevelForm;
}(_react["default"].Component);

exports.CreateLevelForm = CreateLevelForm;

/***/ }),
/* 12 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.CreateTopicForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

// const serverUrl = "https://complexitybot.azurewebsites.net"
// const serverUrl = "http://localhost:5000"
var CreateTopicForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(CreateTopicForm, _React$Component);

  function CreateTopicForm(props) {
    var _this;

    _classCallCheck(this, CreateTopicForm);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(CreateTopicForm).call(this, props));
    _this.state = {
      value: '',
      description: ''
    };
    _this.handleChange = _this.handleChange.bind(_assertThisInitialized(_this));
    _this.handleSubmit = _this.handleSubmit.bind(_assertThisInitialized(_this));
    return _this;
  }

  _createClass(CreateTopicForm, [{
    key: "handleChange",
    value: function handleChange(event) {
      var target = event.target;
      var name = target.name;
      var value = target.value;
      this.setState(_defineProperty({}, name, value));
    }
  }, {
    key: "handleSubmit",
    value: function handleSubmit(event) {
      var _this2 = this;

      fetch("./service/addTopic", {
        method: "post",
        mode: "same-origin",
        headers: {
          "Content-Type": "application/json",
          "Accept": "text/plain"
        },
        body: JSON.stringify({
          "name": this.state.value,
          "description": this.state.description
        })
      })["catch"](function (resp) {
        console.log("error");
      }).then(function () {
        alert('Был создан пустой Topic: ' + _this2.state.value);
        event.preventDefault();
      })["catch"](function (resp) {
        console.log("error");
      });
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSubmit
      }, _react["default"].createElement("h3", null, "\u0414\u043E\u0431\u0430\u0432\u043B\u0435\u043D\u0438\u0435 Topic"), _react["default"].createElement("label", null, "\u0418\u043C\u044F Topic, \u043A\u043E\u0442\u043E\u0440\u044B\u0439 \u0445\u043E\u0442\u0438\u0442\u0435 \u0434\u043E\u0431\u0430\u0432\u0438\u0442\u044C:", _react["default"].createElement("textarea", {
        name: "value",
        value: this.state.value,
        onChange: this.handleChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "\u041E\u043F\u0438\u0441\u0430\u043D\u0438\u0435 \u0437\u0430\u0434\u0430\u043D\u0438\u044F \u0434\u043B\u044F Topic:", _react["default"].createElement("textarea", {
        name: "description",
        value: this.state.description,
        onChange: this.handleChange
      })), _react["default"].createElement("input", {
        type: "submit",
        value: "Create Topic"
      }));
    }
  }]);

  return CreateTopicForm;
}(_react["default"].Component);

exports.CreateTopicForm = CreateTopicForm;

/***/ }),
/* 13 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.DeleteGeneratorForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var DeleteGeneratorForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(DeleteGeneratorForm, _React$Component);

  function DeleteGeneratorForm(props) {
    var _this;

    _classCallCheck(this, DeleteGeneratorForm);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(DeleteGeneratorForm).call(this, props));
    _this.state = {
      topic: 'Cложность алгоритмов',
      level: 'Циклы',
      generator: 'Generator 1'
    };
    _this.handleInputChange = _this.handleInputChange.bind(_assertThisInitialized(_this));
    _this.handleSubmit = _this.handleSubmit.bind(_assertThisInitialized(_this));
    return _this;
  }

  _createClass(DeleteGeneratorForm, [{
    key: "handleInputChange",
    value: function handleInputChange(event) {
      var target = event.target;
      var name = target.name;
      var value = event.target.value;
      this.setState(_defineProperty({}, name, value));
    }
  }, {
    key: "handleSubmit",
    value: function handleSubmit(event) {
      alert('Вы удалили Generator: ' + this.state.generator);
      event.preventDefault();
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSubmit
      }, _react["default"].createElement("h3", null, "\u0423\u0434\u0430\u043B\u0435\u043D\u0438\u0435 Generator"), _react["default"].createElement("label", null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 Topic, \u0438\u0437 \u043A\u043E\u0442\u043E\u0440\u043E\u0433\u043E \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C Generator:", _react["default"].createElement("select", {
        name: "topic",
        value: this.state.topic,
        onChange: this.handleInputChange
      }, _react["default"].createElement("option", {
        value: "\u0421\u043B\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0430\u043B\u0433\u043E\u0440\u0438\u0442\u043C\u043E\u0432"
      }, "\u0421\u043B\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0430\u043B\u0433\u043E\u0440\u0438\u0442\u043C\u043E\u0432"))), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 Level, \u0438\u0437 \u043A\u043E\u0442\u043E\u0440\u043E\u0433\u043E \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C Generator:", _react["default"].createElement("select", {
        name: "level",
        value: this.state.level,
        onChange: this.handleInputChange
      }, _react["default"].createElement("option", {
        value: "\u0426\u0438\u043A\u043B\u044B"
      }, "\u0426\u0438\u043A\u043B\u044B"), _react["default"].createElement("option", {
        value: "\u0414\u0432\u043E\u0439\u043D\u044B\u0435 \u0446\u0438\u043A\u043B\u044B"
      }, "\u0414\u0432\u043E\u0439\u043D\u044B\u0435 \u0446\u0438\u043A\u043B\u044B"))), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 Generator, \u043A\u043E\u0442\u043E\u0440\u044B\u0439 \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C:", _react["default"].createElement("select", {
        name: "generator",
        value: this.state.generator,
        onChange: this.handleInputChange
      }, _react["default"].createElement("option", {
        value: "generator_1"
      }, "Generator 1"))), _react["default"].createElement("input", {
        type: "submit",
        value: "Submit"
      }));
    }
  }]);

  return DeleteGeneratorForm;
}(_react["default"].Component);

exports.DeleteGeneratorForm = DeleteGeneratorForm;

/***/ }),
/* 14 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.DeleteLevelForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var DeleteLevelForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(DeleteLevelForm, _React$Component);

  function DeleteLevelForm(props) {
    var _this;

    _classCallCheck(this, DeleteLevelForm);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(DeleteLevelForm).call(this, props));
    _this.state = {
      topic: 'Cложность алгоритмов',
      level: 'Циклы'
    };
    _this.handleInputChange = _this.handleInputChange.bind(_assertThisInitialized(_this));
    _this.handleSubmit = _this.handleSubmit.bind(_assertThisInitialized(_this));
    return _this;
  }

  _createClass(DeleteLevelForm, [{
    key: "handleInputChange",
    value: function handleInputChange(event) {
      var target = event.target;
      var name = target.name;
      var value = event.target.value;
      this.setState(_defineProperty({}, name, value));
    }
  }, {
    key: "handleSubmit",
    value: function handleSubmit(event) {
      alert('Вы удалили Level: ' + this.state.level);
      event.preventDefault();
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSubmit
      }, _react["default"].createElement("h3", null, "\u0423\u0434\u0430\u043B\u0435\u043D\u0438\u0435 Level"), _react["default"].createElement("label", null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 Topic, \u0438\u0437 \u043A\u043E\u0442\u043E\u0440\u043E\u0433\u043E \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C Level:", _react["default"].createElement("select", {
        name: "topic",
        value: this.state.topic,
        onChange: this.handleInputChange
      }, _react["default"].createElement("option", {
        value: "\u0421\u043B\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0430\u043B\u0433\u043E\u0440\u0438\u0442\u043C\u043E\u0432"
      }, "\u0421\u043B\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0430\u043B\u0433\u043E\u0440\u0438\u0442\u043C\u043E\u0432"))), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 Level, \u043A\u043E\u0442\u043E\u0440\u044B\u0439 \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C:", _react["default"].createElement("select", {
        name: "level",
        value: this.state.level,
        onChange: this.handleInputChange
      }, _react["default"].createElement("option", {
        value: "\u0426\u0438\u043A\u043B\u044B"
      }, "\u0426\u0438\u043A\u043B\u044B"), _react["default"].createElement("option", {
        value: "\u0414\u0432\u043E\u0439\u043D\u044B\u0435 \u0446\u0438\u043A\u043B\u044B"
      }, "\u0414\u0432\u043E\u0439\u043D\u044B\u0435 \u0446\u0438\u043A\u043B\u044B"))), _react["default"].createElement("input", {
        type: "submit",
        value: "Submit"
      }));
    }
  }]);

  return DeleteLevelForm;
}(_react["default"].Component);

exports.DeleteLevelForm = DeleteLevelForm;

/***/ }),
/* 15 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.DeleteTopicForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var DeleteTopicForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(DeleteTopicForm, _React$Component);

  function DeleteTopicForm(props) {
    var _this;

    _classCallCheck(this, DeleteTopicForm);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(DeleteTopicForm).call(this, props));
    _this.state = {
      value: 'Сложность алгоритмов',
      id: '0',
      data: []
    };
    _this.handleChange = _this.handleChange.bind(_assertThisInitialized(_this));
    _this.handleSubmit = _this.handleSubmit.bind(_assertThisInitialized(_this));
    return _this;
  }

  _createClass(DeleteTopicForm, [{
    key: "componentWillMount",
    value: function componentWillMount() {
      var _this2 = this;

      fetch("./service/topics").then(function (response) {
        return response.json();
      }).then(function (d) {
        _this2.setState({
          data: d
        });

        console.log(d);
      })["catch"](function (error) {
        return console.error(error);
      });
    }
  }, {
    key: "handleChange",
    value: function handleChange(event) {
      this.setState({
        id: event.target.value,
        value: event.target.label
      });
    }
  }, {
    key: "handleSubmit",
    value: function handleSubmit(event) {
      var _this3 = this;

      fetch("./service/deleteTopic/" + this.state.id, {
        method: "delete"
      }).then(function () {
        alert('Вы удалили Topic: ' + _this3.state.value + ' с Id: ' + _this3.state.id);
        event.preventDefault();
      });
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSubmit
      }, _react["default"].createElement("h3", null, "\u0423\u0434\u0430\u043B\u0435\u043D\u0438\u0435 Topic"), _react["default"].createElement("label", null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 Topic, \u043A\u043E\u0442\u043E\u0440\u044B\u0439 \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C:", _react["default"].createElement("select", {
        value: this.state.id,
        onChange: this.handleChange
      }, this.state.data.map(function (fbb) {
        return _react["default"].createElement("option", {
          label: fbb.name,
          value: fbb.id,
          name: fbb.name
        }, fbb.name);
      }), ";")), _react["default"].createElement("input", {
        type: "submit",
        value: "Submit"
      }));
    }
  }]);

  return DeleteTopicForm;
}(_react["default"].Component);

exports.DeleteTopicForm = DeleteTopicForm;

/***/ }),
/* 16 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.RenderTaskForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var RenderTaskForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(RenderTaskForm, _React$Component);

  function RenderTaskForm(props) {
    var _this;

    _classCallCheck(this, RenderTaskForm);

    _this = _possibleConstructorReturn(this, _getPrototypeOf(RenderTaskForm).call(this, props));
    _this.state = {
      template: "for (int i = {{from1}}; i < {{to1}}; i += {{iter1}})\\r\\nc++\\r\\n",
      possibleAnswers: '["O(n)", "O(n*log(n)", "O(log(n)"]',
      rightAnswers: "O(1)",
      hints: "[]"
    };
    _this.handleInputChange = _this.handleInputChange.bind(_assertThisInitialized(_this));
    _this.handleSubmit = _this.handleSubmit.bind(_assertThisInitialized(_this));
    return _this;
  }

  _createClass(RenderTaskForm, [{
    key: "handleInputChange",
    value: function handleInputChange(event) {
      var target = event.target;
      var name = target.name;
      var value = event.target.value;
      this.setState(_defineProperty({}, name, value));
    }
  }, {
    key: "handleSubmit",
    value: function handleSubmit(event) {
      alert('Отрендереный генератор: ' + this.state.template);
      event.preventDefault();
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSubmit
      }, _react["default"].createElement("h3", null, "\u041F\u0430\u0440\u0430\u043C\u0435\u0442\u0440\u044B \u0441 \u043A\u043E\u0442\u043E\u0440\u044B\u043C\u0438 \u0445\u043E\u0442\u0438\u0442\u0435 \u043E\u0442\u0440\u0435\u043D\u0434\u0435\u0440\u0438\u0442\u044C Task"), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Template:", _react["default"].createElement("input", {
        name: "template",
        type: "text",
        value: this.state.template,
        onChange: this.handleInputChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Possible Answers:", _react["default"].createElement("input", {
        name: "possibleAnswer",
        type: "text",
        value: this.state.possibleAnswers,
        onChange: this.handleInputChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Right Answer:", _react["default"].createElement("input", {
        name: "rightAnswer",
        type: "text",
        value: this.state.rightAnswers,
        onChange: this.handleInputChange
      })), _react["default"].createElement("br", null), _react["default"].createElement("label", null, "Hints:", _react["default"].createElement("input", {
        name: "hints",
        type: "text",
        value: this.state.hints,
        onChange: this.handleInputChange
      })), _react["default"].createElement("input", {
        type: "submit",
        value: "Submit"
      }));
    }
  }]);

  return RenderTaskForm;
}(_react["default"].Component);

exports.RenderTaskForm = RenderTaskForm;

/***/ }),
/* 17 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.LoginForm = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

var _auth = __webpack_require__(1);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var users = [{
  username: "admin",
  password: "pass",
  roles: ['user', 'admin'],
  rights: ['can_edit_levels', 'can_edit_topics', 'can_edit_generators', 'can_render_tasks', 'can_get_levels_tasks_generators']
}, {
  username: "user",
  password: "pass",
  roles: ['user'],
  rights: ['can_render_tasks', 'can_get_levels_tasks_generators']
}];

var LoginForm =
/*#__PURE__*/
function (_React$Component) {
  _inherits(LoginForm, _React$Component);

  function LoginForm() {
    _classCallCheck(this, LoginForm);

    return _possibleConstructorReturn(this, _getPrototypeOf(LoginForm).apply(this, arguments));
  }

  _createClass(LoginForm, [{
    key: "handleSignIn",
    // Using a class based component here because we're accessing DOM refs
    value: function handleSignIn(e) {
      e.preventDefault();
      var username = this.refs.username.value;
      var password = this.refs.password.value;
      var user = LoginForm.validateUser(username, password);
      if (user) this.props.onSignIn(user);
    }
  }, {
    key: "render",
    value: function render() {
      return _react["default"].createElement("form", {
        onSubmit: this.handleSignIn.bind(this)
      }, _react["default"].createElement("h3", null, "Sign in"), _react["default"].createElement("input", {
        type: "text",
        ref: "username",
        placeholder: "enter you username"
      }), _react["default"].createElement("input", {
        type: "password",
        ref: "password",
        placeholder: "enter password"
      }), _react["default"].createElement("input", {
        type: "submit",
        value: "Login"
      }));
    }
  }], [{
    key: "validateUser",
    value: function validateUser(username, password) {
      // ToDo validate user on backend
      for (var _i = 0, _users = users; _i < _users.length; _i++) {
        var user = _users[_i];

        if (username === user.username && password === user.password) {
          return user;
        }
      }
    }
  }]);

  return LoginForm;
}(_react["default"].Component);

exports.LoginForm = LoginForm;

/***/ }),
/* 18 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.Welcome = void 0;

var _react = _interopRequireDefault(__webpack_require__(0));

var _RightsViewer = __webpack_require__(3);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

var Welcome = function Welcome(_ref) {
  var user = _ref.user,
      rights = _ref.rights,
      onRights = _ref.onRights,
      onSignOut = _ref.onSignOut;
  // This is a dumb "stateless" component
  return _react["default"].createElement("div", null, "Welcome ", _react["default"].createElement("strong", null, user.username), "!", _react["default"].createElement("br", null), _react["default"].createElement("a", {
    href: "javascript:",
    onClick: onRights
  }, rights ? "Скрыть права пользователя" : "Посмотреть права пользователя"), _react["default"].createElement("br", null), _react["default"].createElement("a", {
    href: "javascript:",
    onClick: onSignOut
  }, "Sign out"));
};

exports.Welcome = Welcome;

/***/ }),
/* 19 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* eslint-disable global-require */


if (true) {
  module.exports = __webpack_require__(20);
} else {
  module.exports = require('./AppContainer.dev');
}

/***/ }),
/* 20 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* eslint-disable react/prop-types */


var _createClass = function () {
  function defineProperties(target, props) {
    for (var i = 0; i < props.length; i++) {
      var descriptor = props[i];
      descriptor.enumerable = descriptor.enumerable || false;
      descriptor.configurable = true;
      if ("value" in descriptor) descriptor.writable = true;
      Object.defineProperty(target, descriptor.key, descriptor);
    }
  }

  return function (Constructor, protoProps, staticProps) {
    if (protoProps) defineProperties(Constructor.prototype, protoProps);
    if (staticProps) defineProperties(Constructor, staticProps);
    return Constructor;
  };
}();

function _classCallCheck(instance, Constructor) {
  if (!(instance instanceof Constructor)) {
    throw new TypeError("Cannot call a class as a function");
  }
}

function _possibleConstructorReturn(self, call) {
  if (!self) {
    throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
  }

  return call && (typeof call === "object" || typeof call === "function") ? call : self;
}

function _inherits(subClass, superClass) {
  if (typeof superClass !== "function" && superClass !== null) {
    throw new TypeError("Super expression must either be null or a function, not " + typeof superClass);
  }

  subClass.prototype = Object.create(superClass && superClass.prototype, {
    constructor: {
      value: subClass,
      enumerable: false,
      writable: true,
      configurable: true
    }
  });
  if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass;
}

var React = __webpack_require__(0);

var Component = React.Component;

var AppContainer = function (_Component) {
  _inherits(AppContainer, _Component);

  function AppContainer() {
    _classCallCheck(this, AppContainer);

    return _possibleConstructorReturn(this, (AppContainer.__proto__ || Object.getPrototypeOf(AppContainer)).apply(this, arguments));
  }

  _createClass(AppContainer, [{
    key: 'render',
    value: function render() {
      if (this.props.component) {
        return React.createElement(this.props.component, this.props.props);
      }

      return React.Children.only(this.props.children);
    }
  }]);

  return AppContainer;
}(Component);

module.exports = AppContainer;

/***/ }),
/* 21 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* eslint-disable global-require */



if (true) {
  module.exports = __webpack_require__(22);
} else {
  module.exports = require('./index.dev');
}

/***/ }),
/* 22 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


module.exports.AppContainer = __webpack_require__(19);

/***/ })
/******/ ]);
//# sourceMappingURL=main.js.map