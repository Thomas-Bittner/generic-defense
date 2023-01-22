using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public float Health = 1;

    private float maxHealth = 1;

    [SerializeField]
    private Image HealthBarFill;

    public void Start()
    {
        this.maxHealth = this.Health;

        if (this.HealthBarFill != null)
        {
            this.HealthBarFill.fillAmount = 1;
        }
    }

    public void Damage(float damage)
    {
        this.Health -= damage;

        if (this.HealthBarFill != null)
        {
            var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
            audioPlayer.PlayBuildingDemolishSound();

            this.AdjustHealthBarFill();
        }

        if (this.Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void AdjustHealthBarFill()
    {
        this.HealthBarFill.fillAmount = 1 / this.maxHealth * this.Health;
    }
}
