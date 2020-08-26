using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestWebSocket.Models;

namespace TestWebSocket {
    public class SocketServiceHandler : WebSocketHandler {
        public SocketServiceHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager) {
        }

        public override async Task OnConnected(WebSocket socket) {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);           
            await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer) {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var incMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var message = JsonConvert.SerializeObject(new LampadaMessageModel {
                Sender = socketId,
                State = incMessage.ToLower() == "on" ? State.On : State.Off
            });
            await SendMessageToAllAsync(message);
        }

        public override Task OnDisconnected(WebSocket socket) {
            return base.OnDisconnected(socket);
        }
    }
}

