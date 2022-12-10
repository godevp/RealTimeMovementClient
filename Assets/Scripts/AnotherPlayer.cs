using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AnotherPlayer : MonoBehaviour
{
    public int id;
    public Vector2 characterPositionInPercent;
    public Vector2 characterVelocityInPercent;

    public bool pressedW = false;
    public bool pressedS = false;
    public bool pressedA = false;
    public bool pressedD = false;

    void Start()
    {
        Sprite circleTexture = Resources.Load<Sprite>("Circle");
        //this.gameObject.AddComponent<SpriteRenderer>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = circleTexture;
        characterVelocityInPercent = Vector2.zero;
    }

    void Update()
    {
        Movement();
    }
    void Movement()
    {
        characterVelocityInPercent = Vector2.zero;

        if (pressedW && pressedD)
        {
            characterVelocityInPercent.x = NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;
            characterVelocityInPercent.y = NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;
           
        }
        else if (pressedW && pressedA)
        {
            characterVelocityInPercent.x = -NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;
            characterVelocityInPercent.y = NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;

        }
        else if (pressedS && pressedD)
        {
            characterVelocityInPercent.x = NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;
            characterVelocityInPercent.y = -NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;

        }
        else if (pressedS && pressedA)
        {
            characterVelocityInPercent.x = -NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;
            characterVelocityInPercent.y = -NetworkedClientProcessing.gameLogic.DiagonalCharacterSpeed;

        }
        else if (pressedD)
            characterVelocityInPercent.x = NetworkedClientProcessing.gameLogic.CharacterSpeed;
        else if (pressedA)
            characterVelocityInPercent.x = -NetworkedClientProcessing.gameLogic.CharacterSpeed;
        else if (pressedW)
            characterVelocityInPercent.y = NetworkedClientProcessing.gameLogic.CharacterSpeed;
        else if (pressedS)
            characterVelocityInPercent.y = -NetworkedClientProcessing.gameLogic.CharacterSpeed;


        characterPositionInPercent += (characterVelocityInPercent * NetworkedClientProcessing.gameLogic.fixedDeltaTime);

        Vector2 screenPos = new Vector2(characterPositionInPercent.x * (float)Screen.width, characterPositionInPercent.y * (float)Screen.height);
        Vector3 characterPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        characterPos.z = 0;
        this.gameObject.transform.position = characterPos;
    }

}
