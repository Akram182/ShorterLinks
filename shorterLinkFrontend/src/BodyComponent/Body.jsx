import axios from "axios"
import "./bodyStlye.css"
import { useState } from "react"
import linkIcon from "../assets/linkIcon.png"
import copyIcon from "../assets/copyIcon.png"

import fakeQr from "../assets/Qr.svg"

function Body(props) {
    const [originalUrl, setOriginalUrl] = useState("asd");
    const [shortLink, setshortLink] = useState("https://localhost/asdas/asdas");

    function handleOriginalLinkChange(e) {
        setOriginalUrl(e.target.value);
    }

    function handleCopyButton() {
        navigator.clipboard.writeText(shortLink).then(function () {
            alert('Текст скопирован!');
        }).catch(function (err) {
            console.error('Ошибка копирования: ', err);
        });
    }

    function handleShortButton() {
        shortLinkResponse();
    }

    async function shortLinkResponse() {
        try {
            const response = await axios.post("https://localhost:7132/api/Links", {
                "OriginalUrl": originalUrl
            }, {
                headers: {
                    'Authorization': `Bearer ${props.tokenVal}`
                }
            });
            console.log(response.data);
            setshortLink(response.data.shortUrl);
        }
        catch (error) {
            alert(error);
        }

    }
    return (
        <>
            <div className="bodyContainer">
                <h1>Shorten Your Loooong Links :&#41;</h1>
                <div className="linkInputContainer">
                    <input onChange={handleOriginalLinkChange} type="text" placeholder="Enter the link here" />
                    <button onClick={handleShortButton}>Shorter Now</button>
                </div>

                <div className="linksCardContainer">
                    <div className="linksStats">
                        <h5>Short Links</h5>
                        <h5>Original Link</h5>
                        <h5>QR Code</h5>
                        <h5>Clicks</h5>
                        <h5>Date</h5>

                    </div>
                    <div className="linkCard">
                        <a href={shortLink} target="blank">{shortLink}</a>
                        <img src={copyIcon} alt="copuIOcon" onClick={handleCopyButton} />
                        {/* <button onClick={handleCopyButton}>Копировать</button> */}
                        <a href="#" target="blank">https://sky.pro/wiki/html/knopka-vnutri-input-html-css-vizualnoe-razmeschenie/</a>
                        <img id="qrCode" src={fakeQr} alt="Qr code" />
                        <h4>1354</h4>
                        <h4>13.05.2025</h4>
                    </div>
                     <div className="linkCard">
                        <a href={shortLink} target="blank">{shortLink}</a>
                        <img src={copyIcon} alt="copuIOcon" onClick={handleCopyButton} />
                        {/* <button onClick={handleCopyButton}>Копировать</button> */}
                        <a href="#" target="blank">https://sky.pro/wiki/html/knopka-vnutri-input-html-css-vizualnoe-razmeschenie/</a>
                        <img id="qrCode" src={fakeQr} alt="Qr code" />
                        <h4>1354</h4>
                        <h4>13.05.2025</h4>
                    </div> <div className="linkCard">
                        <a href={shortLink} target="blank">{shortLink}</a>
                        <img src={copyIcon} alt="copuIOcon" onClick={handleCopyButton} />
                        {/* <button onClick={handleCopyButton}>Копировать</button> */}
                        <a href="#" target="blank">https://sky.pro/wiki/html/knopka-vnutri-input-html-css-vizualnoe-razmeschenie/</a>
                        <img id="qrCode" src={fakeQr} alt="Qr code" />
                        <h4>1354</h4>
                        <h4>13.05.2025</h4>
                    </div>
                </div>
            </div>
            {/* Если не было короткой ссылки */}
            {/* {shortLink &&
                <div className="linksContainer">
                    <div className="linksStats">
                        <h5>Short Links</h5>
                    </div>
                    <div className="linkCard">
                        <a href={shortLink} target="blank"> <h2>{shortLink}</h2></a>
                        <button onClick={handleCopyButton}>Копировать</button>
                        <img id="qrCode" src={fakeQr} alt="Qr code" />
                    </div>
                </div>
            } */}

        </>
    )
}

export default Body