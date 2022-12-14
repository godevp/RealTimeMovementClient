using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

static public class NetworkedClientProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg)
    {
       Debug.Log("msg received = " + msg + ".");

        string[] csv = msg.Split('|');
        int signifier = 0;
        if (int.TryParse(csv[0], out _))
        {
            signifier = int.Parse(csv[0]);
        }
        switch (signifier)
        {
            case ServerToClientSignifiers.RequestForPosition:
                gameLogic.SendPlayersPositionToServer();
                break;
            case ServerToClientSignifiers.SendBackID:
                gameLogic.CreateNewCharacter(int.Parse(csv[1]));
                
                break;
            case ServerToClientSignifiers.HereNewDataForPlayerByTheID:
                gameLogic.SetCertainPlayerPos(int.Parse(csv[1]), float.Parse(csv[2]), float.Parse(csv[3]));

                break;
            case ServerToClientSignifiers.NewClientJoined:
                gameLogic.CreateNewCharacter(int.Parse(csv[1]));
                gameLogic.SetAnotherPlayer(int.Parse(csv[1]), float.Parse(csv[2]), float.Parse(csv[3]));

                break;

            case ServerToClientSignifiers.SendAllClients:
                gameLogic.CreateNewCharacter(int.Parse(csv[1]));
                gameLogic.SetAnotherPlayer(int.Parse(csv[1]), float.Parse(csv[2]), float.Parse(csv[3]));
                break;
            case ServerToClientSignifiers.DestroyCertainPlayer:
                gameLogic.DestroyByID(int.Parse(csv[1]));
                break;
        }


    }

    static public void SendMessageToServer(string msg)
    {
        networkedClient.SendMessageToServer(msg);
    }

    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkedClient.IsConnected();
    }
    static public void ConnectToServer()
    {
        networkedClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkedClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkedClient networkedClient;
    public static GameLogic gameLogic;

    static public void SetNetworkedClient(NetworkedClient NetworkedClient)
    {
        networkedClient = NetworkedClient;
    }
    static public NetworkedClient GetNetworkedClient()
    {
        return networkedClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }
    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int HereIsMyPosition = 1;
    public const int PressedButton = 2;
    public const int ButtonReleased = 3;
}

static public class ServerToClientSignifiers
{
    public const int RequestForPosition = 1;
    public const int NewClientJoined = 2;
    public const int SendBackID = 3;
    public const int SendAllClients = 4;
    public const int DestroyCertainPlayer = 5;
    public const int HereNewDataForPlayerByTheID = 6;
}

#endregion

