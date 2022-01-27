using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public class PlayerActionInfos
    {
        public GameObject boxMoved;
        public Vector3 boxMovedPosition;
        public Vector3 playerPosition;
    }

    [SerializeField] private float boxSpeed;
    public float BoxSpeed {  get { return boxSpeed; } private set { boxSpeed = value; } }

    [SerializeField]
    private Vector2 maxBoxMovesLimit;

    private Character character;
    private List<PlayerActionInfos> actions;

    void Start()
    {
        character = FindObjectOfType<Character>();

        actions = new List<PlayerActionInfos>();
    }

    public void AddPlayerAction(GameObject boxMoved, Vector3 boxMovedPosition, Vector3 playerPosition)
    {
        PlayerActionInfos newAction = new PlayerActionInfos();

        newAction.boxMoved = boxMoved;
        newAction.boxMovedPosition = boxMovedPosition;
        newAction.playerPosition = playerPosition;

        actions.Add(newAction);
    }

    public void UndoAction()
    {
        if(actions.Count > 0)
        {
            PlayerActionInfos lastAction = actions[actions.Count - 1];

            lastAction.boxMoved.transform.position = lastAction.boxMovedPosition;
            character.transform.position = lastAction.playerPosition;
            character.ResetVelocity();

            actions.RemoveAt(actions.Count - 1);
        }
    }
    private void OnValidate()
    {
        foreach (var movingBoxes in FindObjectsOfType<MovingBox>())
        {
            movingBoxes.Speed = BoxSpeed;
        }
    }

    public bool IsPositionOutOfterrainBounds(Vector3 position)
    {
        return Mathf.Abs(position.x) > maxBoxMovesLimit.x || Mathf.Abs(position.z) > maxBoxMovesLimit.y;
    }
}
