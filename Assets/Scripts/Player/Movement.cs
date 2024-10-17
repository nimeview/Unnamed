using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float boostSpeed = 15f;
    public float boostDuration = 3f;
    public float boostCooldown = 3f;
    public float boostRechargeRate = 0.5f;
    public int maxDashes = 2;
    public float dashCooldown = 0.5f;
    private int remainingDashes;
    private float dashTime;
    private float boostTime;
    private float boostCooldownTime;
    private bool canBoost = true;
    private float dashCooldownTime;

    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingDashes = maxDashes;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTime <= 0 && remainingDashes > 0)
        {
            dashTime = dashDuration;
            remainingDashes--;
            dashCooldownTime = dashCooldown;
        }

        if (Input.GetKey(KeyCode.Space) && canBoost && boostTime <= 0)
        {
            boostTime = boostDuration;
            canBoost = false;
            boostCooldownTime = boostCooldown;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            boostTime = 0;
        }

        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        float currentMoveSpeed = moveSpeed;

        if (dashTime > 0)
        {
            currentMoveSpeed = dashSpeed;
            dashTime -= Time.fixedDeltaTime;
        }
        else if (boostTime > 0)
        {
            currentMoveSpeed = boostSpeed;
            boostTime -= Time.fixedDeltaTime;
        }
        else if (!canBoost)
        {
            boostCooldownTime -= Time.fixedDeltaTime;
            if (boostCooldownTime <= 0)
            {
                canBoost = true;
            }
        }

        if (dashCooldownTime > 0)
        {
            dashCooldownTime -= Time.fixedDeltaTime;
        }
        else if (remainingDashes < maxDashes)
        {
            remainingDashes++;
        }

        rb.MovePosition(rb.position + movement * currentMoveSpeed * Time.fixedDeltaTime);
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}