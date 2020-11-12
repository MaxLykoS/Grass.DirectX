using SharpDX;

namespace GrassRendering.Components
{
    // Use these namespaces here to override SharpDX.Direct3D11
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Input;

    class ShadowCamera
    {
        #region Properties
        const float SPEED = 5.0f;
        public Vector3 Position { get; private set; }

        public int BackBufferWidth { get; set; }
        public int BackBufferHeight { get; set; }
        #endregion

        #region Public Methods

        public ShadowCamera(float backBufferWidth = 800, float backBufferHeight = 600)
        {
            this.BackBufferWidth = (int)backBufferWidth;
            this.BackBufferHeight = (int)backBufferHeight;
            this.Position = new Vector3(256, 400, 256);
        }

        public void Update()
        {
            KeyboardState _kbs = InputController.Instance.Keyboard.GetState();

            if (_kbs.IsKeyDown(Keys.Up))
                Position += new Vector3(0, 0, 10* SPEED);
            if (_kbs.IsKeyDown(Keys.Down))
                Position += new Vector3(0, 0, -10* SPEED);
            if (_kbs.IsKeyDown(Keys.Left))
                Position += new Vector3(-10* SPEED, 0, 0);
            if (_kbs.IsKeyDown(Keys.Right))
                Position += new Vector3(10* SPEED, 0, 0);
        }

        #endregion
    }
}
