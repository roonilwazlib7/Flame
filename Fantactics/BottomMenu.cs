using Flame.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Sprites;

namespace Fantactics
{
    class BottomMenu
    {
        private Sprite _background;
        private Sprite[] _abilitySlots;
        private List<Sprite> _contents;
        private Game _game;

        private const int MAX_ABILITIES = 5;
        private const int PADDING = 32;
        private string[] ABILITY_KEYS = new string[] { "Q", "W", "E", "R", "T" };

        public static void Load(Game game)
        {
            game.Assets.LoadTexture("Assets/bottom-menu-background.png", "bottom-menu-background");
        }

        public BottomMenu(Game game)
        {
            _game = game;
            int x = PADDING;
            int y = game.ClientSize.Height - 128 - PADDING;

            _abilitySlots = new Sprite[MAX_ABILITIES];
            _contents = new List<Sprite>();

            _background = new Sprite(game, 0, game.ClientSize.Height - 200);
            _background.BindToTexture("bottom-menu-background");
            _background.Rectangle.Width = game.ClientSize.Width;
            game.Add(_background);
            _contents.Add(_background);
            for (int i = 0; i < MAX_ABILITIES; i++)
            {
                Sprite s = new Sprite(game, x += 128 + PADDING, y);
                s.BindToTexture("Ability-Default");
                game.Add(s);

                _contents.Add(s);
                _abilitySlots[i] = s;
            }

        }

        public void Hide()
        {
            foreach(Sprite s in _contents)
            {
                s.Opacity.Value = 0;
            }
        }

        public void Show()
        {
            foreach (Sprite s in _contents)
            {
                s.Opacity.Value = 1;
            }
        }

        public void SetAbilities(Ability[] abilities)
        {
            int i = 0;
            foreach(Ability ability in abilities)
            {
                if (_game.Assets.TextureExists(ability.Name + "-Icon"))
                {
                    _abilitySlots[i++].BindToTexture(ability.Name + "-Icon");
                }
            }

            for (; i < _abilitySlots.Length; i++)
            {
                _abilitySlots[i].BindToTexture("Ability-Default");
            }
        }
    }
}
