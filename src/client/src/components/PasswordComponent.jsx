import * as React from 'react';
import '../css/PasswordComponent.css';
import { Delete,Update } from '../api/api';
import Form from '../components/Form';


export default function PasswordComponent (props) {
  const [id, setId] = React.useState(props.id);
  const [showpassword, setShowPassword] = React.useState(false);
  const deleteCallback = () => {
    Delete(id)
    .then((res) => {
      if (res.status === 200) {
        props.UpdateList();
      }
    });
  }
  return (
    <tr>
      <td>{props.name}</td>
      <td style={{cursor: 'pointer'}} onClick={() => setShowPassword(!showpassword)} >{showpassword ? props.value : '********'}</td>
      <td>{`${props.time.getHours()}:${props.time.getMinutes()}:${props.time.getSeconds()} ${props.time.getDate()}/${props.time.getMonth()}/${props.time.getFullYear()}`}</td>
      <td>{props.type}</td>
      <td>
        <div onClick={deleteCallback} className="delete-btn btn">Delete</div>
      </td>
      <td>
        <div onClick={() => props.Update(id)} className="edit-btn btn">Edit</div>
      </td>
    </tr>
  );
}
