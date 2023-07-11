using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        
    }

    public void PlayAnim(AnimType animType) {

        switch (animType) {
            case AnimType.SMILE:

                animator.SetTrigger("smile");

                break;
            case AnimType.LAUGH:

                animator.SetTrigger("laugh");

                break;
            case AnimType.CRY:

                animator.SetTrigger("cry");

                break;
        }  
    }
}

public enum AnimType {
    SMILE,
    LAUGH,
    CRY
}
