using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 0;
    public float travelTime = 0.2f;
    public Rigidbody2D rigidbody2D;
    public float rotationSpeedUp = 0.2f;
    public float rotationSpeedDown = 0.4f;

    public CharacterConfiguration[] characterConfiguration;
    public SpriteRenderer spriteRenderer;
    private void Update()
    {
        if (GameController.instance.canPlay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetVelocity();
                LeanTween.cancel(gameObject);
                LeanTween.moveY(gameObject, transform.position.y + jumpForce, travelTime).setOnComplete(Rotate);
                //Debug.Log("viva el salto");

                LeanTween.rotateZ(gameObject, 20, rotationSpeedUp);
            }
        }
    }

    public void Rotate()
    {
        LeanTween.rotateZ(gameObject, -40, rotationSpeedDown);
    }


    public void ToggleRigidBody()
    {
        if (rigidbody2D.bodyType == RigidbodyType2D.Dynamic)
        {
            ResetVelocity();
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            ResetVelocity();
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void ResetVelocity()
    {
        rigidbody2D.linearVelocity = new Vector2(0, 0);
        rigidbody2D.angularVelocity = 0f;
    }
    public void SelectCharacter(int character)
    {
        switch (character)
        {
            case 0:
                spriteRenderer.sprite = characterConfiguration[0].sprite;
                break;
            case 1:
                spriteRenderer.sprite = characterConfiguration[1].sprite;
                break;
            case 2:
                spriteRenderer.sprite = characterConfiguration[2].sprite;
                break;
            default:
                break;
        }
    }

}
