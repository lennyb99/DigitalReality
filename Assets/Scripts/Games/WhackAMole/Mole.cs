using FMODUnity;
using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private WhackAMoleManager manager;


    [SerializeField] private Animator animator;
    [SerializeField] private StudioEventEmitter hitMoleSound;
    [SerializeField] private StudioEventEmitter moleInSound;
    [SerializeField] private StudioEventEmitter moleOutSound;
    
    private bool _isActive;
    public bool Active
    {
        get { return _isActive; }
        set { 
            _isActive = value; 
            if (value == true) {
                moleInSound.Play();
                animator.SetBool("Out", true);
            }
            else
            {
                moleOutSound.Play();
                animator.SetBool("Out", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Active) return; 

        if (other.gameObject.tag.Equals("Hammer"))
        {
            IsHit();
        }
    }

    private void IsHit() {
        
        hitMoleSound.Play();
        animator.SetTrigger("Hit");
        
        Active = false;

        manager.IncrementCounter();
    }

    public void ActivateMole()
    {
        Active = true;
        StartCoroutine(RunMoleLife());
    }

    IEnumerator RunMoleLife()
    {
        yield return new WaitForSeconds(Random.Range(manager.moleMinActiveTime, manager.moleMaxActiveTime));

        InactivateMole();
    }

    public void InactivateMole()
    {
        Active = false;
    }

    public void RegisterManager(WhackAMoleManager mgr)
    {
        manager = mgr;
    }

}
