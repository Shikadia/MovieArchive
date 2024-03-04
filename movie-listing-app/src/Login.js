import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from 'react-toastify';

const Login = () =>
{

    const [email, emailupdate] = useState('');
    const [password, passwordupdate] = useState('');

    const navigate = useNavigate();

    const ProceedLoginusingAPI = (e) =>
    {
        e.preventDefault();

        let login = {
            "email": email,
            "password": password
        };
        console.log(login);



        fetch("https://localhost:7181/api/Auth/login", {
            method: 'POST',
            headers: { 'content-type': 'application/json' },
            body: JSON.stringify(login)
        }).then((res) =>
        {
            return res.json();
        }).then((resp) =>
        {
            console.log(resp);
            toast.success('successful ' + resp.message);
            sessionStorage.setItem('id', resp.data.id);
            sessionStorage.setItem('jwttoken', resp.data.token)
            sessionStorage.setItem('classId', resp.data.classId);

            if (resp.data.roles === 0)
            {
                navigate('/admin')
            } else if (resp.data.roles === 1)
            {
                navigate('/teacher')
            } else if (resp.data.roles === 2)
            {
                navigate('/student')
            }

        })
            .catch((res) =>
            {
                toast.error('Registration Failed: ' + res.error);
            });
    }
    return (
        <div>

            <div className="login-form-container">
                <form onSubmit={ProceedLoginusingAPI} className="login-form" >
                    <label for="email" className="input-label">Email</label>
                    <input required className="inpute" value={email} onChange={e => emailupdate(e.target.value)} type="email" name="email" id="email" placeholder="Email"></input>
                    <label for="password" className="input-label">Password</label>
                    <input required className="inpute" value={password} onChange={e => passwordupdate(e.target.value)} name="password" id="email" placeholder="Password" ></input>
                    <button className="button" type="submit">Login</button>
                    <button className="login-home-button"><Link to={'/'} style={{ textDecoration: 'none' }}><h3 className="login-home-text">Home</h3></Link></button>
                </form>
            </div>

        </div>
    );
}

export default Login;