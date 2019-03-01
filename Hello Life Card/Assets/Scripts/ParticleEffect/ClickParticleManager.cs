using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickParticleManager : MonoBehaviour
{
    public Transform particleCanvas;
    public GameObject particleGenerater;
    public Camera myCamera;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //when player click, generate a particle
        if(Input.GetMouseButtonDown(0))
        {
            //Vector3 pos = myCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = ParticleManager.ScreenToCanvasPoint(Input.mousePosition, particleCanvas);
            //Debug.Log(pos);
            //Debug.Log(Input.mousePosition);
            //pos.z = -9;
            GameObject particle = Instantiate(particleGenerater, pos, Quaternion.identity);
            particle.transform.SetParent(particleCanvas, false);
            particle.GetComponent<ParticleGenerator>().Play();
            Destroy(particle);
            //Destroy(particle);
            //particle.GetComponent<ParticleSystem>().Emit();
            //Destroy(particle, 2f);
        }
    }

}
