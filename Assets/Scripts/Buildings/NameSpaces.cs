using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�


namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE };  
}
namespace PoliceNS
{
    namespace PolicePathNS
    {
        public struct PolicePath
        {
            public int Behaviour;
            public float Value;

            public PolicePath(int behaviour, float value)
            {
                this.Behaviour = behaviour;
                this.Value = value;
            }
        }
    }

    namespace PoliceStateNS
    {
        public enum PoliceState
        {
            NONE, MOVING, STOP
        };
    }
}