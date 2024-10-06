import React from 'react';
import './App.css';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom'
import HeaderComponent from './components/HeaderComponent';
import FooterComponent from './components/FooterComponent';
import ListUserComponent from './components/ListUserComponent';
import CreateUserComponent from './components/CreateUserComponent';
import ViewUserComponent from './components/ViewUserComponent';
import ListShowComponent from './components/ListShowComponent';
import CreateShowComponent from './components/CreateShowComponent';
import ViewShowComponent from './components/ViewShowComponent';

function App() {
  return (
    <div>
        <Router>
              <HeaderComponent />
                <div className="container">
                    <Switch> 
                          <Route path = "/" exact component = {ListShowComponent}></Route>
                          <Route path = "/shows" component = {ListShowComponent}></Route>
                          <Route path = "/add-show/:id" component = {CreateShowComponent}></Route>
                          <Route path = "/view-show/:id" component = {ViewShowComponent}></Route>
                          <Route path = "/users" component = {ListUserComponent}></Route>
                          <Route path = "/add-user/:id" component = {CreateUserComponent}></Route>
                          <Route path = "/view-user/:id" component = {ViewUserComponent}></Route>
                    </Switch>
                </div>
        </Router>
    </div>
    
  );
}

export default App;
