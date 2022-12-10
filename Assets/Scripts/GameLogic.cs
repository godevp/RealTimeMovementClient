using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameObject character; // TO DO: CREATE LIST OF THESE and use it
    public List<GameObject> playersList;

    float CharacterSpeed;
    float fixedDeltaTime;
    float DiagonalCharacterSpeed;

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
                Debug.Log("SettingAnotherPlayer");
                player.GetComponent<AnotherPlayer>().characterPositionInPercent = new Vector2(posX + 1.0f, posY);
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
    public void SendPlayersPositionToClient()
    {
        NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.HereIsMyPosition.ToString() +
                                                        '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.x +
                                                        '|' + playersList[0].GetComponent<AnotherPlayer>().characterPositionInPercent.y);
    }

    public void SetSpeedDeltaTime(float fixedDeltaTime, float charSpeed)
    {
        this.fixedDeltaTime = fixedDeltaTime;
        CharacterSpeed = charSpeed;
    }
}

