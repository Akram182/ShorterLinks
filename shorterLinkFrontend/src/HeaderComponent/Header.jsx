import logo from "../assets/headerLogo.png";
import "./headerStyle.css"
function Header() {

    return (
        <div className="headerConatainer">
            <nav>
                <img src={logo} alt="Logo" />
                <h1>LinkShorter</h1>
                <button id="signUp">Зарегистрироваться</button>
                <button id="signIn">Войти</button>
                {/*TODO: При входе заменить на фото */}
                {/* <img src="" alt="AvatarPlaceholder" /> */}
            </nav>

        </div>
    )

}

export default Header