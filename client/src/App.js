import React from 'react';
import './App.css';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom'
import HeaderComponent from './components/HeaderComponent';
import ListShowComponent from './components/ListShowComponent';
import CreateShowComponent from './components/CreateShowComponent';

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
                    </Switch>
                </div>
        </Router>
    </div>
    
  );
}

export default App;
