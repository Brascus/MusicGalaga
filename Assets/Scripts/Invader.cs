using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invader : MonoBehaviour
{
    [SerializeField] Conductor conductorScript;

    public Sprite[] animationSprites;

    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;

    public GameObject endScreen;

    private int _animationFrame;

    public System.Action killed;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        
        conductorScript = GameObject.Find("Main Camera").GetComponent<Conductor>();
        endScreen = GameObject.Find("EndGame");
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    public void Update()
    {
        if (gameObject.transform.position.y < -10  )
        {
            conductorScript.musicSource.Stop();
            endScreen.SetActive(true);

        }
    }
    private void AnimateSprite()
    {
        _animationFrame++;
        if (_animationFrame >= animationSprites.Length)
        {
            _animationFrame = 0;
        }
        _spriteRenderer.sprite = this.animationSprites[_animationFrame];

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            endScreen.SetActive(true);
        }

        if (other.gameObject.tag == "Laser")
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);  
        }
     
    }
}
