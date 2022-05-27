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
    private string[] conclusionStrings= { "Brak poœwiadczenia kradzie¿y powoduje, ¿e Robb nie mo¿e rozliczyæ siê  urzêdem skarbowym.",
    "Lojer mo¿e w s¹dzie próbowaæ wykazaæ, ¿e zagubienie teczki dla Hodowcy by³o wyj¹tkowo korzystne, poniewa¿ móg³by jednoczeœnie pobraæ ubezpieczenie od kradzie¿y oraz odzyskaæ zgubê, a wiêc zarobi³by na z³odzieju.",
    "Robb Banks nie mo¿e byæ chroniony prawami Karty Z³odzieja w przypadku braku poœwiadczenia kradzie¿y.",
    "Lojer mo¿e udowodniæ ¿e Hodowca przez swoj¹ niepoczytalnoœæ zgubi³ teczkê i nie pamiêta³, ¿e móg³ mieæ tam poœwiadczenie.",
    "Wyjœcie na jaw zaniedbania Robba spowoduje ¿e konszachty ponad prawem Gildii Z³odziei i Policji mog¹ siê posypaæ, co by³oby niekorzystne dla obu stron.",
    "Robbowi Banksowi nie mo¿na by³oby nic zarzuciæ gdyby tylko poœwiadczenie istnia³o.",
    "Dochody Hodowcy Krabów s¹ zbyt niskie aby mo¿na by³o go legalnie okraœæ i jeœli wyjdzie to na jaw, Robb straci jedn¹ ze swoich opcji wolnoœci.",
    "Karta Z³odzieja nie chroni przed porwaniami.",
    "Prowadzenie Hodowli okaza³o siê bardzo nieop³acalne, a mimo to Hodowca kontynuuje swoj¹ pasjê ¿yj¹c w nêdznych warunkach. S¹ to samonakrêcaj¹ce siê spirale.",
    "Œlady walki Kraba mog¹ wskazywaæ na porwanie - czyn nie jest wtedy zakwalifikowany jako kradzie¿.",
    "Lojer mo¿e udowodniæ ¿e Kraba skradziono w celu wykorzystania go do pokarmu z krwi dla wampirów wegan.",
    "Œlady jasno wskazuj¹ na to którêdy przemycono kraba i dok¹d, ale rysowane po drodze serduszka wskazuj¹ na to ¿e krab nie czu³ siê porwany.",
    "Zeznania Kelnerki jasno sugeruj¹, ¿e j¹ i wampira musia³o po³¹czyæ jakieœ gor¹ce uczucie.",
    "Chocia¿ w barze znaleŸæ mo¿na by³o potrawê z kraba, nie jest ona nigdzie oferowana - co wiêcej, bar nie ma nawet na ni¹ koncesji.",
    "Opowieœæ kelnerki i nagranie kompletnie siê ze sob¹ nie klej¹, ale brak szczegó³ów na nagraniu powoduje, ¿e nie mo¿na jednoznacznie tego potwierdziæ.",
    "Wampir móg³ potrzebowaæ posi³ku z krwi kraba przez alergiê na ludzk¹ krew.",
    "Nagrania potwierdzaj¹ wystêpowanie na nich wampira, niestety przez to ¿e jest niewidoczny, nie mo¿na go zidentyfikowaæ.",
    "Wampir móg³ potrzebowaæ posi³ku z krwi kraba przez alergiê na ludzk¹ krew.",
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





