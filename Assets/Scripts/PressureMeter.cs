using UnityEngine;
using System.Collections;

public class PressureMeter : MonoBehaviour
{
		public static bool pump1 = false;
		public static bool pump2 = false;
		public static bool pump3 = false;
		public static float pump1Power = 0;
		public static float pump2Power = 0;
		public static float pump3Power = 0;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				pump1 = true;
		}

		// Update is called once per frame
		void FixedUpdate ()
		{

		}
}
