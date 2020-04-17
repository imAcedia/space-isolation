using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [System.Serializable]
    struct DropOffArea
    {
        public float y;
        public float h;
        public float drop;

        public bool Contains(Vector2 point)
        {
            if (point.y > y + h) return false;
            if (point.y < y) return false;
            return true;
        }
    }

    [SerializeField] DropOffArea[] dropOffArea;
    [SerializeField] float lowestLevel = 0f;
    [SerializeField] float highestLevel = 10f;

    public float LowestLevel => transform.position.y + lowestLevel;
    public float HighestLevel => transform.position.y + highestLevel;

    public float? GetDropOffLevel(Vector2 pos)
    {
        for (int i = 0; i < dropOffArea.Length; i++)
        {
            DropOffArea dropOff = dropOffArea[i];
            dropOff.y = transform.position.y + dropOff.y;

            if (dropOff.Contains(pos))
                return dropOff.y + dropOff.drop;
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < dropOffArea.Length; i++)
        {
            Rect drawRect = new Rect(0f, dropOffArea[i].y, 1f, dropOffArea[i].h);
            drawRect.width = 1f;
            drawRect.x = 0f;

            drawRect.position = (Vector2)transform.position + drawRect.position + Vector2.left * .5f;
            UnityEditor.Handles.DrawSolidRectangleWithOutline(drawRect, new Color(0f, 1f, 0f, .25f), Color.green);

            Color prevColor = UnityEditor.Handles.color;

            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.DrawLine(drawRect.position + Vector2.up * dropOffArea[i].drop + Vector2.left, drawRect.position + Vector2.up * dropOffArea[i].drop + Vector2.right);

            UnityEditor.Handles.color = prevColor;
        }

        Vector2 drawPos = transform.position;
        drawPos.y += lowestLevel;

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(drawPos + Vector2.left, drawPos + Vector2.right);

        drawPos = transform.position;
        drawPos.y += highestLevel;

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(drawPos + Vector2.left, drawPos + Vector2.right);
    }
}
