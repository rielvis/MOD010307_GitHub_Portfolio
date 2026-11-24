using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace MicrogameCourse.Microgames
{
    public class MG4GameManager : MicrogameBehaviour
    {
        public int score;
        public Text scoreText;
        public SpriteRenderer playAreaSprite;
        public float scoreFeedbackTimer;
        public GameObject[] columns = new GameObject[5];
        public GameObject[] slots = new GameObject[5];
        public int[] dataValues = new int[5];
        public float[] slotsPosX = new float[5];

        [Header("Slots")]
        public float slot1_Slot2_Midpoint;
        public float slot2_Slot3_Midpoint;
        public float slot3_Slot4_Midpoint;
        public float slot4_Slot5_Midpoint;
        public GameObject emptySlot;

        [Header("State of Data")]
        [SerializeField] private bool columnsInOrder;
        private bool columnsHaveBeenReset;

        void Start()
        {
            score = 0;
            emptySlot = null;
            UpdateUI();

            for (int i = 0; i < 5; i++)
            {
                slotsPosX[i] = slots[i].transform.position.x;
                dataValues[i] = i;
            }

            slot1_Slot2_Midpoint = (slotsPosX[0] + slotsPosX[1]) / 2;
            slot2_Slot3_Midpoint = (slotsPosX[1] + slotsPosX[2]) / 2;
            slot3_Slot4_Midpoint = (slotsPosX[2] + slotsPosX[3]) / 2;
            slot4_Slot5_Midpoint = (slotsPosX[3] + slotsPosX[4]) / 2;

            RandomiseColumnOrder();
        }

        void Update()
        {
            if (scoreFeedbackTimer > 0)
            {
                playAreaSprite.color = new Color(0f, 1f, 0f);
                scoreFeedbackTimer -= Time.deltaTime;
            }
            else
            {
                ResetSort();
            }
        }

        private void ColumnSlotAssignmentV2()
        {
            for (int i = 0; i < 5; i++) // For each slot...
            {
                int j = dataValues[i];

                slots[i].GetComponent<SlotController>().column = columns[j].gameObject;
                columns[i].GetComponent<ColumnController>().slot = slots[j].gameObject;
            }
        }

        public void ColumnSlotAssignment(GameObject draggedColumn)
        {
            // Set mutual references of slots and columns based on position
            // Swap references when one column overrides another in a given slot
            // Column positioning is handled by their controllers according to their slot reference, so we just need to handle the references here

            // Called by whichever column is being dragged
            // Before calling, the dragged column assigns it's current slot to the emptySlot variable and moves according to drag input
            // Upon calling, it gets the position of the currently dragged column
            //  Checks if the column is in a specific slot area
            //      Checks whether the slot in the area has a column assigned that isn't the dragged column
            //          If so, it moves the other column and the dragged column's emptySlot to each other...
            //          ...then assigns the dragged column to the specific area slot.

            float columnPosX = draggedColumn.transform.position.x;

            if (columnPosX < slot1_Slot2_Midpoint)
            {
                if (slots[0].GetComponent<SlotController>().column != draggedColumn) 
                {
                    slots[0].GetComponent<SlotController>().column.GetComponent<ColumnController>().slot = emptySlot.gameObject;
                    emptySlot.GetComponent<SlotController>().column = slots[0].GetComponent<SlotController>().column;
                }
                draggedColumn.GetComponent<ColumnController>().slot = slots[0].gameObject;
                slots[0].GetComponent<SlotController>().column = draggedColumn.gameObject;
            }
            else if (columnPosX > slot1_Slot2_Midpoint && columnPosX < slot2_Slot3_Midpoint) // repeats for each slot area, could turn this into a loop?
            {
                if (slots[1].GetComponent<SlotController>().column != draggedColumn)
                {
                    slots[1].GetComponent<SlotController>().column.GetComponent<ColumnController>().slot = emptySlot.gameObject;
                    emptySlot.GetComponent<SlotController>().column = slots[1].GetComponent<SlotController>().column;
                }
                draggedColumn.GetComponent<ColumnController>().slot = slots[1].gameObject;
                slots[1].GetComponent<SlotController>().column = draggedColumn.gameObject;
            }
            else if (columnPosX > slot2_Slot3_Midpoint && columnPosX < slot3_Slot4_Midpoint)
            {
                if (slots[2].GetComponent<SlotController>().column != draggedColumn)
                {
                    slots[2].GetComponent<SlotController>().column.GetComponent<ColumnController>().slot = emptySlot.gameObject;
                    emptySlot.GetComponent<SlotController>().column = slots[2].GetComponent<SlotController>().column;
                }
                draggedColumn.GetComponent<ColumnController>().slot = slots[2].gameObject;
                slots[2].GetComponent<SlotController>().column = draggedColumn.gameObject;
            }
            else if (columnPosX > slot3_Slot4_Midpoint && columnPosX < slot4_Slot5_Midpoint)
            {
                if (slots[3].GetComponent<SlotController>().column != draggedColumn)
                {
                    slots[3].GetComponent<SlotController>().column.GetComponent<ColumnController>().slot = emptySlot.gameObject;
                    emptySlot.GetComponent<SlotController>().column = slots[3].GetComponent<SlotController>().column;
                }
                draggedColumn.GetComponent<ColumnController>().slot = slots[3].gameObject;
                slots[3].GetComponent<SlotController>().column = draggedColumn.gameObject;
            }
            else if (columnPosX > slot4_Slot5_Midpoint)
            {
                if (slots[4].GetComponent<SlotController>().column != draggedColumn)
                {
                    slots[4].GetComponent<SlotController>().column.GetComponent<ColumnController>().slot = emptySlot.gameObject;
                    emptySlot.GetComponent<SlotController>().column = slots[4].GetComponent<SlotController>().column;
                }
                draggedColumn.GetComponent<ColumnController>().slot = slots[4].gameObject;
                slots[4].GetComponent<SlotController>().column = draggedColumn.gameObject;
            }
        }

        public void CheckColumnOrder()
        {
            for (int i = 0; i < 5; i++)
            {
                SlotController currentSlot = slots[i].GetComponent<SlotController>();
                ColumnController currentSlotColumn = currentSlot.column.GetComponent<ColumnController>();
                
                if (currentSlotColumn.dataValue == currentSlot.dataValue)
                {
                    columnsInOrder = true;
                }
                else
                {
                    columnsInOrder = false;
                    break;
                }
            }

            if (columnsInOrder == true)
            {
                Score();
            }
        }

        private void Score()
        {
            scoreFeedbackTimer = 0.5f;
            score += 1;
            UpdateUI();
            columnsHaveBeenReset = false;
        }

        private void RandomiseColumnOrder()
        {            
            for (int i = 0; i < 5; i++) // Set all slot and column references to null
            {
                slots[i].GetComponent<SlotController>().column = null;
                columns[i].GetComponent<ColumnController>().slot = null;
            }

            Shuffle(dataValues);

            for (int i = 0; i < 5; i++) // For each slot...
            {
                int j = dataValues[i];

                slots[i].GetComponent<SlotController>().column = columns[j].gameObject;
                columns[j].GetComponent<ColumnController>().slot = slots[i].gameObject;
            }

            columnsInOrder = false;
            emptySlot = null;
        }

        private void ResetSort()
        {
            playAreaSprite.color = new Color(0f, 0f, 0f);

            if (!columnsHaveBeenReset)
            {
                RandomiseColumnOrder();
                columnsHaveBeenReset = true;
            }
        }

        public static void Shuffle<T>(T[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        private void UpdateUI()
        {
            scoreText.text = $"SCORE: {score}";
        }
    }
}

