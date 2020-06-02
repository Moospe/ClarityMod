using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Wizard_Of_Legend_Clarity_Mod
{
    public class ClarityMod : Partiality.Modloader.PartialityMod
    {

      

        public override void Init()
        {
            base.Init();
            this.ModID = "Clarity";
        }


        public override void OnLoad()
        {
            base.OnLoad();
            On.GameDataManager.LoadInitial += HookLoadInitial;

            

        }


        public static void HookLoadInitial(On.GameDataManager.orig_LoadInitial original)
        {
            original();         
        }

        public static void HookBundleLoad(On.ChaosBundle.orig_LoadBundle origonal)
        {
            origonal();

        }
               





    }
}
