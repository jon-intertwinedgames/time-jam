using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.leothelegion.Serializables
{
    [Serializable]
    public class SerializableVector2Int
    {
        [SerializeField]
        int x;
        [SerializeField]
        int y;

        public SerializableVector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public SerializableVector2Int(Vector2Int vec2Int)
        {
            this.x = vec2Int.x;
            this.y = vec2Int.y;
        }

        public Vector2Int ToVector2Int()
        {
            return new Vector2Int(x, y);
        }
    }
}
