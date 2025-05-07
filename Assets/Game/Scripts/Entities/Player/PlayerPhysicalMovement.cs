using AnyColorBall.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPhysicalMovement : MonoBehaviour, ISavedPlayerProgress
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _stickToGroundForce;
    [SerializeField] private float _stickInAir;
    [SerializeField] private float _moveInAirMultiplier;
    [SerializeField] private LayerMask _groundLayerMask;

    private float _moveDelta;
    private bool _isGrounded;

    private IInputService _inputService;

    public void UpdateProgress(PlayerProgress progress)
    {
        if (progress.WorldData.PositionOnLevel.Level == CurrentLevel())
        {
            progress.WorldData.PositionOnLevel.Vector3Data = _rigidbody2D.position.AsVectorData();
        }
    }

    public void ReadProgress(PlayerProgress progress)
    {
        if (progress.WorldData.PositionOnLevel.Level == CurrentLevel())
        {
            _rigidbody2D.Sleep();
            _rigidbody2D.position = progress.WorldData.PositionOnLevel.Vector3Data.AsUnityVector();
        }
    }

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Start()
    {
        _inputService.OnChangeHorizontalInput += OnChangeMoveControll;
        _inputService.OnTapUpButton += OnStartJump;
    }

    private void OnDestroy()
    {
        _inputService.OnChangeHorizontalInput -= OnChangeMoveControll;
        _inputService.OnTapUpButton -= OnStartJump;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody2D.position, Vector2.down, 0.8f, _groundLayerMask);

        _isGrounded = hit.collider != null;
        if (_isGrounded)
        {
            Vector2 normal = hit.normal.normalized;
            Vector2 tangent = new Vector2(normal.y, -normal.x).normalized;
            Vector2 moveDir = tangent * _moveDelta;

            _rigidbody2D.AddForce(moveDir * _moveForce);

            Vector2 stickForce = -normal * _stickToGroundForce;
            _rigidbody2D.AddForce(stickForce);
        }
        else
        {
            _rigidbody2D.AddForce(new Vector2(_moveDelta, _stickInAir) * _moveForce * _moveInAirMultiplier);
        }

        Vector2 horizontalVelocity = new Vector2(_rigidbody2D.linearVelocityX, 0f);
        if (horizontalVelocity.magnitude > _maxSpeed)
        {
            _rigidbody2D.linearVelocity = new Vector2(Mathf.Sign(_rigidbody2D.linearVelocityX) * _maxSpeed, _rigidbody2D.linearVelocityY);
        }
    }

    private void OnChangeMoveControll(float delta)
    {
        _moveDelta = delta;
    }

    private void OnStartJump()
    {
        if (_isGrounded)
        {
            _rigidbody2D.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }
}