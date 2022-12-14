using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameObject character; // TO DO: CREATE LIST OF THESE and use it
    public List<GameObject> playersList;

    public float CharacterSpeed;
    public float fixedDeltaTime;
    public float DiagonalCharacterSpeed;
    public bool pressedW;
    public bool pressedS;
    public bool pressedA;
    public bool pressedD;
    void Start()
    {
        playersList = new List<GameObject>();
        NetworkedClientProcessing.SetGameLogic(this);
    }
    public void CreateNewCharacter(int id)
    {
        Sprite circleTexture = Resources.Load<Sprite>("Circle");
        GameObject newCharacter = new GameObject("Character_" + id);
        newCharacter.AddComponent<SpriteRenderer>();
        newCharacter.GetComponent<SpriteRenderer>().sprite = circleTexture;
        newCharacter.AddComponent<AnotherPlayer>();
        newCharacter.GetComponent<AnotherPlayer>().id = id;
        playersList.Add(newCharacter);
    }
    
    public void SetAnotherPlayer(int id, float posX, float posY)
    {
        foreach (GameObject player in playersList)
        {
            if (player.GetComponent<AnotherPlayer>().id == id)
            {
                player.GetComponent<AnotherPlayer>().characterPositionInPercent += (new Vector2(posX, posY)) * 0.02f ;
            }
        }
    }
    void Update()
    {
        PressingButtons();
    }

    void PressingButtons()
    {
        
        //KeyDown
        if(Input.GetKeyDown(KeyCode.W))
        {
            pressedW = true;
            CheckPessedButtons();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pressedS = true;
            CheckPessedButtons();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pressedD = true;
            CheckPessedButtons();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pressedA = true;
            CheckPessedButtons();
        }

        //KeyUp
        if (Input.GetKeyUp(KeyCode.W))
        {
            pressedW = false;
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "W");

            CheckPessedButtons();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            pressedS = false;
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "S");
            CheckPessedButtons();

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            pressedD = false;
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "D");
            CheckPessedButtons();

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            pressedA = false;
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "A");
            CheckPessedButtons();
        }        
    }
    public void CheckPessedButtons()
    {
        //
        if (pressedW && pressedA)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "WA");
        }
        if (pressedW && pressedD)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "WD");
        }
        if (pressedS && pressedD)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "SD");
        }
        if (pressedS && pressedA)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "SA");
        }
        if (pressedW && !pressedS && !pressedA && !pressedD)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "W");
        }
        if (pressedD && !pressedA && !pressedW && !pressedS)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "D");
        }
        if (pressedS && !pressedW && !pressedA && !pressedD)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "S");
        }
        if (pressedA && !pressedD && !pressedW && !pressedS) 
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "A");
        }

    }
    public void SendPlayersPositionToServer()
    {

            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.HereIsMyPosition.ToString() +
                                                        '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.x +
                                                        '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.y);
        
    }
   
    public void SetCertainPlayerPos(int id,float x, float y)
    {
        if(FindPlayerGameObjectByID(id) != null)
        FindPlayerGameObjectByID(id).GetComponent<AnotherPlayer>().characterPositionInPercent += (new Vector2(x, y)) * 0.02f;
    }    
   
    public void DestroyByID(int id)
    {
        var temp = FindPlayerGameObjectByID(id);
        playersList.Remove(FindPlayerGameObjectByID(id));
        Destroy(temp);
    }
    public GameObject FindPlayerGameObjectByID(int id)
    {
        GameObject temp = null;
        foreach(GameObject player in playersList)
        {
            if(player.GetComponent<AnotherPlayer>().id == id)
            {
                temp = player;
            }
        }
        return temp;
    }
}

