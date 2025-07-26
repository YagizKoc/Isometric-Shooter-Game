using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    private PlayerStats playerStats;
    public Transform cameraTransform;
    public GameObject projectile;
    public GameManager gameManager;
    private float lastFT = -Mathf.Infinity;
    public float weaponRange = 5.0f;
    public float weaponFR = 1.0f; 
    public float mapBoundry = 20.0f;
    private Animator animator;
    private bool backwardsMovement;
    private bool forwardMovement;


    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (gameManager.isGameActive)
        {
            // Hareket

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            bool isIdle = Mathf.Approximately(h, 0f) && Mathf.Approximately(v, 0f);
            if (isIdle) {

                animator.SetInteger("Movement", 0);
                Debug.Log("character is idle");
            }


            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;
            camRight.y = 0f;
            camRight.Normalize();

            Vector3 moveDir = (camForward * v + camRight * h).normalized;
            transform.Translate(moveDir * playerStats.playerMovementSpeed * Time.deltaTime, Space.World);

            Vector3 lookDir = transform.forward;

            if (moveDir.magnitude > 0.1f)
            {
                float angle = Vector3.Angle(lookDir, moveDir);
                if (angle > 90f)
                {
                    Debug.Log("Oyuncu geri geri gidiyor!");
                    animator.SetInteger("Movement", -1);
                    
                }
                else
                {
                    animator.SetInteger("Movement", 1);
                    Debug.Log("Oyuncu ileri gidiyor.");
                }
            }
                /*Buradan başladın
                //forward or backwards ?
                Vector3 moveDir1 = new Vector3(h, 0, v); // input'tan gelen hareket yönü
                moveDir1 = moveDir1.normalized;

                // oyuncunun baktığı yön (örneğin mouse yönü)
                Vector3 lookDir1 = transform.forward;

                // Hareket varsa kontrol et
                if (moveDir1.magnitude > 0.1f)
                {
                    float angle = Vector3.Angle(lookDir1, moveDir1);

                    if (angle > 90f)
                    {
                        Debug.Log("Oyuncu geri geri gidiyor!");
                        backwardsMovement = true;
                        forwardMovement = false;
                        // Burada animasyonu değiştirebilir, farklı yürüyüş efekti vs. oynatabilirsin.
                    }
                    else
                    {
                        forwardMovement = true;
                        backwardsMovement = false;
                        Debug.Log("Oyuncu ileri gidiyor.");
                    }
                }

                //backwards or forward animation check
                if (forwardMovement) {

                    animator.SetBool("forwardMoving", true);
                    animator.SetBool("backwardsMoving", false);
                }
                if (backwardsMovement) {

                    animator.SetBool("backwardsMoving", true);
                    animator.SetBool("forwardMoving", false);
                }
                Buraya kadar */
                //Ateş

                if ((Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastFT + 1f / playerStats.playerAttackSpeed) || (Input.GetKey(KeyCode.Mouse0) && Time.time >= lastFT + 1f / playerStats.playerAttackSpeed))
            {

                Instantiate(projectile, transform.position, transform.rotation);
                lastFT = Time.time;

            }

            // 🧭 Mouse'a dön (isometric kamera için)
            RotateToMouse();

            //Map Boundries
            if (transform.position.x < -playerStats.mapSize)
            {

                Vector3 playerPos = transform.position;
                playerPos.x = -playerStats.mapSize + 0.001f;
                transform.position = playerPos;
            }
            if (transform.position.x > playerStats.mapSize)
            {

                Vector3 playerPos = transform.position;
                playerPos.x = playerStats.mapSize - 0.001f;
                transform.position = playerPos;
            }
            if (transform.position.z > playerStats.mapSize)
            {

                Vector3 playerPos = transform.position;
                playerPos.z = playerStats.mapSize - 0.001f;
                transform.position = playerPos;
            }
            if (transform.position.z < -playerStats.mapSize)
            {

                Vector3 playerPos = transform.position;
                playerPos.z = -playerStats.mapSize + 0.001f;
                transform.position = playerPos;
            }
        }
        

    }

    void RotateToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Mouse pozisyonundan ray gönder
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Yatay düzlem (XZ)

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter); // Ray'in yere çarptığı noktayı al
            Vector3 direction = (hitPoint - transform.position);
            direction.y = 0f; // Sadece yatay eksende döndür
            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime); // yumuşak dönüş
            }
        }
    }
}
