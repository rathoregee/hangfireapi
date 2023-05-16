import logo from './logo.svg';
import './App.css';
import { useState, useEffect } from "react";
import Chat from './Chat/Chat';
import ThemeProvider from 'react-bootstrap/ThemeProvider';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import ProgressBar from 'react-bootstrap/ProgressBar';

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
      <ThemeProvider prefixes={{ btn: 'my-btn' }}>
            <Navbar  bg="primary" variant="dark" expand="lg">
            <Container>
              <Navbar.Brand href="#home">React-Bootstrap</Navbar.Brand>
              <Navbar.Toggle aria-controls="basic-navbar-nav" />
              <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto">
                  <Nav.Link href="#home">Home</Nav.Link>
                  <Nav.Link href="#link">Link</Nav.Link>
                  <NavDropdown title="Dropdown" id="basic-nav-dropdown">
                    <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                    <NavDropdown.Item href="#action/3.2">
                      Another action
                    </NavDropdown.Item>
                    <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item href="#action/3.4">
                      Separated link
                    </NavDropdown.Item>
                  </NavDropdown>
                </Nav>
              </Navbar.Collapse>
            </Container>
          </Navbar>
      
          <Container>
          <Row>
            <Col xs={2}>  <img src={logo} className="App-logo" alt="logo" /></Col>            
            <Col xs={10}> <Chat /></Col>
          </Row>
          <Row>
            <Col>    <ProgressBar now={60} />;</Col>
          </Row> 
          <Row>
            <Col>
              <b>API Cors Test status : {data}</b>
            </Col>
          </Row>         
        </Container>
      </ThemeProvider>

      <header className="App-header">
      </header>
    </div>
  );
}

export default App;
