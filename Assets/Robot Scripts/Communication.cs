using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class Communication : MonoBehaviour
{

    public string IpAdress;
    private const int LocalPort = 8000;
    private TcpClient Client;
    private NetworkStream Stream;
    public string message;
    public string message_received;
    private bool UpdateRobot = false;
    public GameObject JointSetter;

    // Start is called before the first frame update
    void Start()
    {
        Client = new TcpClient(IpAdress, LocalPort);    
        Stream = Client.GetStream();
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateRobot)
        {
            byte[] bytesToRead = new byte[Client.ReceiveBufferSize];
            int bytesRead = Stream.Read(bytesToRead, 0, Client.ReceiveBufferSize);
            message_received = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

            if (message_received != "Finished")
            {

                JointSetter.GetComponent<JointSetter>().UpdateJoints(message_received);


                message = "Update";
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
                Stream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            else
            {
                message = "Received";
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
                Stream.Write(bytesToSend, 0, bytesToSend.Length);
                UpdateRobot = false;
            }
        }
    }


    public void SendMessage()
    {

        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
        Debug.Log("Sending : " + message);
        Stream.Write(bytesToSend,0,bytesToSend.Length);

        byte[] bytesToRead = new byte[Client.ReceiveBufferSize];
        int bytesRead = Stream.Read(bytesToRead, 0, Client.ReceiveBufferSize);
        message_received = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
        Debug.Log("Received : " + message_received);


    }

    public void StartPath()
    {
        message = "SP";
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
        Debug.Log("Sending : " + message);
        Stream.Write(bytesToSend, 0, bytesToSend.Length);

        UpdateRobot = true;

    }

    public void JogJoints()
    {
        string JointString;

        message = "JJ";
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
        Debug.Log("Sending : " + message);
        Stream.Write(bytesToSend, 0, bytesToSend.Length);

        byte[] bytesToRead = new byte[Client.ReceiveBufferSize];
        int bytesRead = Stream.Read(bytesToRead, 0, Client.ReceiveBufferSize);
        message_received = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

        for (int i = 0; i < JointSetter.GetComponent<JointSetter>().JointNumber; i++)
        {   
            float JointValue;
            JointValue = (JointSetter.GetComponent<JointSetter>().slides[i].SliderValue - 0.5f) * 360f;
            JointString = JointValue.ToString();
            JointString = JointString.Replace(',','.');
            bytesToSend = ASCIIEncoding.ASCII.GetBytes(JointString);
            Stream.Write(bytesToSend, 0, bytesToSend.Length);

            bytesToRead = new byte[Client.ReceiveBufferSize];
            bytesRead = Stream.Read(bytesToRead, 0, Client.ReceiveBufferSize);
            message_received = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

        }

        message = "Start";
        bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
        Debug.Log("Sending : " + message);
        Stream.Write(bytesToSend, 0, bytesToSend.Length);

        UpdateRobot = true;
    }
}
