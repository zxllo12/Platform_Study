using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigid; // ������Ʈ�� Rigidbody2D ������Ʈ�� �ν����Ϳ��� ����

    [SerializeField] float _movePower = 10f; // �̵� �� ���� ���� ũ��

    [SerializeField] float _jumpPower = 5f; // ���� �� ���� ���� ũ��

    [SerializeField] float _maxMoveSpeed = 10f; // �ִ� �̵� �ӵ�

    float _x; // �Էµ� �¿� ���� ���� �����ϴ� ����

    bool _isGrounded; // ���鿡 ����ִ����� Ȯ���ϴ� ����

    bool _jumpRequested; // ���� �Է� ���θ� ��Ÿ���� �÷��� ����

    private void Update()
    {
        _x = Input.GetAxisRaw("Horizontal"); // �¿� �Է� ���� ������ (����: -1, �߸�: 0, ������: 1)

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _jumpRequested = true; // ���� ��û �÷��� Ȱ��ȭ
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (_jumpRequested) // ���� ��û�� ���� ���
        {
            Jump();
            _jumpRequested = false; // ���� ��û �ʱ�ȭ
        }
    }

    private void Move()
    {
        // ���� �ӵ��� �ִ� �̵� �ӵ����� ���� ��쿡�� �̵� ���� �߰�
        if (Mathf.Abs(_rigid.velocity.x) < _maxMoveSpeed)
        {
            _rigid.AddForce(Vector2.right * _x * _movePower, ForceMode2D.Force);
        }
    }

    private void Jump()
    {
        _rigid.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Ground"�� ��� ���鿡 ��Ҵٰ� �Ǵ�
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹�� ���� ������Ʈ�� �±װ� "Ground"�� ��� ���鿡�� ����ٰ� �Ǵ�
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
