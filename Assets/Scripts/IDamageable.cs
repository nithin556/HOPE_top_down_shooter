public interface IDamageable
{
    float invincibility_Time { get; set; }
    bool TakeDamage(float damageAmount);
    void Shield(float shield_Duration);
}