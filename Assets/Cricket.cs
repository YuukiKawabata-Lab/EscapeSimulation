using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

namespace pray
{
	public class CricketObj
	{
		//Define GameObjects variables
		public GameObject Cricket;
		public GameObject StatusCricket_Text;

		//Initial parameters (Imutable)
		public readonly float CRICKET_INIT_POSITION_X;
		public readonly float CRICKET_INIT_POSITION_Y;
		public readonly string CRICKET_INIT_STATUS = "NotCaught";

		//Mutable parameters
		public string StatusCricket;

		//Constructer
		public CricketObj(string pray = "criket", string status = "State")
		{ 
			//Get a cricket object
			this.Cricket = GameObject.Find(pray);
			//Get a cricket initial position
            this.CRICKET_INIT_POSITION_X = this.Cricket.GetComponent<Transform>().position.x;
			this.CRICKET_INIT_POSITION_Y = this.Cricket.GetComponent<Transform>().position.y;
			//Set a initial status of a cricket
			this.StatusCricket = this.CRICKET_INIT_STATUS;

			//Get a display text of a cricket's status
			this.StatusCricket_Text = GameObject.Find(status);
		}


		//Reset cricket's status and position
		public void ResetCricket()
		{
			this.StatusCricket_Text.GetComponent<Text>().text = this.CRICKET_INIT_STATUS;
			this.Cricket.transform.position = new Vector3(this.CRICKET_INIT_POSITION_X, this.CRICKET_INIT_POSITION_Y, 0f);
			this.Cricket.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}

		//Update cricket's status and position
		public void CriketMoving(int RowNumber, List<List<float>> coordinations)
		{
			//Calculate XY cortdinations
			//Unity's 1 unit = 1 cm = 10 mm
			float X_POSITION = coordinations[0][RowNumber] * 0.1f + this.CRICKET_INIT_POSITION_X;
			float Y_POSITION = coordinations[1][RowNumber] * 0.1f + this.CRICKET_INIT_POSITION_Y;

			//Moving
			this.Cricket.transform.position = new Vector3(X_POSITION, Y_POSITION, 0f);
			//Rotating
			this.Cricket.transform.rotation = Quaternion.Euler(0f, 0f, coordinations[2][RowNumber]);
		}

		public void UpdateCricketStatus(string status)
		{
			this.StatusCricket_Text.GetComponent<Text>().text = status;
		}
	}
}
