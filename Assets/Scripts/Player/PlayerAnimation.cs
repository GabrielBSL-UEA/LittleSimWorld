using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController Controller { get; set; }

    private enum direction
    {
        up,
        down,
        left,
        right
    }

    private direction _currentDirection = direction.down;

    [Header("Main")]
    [SerializeField] private SpriteRenderer playerSprite;

    // Update is called once per frame
    void Update()
    {
        
    }
}
