using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

public class MiniGameSquare : MonoBehaviour
{
    private bool _isTouchingDot;
    //square moving
    private Rigidbody2D _rb;
    
    private readonly float _speed = 3f;
    
    //dots
    public Rigidbody2D[] dots;
    
    //text
    public TextMeshProUGUI introversion;
    public TextMeshProUGUI extraversion;
    
    //interact
    public GameObject introversionWall;
    public GameObject miniGame;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            var touchedDot = dots.Where(d => d.IsTouching(
                collider: introversionWall.GetComponent<PolygonCollider2D>()
            )).ToList();
            introversion.text = touchedDot.Count.ToString();
        }
        for (int i = 0; i < dots.Length; i++)
        {
            var touchedDot = dots.Where(d => d.IsTouching(
                collider: _rb.GetComponentInChildren<CircleCollider2D>()
            )).ToList();
            extraversion.text = touchedDot.Count.ToString();
        }

        if (introversion.text == "10" || extraversion.text == "10")
            StartCoroutine(CLoseMiniGame());
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed;
        var vertical = Input.GetAxis("Vertical") * _speed;

        _rb.velocity = new Vector2(horizontal, vertical);

        if (_isTouchingDot)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    var touchedDot = dots.Where(d => d.IsTouching(
                        collider: _rb.GetComponentInChildren<CircleCollider2D>()
                    )).ToList();
                    touchedDot.ForEach(d => d.AddForce(Vector2.left, ForceMode2D.Force));
                }
            }

            if (Input.GetMouseButton(1))
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    var touchedDot = dots.Where(d => d.IsTouching(
                        collider: _rb.GetComponentInChildren<CircleCollider2D>()
                    )).ToList();
                    touchedDot.ForEach(d => d.position = new Vector2(_rb.position.x + .25f, _rb.position.y));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Dot"))
            _isTouchingDot = true;
    }

    IEnumerator CLoseMiniGame()
    {
        yield return new WaitForSeconds(1f);
        miniGame.SetActive(false);
    }
}