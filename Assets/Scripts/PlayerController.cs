using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigid; // 오브젝트의 Rigidbody2D 컴포넌트를 인스펙터에서 연결

    [SerializeField] float _movePower = 10f; // 이동 시 가할 힘의 크기

    [SerializeField] float _jumpPower = 5f; // 점프 시 가할 힘의 크기

    [SerializeField] float _maxMoveSpeed = 10f; // 최대 이동 속도

    float _x; // 입력된 좌우 방향 값을 저장하는 변수

    bool _isGrounded; // 지면에 닿아있는지를 확인하는 변수

    bool _jumpRequested; // 점프 입력 여부를 나타내는 플래그 변수

    private void Update()
    {
        _x = Input.GetAxisRaw("Horizontal"); // 좌우 입력 값을 가져옴 (왼쪽: -1, 중립: 0, 오른쪽: 1)

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _jumpRequested = true; // 점프 요청 플래그 활성화
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (_jumpRequested) // 점프 요청이 있을 경우
        {
            Jump();
            _jumpRequested = false; // 점프 요청 초기화
        }
    }

    private void Move()
    {
        // 현재 속도가 최대 이동 속도보다 낮을 경우에만 이동 힘을 추가
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
        // 충돌한 오브젝트의 태그가 "Ground"일 경우 지면에 닿았다고 판단
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌이 끝난 오브젝트의 태그가 "Ground"일 경우 지면에서 벗어났다고 판단
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
