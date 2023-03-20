using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    public GameObject miniGame;
    
    private readonly float _speed = 4.5f;
    private float _horizontal;
    private float _vertical;

    private bool _isFacingRight;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (miniGame.activeInHierarchy)
            _rb.bodyType = RigidbodyType2D.Static;
        else
            _rb.bodyType = RigidbodyType2D.Dynamic;
        if(_rb.bodyType != RigidbodyType2D.Static)
            Flip();
    }

    void FixedUpdate()
    {
        _horizontal = Input.GetAxis("Horizontal") * _speed;
        _vertical = Input.GetAxis("Vertical") * _speed;
        _rb.velocity = new Vector2(_horizontal, _vertical);
        if ((_horizontal != 0 || _vertical != 0) && _rb.bodyType != RigidbodyType2D.Static)
        {
            _animator.SetBool("isWalk", true);
        }
        else
        {
            _animator.SetBool("isWalk", false);
        }
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0 || !_isFacingRight && _horizontal > 0)
        {
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
