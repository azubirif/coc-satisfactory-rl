using System.Transactions;
using UnityEngine;

namespace LemEngine
{
    public static class Utils
    {
        public static Transform GetClosestTransform(Transform origin, Transform[] transforms)
        {
            Transform closestTrans = transforms[0];
            float minDist = (origin.position - closestTrans.position).sqrMagnitude;

            foreach (Transform trans in transforms)
            {
                float currentDist = (origin.position - trans.position).sqrMagnitude;
                if (currentDist < minDist)
                {
                    minDist = currentDist;
                    closestTrans = trans;
                }
            }

            return closestTrans;
        }
    }
}