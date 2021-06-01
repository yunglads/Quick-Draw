using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera initialAnimation;
    [SerializeField]
    private Camera levelAnimation;
    [SerializeField]
    private Animator mainCamera;

    void Start()
    {
        mainCamera = initialAnimation.GetComponent<Animator>();
        levelAnimation.gameObject.SetActive(false);
    }

    public void DisableCameras()
    {
        initialAnimation.gameObject.SetActive(false);
        levelAnimation.gameObject.SetActive(false);
    }

    public void EnableMenuCamera()
    {
        initialAnimation.gameObject.SetActive(true);
        mainCamera = initialAnimation.GetComponent<Animator>();
    }

    public void EnableLevelCamera()
    {
        levelAnimation.gameObject.SetActive(true);
        mainCamera = levelAnimation.GetComponent<Animator>();
    }

    public bool GetAnimationInfoLevel()
    {
        return mainCamera.GetCurrentAnimatorStateInfo(0).IsTag("LevelBoard");
    } 
    
    public bool GetAnimationInfoShop()
    {
        return mainCamera.GetCurrentAnimatorStateInfo(0).IsTag("Shop");
    } 

    public void LevelButtonAnimation()
    {
        mainCamera.SetBool("levelButtonClicked", true);
        mainCamera.SetBool("backToMenuButtonClicked", false);
        mainCamera.SetBool("menuFromShopClicked", false);
    }

    public void BackMenuButtonAnimation()
    {
        mainCamera.SetBool("backToMenuButtonClicked", true);
        mainCamera.SetBool("levelButtonClicked", false);
    }
    
    public void ShopButtonAnimation()
    {
        mainCamera.SetBool("shopButtonClicked", true);
        mainCamera.SetBool("menuFromShopClicked", false);
        mainCamera.SetBool("backToMenuButtonClicked", false);
    }
    
    public void BackFromShopAnimation()
    {
        mainCamera.SetBool("menuFromShopClicked", true);
        mainCamera.SetBool("shopButtonClicked", false);
    }
}
