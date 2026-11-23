using MicrogameCourse.Framework;
using UnityEngine;

namespace MicrogameCourse.Microgames
{
    public class MG4GameManager : MicrogameBehaviour
    {
        [SerializeField] private GameObject[] slots = new GameObject[5];
        [SerializeField] public float[] slotsPosX = new float[5];

        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                slotsPosX[i] = slots[i].transform.position.x;
            }
        }

        public void SwapColumns(GameObject draggedColumn)
        {
            
        }
    }
}

