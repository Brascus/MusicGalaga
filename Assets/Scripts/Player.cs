using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] Conductor conductorScript;
    [SerializeField] float CurrentBeat;
    [SerializeField] float nextBeat;
    public Projectile laserPrefab;
    public Color defaultColor;
    public Color missColor;
    public Color hitColor;
    public float speed = 5.0f;
    public int HitCount = 0;
    public int PressGap = 10; //  The Frame An Input is Read For
    public int AllowedGap = 6; // The Frame Window For A Hit
    public int ColorCounter = 0;
    public int ColorLimit = 20;
    public bool ColorChanged = false;
    public GameObject endScreen;
    public SpriteRenderer Shooter;


    private void Start()
    {
        CurrentBeat = conductorScript.songPositionInBeats;
        nextBeat = (((int)conductorScript.songPositionInBeats)) + 1f;
        defaultColor = Color.white;
        missColor = Color.red;
        hitColor = Color.green;
        endScreen = GameObject.Find("EndGame");
        endScreen.SetActive(false);
    }

    private void Update()
    {
        if (ColorChanged == true)
        {
            ColorCounter++;
            if (ColorCounter == ColorLimit) 
            {
                Shooter.color = defaultColor;
                ColorCounter = 0;
                ColorChanged = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (Shooter.color != missColor )
                if(transform.position.x > -15)
                this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (Shooter.color != missColor)
                if (transform.position.x < 15)
                    this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && HitCount == 0)
        {
            HitCount = 1;
        }
        else if (HitCount > AllowedGap && ColorChanged == false)
        {
            Shooter.color = missColor;
            ColorChanged = true;
        }

        else if (HitCount > 0)
        {
            HitCount++;
            if (HitCount == PressGap)
            {
                HitCount = 0;
            }
        }
        

        CurrentBeat = conductorScript.songPositionInBeats;

        if (CurrentBeat >= nextBeat)
        {
            if (HitCount <= AllowedGap && HitCount > 0)
            {
                Shoot();
                Shooter.color = hitColor;
                ColorChanged = true;
                HitCount = 0;
            }
        }

        nextBeat = (((int)conductorScript.songPositionInBeats)) + 1f;

    }
    public void Shoot()
    { 
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Invader" || other.gameObject.tag == "MIssile" )
        {
            endScreen.SetActive(true);
            conductorScript.musicSource.Stop();
            Destroy(this.gameObject);
        }

    }
    
}

