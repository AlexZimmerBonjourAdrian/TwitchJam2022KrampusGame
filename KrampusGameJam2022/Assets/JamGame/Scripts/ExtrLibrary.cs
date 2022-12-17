using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraLibrary
{
    public class ExtrLibrary : MonoBehaviour
    {

        public static bool isArrayTransformNull(Object[] a)
        {
            if (a == null)
            {
                return true;
            }
            else
                return false;
        }

        public static bool isArrayemptyTransform(Object[] Array)
        {
          if(!isArrayTransformNull(Array))
            {
                foreach(var a in Array)
                {
                    if(a == null)
                    {
                        return true;
                    }
                }
            }
            return false;
         
        }
    }
}
