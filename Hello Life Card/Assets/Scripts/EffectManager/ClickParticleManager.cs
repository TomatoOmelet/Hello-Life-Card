using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickParticleManager : MonoBehaviour
{
    public GameObject particleObject;
    public Camera myCamera;

    // Update is called once per frame
    void Update()
    {
        //when player click, generate a particle
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = myCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -9;
            GameObject particle = Instantiate(particleObject, pos, Quaternion.Euler(-90, 0, 0));
            //particle.GetComponent<ParticleSystem>().Emit();
            Destroy(particle, 2f);
        }
    }
}
