using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerController controller;
    public PlayerController Controller
    {
        get
        {
            return controller = controller != null ? controller : GetComponent<PlayerController>();
        }
    }

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        }
    }

    private Animator characterAnimator;
    public Animator CharacterAnimator
    {
        get
        {
            return characterAnimator = characterAnimator != null ? characterAnimator : GetComponent<Animator>();
        }
    }

    [SerializeField][Range(0.0f, 10.0f)] private float movementSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        if ((!SpriteRenderer.flipX && hAxis < 0) || (SpriteRenderer.flipX && hAxis > 0))
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            CharacterAnimator.SetTrigger("Run");
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            CharacterAnimator.SetTrigger("Idle");
        }
        transform.Translate(Vector3.right * hAxis * movementSpeed * Time.deltaTime);
    }
}
