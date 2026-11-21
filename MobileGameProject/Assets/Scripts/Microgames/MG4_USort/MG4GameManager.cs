using MicrogameCourse.Framework;
using UnityEngine;

namespace MicrogameCourse.Microgames
{
    public class MG4GameManager : MicrogameBehaviour
    {
        public GameObject slot1; public float s1posX;
        public GameObject slot2; public float s2posX;
        public GameObject slot3; public float s3posX;
        public GameObject slot4; public float s4posX;
        public GameObject slot5; public float s5posX;

        void Start()
        {
            s1posX = slot1.transform.position.x;
            s2posX = slot2.transform.position.x;
            s3posX = slot3.transform.position.x;
            s4posX = slot4.transform.position.x;
            s5posX = slot5.transform.position.x;
        }
    }
}

