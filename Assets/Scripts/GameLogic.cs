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
                player.GetComponent<AnotherPlayer>().characterPositionInPercent = new Vector2(posX, posY);
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
        if(Input.GetKey(KeyCode.W) &&!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "W");
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "S");
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "D");
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "A");
        }
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "WD");
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "WA");
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "SD");
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "SA");
        }



        //KeyUp
        if (Input.GetKeyUp(KeyCode.W))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "W");
                                                                           
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "S");
                                                                             
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "D");
                                                                           
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "A");
                                                                           
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
        FindPlayerGameObjectByID(id).GetComponent<AnotherPlayer>().characterPositionInPercent = new Vector2(x, y);
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

