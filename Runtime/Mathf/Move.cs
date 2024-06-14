using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.Interaction;
namespace HLVR.Arithmetic 
{
    public class Move : MonoBehaviour
    {

        public bool rightaway;
        Vector3 oriPosition;
        Quaternion oriRotation;
        public float movespeed;
        public float angleSpeed;
        GrabObjects grabObjects;
        float timer;
        bool auto=true;
        Rigidbody rig;
        private void Awake()
        {
            grabObjects = GetComponent<GrabObjects>();
            rig = GetComponent<Rigidbody>();
            oriPosition = transform.position;
            oriRotation = transform.rotation;
        }
        private void OnEnable()
        {
            grabObjects.grabEvent.EnterTouch.AddListener(PauseSyn);
            //grabObjects.grabEvent.EixtTouch.AddListener(StarSyn);
            grabObjects.grabEvent.grabEnter.AddListener(StopSyn);
        }

        private void OnDisable()
        {
            grabObjects.grabEvent.EnterTouch.RemoveListener(PauseSyn);
           
            grabObjects.grabEvent.grabEnter.RemoveListener(StopSyn);
        }
        private void Update()
        {
            if (grabObjects.currentHand == null && transform.position != oriPosition && transform.rotation != oriRotation)
            {
                auto = true;
            }

            if (!rightaway) 
            {
                if (auto)
                {
                    timer += Time.deltaTime;
                    if (timer > 3)
                    {
                        rig.isKinematic = true;
                        bool a = MoveOriginPosition();
                        bool b = RoateOriginRotation();
                        if (a && b)
                        {
                            auto = true;
                        }
                    }
                }
            }
            else if (rightaway)
            {
                if (auto)
                {
                    rig.isKinematic = true;
                    bool a = MoveOriginPosition();
                    bool b = RoateOriginRotation();
                    if (a && b)
                    {
                        auto = true;
                    }
                }
            }


        }

        public void StopSyn ()
        {
            auto = false;
            timer = 0;
        }

        public void PauseSyn()
        {
            auto = false;
        }

        public void StarSyn()
        {
            auto = true;
        }
        bool MoveOriginPosition()
        {
            if (transform.position != oriPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, oriPosition, movespeed * Time.deltaTime);
                return false;
            }
            else 
            {
                return true;
            }
           
        }

        bool RoateOriginRotation()
        {
            if (transform.rotation != oriRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, oriRotation, angleSpeed * Time.deltaTime);
                return false;
            }
            else 
            {
                return true;
            }        
        }

        public void SetTransformOriginPoint()
        {
            rig.isKinematic = true;
            transform.position = oriPosition;
            transform.rotation = oriRotation;
        }
    }
}

