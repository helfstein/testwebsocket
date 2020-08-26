using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSocket.Models {
    public class LampadaMessageModel {

        public string Sender { get; set; }
        public State State { get; set; }


    }

    public enum State {
        Off,
        On
    }
}
