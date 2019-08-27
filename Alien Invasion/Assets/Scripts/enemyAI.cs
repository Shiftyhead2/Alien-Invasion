using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public Transform Target;
    public float MoveSpeed;
    private float Speed;
    public float StoppingDistance;
    private Rigidbody2D RB;
    private Vector2 Movement;
    public float CoolDown;
    private float FireRate;
    public Transform ShootPoint;
    public LineRenderer LineRenderer;
    public float Damage;



    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Earth").transform;
        RB = GetComponent<Rigidbody2D>();
        Speed = MoveSpeed;
        FireRate = CoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {

            Vector3 direction = Target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            RB.rotation = angle;
            Movement = direction.normalized;
            

            if ((transform.position - Target.position).sqrMagnitude > StoppingDistance * StoppingDistance)
            {
                Speed = MoveSpeed;
            }
            else if ((transform.position - Target.position).sqrMagnitude < StoppingDistance * StoppingDistance)
            {
                FireRate -= Time.deltaTime;
                if (FireRate <= 0f)
                {
                    StartCoroutine(Shoot());
                    
                }
                Speed = 0f;
            }
        }

    }

    private void FixedUpdate()
    {
        MoveCharacter(Movement);
    }

    void MoveCharacter(Vector2 Direction)
    {
        RB.MovePosition((Vector2)transform.position + (Direction * Speed * Time.deltaTime));
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(ShootPoint.position, ShootPoint.right);
        if (hitInfo)
        {
            //Debug.Log(hitInfo.transform.name);
            if (hitInfo.collider.CompareTag("Earth"))
            {
                hitInfo.collider.GetComponent<EarthHealth>().TakeDamage(Damage);
            }else if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<PlayerHealth>().TakeDamage(Damage);
            }
            LineRenderer.SetPosition(0, ShootPoint.position);
            LineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            LineRenderer.SetPosition(0, ShootPoint.position);
            LineRenderer.SetPosition(1, ShootPoint.position + ShootPoint.right * 100);
        }
        
        LineRenderer.enabled = true;

        yield return 0.02f;

        LineRenderer.enabled = false;
        FireRate = CoolDown;

    }

}
