import * as React from 'react';
import '../css/PasswordComponent.css';
import {Update } from '../api/api';
import PasswordComponent from './PasswordComponent';
import Form from './Form';


export default function PasswordTableComponent (props) {
  const [selectedId, setId] = React.useState();
  const UpdateForm = (id) => {
    setId(id)
    document.querySelector('#password-table > .modal').classList.add('visible')
  }
  return (
    <div id='password-table'>
    <Form UpdateList={props.UpdateList} Submit={(request) => {request.id = selectedId; return Update(request)}}></Form>
    <table className="password-table">
      
            <tr>
              <th>Name</th>
              <th>Value</th>
              <th>Time</th>
              <th>Password Type</th>
              <th>Delete</th>
              <th>Edit</th>
            </tr>
            {props.passwords.map((password) => (
              <PasswordComponent
                key={password.id}
                id={password.id}
                name={password.name}
                value={password.value}
                time={new Date(password.createdAt)}
                type={password.type}
                UpdateList={props.UpdateList}
                Update={UpdateForm}
              />
            ))}
          </table>
          </div>
  );
}
