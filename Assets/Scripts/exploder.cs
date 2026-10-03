using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exploder : MonoBehaviour
{
    public float destroyTime;
   public void PlayAndDestroy(ParticleSystem part){
       part = GetComponent<ParticleSystem>();
       part.Play();
       gameObject.GetComponent<BoxCollider>().enabled = false;
       Destroy(gameObject, destroyTime);
   }
}
