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
            Vector3 pos = ScreenToCanvasPoint(Input.mousePosition);
            //Debug.Log(pos);
            //Debug.Log(Input.mousePosition);
            //pos.z = -9;
            GameObject particle = Instantiate(particleGenerater, pos, Quaternion.identity);
            particle.transform.SetParent(particleCanvas, false);
            //particle.GetComponent<ParticleSystem>().Emit();
            //Destroy(particle, 2f);
        }
    }

    //transform a screen point to a canvas point
    public Vector3 ScreenToCanvasPoint(Vector3 point)
    {
        //scale mouse to canvas
        //get canvas size
        float canvasX = (point.x - particleCanvas.transform.position.x) / particleCanvas.transform.localScale.x;
        float canvasY = (point.y - particleCanvas.transform.position.y) / particleCanvas.transform.localScale.y;
        return new Vector3(canvasX, canvasY, 0);
    }
}
