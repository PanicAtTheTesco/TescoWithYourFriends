using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utility
{

    public class UtilityClass : MonoBehaviour
    {
        public static void LineRenderer(GameObject obj,Transform destination, List<Vector3> PathList)
        {
            LineRenderer line;
            bool b = obj.TryGetComponent<LineRenderer>(out line);

            if (b)
            {
                line.positionCount = (int)Vector3.Distance(destination.transform.position, obj.transform.position);

                // set the line positions
                for (int i = 0; i < PathList.Count; i++)
                {
                    line.SetPosition(i, PathList[i]);
                }

            }

        }


        /// <summary>
        /// Use it when you want to clamp vertical rotation (based on the input)
        /// </summary>
        /// <param name="minVal">Minimum Rotate Value</param>
        /// <param name="maxVal">Maximum Rotate Value</param>
        /// <param name="ObjectToRotate">The Vector of the Object to Rotate</param>
        /// <param name="inputVector">Input Vector</param>
        /// <returns></returns>
        public static Vector3 VerticalClampRot(int minVal, int maxVal, Vector3 ObjectToRotate, Vector3 inputVector)
        {
            Vector3 finalRot = new Vector3(0, inputVector.y, 0) + ObjectToRotate;
            finalRot.y = Mathf.Clamp(finalRot.y, minVal, maxVal);

            return finalRot;
        }
        /// <summary>
        /// Use it when you want to clamp vertical rotation
        /// </summary>
        /// <param name="minVal">Minimum Rotate Value</param>
        /// <param name="maxVal">Maximum Rotate Value</param>
        /// <param name="ObjectToRotate">The Vector of the Object to Rotate</param>
        /// <returns></returns>
        public static Vector3 VerticalClampRot(int minVal, int maxVal, Vector3 ObjectToRotate)
        {
            Vector3 finalRot = new Vector3(0, ObjectToRotate.y, 0) + ObjectToRotate;
            finalRot.y = Mathf.Clamp(finalRot.y, minVal, maxVal);

            return finalRot;
        }

        /// <summary>
        /// Use it when you want to slerp a rotaion and the target position(Vector 3/2) is given
        /// </summary>
        /// <param name="targetPos">Target to look at</param>
        /// <param name="obj">the actor who would be looking at the target</param>
        /// <returns></returns>
        public static Quaternion RotateTowardsTargetUsingSlerp(Vector3 targetPos, GameObject obj)
        {
            Quaternion lookRot = Quaternion.LookRotation(targetPos - obj.transform.position);
            Quaternion slerpRot = Quaternion.Slerp(obj.transform.rotation, lookRot, Time.deltaTime);

            return slerpRot;
        }




        
    }
}