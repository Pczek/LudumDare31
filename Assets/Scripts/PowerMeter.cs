using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerMeter : MonoBehaviour
{

		public GameObject needle;
		public Text supplyLabel;
		private float maxAngle = 65f;
		public float delta = 0.2f;
		private float desiredValue = 0f;
		private float currentValue = 0f;
		

		// Use this for initialization
		void Start ()
		{
				// first value is zero
				this.currentValue = this.maxAngle;
				// no desired value available, so it's the same as the current value
				this.desiredValue = this.currentValue;
		}

		public void setDesiredValue (float v)
		{
				this.desiredValue = -1 * ((v * (this.maxAngle*2)) - this.maxAngle);
		}

		void FixedUpdate ()
		{
				// decreasing
				if (this.currentValue < this.desiredValue) {
						this.delta = 0.05f;
				}
				// increasing
				else {
						this.delta = -Mathf.Abs (this.currentValue - this.desiredValue) / 130;
				}

				this.currentValue += this.delta;

				this.needle.transform.eulerAngles = new Vector3 (0f, 0f, this.currentValue);
				float factor = ((-this.currentValue + this.maxAngle) / (2 * this.maxAngle));
				NuclearPowerPlant.setSupply (factor);
				this.audio.pitch = (2.5f * factor)+0.5f;
		}
}
