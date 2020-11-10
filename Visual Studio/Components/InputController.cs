using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace GrassRendering.Components
{
    class InputController
    {
        public static InputController Instance { get { return instance; } }
        private static InputController instance;

        public static bool Created { get { return created; } }
        private static bool created;

        public KeyboardManager Keyboard { get { return keyboard; } }
        private KeyboardManager keyboard;

        public MouseManager Mouse { get { return mouse; } }
        private MouseManager mouse;

        private InputController(Game game)
        {
            this.keyboard = new KeyboardManager(game);
            this.mouse = new MouseManager(game);
        }

        public static void CreateInstance(Game game)
        {
            if (Created)
            {
                throw new System.Exception("InputController重复构造");
            }
            else
            {
                instance = new InputController(game);
                created = true;
            }
        }
    }
}
