using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float CoolDown;
    private float FireRate;
    public Transform ShootPoint;
    public LineRenderer LineRenderer;
    public float Damage;
    

    private Rigidbody2D myRB;

    Vector2 Movement;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        FireRate = CoolDown;
        
    }

    // Update is called once per frame
    void Update()
    {
       Movement.x = Input.GetAxisRaw("Horizontal");
       Movement.y = Input.GetAxisRaw("Vertical");
       FaceMouse();
        FireRate -= Time.deltaTime;
        if (Input.GetButtonDown("Fire2")){
            if(FireRate <= 0f)
            {
                StartCoroutine(Shoot());
                
            }
            else
            {
                return;
            }
        }
    }

    void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + Movement * moveSpeed * Time.fixedDeltaTime);
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(ShootPoint.position, ShootPoint.up);
        if (hitInfo)
        {
            //Debug.Log(hitInfo.transform.name);
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyHealth>().TakeDamage(Damage, this.transform);
            }
            LineRenderer.SetPosition(0, ShootPoint.position);
            LineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            LineRenderer.SetPosition(0, ShootPoint.position);
            LineRenderer.SetPosition(1, ShootPoint.position + ShootPoint.up * 100);
        }
        
        LineRenderer.enabled = true;
        yield return 0.02f;

        LineRenderer.enabled = false;
        FireRate = CoolDown;


    }

}
