using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootBeat : MonoBehaviour
{
    [SerializeField] Conductor conductorScript;
    [SerializeField] float CurrentBeat;
    [SerializeField] float nextBeat;
    [SerializeField] Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        conductorScript = GetComponent<Conductor>();
        CurrentBeat = conductorScript.songPositionInBeats;
        nextBeat = (((int)conductorScript.songPositionInBeats)) + 1f;
 
    }

    // Update is called once per frame
    void Update()
    {
        CurrentBeat = conductorScript.songPositionInBeats;

        if (CurrentBeat >= nextBeat)
        {
            playerScript.Shoot();
        }

        nextBeat = (((int)conductorScript.songPositionInBeats)) + 1f;

    }


}
