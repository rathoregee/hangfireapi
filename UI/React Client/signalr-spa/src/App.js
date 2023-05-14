import logo from './logo.svg';
import './App.css';
import { useState, useEffect } from "react";
import Chat from './Chat/Chat';
function App() {
  const [data, setData] = useState('no data');
  useEffect(() => {
    // declare the async data fetching function
    const fetchData = async () => {
    // get the data from the api
    const response = await fetch('https://localhost:44306/healthcheck');
    
    // convert the data to json
    const json = await response.json();
      // set state with the result
      setData(json);
    }

    // call the function
    fetchData()
      // make sure to catch any error
      .catch(console.error);;
   
  }, []); // <- add empty brackets here

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <h1>API Cors Test status : {data}</h1>
        <div style={{ margin: '0 30%' }}>
          <Chat />
        </div>
      </header>    
    </div>
  );
}

export default App;
