using Godot;

namespace therorogame.scripts
{
    public class Notification : Object
    {
        public Texture texture;
        public string text;
        public int timeout;

        public Notification(Texture texture, string text, int timeout)
        {
            this.texture = texture;
            this.text = text;
            this.timeout = timeout;
        }
    }
}