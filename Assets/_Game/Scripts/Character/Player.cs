using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private InputActionAsset inputActions;

    private InputAction moveAction;

    private Vector2 moveAmount;

    private float speed = 5f;
    private float rotateAngle; 

    public override void OnInit()
    {
        base.OnInit();
        
        moveAction = inputActions.FindAction("Move");
        moveAction.Enable();
    }


    private void OnDisable()
    {
        moveAction.Disable();
    }


    private void Update()
    {
        moveAmount = moveAction.ReadValue<Vector2>().normalized;

        // Vector3 move = new Vector3(moveAmount.x, 0, moveAmount.y);
        // //TODO : dùng lerp cho mượt, cân nhắc sửa thành rb moveposition để mượt hơn hi lên dốc và xuống dốc
        // if(move.sqrMagnitude   > 0.01f)
        // {
        // rotatePart.rotation = Quaternion.LookRotation(move);
        // transform.Translate(
        //     move * speed * Time.deltaTime
        // );
        // }

    }
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveAmount.x, 0, moveAmount.y);
        if(move.sqrMagnitude   > 0.01f)
        {
        rotatePart.rotation = Quaternion.LookRotation(move);
        transform.Translate(
            move * speed * Time.fixedDeltaTime
        );
        }
    }
    public override bool CheckCharacterGoUpStair()
    {
        if(rotatePart.forward.z < 0)
        {
            return false;
        }
        return true;
    }
}