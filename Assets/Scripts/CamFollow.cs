using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Vector3 camOffSet = new Vector3 (-7.28472f, 14.14748f, -13.81851f);
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + camOffSet;
    }
}
