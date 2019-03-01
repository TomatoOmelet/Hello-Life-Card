using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingRing : MonoBehaviour
{
    public float width;
    public float shrinkSpeed;
    public float rotateSpeed;
    public float particleRotateSpeed;
    public int particleNum;
    public GameObject particle;
    public Transform canvas;
    private List<GameObject> particleList = new List<GameObject>();
    private List<Vector3> particlePositionList = new List<Vector3>();
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }

    public IEnumerator Play()
    {
        //display all particles
        float interval = 6.28f/particleNum; 
        for(int x = 0; x < particleNum; x++)
        {
            //get screen position
            Vector3 pos = new Vector3(Screen.width/2, Screen.height/2, 0);
            pos += new Vector3(Mathf.Cos(x * interval), Mathf.Sin(x * interval), 0) * width * Screen.width;
            pos = ParticleManager.ScreenToCanvasPoint(pos, canvas);
            pos.z = transform.position.z;
            
            //generate particle
            GameObject parti = Instantiate(particle, pos, Quaternion.identity);
            parti.transform.SetParent(transform, false);
            
            parti.GetComponent<Particle>().Initialize(Vector3.zero, new Vector3(0, 0, particleRotateSpeed));
            //parti.GetComponent<Particle>().Initialize(-pos.normalized * shrinkSpeed, new Vector3(0, 0, particleRotateSpeed));
            particleList.Add(parti);
            particlePositionList.Add(parti.transform.localPosition);
        }
        //start at a random rotation
        transform.Rotate(0, 0, Random.Range(0, 360));
        //move particles towards the center
        float len = width;
        for(float x = len; x > 0; x -= shrinkSpeed)
        {
            for(int index = 0; index < particleList.Count; ++index)
                particleList[index].transform.localPosition = particlePositionList[index] * x/len;
            yield return new WaitForSeconds(0.02f);
        }
        //destroy particles
        foreach(GameObject parti in particleList)
            Destroy(parti.gameObject);
        particleList.Clear();
        particlePositionList.Clear();
    }

}
