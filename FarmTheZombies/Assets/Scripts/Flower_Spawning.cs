using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_Spawning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform Red_Flower;
    [SerializeField] private Transform Yellow_Flower;
    [SerializeField] private Transform Pink_Flower;
    public int numberOfFlowers;
    public int Xmax=1900;
    public int Ymax = 700;
    public int Xmin = -100;
    public int Ymin = -100;

    void Start()
    {
        for(int i = 0; i < numberOfFlowers; i++)
        {
            
            Transform Red= Instantiate(Red_Flower);
            Red.position = new Vector2(Random.Range(Xmin, Xmax), Random.Range(Ymin, Ymax));
            int ScaleRandomValue = Random.Range(50, 110);
            Red.localScale = new Vector2(ScaleRandomValue, ScaleRandomValue);
            Transform Yellow = Instantiate(Yellow_Flower);
            Yellow.position = new Vector2(Random.Range(Xmin, Xmax), Random.Range(Ymin, Ymax));
            Yellow.localScale = new Vector2(ScaleRandomValue/100f, ScaleRandomValue/100f);
            Transform Pink = Instantiate(Pink_Flower);
            Pink.position = new Vector2(Random.Range(Xmin, Xmax), Random.Range(Ymin, Ymax));
            Pink.localScale = new Vector2(ScaleRandomValue, ScaleRandomValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
