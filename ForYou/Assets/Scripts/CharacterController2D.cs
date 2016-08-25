using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

    // static reference to controller so can be called from other scripts directly (not just through gameobject component)
    public static CharacterController2D cc2d;

    // player controls
    [Range(0.0f, 10.0f)] // create a slider in the editor and set limits on moveSpeed
    public float moveSpeed = 3f;

    [Range(0.0f, 5.0f)]
    public float runMultiplier = 2f;
    
    [Range(0.0f, 1000.0f)]
    public float jumpForce = 600f;

    // player health
    public int playerHealth = 1;

    // LayerMask to determine what is considered ground for the player
    public LayerMask whatIsGround;

    // Transform just below feet for checking if player is grounded
    public Transform groundCheck;

    // player can move?
    // we want this public so other scripts can access it but we don't want to show in editor as it might confuse designer
    [HideInInspector]
    public bool playerCanMove = true;

    // SFXs
    public AudioClip coinSFX;
    public AudioClip deathSFX;
    public AudioClip fallSFX;
    public AudioClip jumpSFX;
    public AudioClip victorySFX;

    // private variables below

    // store references to components on the gameObject
    Transform _transform;
    Rigidbody2D _rigidbody;
    //Animator _animator;
    AudioSource _audio;

    // hold player motion in this timestep
    float _vx;
    float _vy;

    // player tracking
    bool _facingRight = true;
    public bool _isGrounded = false;
    bool _isRunning = false;
    bool _canDoubleJump = false;
    bool ifleftshift = false;

    // store the layer the player is on (setup in Awake)
    int _playerLayer;

    // number of layer that Platforms are on (setup in Awake)
    int _platformLayer;

    // set up mechanic constraints based on level
    bool _canRun = true;
    bool _canJump = true;
    bool _canDJump = true;
    bool _canFly = false;
    private const string level1 = "Level 1";
    private const string level2 = "Level 2";
    private const string level3 = "Level 3";
    private const string level4 = "Level 4";
    private const string level5 = "Level 5";
    private const string level5a = "Level 5a";
    private const string level5b = "Level 5b";
    private const string level6 = "Level 6";
    private const string level6a = "Level 6a";
    private const string level6b = "Level 6b";
    private const string level6c = "Level 6c";



    public string currentLevel;   

    void Awake()
    {
        currentLevel = Application.loadedLevelName;
        // get a reference to the components we are going to be changing and store a reference for efficiency purposes
        _transform = GetComponent<Transform>();

        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null) // if Rigidbody is missing
            Debug.LogError("Rigidbody2D component missing from this gameobject");

        //_animator = GetComponent<Animator>();
        //if (_animator == null) // if Animator is missing
        //    Debug.LogError("Animator component missing from this gameobject");

        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            // let's just add the AudioSource component dynamically
            _audio = gameObject.AddComponent<AudioSource>();
        }

        // determine the player's specified layer
        _playerLayer = this.gameObject.layer;

        // determine the platform's specified layer
        _platformLayer = LayerMask.NameToLayer("Platform");

        //set up mechanic constraints
        switch (currentLevel)
        {
            case level1:
                _canRun = false;
                _canJump = false;
                _canDJump = false;
                break;
            case level2:
                _canRun = false;
                _canDJump = false;
                break;
            case level3:
                _canRun = false;
                _canDJump = false;
                break;
            case level4:
                _canDJump = false;
                break;
            case level5:
                _canDJump = false;
                break;
            case level5a:
                _canRun = false;
                _canDJump = false;
                break;
            case level5b:
                _canRun = false;
                _canDJump = false;
                break;
            case level6:
                _canRun = false;
                _canJump = false;
                _canDJump = false;
                break;
            case level6a:
                _canRun = false;
                _canJump = false;
                _canDJump = false;
                break;
            case level6b:
                _canRun = false;
                _canJump = false;
                _canDJump = false;
                break;
            case level6c:
                _canRun = false;
                _canJump = false;
                _canDJump = false;
                break;

            default:
                break;
        }

    }


    // this is where most of the player controller magic happens each game event loop
    void Update()
    { 
        // exit update if player cannot move or game is paused
        if (!playerCanMove || (Time.timeScale == 0f))
            return;

        // determine horizontal velocity change based on the horizontal input
        _vx = Input.GetAxisRaw("Horizontal");

        // Determine if running based on the horizontal movement
        if (_vx != 0)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }

        // set the running animation state
        //_animator.SetBool("Running", _isRunning);

        // get the current vertical velocity from the rigidbody component
        _vy = _rigidbody.velocity.y;

        // Check to see if character is grounded by raycasting from the middle of the player
        // down to the groundCheck position and see if collected with gameobjects on the
        // whatIsGround layer
        _isGrounded = Physics2D.Linecast(_transform.position, groundCheck.position, whatIsGround);

        // allow double jump after grounded
        if (_isGrounded)
        {
            _canDoubleJump = true;
        }

        // Set the grounded animation states
        //_animator.SetBool("Grounded", _isGrounded);

        // jump only if allowed in level
        if (_canJump)
        { 
            if (_isGrounded && Input.GetButtonDown("Jump")) // If grounded AND jump button pressed, then allow the player to jump
            {
                DoJump();
            }
            else if (_canDoubleJump && Input.GetButtonDown("Jump") && _canDJump)
            {
                DoJump();
                _canDoubleJump = false;
            }
        }
        else if (_canFly)
        {
            if (Input.GetButtonDown("Jump")) 
            {
                DoJump();
            }
        }

        // If the player stops jumping mid jump and player is not yet falling
        // then set the vertical velocity to 0 (he will start to fall from gravity)
        if (Input.GetButtonUp("Jump") && _vy > 0f)
        {
            _vy = 0f;
        }

        // add multiplier for running
        // Change the actual velocity on the rigidbody
        if (_canRun && Input.GetKey(KeyCode.LeftShift))
        {
            _rigidbody.velocity = new Vector2(_vx * moveSpeed * runMultiplier, _vy);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_vx * moveSpeed, _vy);
        }


        // if moving up then don't collide with platform layer
        // this allows the player to jump up through things on the platform layer
        // NOTE: requires the platforms to be on a layer named "Platform"
        Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, (_vy > 0.0f));
    }

    // Checking to see if the sprite should be flipped
    // this is done in LateUpdate since the Animator may override the localScale
    // this code will flip the player even if the animator is controlling scale
    void LateUpdate()
    {
        // get the current scale
        Vector3 localScale = _transform.localScale;

        if (_vx > 0) // moving right so face right
        {
            _facingRight = true;
        }
        else if (_vx < 0)
        { // moving left so face left
            _facingRight = false;
        }

        // check to see if scale x is right for the player
        // if not, multiple by -1 which is an easy way to flip a sprite
        if (((_facingRight) && (localScale.x < 0)) || ((!_facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        // update the scale
        _transform.localScale = localScale;
    }

    // if the player collides with a MovingPlatform, then make it a child of that platform
    // so it will go for a ride on the MovingPlatform
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = other.transform;
        }
    }

    // if the player exits a collision with a moving platform, then unchild it
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }

    // makes player jump
    void DoJump()
    {
        // reset current vertical motion to 0 prior to jump
        _vy = 0f;
        // add a force in the up direction
        _rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        // play the jump sound
        PlaySound(jumpSFX);
    }

    // do what needs to be done to freeze the player
    void FreezeMotion()
    {
        playerCanMove = false;
        _rigidbody.isKinematic = true;
    }

    // do what needs to be done to unfreeze the player
    void UnFreezeMotion()
    {
        playerCanMove = true;
        _rigidbody.isKinematic = false;
    }

    // play sound through the audiosource on the gameobject
    void PlaySound(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    // public function to apply damage to the player
    public void ApplyDamage(int damage)
    {
        if (playerCanMove)
        {
            playerHealth -= damage;

            if (playerHealth <= 0)
            { // player is now dead, so start dying
                PlaySound(deathSFX);
                StartCoroutine(KillPlayer());
            }
        }
    }

    // public function to kill the player when they have a fall death
    public void FallDeath()
    {
        if (playerCanMove)
        {
            playerHealth = 0;
            PlaySound(fallSFX);
            StartCoroutine(KillPlayer());
        }
    }

    // coroutine to kill the player
    IEnumerator KillPlayer()
    {
        if (playerCanMove)
        {
            // freeze the player
            FreezeMotion();

            // play the death animation
           // _animator.SetTrigger("Death");

            // After waiting tell the GameManager to reset the game
            yield return new WaitForSeconds(1.0f);

            if (GameManager.gm) // if the gameManager is available, tell it to reset the game
                GameManager.gm.ResetGame();
            else // otherwise, just reload the current level
                Application.LoadLevel(Application.loadedLevelName);
        }
    }

    // public function on victory over the level
    public void Victory()
    {
        PlaySound(victorySFX);
        FreezeMotion();
        //_animator.SetTrigger("Victory");

        if (GameManager.gm) // do the game manager level compete stuff, if it is available
            GameManager.gm.LevelCompete();
    }

    // public function to respawn the player at the appropriate location
    public void Respawn(Vector3 spawnloc)
    {
        UnFreezeMotion();
        playerHealth = 1;
        _transform.parent = null;
        _transform.position = spawnloc;
        //_animator.SetTrigger("Respawn");
    }

}
