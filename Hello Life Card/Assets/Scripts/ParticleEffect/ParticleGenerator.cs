using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleGenerator : MonoBehaviour
{
    public int minNum;
    public int maxNum;
    public float minSpeed;
    public float maxSpeed;
    public float lifeTime;
    public float rotateSpeed;
    public bool randomColor = false;
    // Start is called before the first frame update
    public GameObject particle;
    void Start()
    {
        
    }

    public void Play()
    {
        int num = Random.Range(minNum, maxNum + 1);
        for(int x = 0; x< num;++x)
        {
            GameObject particleOb = Instantiate(particle, this.transform.position, Quaternion.identity);
            particleOb.transform.SetParent(transform.parent);
            if(randomColor)
                particleOb.GetComponent<Image>().color = new Color(Random.Range(0,1f), Random.Range(0,1f), Random.Range(0,1f), 1);
            float angle = Random.Range(0, 6.28f);
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;
            particleOb.GetComponent<Particle>().Move(direction * Random.Range(minSpeed, maxSpeed), new Vector3(0,0,1)*rotateSpeed, lifeTime);
        }

    }

}
