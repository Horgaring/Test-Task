import * as React from 'react';
import '../css/form.css';
import { Create, Delete,GetAll,Update } from '../api/api';


export default function Form (props) {
  const [id, setId] = React.useState(props.id);
  const [name, setName] = React.useState();
  const [value, setValue] = React.useState();
  const [type, setType] = React.useState();

  const submitCallback = () => {
    Array.prototype.map.call(document.getElementsByClassName('validation'), (element) => {
      element.innerHTML = '';
    })
    let params = {name, value, type};
    if (id) {
      params.id = id;
    }
    props.Submit(params)
    .then((res) => {
      if (res.status === 201
        || res.status === 200
      ) {
        Array.prototype.map.call(document.getElementsByClassName('modal'), (element) => {
          element.classList.remove('visible');
        });
        props.UpdateList();
        console.log(props.UpdateList);
      }
      console.log(res.data);
    })
    .catch((err) => {
      if(err.response.data.errors) {
        err.response.data.errors.forEach(error => {
          Array.prototype.map.call(document.getElementsByName(error.propertyName.toLowerCase()), (element) => {
            element.innerHTML = error.message
          })
        })
      }
    })
  }
  const cancelCallback = () => {
    Array.prototype.map.call(document.getElementsByClassName('modal'), (element) => {
      element.classList.remove('visible');
    })
  }
  return (
    <div class="modal">
      <div class="modal-con">
        <form>
          <div class="form-group">
            <label>Name</label>
            <span name='name' className='validation'></span>
            <input
              type="text"
              class="form-control"
              id="name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </div>
          <div class="form-group">
            <label>Value</label>
            <span name='value' className='validation'></span>
            <input
              type="text"
              class="form-control"
              id="value"
              value={value}
              onChange={(e) => setValue(e.target.value)}
            />
          </div>
          <div class="form-group">
            <label>Type</label>
            <select
              class="form-control"
              id="type"
              value={type}
              onChange={(e) => setType(e.target.value)}
            >
              <option value="0">Email</option>
              <option value="1">Web</option>
            </select>
          </div>
          <div class="btn-con">
            <div
              class="btn add-btn"
              onClick={submitCallback}>
              Submit
            </div>
            <div class="btn delete-btn" onClick={cancelCallback}>
              Cancel
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}
