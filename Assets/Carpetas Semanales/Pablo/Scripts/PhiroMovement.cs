using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}

public enum TipoPlataformas
{
    Plat_Red,
    Plat_Yellow,
    Stairs
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PhiroMovement : MonoBehaviour {
    public static bool grounded = true;
    public static bool onStairs;
    public bool zip_line;
    private Rigidbody2D _phiroRGD;
    private CapsuleCollider2D _phiroCOLL;
    private float colliderSizeBackup, hitDistance;
    private bool finished;


    public bool crouching;
    [Range(500, 5000)]
    public float velocity;
    public float distance_max = 1f;//DISTANCIA MAXIMA
    public float plat_height = 1f;
    float x_axis;

    //ENEMY COLLISION
    public Vector2 knockbackForce = new Vector2(5000, 10000);
    private Vector2 inverseKnockbackForce;
    private bool knockingBack;
    //EffectsOnCollision
    public ParticleSystem hitParticles;

    //DASH
    public DashState dashState;
    public float dashDuration = 0.2f;
    public float dash_cooldown = 1f;
    public float dash_speed;
    private float dash_force = 0;
    private float dash_duration_BK, dash_cooldown_BK;

    //STAIRS
    public bool stairsWithPlatform;
    public float stairs_speed;
    private float stairs_velocity;
    private float gravityStore;

    //LIGHTS
    public int light_counter = 0;
    public bool light_caught;
    public doorLightCounter doorLighCounterRef;

    void Start () {
        hitParticles = Instantiate(hitParticles);
        hitParticles.gameObject.SetActive(false);
        onStairs = false;
        stairsWithPlatform = false;
        light_caught = false;
        _phiroRGD = GetComponent<Rigidbody2D>();
        _phiroCOLL = GetComponent<CapsuleCollider2D>();
        gravityStore = _phiroRGD.gravityScale;
        colliderSizeBackup = _phiroCOLL.size.y;
        dash_duration_BK = dashDuration;
        dash_cooldown_BK = dash_cooldown;
        dashState = DashState.Ready;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !onStairs)
        {
            Crouch();
        }

        if (crouching && Input.GetKeyUp(KeyCode.S))
        {
            StandUp();
        }

        if (onStairs)
        {
            _phiroRGD.gravityScale = 0f;
            stairs_velocity = stairs_speed * Input.GetAxisRaw("Vertical");
            _phiroRGD.velocity = new Vector2(0, stairs_velocity);
        }

        if (!onStairs)
        {
            _phiroRGD.gravityScale = gravityStore;
        }

    }


