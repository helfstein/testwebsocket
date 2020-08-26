using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TestWebSocket.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppWebSocket.ViewModels {
    public class AboutViewModel : BaseViewModel {

        private bool _isLightOn;

        public string Endpoint { get; set; }
        public ICommand LightControlCommand { get; set; }
        public ICommand OpenWebCommand { get; }

        private bool IsConnected => client.State == WebSocketState.Open;

        ClientWebSocket client ;
        CancellationTokenSource cts;
        public bool IsLightOn { 
            get => _isLightOn; 
            set => SetProperty(ref _isLightOn, value); 
        }

        public AboutViewModel() {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
            LightControlCommand = new Command(OnLightControlCommand);
            client = new ClientWebSocket();
            cts = new CancellationTokenSource();
            ConnectToServerAsync();
        }

        async void ConnectToServerAsync() {
            try {
                await client.ConnectAsync(new Uri("ws://192.168.15.14:8000/ws"), cts.Token);

                await Task.Factory.StartNew(async () => {
                    while (true) {
                        WebSocketReceiveResult result;
                        var message = new ArraySegment<byte>(new byte[4096]);
                        do {
                            result = await client.ReceiveAsync(message, cts.Token);
                            var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                            string serialisedMessae = Encoding.UTF8.GetString(messageBytes);

                            try {
                                var lampMessage = JsonConvert.DeserializeObject<LampadaMessageModel>(serialisedMessae);
                                try {
                                    if (lampMessage.State == State.Off) {
                                        // Turn Off
                                        IsLightOn = false;
                                        await Flashlight.TurnOffAsync();
                                        //SendMessageAsync("off");
                                    }
                                    else {
                                        // Turn On
                                        IsLightOn = true;
                                        await Flashlight.TurnOnAsync();
                                        //SendMessageAsync("on");
                                    }

                                }
                                catch (FeatureNotSupportedException fnsEx) {
                                    // Handle not supported on device exception
                                    Console.WriteLine(fnsEx);
                                }
                                catch (PermissionException pEx) {
                                    // Handle permission exception
                                    Console.WriteLine(pEx);
                                }
                                catch (Exception ex) {
                                    // Unable to turn on/off flashlight
                                    Console.WriteLine(ex);
                                }
                            }
                            catch (Exception ex) {
                                Console.WriteLine($"Invalide message format. {ex.Message}");
                            }

                        } while (!result.EndOfMessage);
                    }
                }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
            

            
        }

        async void SendMessageAsync(string message) {
            
            var byteMessage = Encoding.UTF8.GetBytes(message);
            var segmnet = new ArraySegment<byte>(byteMessage);
            try {
                await client.SendAsync(segmnet, WebSocketMessageType.Text, true, cts.Token);
            }
            catch (Exception ex) {

                Console.WriteLine(ex);
            }
            
            //MessageText = string.Empty;
        }

        private async void OnLightControlCommand() {
            try {
                if (IsLightOn) {
                    // Turn Off
                    IsLightOn = false;
                    await Flashlight.TurnOffAsync();
                    SendMessageAsync("off");
                }
                else {
                    // Turn On
                    IsLightOn = true;
                    await Flashlight.TurnOnAsync();
                    SendMessageAsync("on");
                }
                
            }
            catch (FeatureNotSupportedException fnsEx) {
                // Handle not supported on device exception
                Console.WriteLine(fnsEx);
            }
            catch (PermissionException pEx) {
                // Handle permission exception
                Console.WriteLine(pEx);
            }
            catch (Exception ex) {
                // Unable to turn on/off flashlight
                Console.WriteLine(ex);
            }
            //throw new NotImplementedException();
        }


    }
}