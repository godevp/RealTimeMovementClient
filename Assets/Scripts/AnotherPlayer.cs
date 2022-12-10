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
    public float speed;
    public float fxdDeltaTime;
    public float DiagonalCharacterSpeed;

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
            characterVelocityInPercent.x = DiagonalCharacterSpeed;
            characterVelocityInPercent.y = DiagonalCharacterSpeed;
           
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
            characterVelocityInPercent.x = speed;
        else if (pressedA)
            characterVelocityInPercent.x = -speed;
        else if (pressedW)
            characterVelocityInPercent.y = speed;
        else if (pressedS)
            characterVelocityInPercent.y = -speed;


        characterPositionInPercent += (characterVelocityInPercent * speed);

        Vector2 screenPos = new Vector2(characterPositionInPercent.x * (float)Screen.width, characterPositionInPercent.y * (float)Screen.height);
        Vector3 characterPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        characterPos.z = 0;
        this.gameObject.transform.position = characterPos;
    }
    public void SetVariables(float CharacterSpeed, float fixedDeltaTime)
    {
        speed = CharacterSpeed;
        fxdDeltaTime = fixedDeltaTime;
        DiagonalCharacterSpeed = Mathf.Sqrt(CharacterSpeed * CharacterSpeed + CharacterSpeed * CharacterSpeed) / 2f;
    }
}
