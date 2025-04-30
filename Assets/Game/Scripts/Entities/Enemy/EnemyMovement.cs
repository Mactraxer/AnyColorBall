using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform _leftMoveBound;
    [SerializeField] private Transform _rightMoveBound;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDiraction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveDiraction = Vector2.left;
    }

    private void FixedUpdate()
    {
        if (IsNeedChangeDirection())
        {
            ChangeDirection();
        }

        _rigidbody.angularVelocity = -_moveDiraction.x * _movementSpeed;
    }

    private void ChangeDirection()
    {
        if (_moveDiraction == Vector2.left)
        {
            _moveDiraction = Vector2.right;
        } 
        else if (_moveDiraction == Vector2.right) 
        {
            _moveDiraction = Vector2.left;
        }
    }

    private bool IsNeedChangeDirection()
    {
        if (_moveDiraction == Vector2.left && _rigidbody.transform.position.x < _leftMoveBound.position.x)
        {
            return true;
        } 
        else if (_moveDiraction == Vector2.right && _rigidbody.transform.position.x > _rightMoveBound.position.x)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }
}
