using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EvidenceDisplay : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer Renderer;
    [SerializeField]
    private GameObject Plane;
    public Evidence Evidence;
    private string[] conclusionStrings= { "Brak po�wiadczenia kradzie�y powoduje, �e Robb nie mo�e rozliczy� si�  urz�dem skarbowym.",
    "Lojer mo�e w s�dzie pr�bowa� wykaza�, �e zagubienie teczki dla Hodowcy by�o wyj�tkowo korzystne, poniewa� m�g�by jednocze�nie pobra� ubezpieczenie od kradzie�y oraz odzyska� zgub�, a wi�c zarobi�by na z�odzieju.",
    "Robb Banks nie mo�e by� chroniony prawami Karty Z�odzieja w przypadku braku po�wiadczenia kradzie�y.",
    "Lojer mo�e udowodni� �e Hodowca przez swoj� niepoczytalno�� zgubi� teczk� i nie pami�ta�, �e m�g� mie� tam po�wiadczenie.",
    "Wyj�cie na jaw zaniedbania Robba spowoduje �e konszachty ponad prawem Gildii Z�odziei i Policji mog� si� posypa�, co by�oby niekorzystne dla obu stron.",
    "Robbowi Banksowi nie mo�na by�oby nic zarzuci� gdyby tylko po�wiadczenie istnia�o.",
    "Dochody Hodowcy Krab�w s� zbyt niskie aby mo�na by�o go legalnie okra�� i je�li wyjdzie to na jaw, Robb straci jedn� ze swoich opcji wolno�ci.",
    "Karta Z�odzieja nie chroni przed porwaniami.",
    "Prowadzenie Hodowli okaza�o si� bardzo nieop�acalne, a mimo to Hodowca kontynuuje swoj� pasj� �yj�c w n�dznych warunkach. S� to samonakr�caj�ce si� spirale.",
    "�lady walki Kraba mog� wskazywa� na porwanie - czyn nie jest wtedy zakwalifikowany jako kradzie�.",
    "Lojer mo�e udowodni� �e Kraba skradziono w celu wykorzystania go do pokarmu z krwi dla wampir�w wegan.",
    "�lady jasno wskazuj� na to kt�r�dy przemycono kraba i dok�d, ale rysowane po drodze serduszka wskazuj� na to �e krab nie czu� si� porwany.",
    "Zeznania Kelnerki jasno sugeruj�, �e j� i wampira musia�o po��czy� jakie� gor�ce uczucie.",
    "Chocia� w barze znale�� mo�na by�o potraw� z kraba, nie jest ona nigdzie oferowana - co wi�cej, bar nie ma nawet na ni� koncesji.",
    "Opowie�� kelnerki i nagranie kompletnie si� ze sob� nie klej�, ale brak szczeg��w na nagraniu powoduje, �e nie mo�na jednoznacznie tego potwierdzi�.",
    "Wampir m�g� potrzebowa� posi�ku z krwi kraba przez alergi� na ludzk� krew.",
    "Nagrania potwierdzaj� wyst�powanie na nich wampira, niestety przez to �e jest niewidoczny, nie mo�na go zidentyfikowa�.",
    "Wampir m�g� potrzebowa� posi�ku z krwi kraba przez alergi� na ludzk� krew.",
    "Dowod20"

    };
    
    private void Awake()
    {
        
        Evidence.Layer = 7;
       
            SpriteRender();
        
        if (Evidence.orientation.ToString() == "Vertical")
        {
            Plane.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        SetObjectName();
        ConclusionRender();
    }
    private void SetObjectName()
    {
        transform.name = Evidence.Name;
    }
    void ConclusionRender()
    { 
        foreach(Evidence.IsConectedTo conection  in Evidence.Conections)
        {
           // conection.Conclusion = conclusionStrings[conection.conectNumber-1];
        }
    }
    void SpriteRender()
    {
        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture= Evidence.Artwork.texture;
        
        Renderer.material=material;

    }
 
    
}





