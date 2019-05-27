using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueMusic : MonoBehaviour
{
    void Start()
    {
		this.GetComponent<AudioSource>().time = Time.time % this.GetComponent<AudioSource>().clip.length;
    }
	
}
