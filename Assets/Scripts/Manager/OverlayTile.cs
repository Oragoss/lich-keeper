using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class OverlayTile : MonoBehaviour
    {
        public int G;
        public int H;
        public int F { get { return G + H; } }

        public bool isBlocked = false;

        public OverlayTile Previous;

        public Vector3Int gridLocation;


        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        HideTile();
        //    }
        //}

        //public void HideTile()
        //{
        //    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //}

    }
}