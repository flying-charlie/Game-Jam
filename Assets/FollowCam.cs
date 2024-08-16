using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject Follow;
    Transform m_transform;
    public float STICKYNESS;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 target = new Vector3(Follow.transform.position.x, Follow.transform.position.y, m_transform.position.z);
        m_transform.position = Vector3.Lerp(m_transform.position, target, STICKYNESS);
    }
}
