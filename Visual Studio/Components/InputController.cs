using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace GrassRendering.Components
{
    class InputController
    {
        public static InputController Instance { get; private set; }
        public static void CreateInstance(Game game)
        {
            if (Created)
            {
                throw new System.Exception("InputController重复构造");
            }
            else
            {
                Instance = new InputController(game);
                Created = true;
            }
        }
        public static bool Created { get; private set; }
        public KeyboardManager Keyboard { get; private set; }
        public MouseManager Mouse { get; private set; }
        private InputController(Game game)
        {
            this.Keyboard = new KeyboardManager(game);
            this.Mouse = new MouseManager(game);
        }
    }
}
