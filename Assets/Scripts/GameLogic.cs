using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameObject character;

    Vector2 characterPositionInPercent;
    Vector2 characterVelocityInPercent;

    bool pressedW = false;
    bool pressedS = false;
    bool pressedA = false;
    bool pressedD = false;
    float CharacterSpeed;
    float fixedDeltaTime;
    float DiagonalCharacterSpeed;

    void Start()
    {
        
        NetworkedClientProcessing.SetGameLogic(this);
      
        Sprite circleTexture = Resources.Load<Sprite>("Circle");

        character = new GameObject("Character");

        character.AddComponent<SpriteRenderer>();
        character.GetComponent<SpriteRenderer>().sprite = circleTexture;
    }
    void Update()
    {
        PressingButtons();
            characterVelocityInPercent = Vector2.zero;

            if (pressedW && pressedD)
            {
                characterVelocityInPercent.x = DiagonalCharacterSpeed;
                characterVelocityInPercent.y = DiagonalCharacterSpeed;
                SendPlayersPositionToClient();
            }
            else if (pressedW && pressedA)
            {
                characterVelocityInPercent.x = -DiagonalCharacterSpeed;
                characterVelocityInPercent.y = DiagonalCharacterSpeed;

            }
            else if (pressedS && pressedD)
            {
                characterVelocityInPercent.x = DiagonalCharacterSpeed;
                characterVelocityInPercent.y = -DiagonalCharacterSpeed;

            }
            else if (pressedS && pressedA)
            {
                characterVelocityInPercent.x = -DiagonalCharacterSpeed;
                characterVelocityInPercent.y = -DiagonalCharacterSpeed;

            }
            else if (pressedD)
                characterVelocityInPercent.x = CharacterSpeed;
            else if (pressedA)
                characterVelocityInPercent.x = -CharacterSpeed;
            else if (pressedW)
                characterVelocityInPercent.y = CharacterSpeed;
            else if (pressedS)
                characterVelocityInPercent.y = -CharacterSpeed;


        characterPositionInPercent += (characterVelocityInPercent * fixedDeltaTime);

        Vector2 screenPos = new Vector2(characterPositionInPercent.x * (float)Screen.width, characterPositionInPercent.y * (float)Screen.height);
        Vector3 characterPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        characterPos.z = 0;
        character.transform.position = characterPos;



    }

    void PressingButtons()
    {
        //KeyDown
        if(Input.GetKeyDown(KeyCode.W))
        {
            pressedW = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pressedS = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pressedD = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pressedA = true;
        }

        //KeyUp
        if (Input.GetKeyUp(KeyCode.W))
        {
            pressedW = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            pressedS = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            pressedD = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            pressedA = false;
        }

    }
    public void SendPlayersPositionToClient()
    {
        NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.HereIsMyPosition.ToString() + '|' + characterPositionInPercent.x + '|' + characterPositionInPercent.y);
    }

    public void SetSpeedDeltaTime(float fixedDeltaTime, float charSpeed)
    {
        this.fixedDeltaTime = fixedDeltaTime;
        CharacterSpeed = charSpeed;
        DiagonalCharacterSpeed = Mathf.Sqrt(CharacterSpeed * CharacterSpeed + CharacterSpeed * CharacterSpeed) / 2f;
    }
}