    void FixedUpdate () {

        if (zip_line)
        {
            Vector3 f = _phiroRGD.transform.forward;
            Debug.Log(f);
            _phiroRGD.velocity = new Vector2(2, 0);
        }
        else
        {
            if (!knockingBack)
            {
                x_axis = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * velocity;
                _phiroRGD.velocity = new Vector2(x_axis + dash_force, _phiroRGD.velocity.y);
            }
        }

        switch (dashState)
        {
            case DashState.Ready:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dash_force = dash_speed;
                    dashState = DashState.Dashing;
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    dash_force = -dash_speed;
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashDuration -= Time.deltaTime;
                if(dashDuration < 0)
                {
                    dash_force = 0;
                    dashDuration = dash_duration_BK;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dash_cooldown -= Time.deltaTime;
                if (dash_cooldown < 0)
                {
                    dash_cooldown = dash_cooldown_BK;
                    dashState = DashState.Ready;
                }
                break;
        }


        if (Input.GetKey(KeyCode.Space) && grounded)//Escalado
        {
            hitDistance = PlatformChecker(distance_max);//HitDistance --> Altura a la que colisiona en Y
            //Debug.Log("Posicion en Y: " + hitDistance);
            if(hitDistance > Mathf.NegativeInfinity)
            {
                StartCoroutine(Climb(hitDistance, plat_height));
            }
            else
            {
                grounded = true;
            }
        }
    }

    private void Crouch()
    {
        crouching = true;
        _phiroCOLL.size = new Vector2(_phiroCOLL.size.x, _phiroCOLL.size.y * 0.75f);
    }
    private void StandUp()
    {
        crouching = false;
        _phiroCOLL.size = new Vector2(_phiroCOLL.size.x, colliderSizeBackup);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "Plat_Red" || collision.gameObject.tag == "Plat_Yellow")
        {
            grounded = true;
        }

        if (collision.gameObject.tag == "zip_line")
        {
            zip_line = true;
        }

        if (collision.gameObject.tag == "light")
        {
            light_counter += 1;
            doorLighCounterRef.setActiveLight(light_counter);
        }
        if(collision.gameObject.tag == "Enemigo")
        {
            Debug.Log("Auch!");
            knockingBack = true;

            ContactPoint2D pointOfCollision = collision.contacts[collision.contacts.Length/2];
            hitParticles.gameObject.SetActive(false);
            Vector2 pointToAddForce = pointOfCollision.point;
            hitParticles.transform.position = pointToAddForce;
            hitParticles.gameObject.SetActive(true);
            if (pointOfCollision.normal.x > 0)
            {
                Debug.Log("Right");
                _phiroRGD.AddForceAtPosition(knockbackForce, pointToAddForce);
                Debug.Log("BackForce: " + knockbackForce);
            }
            else if(pointOfCollision.normal.x < 0)
            {
                Debug.Log("Left");
                inverseKnockbackForce = new Vector2(-knockbackForce.x, knockbackForce.y);
                Debug.Log("BackForce: " + inverseKnockbackForce);
                _phiroRGD.AddForceAtPosition(inverseKnockbackForce, pointToAddForce);
            }
            Vector3 particlePosition = new Vector3(pointToAddForce.x, pointToAddForce.y, -5);
            Debug.Log("Spawn de particulas en: " + particlePosition);
            StartCoroutine(KnockCooldown());

        }

    }

    IEnumerator KnockCooldown()
    {
        float timer = 0.5f;
        while((timer -= Time.deltaTime) > 0)
        {
            knockingBack = true;
            yield return null;
        }
        knockingBack = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            onStairs = true;
        }

        if (collision.gameObject.tag == "up_stair")
        {
            stairsWithPlatform = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            onStairs = false;
        }

        if (collision.gameObject.tag == "up_stair")
        {
            stairsWithPlatform = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "Plat_Red" || collision.gameObject.tag == "Plat_Yellow")
        {
            grounded = false;
        }

        if (collision.gameObject.tag == "zip_line")
        {
            zip_line = false;
        }

        if (collision.gameObject.tag == "light")
        {
            light_caught = false;
        }
    }

   
    private float PlatformChecker(float distance)
    {
        Vector3 posToRay = new Vector3(_phiroRGD.transform.position.x, _phiroRGD.transform.position.y + 1.3f, _phiroRGD.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(posToRay, _phiroRGD.transform.up, distance);
        Debug.DrawLine(posToRay,  hit.point, Color.red,100);
        Debug.Log("ChildPosition: " + posToRay);
        Debug.Log("Direction: " + _phiroRGD.transform.up);
        Debug.Log("Distance: " + distance);
        Debug.DrawLine(posToRay, new Vector3(posToRay.x, posToRay.y + 10, posToRay.z));

        if (hit.collider != null && hit.collider.tag == "Plat_Yellow")
        {
            grounded = false;
            Debug.Log("Ha colisionado");
            return hit.point.y;
        }
        else
        {
            Debug.Log("No ha colisionado");
            return Mathf.NegativeInfinity;
        }
    }
    IEnumerator Climb(float distance, float platformHeight)
    {      
        while (_phiroRGD.transform.position.y - 1 < distance + platformHeight)
        {
            _phiroRGD.gravityScale = -1f;
            _phiroRGD.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            yield return null;
        }
        _phiroRGD.gravityScale = 4f;
        _phiroRGD.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }
}
