async function Test() {
    let response = null;
    response = await fetch("/api/room?slots=5", { method: "POST" });
    var code = await response.json();
    console.log(`Code: ${code}`);
    var websocket = new WebSocket(`ws://localhost:5032/api/room/join?code=${code}`);
    websocket.onmessage = ev => {
        console.log(`Message: ${ev.data}`);
    };
    websocket.onopen = ev => {
        setInterval(() => websocket.send("Generic message"), 2000);
    };
}

Test();