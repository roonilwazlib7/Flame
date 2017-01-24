using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Flame.Games;
using Flame.Debug;

namespace Cogad
{
    public static class Map
    {
        public static void Create(string map, Game game)
        {
            string[] lines = map.Split('\n');
            DebugConsole.Output("Map", "Creating map...");

            foreach(string line in lines)
            {
                if (line[0] == '#')
                {
                    continue;
                }
                List<string> parts = line.Split(' ').ToList();
                List<string> commandParts = new List<string>();

                for (int i = 1; i < parts.Count; i++)
                {
                    commandParts.Add(parts[i].Replace("\n", "").Replace("\r",""));
                }

                switch(parts[0])
                {
                    case "put":
                        CommandPut(commandParts, game);
                        break;
                    case "seed":
                        CommandSeed(commandParts, game);
                        break;
                }
            }
        }

        public static void CommandPut(List<string> parts, Game game)
        {
            string thingType = parts[0];
            string thingId = parts[1];
            int column = int.Parse(parts[2]);
            int row = int.Parse(parts[3]);

            Cogad cogad = game as Cogad;

            DebugConsole.Output("Map", string.Format("Putting {0} at {1},{2}", thingId, column, row));

            switch(thingType)
            {
                case "building":
                    Building.Generate(game, thingId, column, row);
                    break;
                case "unit":
                    Unit.Generate(game, thingId, column, row);
                    break;
            }
        }

        public static void CommandSeed(List<string> parts, Game game)
        {
            string terrianId = parts[0];
            int column = int.Parse(parts[1]);
            int row = int.Parse(parts[2]);
            int radius = int.Parse(parts[3]);

            Cogad cogad = game as Cogad;

            DebugConsole.Output("Map", string.Format("Seeding {0} at {1},{2}", terrianId, column, row));

            cogad.GameGrid.Seed(column, row, terrianId, radius);
        }
    }
}
