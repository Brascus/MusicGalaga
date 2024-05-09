using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    [SerializeField] Conductor conductorScript;

    [SerializeField] GameObject player;

    public Invader[] prefabs;

    public int rows = 5;

    public AudioSource WinMusic;

    public int cols = 11;

    public AnimationCurve speed;

    public GameObject WinScreen;

    public float missileAttackRate = 10000.0f;
    public Projectile missilePrefab;
    public int amountKilled { get; private set; }
    public int amountAlive => this.totalInvader - this.amountKilled;
    public int totalInvader => this.rows * this.cols;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvader;
    private Vector3 _direction = Vector2.right;
    private void Awake()
    {
        for (int row = 0; row<this.rows; row++)
        {
            float width = 5f * (this.cols - 1);
            float height = 2.0f * (this.rows - 1);
            Vector3 centering = new Vector3(-width/2, -height/2+3);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row*2.5f), 0.0f); 
            for (int cols = 0; cols<this.cols; cols++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += cols * 5.0f;
                invader.transform.position = position;
            }
        }
    }
    private void Start()
    {

        WinScreen = GameObject.Find("WinScreen");
        WinScreen.SetActive(false);
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }
    private void Update()
    {
        
        this.transform.position += (_direction * this.speed.Evaluate(this.percentKilled)  * Time.deltaTime)*10;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if(_direction == Vector3.right && invader.position.x >= (rightEdge.x-1.0f))
            {
                AdvanceRow();
            } 
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }

        }
    }
    
    private void MissileAttack()
    {
        foreach(Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
    private void AdvanceRow()
    {
        _direction.x *= -1f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void InvaderKilled()
    {
        this.amountKilled++;
        Debug.Log(amountKilled);
        if(this.amountKilled >= this.totalInvader)
        {
            conductorScript.musicSource.Stop();
            WinMusic.Play();
            Destroy(player);
            WinScreen.SetActive(true);
        }
    }
}
