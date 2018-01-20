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

    private Rigidbody2D _phiroRGD;
    private CapsuleCollider2D _phiroCOLL;
    AudioManager audio;

    [Header("Movement")]
    public bool grounded = true;
    public static bool onStairs;
    public bool zip_line;
    private float colliderSizeBackup, hitDistance;
    private bool finished;
    public bool crouching;
    [Range(500, 5000)]
    public float velocity;
    public float distance_max = 1f;//DISTANCIA MAXIMA
    public float plat_height = 1f;
    float x_axis;
    [Space(10)]

    //ANIMATION
    [Header("Animation Integration")]
    private AnimacionsPhiro anim;
    public bool canClimb, climbing;
    [Space(10)]

    //ENEMY COLLISION
    [Header("Enemy Hit Variables")]
    public Vector2 knockbackForce = new Vector2(5000, 10000);
    private Vector2 inverseKnockbackForce;
    private bool knockingBack;
    public bool hit;
    public bool invencible = false;
    //EffectsOnCollision
    public ParticleSystem hitParticles;
    [Space(10)]

    //DASH
    [Header("Dash Variables")]
    public DashState dashState;
    public float dashDuration = 0.2f;
    public float dash_cooldown = 1f;
    public float dash_speed;
    private float dash_force = 0;
    private float dash_duration_BK, dash_cooldown_BK;
    public ParticleSystem coolDownPs;
    ParticleSystem.MainModule mainP;
    [Space(10)]

    //STAIRS
    [Header("Stair Variables")]
    public bool stairsWithPlatform;
    public float stairs_speed;
    private float stairs_velocity;
    private float gravityStore;
    [Space(10)]

    //LIGHTS
    [Header("Lights")]
    public int light_counter = 0;
    public bool light_caught;
    public doorLightCounter doorLighCounterRef;

    void Start () {
        hitParticles = Instantiate(hitParticles);
        hitParticles.gameObject.SetActive(false);
        mainP = coolDownPs.main;
        climbing = false;
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
        anim = this.GetComponent<AnimacionsPhiro>();
        doorLighCounterRef = GameObject.Find("DoorLightController").GetComponent<doorLightCounter>();
        audio = FindObjectOfType<AudioManager>();



    }
    public void Reset()
    {
        crouching = false;
        knockingBack = false;
        canClimb = false;
        climbing = false;
        //anim.phiroAnims.SetTrigger("idle");
    }
    


    void FixedUpdate () {

        if (invencible)
        {
            Physics2D.IgnoreLayerCollision(8, 9, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(8, 9, false);

        }
        if (zip_line)
        {
            if (anim.GetFacing()) _phiroRGD.velocity = new Vector2(2, 0);
            else _phiroRGD.velocity = new Vector2(-2, 0);
        }
        else
        {
            if (!knockingBack && !anim.GetAgachado())
            {
                x_axis = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * velocity;
                _phiroRGD.velocity = new Vector2(x_axis + dash_force, _phiroRGD.velocity.y);
            }
        }

        switch (dashState)
        {
            case DashState.Ready:
                
                if (Input.GetKey(KeyCode.LeftShift) && !knockingBack)
                {
                    mainP.startColor = new Color(0, 0, 150, 255);
                    if (anim.GetFacing()) dash_force = dash_speed;
                    else dash_force = -dash_speed;

                    dashState = DashState.Dashing;
              
                }
                break;
            case DashState.Dashing:
                dashDuration -= Time.deltaTime;
                invencible = true;
                if (dashDuration < 0)
                {
                    dash_force = 0;
                    if(dashDuration < -1)
                    {
                        invencible = false;
                        
                        dashDuration = dash_duration_BK;
                        dashState = DashState.Cooldown;
                    }

                }
                break;
            case DashState.Cooldown:
                dash_cooldown -= Time.deltaTime;
                if (dash_cooldown-1 < 0)
                {
                    dash_cooldown = dash_cooldown_BK;
                    dashState = DashState.Ready;
                    mainP.startColor = new Color(255, 69, 0, 255);
                }
                break;
        }
    }

    private void LateUpdate()
    {
        hit = false;
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


        if (Input.GetKeyDown(KeyCode.Space) && grounded && !climbing)//Escalado
        {
            hitDistance = PlatformChecker(distance_max);
            if (hitDistance > Mathf.NegativeInfinity)
            {
                anim.phiroAnims.SetTrigger("estirabrazos");
                StartCoroutine(WaitForClimb(hitDistance, plat_height));
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
        _phiroCOLL.size = new Vector2(_phiroCOLL.size.x, _phiroCOLL.size.y * 0.65f); //sirve para cogelar el movimiento de Phiroq
        _phiroRGD.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
    private void StandUp()
    {
        crouching = false;
        _phiroRGD.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        _phiroCOLL.size = new Vector2(_phiroCOLL.size.x, colliderSizeBackup);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "Plat_Red" || collision.gameObject.tag == "Plat_Yellow")
        {
            ContactPoint2D pointOfCollision = collision.contacts[collision.contacts.Length / 2];
            if (pointOfCollision.normal.y > 0.8f)
            {
                Debug.Log("Ground Collision Normal: " + pointOfCollision.normal);
                grounded = true;
                climbing = false;
            }
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
            if (!invencible)
            {
                hit = true;
                Debug.Log("Auch!");
                knockingBack = true;
                audio.Play("Damage");

                ContactPoint2D pointOfCollision = collision.contacts[collision.contacts.Length / 2];
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
                else if (pointOfCollision.normal.x < 0)
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

    }

    public IEnumerator KnockCooldown()
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
            audio.Play("Light");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo" || collision.gameObject.tag == "Plat_Red" || collision.gameObject.tag == "Plat_Yellow")
        {
            grounded = true;
        }
    }


    private float PlatformChecker(float distance)
    {
        Vector3 posToRay = new Vector3(_phiroRGD.transform.position.x, _phiroRGD.transform.position.y + 1.3f, _phiroRGD.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(posToRay, _phiroRGD.transform.up, distance);

        //<DEBUGS>----------------------------------------------------------------------------------------------------
        Debug.DrawLine(posToRay,  hit.point, Color.red,100);
        Debug.Log("ChildPosition: " + posToRay);
        Debug.Log("Direction: " + _phiroRGD.transform.up);
        Debug.Log("Distance: " + distance);
        Debug.DrawLine(posToRay, new Vector3(posToRay.x, posToRay.y + 10, posToRay.z), Color.green, 2f);
        //</DEBUGS>---------------------------------------------------------------------------------------------------
        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !anim.GetAgachado())
        {
            if (hit.collider != null && hit.collider.tag == "Plat_Yellow" &&  hit.collider.tag != "UnderStairs" && !stairsWithPlatform)
            {
                grounded = false;
                canClimb = true;
                climbing = true;
                Debug.Log("Ha colisionado");
                return hit.point.y;

            }
            else
            {
                canClimb = false;
                Debug.Log("No ha colisionado");
                return Mathf.NegativeInfinity;
            }
        }
        canClimb = false;
        return Mathf.NegativeInfinity;

    }
    IEnumerator Climb(float distance, float platformHeight)
    {
        anim.phiroAnims.SetTrigger("subir_plataforma");
        while (_phiroRGD.transform.position.y - 1 < distance + platformHeight)
        {
            invencible = true;
            _phiroRGD.gravityScale = -1f;
            _phiroRGD.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            yield return null;
        }
        _phiroRGD.gravityScale = 4f;
        invencible = false;
        canClimb = false;
        _phiroRGD.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }
    
    public IEnumerator WaitForClimb(float distance, float platformHeight)
    {
        float timer = 0.7f;
        while ((timer -= Time.deltaTime) > 0)
        {
            //Debug.Log("climbing");
            yield return null;
        }
        Debug.Log("HA FINALIZADO WAIT FOR CLIMB");
        StartCoroutine(Climb(distance, platformHeight));
        yield return null;
    }

    public bool GetZip() { return zip_line; }
    public bool GetStairs() { return onStairs; }

}
