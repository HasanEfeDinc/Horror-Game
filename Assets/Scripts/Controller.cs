using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class Controller : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private float speed = 3;
    [SerializeField] private MouseLook m_MouseLook;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    [SerializeField] private float m_StepInterval;
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float fatigue = 0.0f; 
    [SerializeField] private float fatigueIncreaseRate = 25f; 
    [SerializeField] private float fatigueDecreaseRate = 25f; 
    [SerializeField] private bool tired;

    private CharacterController m_CharacterController;
    private float m_StepCycle;
    private float m_NextStep;
    



    private void Start()
    {
        m_CharacterController = gameObject.GetComponent<CharacterController>();
        m_MouseLook.Init(transform , m_Camera.transform);
    }

    private void Update()
    {
        
        fatigue = Mathf.Clamp(fatigue, 0.0f, 100.0f);
        RotateView();
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 MoveDir = new Vector3(HorizontalInput,0f,VerticalInput);
        ProgressStepCycle(speed,MoveDir);

        transform.Translate(MoveDir*speed*Time.deltaTime);
        
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
            m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        MoveDir = Vector3.ProjectOnPlane(MoveDir, hitInfo.normal).normalized;
        if (MoveDir != Vector3.zero)
        {
            m_IsWalking = true;
        }
        else
        {
            m_IsWalking = false;
        }
        
        //Energy Part
        if(Input.GetKey(KeyCode.LeftShift) && m_IsWalking)
        {
            if (!tired)
            {
                fatigue += fatigueIncreaseRate * Time.deltaTime;
                speed = 6;
            }
            if (fatigue >= 95f)
            {
                tired = true;
            }
            if (fatigue <= 1f)
            {
                tired = false;
            }
            if (tired)
            {
                speed = 3;
                fatigue -= fatigueDecreaseRate * Time.deltaTime;
            }
        }
        else
        {
            speed = 3;
        }
        // Energy Part
    }

    private void FixedUpdate()
    {
        
        m_MouseLook.UpdateCursorLock();
        
        
    }
    
    private void RotateView()
    {
        m_MouseLook.LookRotation (transform, m_Camera.transform);
    }
    
    
    private void PlayFootStepAudio()
    {
        
        int n = UnityEngine.Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }
    private void ProgressStepCycle(float speed, Vector3 Dir)
    {
        if (Dir != Vector3.zero)
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                           Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }
    
    
}
