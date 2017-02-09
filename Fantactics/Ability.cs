using System;
using System.IO;
using System.Collections.Generic;
using Flame.Games;
using Flame.Debug;

namespace Fantactics
{
    class Ability
    {
        public static Dictionary<string,string> Defs = new Dictionary<string,string>();
        public Unit Unit {get; set;}
        //presentation
        public string Name {get; set;}
        public string Icon { get; set; }
        public string Image { get; set; }
        //functionality
        public string Affects {get; set;}
        public string Attack { get; set; }
        public string Defense {get; set;}
        public string Speed { get; set; }
        public string Health { get; set; }
        public string Area { get; set; }
        public string Lasts {get; set;}
        public int Cooldown { get; set; }
        public string Enable { get; set; }
        public string Disable { get; set; }

        public static void Load(Game game)
        {
            foreach(FileInfo file in new DirectoryInfo("Assets/Defs/Abilities").EnumerateFiles("*.abl"))
            {
                string abilityName = "";
                StreamReader r = new StreamReader(file.FullName);
                string abilityDef = r.ReadToEnd();
                r.Close();

                string[] lines = abilityDef.Split('\n');

                foreach(string line in lines)
                {
                    string[] parts = line.Split(' ');

                    if (parts[0] == "NAME")
                    {
                        abilityName = parts[1].Replace("\r", "");
                        if (File.Exists("Assets/Abilities/" + abilityName + "-Icon.png"))
                        {
                            game.Assets.LoadTexture("Assets/Abilities/" + abilityName + "-Icon.png", abilityName + "-Icon");
                        }
                    }
                }

                if (abilityName != "")
                {
                    Defs.Add(abilityName, abilityDef);
                    DebugConsole.Output("Fantactics", "Loaded ability: " + abilityName);
                }

            }

            game.Assets.LoadTexture("Assets/Abilities/Default.png", "Ability-Default");
        }

        public static Ability Create(string name)
        {
            string def = Defs[name];
            Ability ability = new Ability();

            string[] lines = def.Split('\n');

            foreach(string line in lines)
            {
                if ( line == "" || line[0] == '#')
                {
                    continue;
                }

                string[] parts = line.Split(' ');
                string command = parts[0];
                string arg = parts[1];

                HandleCommand(command, arg, ability);
            }

            DebugConsole.Output("Fantactics", "Created Ability: " + name);

            return ability;
        }

        public static void HandleCommand(string command, string arg, Ability ability)
        {
            arg = arg.Replace("\r", "");
            switch(command)
            {
                case "NAME":
                    ability.Name = arg;
                    break;
                case "ICON":
                    ability.Icon = arg.Replace("~", "Assets/Defs/Abilities");
                    break;
                case "IMAGE":
                    ability.Image = arg.Replace("~", "Assets/Defs/Abilities");
                    break;
                case "AFFECTS":
                    ability.Affects = arg;
                    break;
                case "ATTACK":
                    ability.Attack = arg;
                    break;
                case "DEFENSE":
                    ability.Defense = arg;
                    break;
                case "SPEED":
                    ability.Speed = arg;
                    break;
                case "HEALTH":
                    ability.Health = arg;
                    break;
                case "AREA":
                    ability.Health = arg;
                    break;
                case "LASTS":
                    ability.Lasts = arg;
                    break;
                case "COOOLDOWN":
                    ability.Cooldown = int.Parse(arg);
                    break;
                case "ENABLE":
                    ability.Enable = arg;
                    break;
                case "DISABLE":
                    ability.Disable = arg;
                    break;
            }
        }

        public void Activate()
        {
            List<Unit> units = new List<Unit>();
            if (Area != null)
            {
                // area attack
            }
            if (Affects.Contains("SELF"))
            {
                units.Add(Unit);
            }

            foreach(Unit u in units)
            {
                Effect e = new Effect(u);

                e.ApplyAttack(Attack);
                //e.ApplyDeffense(Defense);
            }
        }

    }
}
