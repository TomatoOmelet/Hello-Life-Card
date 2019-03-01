using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 ScreenToCanvasPoint(Vector3 point, Transform particleCanvas)
    {
        //scale mouse to canvas
        //get canvas size
        float canvasX = (point.x - particleCanvas.transform.position.x) / particleCanvas.transform.localScale.x;
        float canvasY = (point.y - particleCanvas.transform.position.y) / particleCanvas.transform.localScale.y;
        return new Vector3(canvasX, canvasY, 0);
    }

    public static float ScreenToCanvasWidth(float len, Transform canvas)
    {
        return len/canvas.transform.localScale.x;
    }
}
