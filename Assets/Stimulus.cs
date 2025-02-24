using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace stimulus
{
    public class Stimulus
    {
        GameObject StimulusObj;
        float Flog_Initial_x;
        float Speed = 90f;//cm/s
        float delta_Speed;
        public float TerminalPoint;
        public float TerminalPointEdge;
        int addMoveFrames = -1;//0;
        readonly float TargetPoint_Xaxis;
        float Target_Frame; // Frame at which stimulus movement ends
        public bool NotFinishedSimulation = true;
        float ItrStartPosition;
        float StimulusPosition;
        float MovingDistancePerFrame;
        public float FidDistance;
        public float StartStimulusPosition_X;
        //A mean flog tongue width is 0.578 cm
        readonly float FLOG_TONGUE_WIDTH;
        //Variale of flog tongue size scale
        public float FlogTongScaleX;
        public float FlogTongScaleY;
        // Parameters to make the stimulus a perfect circle in Unity
        readonly float STIMULUS_SCALE_X;
        readonly float STIMULUS_SCALE_Y;
        private float StimulusCenter2EdgeLength; // Distance from the center to the edge of the stimulus
        int wait_loop = 0;
        SpriteRenderer StimulusColor;


        //A predicted crickets' TL is 2.45 cm. A mean flogs' target point is 0.14 TL. 
        public Stimulus(string stimulus = "stimulus_c",float StimulusSpeed = -1f, float CamFps = 200f, float TargetPointX_Axis = 0f, float FlogTongueWidth = 2.205f)  //float FlogTongueWidth = 0.578f, TargetPointX_Axis = 2.45f * 0.142f
        {
            this.StimulusObj = GameObject.Find(stimulus);
            this.FlogTongScaleX = this.FlogTongScaleY = this.FLOG_TONGUE_WIDTH = FlogTongueWidth / 8;
            this.STIMULUS_SCALE_X = this.StimulusObj.GetComponent<Transform>().localScale.x;
            this.STIMULUS_SCALE_Y = this.StimulusObj.GetComponent<Transform>().localScale.y;

            this.StimulusColor = this.StimulusObj.GetComponent<SpriteRenderer>();
            //Predator parameter which calculated from predator-prey experiments
            this.TargetPoint_Xaxis = TargetPointX_Axis;

            //Get a setup value of stimulus mooving speed 
            if (StimulusSpeed == -1f)
            {
                this.Speed = Speed_Input.Set_speed;
            }
            else
            {
                this.Speed = StimulusSpeed;
            }

        
            //Calc moving distances per frames
            this.MovingDistancePerFrame = StimulusSpeed / CamFps;

            //this.StimulusCenter2EdgeLength = this.STIMULUS_SCALE_X / 2f;
            this.StimulusCenter2EdgeLength = this.STIMULUS_SCALE_X / this.STIMULUS_SCALE_X / 2f;

            //A stimulus x cordination which moves to the point
            this.TerminalPoint = this.calcTermianlPoint();
            this.TerminalPointEdge = this.calcTermianlPointEdge();

            this.ItrStartPosition = 2.25f;// 5.85f;//2.25f;
            this.SetStartPosition();
        }

        public void SetStartPosition(float? FID = null)
        {
            float FidFrame;
            if (FID is null){
                FidFrame = calcStartFrame(this.ItrStartPosition);
            }else{
                FidFrame = calcStartFrame((float)FID);
            }
            this.FidDistance = FidFrame * this.MovingDistancePerFrame;
            this.StartStimulusPosition_X = ((FidDistance * -1) - this.FlogTongScaleX / 2) - this.TargetPoint_Xaxis;

            StimulusObj.transform.position = new Vector3(this.StartStimulusPosition_X, 0f, 0f);
            StimulusObj.transform.localScale = new Vector3
                                            (
                                                (this.FlogTongScaleX * this.STIMULUS_SCALE_X), 
                                                (this.FlogTongScaleY * this.STIMULUS_SCALE_Y)
                                            );
            this.PolygonColliderSetAvailability(true);
            this.StimulusColor.color = new Color32(255, 0, 0, 255);
        }

        public bool StimulusMoving()
        {
            this.StimulusPosition = StimulusObj.GetComponent<Transform>().position.x;
            bool output_IsMoving;
            var NextPosition = this.StimulusPosition + this.MovingDistancePerFrame;
            bool IsMoving = Math.Round(this.TerminalPoint,3) >= Math.Round(NextPosition,3);
            if (IsMoving)
            {
                this.StimulusPosition = NextPosition;
                StimulusObj.transform.position = new Vector3(this.StimulusPosition, 0f);
                output_IsMoving = true;
            }
            else
            {
                if (this.wait_loop >= 1)
                {
                    this.PolygonColliderSetDisable();
                    this.StimulusColor.color = new Color32(0, 255, 0, 255);
                    this.wait_loop = 0;
                    output_IsMoving = false;
                }
                else 
                {
                    output_IsMoving = true;
                    this.wait_loop++;
                }
            }
            return output_IsMoving;
        }

        public void SetNextItr()
        {
            // Extend the stimulus endpoint (extend by 0.90 cm (1 frames) twice)
            if (this.addMoveFrames < 6)
            {
                this.addMoveFrames += 1;
            }
            else
            {
                this.addMoveFrames = -1; //0; 
                this.FlogTongScaleX *= 2f;
                this.FlogTongScaleY *= 2f;

                if (this.FlogTongScaleX > this.FLOG_TONGUE_WIDTH * Math.Pow(2f, 5f))
                {
                    this.FlogTongScaleX = this.FLOG_TONGUE_WIDTH;
                    this.FlogTongScaleY = this.FLOG_TONGUE_WIDTH;
                    this.ItrStartPosition += this.MovingDistancePerFrame * 3f;
                    if (this.ItrStartPosition > 8f) // FID starts from 0.45cm*5 and increases to 0.45cm*8, 0.45cm*11, 0.45cm*14, 0.45cm*17
                    {
                        this.NotFinishedSimulation = false;
                    }
                }
            }

            this.TerminalPoint = this.calcTermianlPoint();
            this.TerminalPointEdge = this.calcTermianlPointEdge();
            this.SetStartPosition(this.ItrStartPosition);
        }

        public void PolygonColliderSetDisable()
        {
            this.PolygonColliderSetAvailability(false);
            return;
        }

        float calcStartFrame(float StartPosition)
        {
            double start_frame = StartPosition / this.MovingDistancePerFrame;
            return (float)start_frame;
        }

        float calcTermianlPoint()
        {
            return this.MovingDistancePerFrame * this.addMoveFrames - this.TargetPoint_Xaxis - this.FlogTongScaleX / 2;
            //return this.MovingDistancePerFrame * this.addMoveFrames - this.FlogTongScaleX / 2;
            //return this.MovingDistancePerFrame * this.addMoveFrames - this.TargetPoint_Xaxis - this.StimulusCenter2EdgeLength;
        }
        float calcTermianlPointEdge()
        {
            return this.calcTermianlPoint() + this.FlogTongScaleX / 2;
        }

        void PolygonColliderSetAvailability(bool TrueOrFalse)
        {
            foreach (PolygonCollider2D col in this.StimulusObj.GetComponents<PolygonCollider2D>())
            {
                if (col == null)
                {
                    continue;
                }
                col.enabled = TrueOrFalse;
            }
            return;
        }
    }
}


