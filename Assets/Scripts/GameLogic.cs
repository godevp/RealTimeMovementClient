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
        if(Input.GetKeyDown(KeyCode.W))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "W");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "S");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "D");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.PressedButton.ToString() + '|' + "A");
        }

        //KeyUp
        if (Input.GetKeyUp(KeyCode.W))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "W"
                                                                            + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.x
                                                                            + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.y);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "S"
                                                                             + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.x
                                                                             + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.y);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "D"
                                                                            + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.x
                                                                            + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.y);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.ButtonReleased.ToString() + '|' + "A"
                                                                            + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.x
                                                                            + '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.y);
        }

    }
    public void SendPlayersPositionToServer()
    {

            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.HereIsMyPosition.ToString() +
                                                        '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.x +
                                                        '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.y);
        
    }
    public void SetButtonPressed(int id, string button)
    {
        switch (button)
        {
            case "W":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedW = true;
                break;
            case "S":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedS = true;
                break;
            case "D":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedD = true;
                break;
            case "A":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedA = true;
                break;
        }
    }
    public void SetButtonReleased(int id, string button)
    {
        switch (button)
        {
            case "W":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedW = false;
                break;
            case "S":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedS = false;
                break;
            case "D":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedD = false;
                break;
            case "A":
                if (ForeachForButtons(id) != null)
                    ForeachForButtons(id).GetComponent<AnotherPlayer>().pressedA = false;
                break;
        }
    }
    public void DestroyByID(int id)
    {
        var temp = ForeachForButtons(id);
        playersList.Remove(ForeachForButtons(id));
        Destroy(temp);
    }
    public GameObject ForeachForButtons(int id)
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
    public void SetSpeedDeltaTime(float fixedDeltaTime, float charSpeed)
    {
        this.fixedDeltaTime = fixedDeltaTime;
        CharacterSpeed = charSpeed;
        DiagonalCharacterSpeed = Mathf.Sqrt(CharacterSpeed * CharacterSpeed + CharacterSpeed * CharacterSpeed) / 2f;
    }
}

