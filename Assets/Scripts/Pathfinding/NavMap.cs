﻿using com.leothelegion.Serializables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.leothelegion.Nav
{
    public class NavMap : MonoBehaviour
    {
        SerializableDictionary<Vector2Int, bool> points = new SerializableDictionary<Vector2Int, bool>();
        
        [SerializeField]
        internal Vector2Int mapSize;
        Vector2Int mapSizebuffer;//for graphics

        internal bool isbaked = false;

        static NavMap INSTANCE;

        [SerializeField]
        LayerMask mask;

        private void Start()
        {
            INSTANCE = this;
            Bake(mapSize);
        }

        internal void Bake(Vector2Int mapsize)
        {
            mapSizebuffer = this.mapSize;

            this.mapSize = mapsize;

            points.Clear();
            for (int y = 0; y < mapSize.y; y++)
            {
                for (int x = 0; x < mapSize.x; x++)
                {
                    float xa = x;
                    float ya = y;

                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(xa, ya), Vector2.up, 0.1f,mask);

                    if (hit.collider == null)
                        points.Add(new Vector2Int(x, y), true);
                }
            }
            isbaked = true;
        }
        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            if (!isbaked) return;

            Gizmos.color = Color.blue;
            foreach (var p in points)
            {
                Vector2Int v = p.Key;
                float x = (float)v.x;
                float y = (float)v.y;

                if (p.Value)
                    Gizmos.DrawWireSphere(new Vector2(x, y), 0.1f);
            }
        }

        public static Dictionary<Vector2Int, bool> GetPoints()
        {
            return INSTANCE.points;
        }
    }
}