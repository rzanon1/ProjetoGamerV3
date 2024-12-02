using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    // Unity chama Awake automaticamente
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }
    // Procura no mesmo game object do script outra referencia, no caso, character controller

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if(Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }
    // deltaTime é quanto tempo se passou desde o ultimo frame

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Obstacle")) {

            GameManager.Instance.GameOver();

        }

    }

}
