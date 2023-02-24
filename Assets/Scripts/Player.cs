using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_Speed = 3f;
    [SerializeField] private float m_BonusRunSpeed = 3f;
    [SerializeField] private float m_JumpHeight = 6.5f;
    [SerializeField] private GameObject m_JumpToPointLeft;
    [SerializeField] private GameObject m_JumpToPointRight;

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_MoveVelocity;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private bool m_FacingRight = true;
    private float m_DefoultSpeed;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_DefoultSpeed = m_Speed;
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        m_MoveVelocity = moveInput.normalized * m_Speed;

        m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + m_MoveVelocity * Time.deltaTime);

        //var xMotion = m_Rigidbody2D.velocity.x;

        //if (xMotion > 0.01f)
        //{
        //    m_SpriteRenderer.flipX = false;
        //}
        //else if (xMotion < 0.01f)
        //{
        //    m_SpriteRenderer.flipX = true;
        //}

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))/*Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0*/
        {
            m_Animator.SetBool("Run", true);
        }
        else
        {
            m_Animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            m_Animator.SetTrigger("Jump");

            //if (true)
            //{

            //}
            //transform.position = Vector3.Slerp(transform.position, )
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) == true)
        {
            m_Speed += m_BonusRunSpeed;
            // Тут можно включить анимацию бега

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) == true)
        {
            m_Speed = m_DefoultSpeed;
        }

        if (m_FacingRight == false && Input.GetAxisRaw("Horizontal") > 0)
        {
            Flip();
        }
        if (m_FacingRight == true && Input.GetAxisRaw("Horizontal") < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
