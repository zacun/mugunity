using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller; //reference à la classe CharacterController2D
    private float horizontalMove = 0; //permet de stocker la valeur de l'input que le jour va presser (1 pour la droite jusqu'à -1 pour la gauche)
    private bool jumpMove = false; //permet de changer d'état si on saute ou non
    private bool crouchMove = false; //permet de changer d'état si on s'accroupit ou non
    private bool attack;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update appelé une fois par frame, va nous servir pour capter les input du joueur
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal"); //horizontalMove prend la valeur de l'input horizontale du joueur (configuré dans Unity)
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump")) //Si on a un appui sur la touche Jump (configuré dans Unity)
        {
            jumpMove = true;
            animator.SetBool("Jump", true);
        }

        if (Input.GetButtonDown("Crouch")) //Si on a un appui sur la touche Crouch (configuré dans Unity)
        {
            crouchMove = true;
        }
        else if (Input.GetButtonUp("Crouch")) //Si on relache la touche C-rouch
        {
            crouchMove = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            attack = true;
        }
    }

    private void FixedUpdate() //Deplacer physiquement le personnage à l'aide des input capté dans le Update
    {
        controller.Move(horizontalMove, crouchMove, jumpMove); //appel de la methode Move de la classe CharacterController2D
        jumpMove = false;
        attacking();
        resetValues();
    }

    public void onLand()
    {
        animator.SetBool("Jump", false);
    }

    public void onCrouch(bool isCrouching)
    {
        animator.SetBool("Crouch", isCrouching);
    }

    private void attacking()//handleattack
    {
        if (attack)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void resetValues()
    {
        attack = false;
    }
}
