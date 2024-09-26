using BepInEx;
using BepInEx.Configuration;
using MSU.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSUTemplate
{
    /*
     * This is the Config class for our mod, its setup in such a way that it allows us to utilize the Configuration systems from MSU.
     */
    public class MSUTConfig
    {
        //This is a prefix for your mod's configuration files, ideally you'll want to split each "content" type's configuration to allow
        //for ease of management in the future.
        public const string PREFIX = "MSUTemplate.";
        public const string ID_MAIN = PREFIX + "Main";
        public const string ID_ITEM = PREFIX + "Items";
        public const string ID_EQUIPMENT = PREFIX + "Equipments";

        //This is our mod's ConfigFactory, it works both as a Factory for creating ConfiguredVariables and as a Facade to the general
        //Config System as a whole.
        internal static ConfigFactory configFactory { get; private set; }

        public static ConfigFile configMain { get; private set; }
        public static ConfigFile configItems { get; private set; }
        public static ConfigFile configEquipments { get; private set; }

        //This method can be used if you'd like to register your mod onto the Risk of Option's Mod Setting Manager.
        internal static IEnumerator RegisterToModSettingsManager()
        {
            yield break;
        }

        //Constructor for the class, it creates the config factory. You should also create any ConfigFiles here.
        internal MSUTConfig(BaseUnityPlugin bup)
        {
            configFactory = new ConfigFactory(bup, true);
            configMain = configFactory.CreateConfigFile(ID_MAIN, true);
            configItems = configFactory.CreateConfigFile(ID_ITEM, true);
            configEquipments = configFactory.CreateConfigFile(ID_EQUIPMENT, true);
        }
    }
}