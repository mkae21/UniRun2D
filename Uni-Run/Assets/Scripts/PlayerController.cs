using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
    private AudioSource playerAudio; // 오디오 재생 컴포넌트
    public AudioClip deathClip; // 사망시 재생할 소리


    public float jumpForce = 700f;
    private int jumpCount = 0; // 누적 점프 횟수
    
    private bool isGrounded = false; // 바닥에 닿았는지 여부
    private bool isDead = false;

    private Rigidbody2D rb;
    private Animator animator; //animator는 컴포넌트로 animation 재생

    private void Start() {
        // 초기화
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update() {
        // 사용자 입력을 감지하고 점프하는 처리
        if(isDead)
            return;
        
        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play(); // 점프 사운드 재생
        }
        else if(Input.GetMouseButtonUp(0) && rb.velocity.y > 0) //마우스 버튼 누르는 시간으로 세기 관리 할 수 있도록
        {
            rb.velocity = rb.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void Die() {
        // 사망 처리
        animator.SetTrigger("Die"); // 애니메이션 트리거 설정
        playerAudio.clip = deathClip;
        playerAudio.Play();

        rb.velocity = Vector2.zero;
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if(other.CompareTag("Dead") && !isDead)
        {
            Die();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        if(collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }

    }

    private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}