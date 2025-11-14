import Header from "./HeaderComponent/Header"
import Body from "./BodyComponent/Body"
import Registration from "./RegistrationComponent/Registration.jsx"
import Login from "./LoginComponent/Login"

import { useState } from "react"


function App() {
    const [token, setToken] = useState("");
    const [isRegistered, setRegist] = useState(false);
    const [isLogined, setLogin] = useState(false);

    return (
        <>
            {(!isLogined && isRegistered) && <Login registerFormVisibility={setRegist} logined={setLogin} tokenVal={setToken}></Login>}


            {!isRegistered && <Registration registred={setRegist} />}

            {isLogined && <Header></Header>}
            {isLogined && <Body tokenVal={token}></Body>}

            {/* <Login registerFormVisibility={setRegist} logined={setLogin} tokenVal={setToken}></Login> */}


            {/* <Header></Header>    
                    <Body></Body> */}
        </>)
}

export default App



{/* {!isRegistered && <Registration logined={setLogin} registerFormVisibility={setRegist} />} */ }


































// import { useState } from 'react'
// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
// import './App.css'

// function App() {
//   const [count, setCount] = useState(0)

//   return (
//     <>
//       <div>
//         <a href="https://vite.dev" target="_blank">
//           <img src={viteLogo} className="logo" alt="Vite logo" />
//         </a>
//         <a href="https://react.dev" target="_blank">
//           <img src={reactLogo} className="logo react" alt="React logo" />
//         </a>
//       </div>
//       <h1>Vite + React</h1>
//       <div className="card">
//         <button onClick={() => setCount((count) => count + 1)}>
//           count is {count}
//         </button>
//         <p>
//           Edit <code>src/App.jsx</code> and save to test HMR
//         </p>
//       </div>
//       <p className="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

// export default App
