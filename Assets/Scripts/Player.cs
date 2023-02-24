using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_Speed = 3f;
    [SerializeField] private float m_BonusRunSpeed = 3f;
    //[SerializeField] private float m_JumpHeight = 6.5f;

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_MoveVelocity;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        m_MoveVelocity = moveInput.normalized * m_Speed;

        m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + m_MoveVelocity * Time.deltaTime);

        var xMotion = m_Rigidbody2D.velocity.x;

        if (xMotion > 0.01f)
        {
            m_SpriteRenderer.flipX = false;
        }
        else if (xMotion < 0.01f)
        {
            m_SpriteRenderer.flipX = true;
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            m_Animator.SetBool("isRunning", true);
        }
        else
        {
            m_Animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            m_Speed += m_BonusRunSpeed;
            // Тут можно включить анимацию бега
        }

        //    // Jumping
        //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //    {
        //        r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        //    }       
    }
}
