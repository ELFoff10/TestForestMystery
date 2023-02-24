using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_Speed = 3f;
    [SerializeField] private float m_BonusRunSpeed = 3f;
    [SerializeField] private float m_JumpForce = 3f;

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_MoveVelocity;
    private Animator m_Animator;
    private bool m_FacingRight = true;
    private float m_DefoultSpeed;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_DefoultSpeed = m_Speed;
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        m_MoveVelocity = moveInput.normalized * m_Speed;

        m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + m_MoveVelocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            m_Animator.SetBool("Run", true);
        }
        else
        {
            m_Animator.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Animator.SetTrigger("Jump");

            if (m_FacingRight == true)
            {
                m_Rigidbody2D.velocity = Vector2.up * m_JumpForce + Vector2.right * m_JumpForce;
                StartCoroutine(JumpDefoultRight());
            }
            if (m_FacingRight == false)
            {
                m_Rigidbody2D.velocity = Vector2.up * m_JumpForce - Vector2.right * m_JumpForce;
                StartCoroutine(JumpDefoultLeft());
            }
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

    IEnumerator JumpDefoultRight()
    {
        yield return new WaitForSeconds(0.5f);
        m_Rigidbody2D.velocity -= Vector2.up * m_JumpForce + Vector2.right * m_JumpForce;
        m_Rigidbody2D.velocity = Vector2.down * m_JumpForce + Vector2.right * m_JumpForce;
        yield return new WaitForSeconds(0.5f);
        m_Rigidbody2D.velocity -= Vector2.down * m_JumpForce + Vector2.right * m_JumpForce;
    }
    IEnumerator JumpDefoultLeft()
    {
        yield return new WaitForSeconds(0.5f);
        m_Rigidbody2D.velocity -= Vector2.up * m_JumpForce - Vector2.right * m_JumpForce;
        m_Rigidbody2D.velocity = Vector2.down * m_JumpForce - Vector2.right * m_JumpForce;
        yield return new WaitForSeconds(0.5f);
        m_Rigidbody2D.velocity -= Vector2.down * m_JumpForce - Vector2.right * m_JumpForce;
    }
}
