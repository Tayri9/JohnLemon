using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Observer : MonoBehaviour
{
    public GameEnding gameEnding;
    public Transform player;
    public float time = 2f;
    public GameObject textExclamacion;

    AudioSource m_AudioSource;
    bool m_IsPlayerInRange;
    float m_timer = 0;
    float m_timerExclamacion = 0;
    bool m_visto;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    m_visto = true;
                    m_timer += Time.deltaTime;

                    //textExclamacion.SetActive(true);
                    if (!m_AudioSource.isPlaying)
                    {
                        m_AudioSource.Play();
                    }

                    if (m_timer >= time)
                    {
                        gameEnding.CaughtPlayer();
                        m_timer = 0;
                        //textExclamacion.SetActive(false);
                    }                    
                }
            }
        }

        if (m_visto)
        {
            textExclamacion.SetActive(true);
            m_timerExclamacion += Time.deltaTime;
            if(m_timerExclamacion >= time)
            {
                textExclamacion.SetActive(false);
                m_visto = false;
                m_timerExclamacion = 0;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            m_timer = 0;
            m_AudioSource.Stop();
            //textExclamacion.SetActive(false);
        }
    }
}
