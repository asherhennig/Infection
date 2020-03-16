using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this code was made using posted code on http://forum.brackeys.com/thread/how-to-make-a-bullet-effect-for-raycast-shooting-fps/ and ive tired to implement it here
/// </summary>
public class shotgunBullet : MonoBehaviour
{
    private Vector3 m_startPos;
    private Vector3 m_endPos;
    private float m_travelTime;
    private float m_timer;
    public GameObject sgbulletPrefab;

    //moves from the start pos to end pos then destorys gameobject
    private void Update()
    {
        m_timer += Time.deltaTime;
        transform.position = Vector3.Lerp(m_startPos, m_endPos, m_timer / m_travelTime);
        if (m_timer >= m_travelTime) Destroy(gameObject);
    }

    //sets our values to use during updates
    public void SetValues(Vector3 start, Vector3 end, float duration)
    {
        m_startPos = start;
        m_endPos = end;
        m_travelTime = duration;
        m_timer = 0;
    }
}
