var socket;

var scheme = document.location.protocol === "https:" ? "wss" : "ws";
var port = document.location.port ? (":" + document.location.port) : "";

var socketUrl = scheme + "://" + document.location.hostname + port + "/ws";

console.log(socketUrl);


function onopen(event) {
    console.log("onopen");
};

function onclose(event) {
    console.log("onclose");
};

function onerror(event) {
    console.log("onerror");
};

function onmessage(event) {
    console.log("onmessage");
    updateState(event.data);
    
};
socket = new WebSocket(socketUrl);
socket.onopen = onopen;
socket.onclose = onclose;
socket.onerror = onerror;
socket.onmessage = onmessage;

function updateState(message) {
    var btn = document.getElementById("btnInterruptor");
    var lampada = document.getElementById("lampada");
    if (message.indexOf("State") < 0) {
        return;
    }
    var oMessage = JSON.parse(message);
    console.log(oMessage);


    if (oMessage.State == "1") {//Ligada
        btn.innerHTML = "Desligar";
        lampada.classList.add("ligada");
    }
    else {//Desligada
        btn.innerHTML = "Ligar";
        lampada.classList.remove("ligada");
    }
}
function testar() {
    var btn = document.getElementById("btnInterruptor");
    if (btn.innerHTML == "Ligar") {
        socket.send("on");
        updateState("on")
    }
    else {
        socket.send("off");
        updateState("off")
    }
    
}




