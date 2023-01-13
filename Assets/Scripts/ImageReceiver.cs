using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System;
using UnityEngine;

public class ImageReceiver : MonoBehaviour
{
    //TCP Port 開啟
    Thread receiveThread;
    TcpClient client;
    TcpListener listener; 
    int port;
    // Start is called before the first frame update
    void Start()
    {
        InitTcp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitTcp(){
        port = 25001;
        print("TCP Initialized");
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
        listener = new TcpListener(anyIP);
        listener.Start();
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void OnDestroy() {
        receiveThread.Abort();
    }

    private void ReceiveData(){
        print("received somthing...");

        try{
            while(true){
                client = listener.AcceptTcpClient();
                NetworkStream stream = new NetworkStream(client.Client);
                StreamReader sr = new StreamReader(stream);
                print(sr.ReadToEnd());
            }
        }
        catch(Exception e){
            print(e);
        }
    }
}
