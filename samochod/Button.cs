using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;


namespace samochod
{
    public class Button : Component
    {
        string text;
        public Point cornerPos;
        public Vector2 textPosition;
        public Rectangle hitbox;
        public Rectangle noBezel;
        public int bezelSize = 10;
        Color highlight = new Color (255, 147, 10);
        Color plain = new Color (214, 155, 66);
        Color currentColor;
        public bool hovered { get; set; }
        public Button() { }
        public Button(Point Position, Point Size, string Text) 
        {
            currentColor = plain;
            this.position = Position;
            int x = Text.Length;
            this.size = Size;
            this.text = Text;
            cornerPos = new Point(position.X - size.X / 2, position.Y - size.Y / 2);
            hitbox = new Rectangle(cornerPos, size);
            noBezel = new Rectangle(
                new Point(cornerPos.X+bezelSize, cornerPos.Y+bezelSize),
                new Point(size.X-2*bezelSize, size.Y-2*bezelSize));
            textPosition = new Vector2(
                position.X - x*8,
                position.Y - size.Y/4);

        }
        public override void Update(GameTime gameTime)
        {
            currentColor = plain;
            if (Input.mouseInside(hitbox)) 
            {
                currentColor = highlight;
                if (Input.IsLeftClicked())
                {
                    Game1.gameState = GameState.running;
                }
            }
        }
        public override void Draw(SpriteBatch sb) {

            sb.Draw(textures[2],hitbox,Color.Black);
            sb.Draw(textures[2],noBezel,currentColor);
            sb.DrawString(TextMan.fipps, text, textPosition, Color.Black);
        }


    }
}
