using System.Collections;
using UnityEngine;

public class OpenLink : MonoBehaviour {

    public void link(string link)
    {
        Application.OpenURL(link);
    }

    public void linkDiscord()
    {
        Application.OpenURL("https://discord.gg/FJqkmPc");
    }
    
    public void linkTwitter()
    {
        Application.OpenURL("https://twitter.com/RavenFallGame");
    }

    public void linkYouTube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCz5fez0Nxa7Bv2b5u_kAOpQ");
    }

    public void linkIndieGoGo()
    {
        Application.OpenURL("https://discord.gg/FJqkmPc");
    }

    public void linkTestReport()
    {
        Application.OpenURL("https://discord.gg/FJqkmPc");
    }

}
