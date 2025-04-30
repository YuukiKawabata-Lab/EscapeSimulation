using System;
using UnityEngine;
using UnityEngine.UI;

namespace DangerArea
{
	public class StatusDangerArea
	{
        GameObject DangerArea;
		GameObject DangerAreaRoad;
		GameObject DangerAreaState;
        float DANGER_SCALE_X;
        float DANGER_SCALE_Y;
        float DANGER_ROAD_SCALE_X;
        float DANGER_ROAD_SCALE_Y;
        float AreaPosition_x;
		float AreaPosition_y;
		float AreaSize;
		float StimulusPosition;
		public string State;
        public bool State_Circle_in;
        public bool State_Squre_in;

        public StatusDangerArea()
        {
            this.DangerArea = GameObject.Find("dangerArea");
			this.DangerAreaRoad = GameObject.Find("dangerAreaRoad");
			this.DangerAreaState = GameObject.Find("danger_area_state");
            this.DANGER_SCALE_X = this.DangerArea.GetComponent<Transform>().localScale.x;
            this.DANGER_SCALE_Y = this.DangerArea.GetComponent<Transform>().localScale.y;
            this.DANGER_ROAD_SCALE_X = this.DangerAreaRoad.GetComponent<Transform>().localScale.x;
            this.DANGER_ROAD_SCALE_Y = this.DangerAreaRoad.GetComponent<Transform>().localScale.y;

            this.UpdateState("IN");
        }

        public void UpdateTransform(float positionX, float positionY, float areaSize, float StimulusPosition)
        ///
        /// Update Danger Area
        /// 
        {
            this.AreaPosition_x = positionX;
			this.AreaPosition_y = positionY;
			this.AreaSize = areaSize;
			this.StimulusPosition = StimulusPosition;
            this.DangerArea.transform.position = new Vector3(this.AreaPosition_x, this.AreaPosition_y);
            this.DangerArea.transform.localScale = new Vector3(this.AreaSize * this.DANGER_SCALE_X, this.AreaSize * this.DANGER_SCALE_Y);
            this.DangerAreaRoad.transform.position = new Vector3((this.StimulusPosition+this.AreaPosition_x)/2,this.AreaPosition_y);
            this.DangerAreaRoad.transform.localScale = new Vector3(Math.Abs(this.StimulusPosition-this.AreaPosition_x)/this.AreaSize,1);
            
			return;
		}
		public void UpdateState(string state)
		{
			this.State = state;
			this.DangerAreaState.GetComponent<Text>().text = this.State;
			return;
        }

        public void UpdateState2In()
        {
            this.UpdateState("IN");
            this.State_Circle_in = true;
            this.State_Squre_in = true;
        }

        public void UpdateStateCircleOut()
        {
            this.State_Circle_in = false;
        }
        public void UpdateStateSqureOut()
        {
            this.State_Squre_in = false;
        }

        public void UpdateState2Out()
        {
            Debug.Log($"Before UpdateState2Out: Circle_in={State_Circle_in}, Squre_in={State_Squre_in}");
            if(!(this.State_Circle_in || this.State_Squre_in))
            {
                this.UpdateState("Out");
            }
            else
            {
                this.UpdateState("IN");
            }
            Debug.Log($"After UpdateState2Out: State={State}, Circle_in={State_Circle_in}, Squre_in={State_Squre_in}");
        }

        public float GetScale()
        {
            return (this.AreaSize * this.DANGER_SCALE_X);
        }
    }
}
