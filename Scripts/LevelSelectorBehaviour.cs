using UnityEngine;


public class LevelSelectorBehaviour : MonoBehaviour
{

    [SerializeField] Transform desertPlanet;
    [SerializeField] Transform moon;
    [SerializeField] Transform junglePlanet;
    [SerializeField] Transform icePlanet;
    [SerializeField] Transform lavaPlanet;
    [SerializeField] Transform glowingPlanet;

    [SerializeField] GameObject[] planetLevelMenus;

    private void Awake()
    {
        
    }
  
    public void SelectDesertPlanet()
    {
        transform.position = new Vector3(desertPlanet.position.x, transform.position.y, transform.position.z);
        CheckForActive();
        planetLevelMenus[0].SetActive(true);  
    }

    public void SelectMoon()
    {
        transform.position = new Vector3(moon.position.x, transform.position.y, transform.position.z);
        CheckForActive();
        planetLevelMenus[1].SetActive(true); 
    }

    public void SelectJunglePlanet()
    {
        transform.position = new Vector3(junglePlanet.position.x, transform.position.y, transform.position.z);
        CheckForActive();
        planetLevelMenus[2].SetActive(true);  
    }

    public void SelectIcePlanet()
    {
        transform.position = new Vector3(icePlanet.position.x, transform.position.y, transform.position.z);
        CheckForActive();
        planetLevelMenus[3].SetActive(true); 
    }

    public void SelectLavaPlanet()
    {
        transform.position = new Vector3(lavaPlanet.position.x, transform.position.y, transform.position.z);
        CheckForActive();
        planetLevelMenus[4].SetActive(true);
    }

    public void SelectGlowingPlanet()
    {
        transform.position = new Vector3(glowingPlanet.position.x, transform.position.y, transform.position.z);
        CheckForActive();
        planetLevelMenus[5].SetActive(true);   
    }

    private void CheckForActive()
    {
        foreach (GameObject i in planetLevelMenus)
        {

            if (i.activeInHierarchy)
            {
                i.SetActive(false);
            }

        }
    }
}
