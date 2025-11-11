import "./LoginStyle.css"
import logo from "../assets/headerLogo.png"
function Login() {
    function handleEmailInput() { }
    function handlePasswordInput() { }
    function handleLogin() {

    }
    return (
        < div className="registrationForm" >
            <img src={logo} alt="Logo" />
            <h1>С возвращением!</h1>
            <p>Авторизуйтесь, чтобы получить все возможности сервиса</p>

            <form >
                <input onChange={handleEmailInput} id="emailInput" type="email" placeholder="Email" /><br />
                <input onChange={handlePasswordInput} id="passwordInput" type="password" placeholder="Password" required /><br />
                <input onClick={handleLogin} id="submitInput" type="submit" value="Войти" />
            </form>
        </div >
    )
}

export default Login


// {
// 	"email":"hello@gmail.com",
// 	"password":"hello123"
// }