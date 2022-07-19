using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Set In Inspector")]
    public GameObject cloudObject;
    public int numCloudMin = 6;
    public int numCloudMax = 10;
    public Vector3 cloudOffSetScale = new Vector3(5, 2, 1);
    public Vector2 cloudScaleRangeX = new Vector2(4, 8);
    public Vector2 cloudScaleRangeY = new Vector2(3, 4);
    public Vector2 cloudScaleRangeZ = new Vector2(2, 4);
    public float scaleYMin = 2f;

    private List<GameObject> clouds;

    public void Start()
    {
        clouds = new List<GameObject>();

        int num = Random.Range(numCloudMin, numCloudMax);
        for (int i = 0; i < num; i++)
        {
            GameObject CloudOB = Instantiate<GameObject>(cloudObject);
            clouds.Add(CloudOB);
            Transform clTrans = CloudOB.transform;
            clTrans.SetParent(this.transform);

            //Выбираем случайное местоположение
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= cloudOffSetScale.x;
            offset.y *= cloudOffSetScale.y;
            offset.z *= cloudOffSetScale.z;
            clTrans.localPosition = offset;

            //делаем случайные размеры для облаков
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(cloudScaleRangeX.x, cloudScaleRangeX.y);
            scale.y = Random.Range(cloudScaleRangeY.x, cloudScaleRangeY.y);
            scale.z = Random.Range(cloudScaleRangeZ.x, cloudScaleRangeZ.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / cloudOffSetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);

            clTrans.localScale = scale;

        }
    }

    public void Update()
    {
    /* if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }  */                                    
    }

    void Restart() 
    {
        foreach(GameObject cloud in clouds)
        {
            Destroy(cloud);
        }
        Start();
    }
}
