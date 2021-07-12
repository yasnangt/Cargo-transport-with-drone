using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using WindowsFormsApp3;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    class UDPSocket
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 8889);
        private AsyncCallback recv = null;


        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Server(string address, int port)
        {
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            //Receivee();
        }

        public void Client(string address, int port)
        {
            _socket.Connect(IPAddress.Parse(address), port);
            //Receivee();


        }

        public void Send(string text)
        {

            //byte[] data = Encoding.ASCII.GetBytes(text);
            //_socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            //{
            //    State so = (State)ar.AsyncState;
            //    int bytes = _socket.EndSend(ar);                
            //}, state);            

            byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.Send(data);
        }
        
        public void Receivee()
        {
            //_socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            //{
            //    State so = (State)ar.AsyncState;
            //    int bytes = _socket.EndReceiveFrom(ar, ref epFrom);                           
            //    _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);                            
            //}, state);

            //string s1 = Encoding.ASCII.GetString(state.buffer, 0, bufSize);
            //return s1;


            //byte[] receivedData= new byte[256];
            //_socket.Receive(receivedData);
            //string ret = Encoding.ASCII.GetString(receivedData);
	        //string ret2= ret + " " + "okeyy";
            //return  ret2;
        }

        public void Disconnect()
        {
            _socket.Disconnect(true);
        }        
        
    }
}
    
