/************************************************************************************************************************
 *Name: Justin Katic 
 *Date Created: Thurs April 1 2021
 *Description: Get a refrence for rend to acess the shader change the dissolve value to change the effect of dissolve shader
 *over time.
 *Date Modified: 06/04/2021
 ************************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [Range(0, 1)] public float m_myValue = 0f;

    public GameObject m_surfaceMeshObj;
    public GameObject m_jointsMeshObj;


    SkinnedMeshRenderer m_surfaceRend;
    SkinnedMeshRenderer m_jointsRend;

    public float m_disolveTime;

    public bool m_dissolve;


    private void Start()
    {
        m_surfaceRend = m_surfaceMeshObj.GetComponent<SkinnedMeshRenderer>();
        m_jointsRend = m_jointsMeshObj.GetComponent<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        if (m_dissolve)
        {
            m_myValue += m_disolveTime * Time.deltaTime;
            m_surfaceRend.material.SetFloat("_Amount", m_myValue);
            m_jointsRend.material.SetFloat("_Amount", m_myValue);
        }
    }
}
