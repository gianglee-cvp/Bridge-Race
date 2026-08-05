using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{

    private InputAction moveAction;

    private Vector2 moveAmount;

    private float speed = 5f;
    private bool isMove ; 
    [SerializeField] protected Vector3 boxSize = new Vector3(0.5f, 0.1f, 0.5f); 
    [SerializeField] protected float boxDistance = 0.5f; 
    [SerializeField] protected LayerMask groundLayer;
    public bool IsGrounded { get; private set; }
    protected bool IsFalling = false;
    [SerializeField] private Rigidbody rb;

    public override void OnInit(Vector3 pos)
    {
        base.OnInit(pos);
        moveAction = InputManager.Instance.MoveAction;
        moveAction.Disable();
        rb.position = pos;
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        isMove = false;
        IsFalling = false;
    }
    public override void OnPlay()
    {
        base.OnPlay();
        rb.useGravity = true;
        moveAction.Enable();
    }
    private void Update()
    {
        if(moveAction.enabled){
            moveAmount = moveAction.ReadValue<Vector2>().normalized;
            if (!canMoveUp)
            {
                if(moveAmount.y > 0)
                {
                    moveAmount.y = 0; 
                }
            }
            CheckGround();
    
        }
    }
    private void FixedUpdate()
    {
        if(!moveAction.enabled)
        {
            return;
        }
        Vector3 move = new Vector3(moveAmount.x, 0, moveAmount.y);

        if(move.sqrMagnitude   > 0.01f)
        {
            if (!isMove)
            {
                isMove = true;
                ChangeAnim(AnimatorTrigger.RUN);
            }

            rotatePart.rotation = Quaternion.LookRotation(move);
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime); 
        }
        else
        {
            if (isMove)
            {
                isMove = false;
                ChangeAnim(AnimatorTrigger.IDLE);
            }
        }
    }
    public override void OnFinishLevel()
    {
        base.OnFinishLevel();
        rb.useGravity = false;
        moveAction.Disable();
    }

    public override void ReachNewStage(Stage newStage)
    {
        base.ReachNewStage(newStage);
        SoundManager.Instance.PlaySfx(ENUM_SOUND.ReachStage);
    }
    public override void AddBrick(Brick brick)
    {
        base.AddBrick(brick);
        UIManager.Instance.GetUI<CanvasGamePlay>().UpdateCoin(Point); 
        SoundManager.Instance.PlaySfx(ENUM_SOUND.PickBrick);
    }
    public override void OnWin(int seed)
    {
        base.OnWin(seed);
        GameManager.Instance.ChangeState((int)GameStateType.Win);   
    }
    public override void OnLose()
    {
        base.OnLose();
        GameManager.Instance.ChangeState((int)GameStateType.Lose);
    }
    public void CheckGround()
    {
        Vector3 center = transform.position + Vector3.down * boxDistance; 
        IsGrounded = Physics.CheckBox( center, boxSize * 0.5f, Quaternion.identity, groundLayer );
        bool fall = (!IsGrounded) && (rb.linearVelocity.y < -0.1f);
        if (!IsFalling && fall)
        {
            ChangeAnim(AnimatorTrigger.FALL);
            moveAction.Disable();

        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red; 
        Vector3 center = transform.position + Vector3.down * boxDistance; 
        Gizmos.DrawWireCube(center, boxSize);
    }
}
