import "../HeaderComponent/headerStyle.css"
import loginLogo from "../assets/loginIcon.png"

function Header() {

    return (
        <div className="headerConatainer">
            <h1>Linkly</h1>
            <button id="Login">Login <img src={loginLogo} alt="loginIcon" /> </button>
            <button id="signUp">Regiseter now</button>
        </div>
    )

}

export default Header