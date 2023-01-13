using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;

public class MyListener : MonoBehaviour
{
    bool isConnect = false;
    public GameObject Camera0;
    public GameObject Camera1;

    List<List<float>> total = new List<List<float>>();

    Thread thread;
    public int connectionPort = 25001;
    TcpListener server;
    TcpClient client;
    bool running;

    void Start()
    {
        // Receive on a separate thread so Unity doesn't freeze waiting for data
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();
    }

    void GetData()
    {
        // Create the server
        server = new TcpListener(IPAddress.Parse("127.0.0.1"), connectionPort);
        server.Start();

        // Create a client to get the data stream
        client = server.AcceptTcpClient();

        // Start listening
        running = true;
        while (running)
        {
            Connection();
        }
        server.Stop();
    }

    void Connection()
    {
        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        // Decode the bytes into a string
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        
        // Make sure we're not getting an empty string
        //dataReceived.Trim();
        if (dataReceived != null && dataReceived != "")
        {
            // Convert the received string of data to the format we are using
            ParseData(dataReceived); 
            isConnect = true;            
            //position = ParseData(dataReceived);
            nwStream.Write(buffer, 0, bytesRead);
        }
    }

    // Use-case specific function, need to re-write this to interpret whatever data is being sent
    public void ParseData(string dataString)
    {
        Debug.Log(dataString);

        char[] separators = { '(' };
        char[] separators2 = { ']','['};
        string[] strValues = dataString.Split(separators);

        int i = 2;
        while(i<=8){
            string[] strValues2 = strValues[i].Split(separators2);
            List<float> a  = new List<float>();
            float x = float.Parse(strValues2[2]);
            a.Add(x);
            float y = float.Parse(strValues2[4]);
            a.Add(y);
            float z = float.Parse(strValues2[6]);
            a.Add(z);
            total.Add(a);
            i+=2;
        }

        // Remove the parentheses
        // if (dataString.StartsWith("(") && dataString.EndsWith(")"))
        // {
        //     dataString = dataString.Substring(1, dataString.Length - 2);
        // }

        // // Split the elements into an array
        // string[] stringArray = dataString.Split(',');

        // // Store as a Vector3
        // Vector3 result = new Vector3(
        //     float.Parse(stringArray[0]),
        //     float.Parse(stringArray[1]),
        //     float.Parse(stringArray[2]));

        // return result;
    }

    // Position is the data being received in this example
    Vector3 position0 = Vector3.zero;
    Vector3 position1 = Vector3.zero;

    void Update()
    {
        if(isConnect){
            position0 = new Vector3(total[1][0], total[1][1], total[1][2]);
            position1 = new Vector3(total[3][0], total[3][1], total[3][2]);

            // Set this object's position in the scene according to the position received
            Camera0.transform.position = position0;
            Camera0.transform.rotation = Quaternion.Euler(total[0][0],total[0][1],total[0][2]); 
            Camera1.transform.position = position1;
            Camera1.transform.rotation = Quaternion.Euler(total[2][0],total[2][1],total[2][2]); 
        }
        
    }
}