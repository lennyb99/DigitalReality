using FMODUnity;
using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private WhackAMoleManager manager;


    [SerializeField]private Animator animator;

    private bool _isActive;
    public bool Active
    {
        get { return _isActive; }
        set { 
            _isActive = value; 
            if (value == true)
            {
                animator.SetBool("Out", true);
            }
            else
            {
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

    private void IsHit()
    {
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
        yield return new WaitForSeconds(Random.Range(manager.GetMoleTime()-1, manager.GetMoleTime()+1));

       // InactivateMole();
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
