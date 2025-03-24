using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    private CharacterController _controller;
    private bool _isGrounded;
    private Vector3 _playerVelocity;
    
    [SerializeField] private float maxWidth = 1.5f;
    public float gravity = -65f;
    public float jumpHeight = 1.5f;

    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    private void FixedUpdate()
    {
        _isGrounded = _controller.isGrounded;
    }

    public void MoveBall()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPosition = touch.position;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y,Camera.main.transform.position.z - transform.position.z));

        Vector3 targetPosition = new Vector3(worldPosition.x, transform.position.y, transform.position.z);

        _playerVelocity.y += gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
            SoundManager.Instance.BallJumpSfx();
            Jump();
        }
        _controller.Move(_playerVelocity * Time.deltaTime);
        targetPosition.y = transform.position.y;
        targetPosition.z = transform.position.z;
        targetPosition.x *= -1;

        if (targetPosition.x > maxWidth) targetPosition.x = maxWidth;
        if (targetPosition.x < -maxWidth) targetPosition.x = -maxWidth;
            
        transform.position = targetPosition;
    }
    
    private void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Perfect"))
        {
            Debug.Log("Perfect");
            GameManager.Instance.PerfectShot();
            GameManager.Instance.PlatformExplode(other.GetComponentInParent<PlatformEffector>());
        }
        
        if (other.gameObject.CompareTag("Death"))
        {
            GameManager.Instance.Death();
        }
    }
}
