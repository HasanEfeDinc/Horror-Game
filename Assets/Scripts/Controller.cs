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
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private MouseLook m_MouseLook;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    private float m_RunstepLenghten = 0.593f;
    [SerializeField] private float m_StepInterval = 7;
    [SerializeField] AudioSource m_AudioSource;
     private bool m_IsWalking;
     private float m_GravityMultiplier = 2;
     private float fatigue = 0.0f; 
     private float fatigueIncreaseRate = 25f; 
     private float fatigueDecreaseRate = 25f; 
     private bool tired = false;

    private CharacterController m_CharacterController;
    private Rigidbody m_Rigibody;
    private CollisionFlags m_CollisionFlags;
    private Vector3 m_MoveDir = Vector3.zero;
    private Vector2 m_Input;
    
    private float m_StepCycle;
    private float m_NextStep;
    private bool m_PreviouslyGrounded;
    private bool pressedshift;
    



    private void Start()
    {
        m_CharacterController = gameObject.GetComponent<CharacterController>();
        m_Rigibody = gameObject.GetComponent<Rigidbody>();
        m_MouseLook.Init(transform , m_Camera.transform);
    }

    private void Update()
    {
        
        if (!m_CharacterController.isGrounded)
        {
            m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
        }
        
        fatigue = Mathf.Clamp(fatigue, 0.0f, 100.0f);
        RotateView();
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        m_Input = new Vector2(HorizontalInput, VerticalInput);
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
        Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
            m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x*speed;
        m_MoveDir.z = desiredMove.z*speed;
        
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
        
        ProgressStepCycle(speed);
        
        if (m_MoveDir != Vector3.zero)
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
                speed = 5;
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
                speed = 2.5f;
                fatigue -= fatigueDecreaseRate * Time.deltaTime;
            }
            
        }
        else
        {
            speed = 2.5f;
        }

        if (!pressedshift)
        {
            fatigue -= fatigueDecreaseRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pressedshift = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pressedshift = false;
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
    private void ProgressStepCycle(float speedd)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speedd*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                           Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
    }
    
    
}
