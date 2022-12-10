using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AnotherPlayer : MonoBehaviour
{
    public int id;
    public Vector2 characterPositionInPercent;
    public Vector2 characterVelocityInPercent;

    void Start()
    {
        Sprite circleTexture = Resources.Load<Sprite>("Circle");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = circleTexture;
        characterVelocityInPercent = Vector2.zero;
        characterPositionInPercent = Vector2.zero;
    }

    void Update()
    {
        Movement();
    }
    void Movement()
    {
        Vector2 screenPos = new Vector2(characterPositionInPercent.x * (float)Screen.width, characterPositionInPercent.y * (float)Screen.height);
        Vector3 characterPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        characterPos.z = 0;
        this.gameObject.transform.position = characterPos;
    }

}
