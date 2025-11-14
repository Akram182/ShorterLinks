import { useState } from "react";
import axios from "axios";
import "./LoginStyle.css"
// import logo from "../assets/headerLogo.png"


function Login(props) {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    function handleEmailInput(e) {
        setEmail(e.target.value);
    }
    function handlePasswordInput(e) {
        setPassword(e.target.value);
    }


    function handleLogin(event) {
        event.preventDefault();
        console.log(`${email} + ${password}`);
        loginResponse();
    }
    function handleRegistration() {
        props.registerFormVisibility(true)
    }
    async function loginResponse() {
        try {
            const response = await axios.post("https://localhost:7132/api/Auth/login", {
                "email": email,
                "password": password
            });
            if (response.data) {
                props.tokenVal(response.data.token);
                props.logined(true)
                console.log(response.data.token);
            }
        }
        catch (error) {
            console.log(error);
        }


    }
    return (
        < div className="registrationForm" >
            <h1>Welcome back!</h1>
            <p>Log in to get all the features of the service.</p>

            <form >
                <input onChange={handleEmailInput} id="emailInput" type="email" placeholder="Email" value={email} /><br />
                <input onChange={handlePasswordInput} id="passwordInput" type="password" placeholder="Password" required value={password} /><br />
                <input onClick={handleLogin} id="submitInput" type="submit" value="Войти" />
                <button onClick={handleRegistration}id="loginBtn">Зарегистрироваться</button>
            </form>
        </div >
    )
}

export default Login


// {
// 	"email":"hello@gmail.com",
// 	"password":"hello123"
// }