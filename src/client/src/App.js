import logo from './logo.svg';
import './css/App.css';
import PasswordComponent from './components/PasswordComponent';
import React from 'react';
import {Create, Delete, GetAll, Update} from './api/api';
import Form from './components/Form';
import PasswordTableComponent from './components/PasswordTableComponent';


function App() {
  
  const [passwords, setPasswords] = React.useState([]);
  const [search, setSearch] = React.useState('');
  const [type, setType] = React.useState('');
  const UpdateList = () => {
    GetAll(search,type)
        .then((res) => {
          setPasswords(e =>e = res.data);
        })
  }
  React.useEffect(() => {
    UpdateList();
  }, [search,type]);
  return (
    <>
      <div className="password-con">
        <Form UpdateList={UpdateList} Submit={Create}></Form>
        <div>
          <div className="search-con">
            <input className="search" onChange={e => setSearch(e.target.value)} type="text" />
            <select className="search" onChange={e => setType(e.target.value)} type="text">
              <option value="">All</option>
              <option value="0">Email</option>
              <option value="1">Web</option>
            </select>
            <div onClick={() => 
            {
              document.getElementsByClassName('modal')[0].classList.add('visible')
            }} className="btn add-btn">Add</div>
          </div>
          <PasswordTableComponent UpdateList={UpdateList} passwords={passwords}></PasswordTableComponent>
        </div>
      </div>
    </>
  );
}
export default App;
