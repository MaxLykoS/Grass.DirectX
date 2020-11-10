using SharpDX;
using SharpDX.Toolkit.Input;

namespace GrassRendering.Components
{
    using GrassRendering.Core;
    // Use these namespaces here to override SharpDX.Direct3D11
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Graphics;

    class Camera:GameObject
    {
        #region Fields

        // Camera Movement
        const float RotationSpeed = 0.01f;
        const float MovementSpeed = 2.5f;

        MouseState originalMouseState;

        #endregion

        #region Properties
        public Matrix World { get; private set; }
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public int BackBufferWidth { get; set; }
        public int BackBufferHeight { get; set; }
        #endregion

        #region Public Methods

        public Camera(int backBufferWidth, int backBufferHeight):base(new Vector3(256, 0, 256))
        {
            this.BackBufferWidth = backBufferWidth;
            this.BackBufferHeight = backBufferHeight;

            // Create default camera position
            this.World = Matrix.Identity;

            // Create default camera rotation
            Rotation.X = -MathUtil.Pi / 10.0f;
            Rotation.Y = MathUtil.PiOverTwo; 

            // Calculates the world and the view based on the model size
            this.View = Matrix.LookAtRH(this.Position, new Vector3(0, 0, 0), Vector3.UnitY);
            this.Projection = Matrix.PerspectiveFovRH(0.9f, (float)BackBufferWidth / (float)BackBufferHeight, 0.1f, 10000.0f);

            InputController.Instance.Mouse.SetPosition(new Vector2(0.5f, 0.5f));
            originalMouseState = InputController.Instance.Mouse.GetState();
        }

        public void Update(bool isActive)
        {
            float amount = 0.25f;

            // Handle mouse input
            MouseState currentMouseState = InputController.Instance.Mouse.GetState();
            if (currentMouseState != originalMouseState)
            {
                float xDifference = (currentMouseState.X * BackBufferWidth) - (originalMouseState.X * BackBufferWidth);
                float yDifference = (currentMouseState.Y * BackBufferHeight) - (originalMouseState.Y * BackBufferHeight);
                Rotation.X -= RotationSpeed * yDifference * amount;
                Rotation.Y -= RotationSpeed * xDifference * amount;

                Rotation.X = MathUtil.Clamp(Rotation.X, -1.0f, 1.0f);
                

                if (isActive)
                {
                    InputController.Instance.Mouse.SetPosition(new Vector2(0.5f, 0.5f));
                    UpdateViewMatrix();
                }
            }

            // Handle keyboard input
            Vector3 moveVector = new Vector3(0, 0, 0);
            KeyboardState keyState = InputController.Instance.Keyboard.GetState();

            #region 键盘输入
            if (keyState.IsKeyDown(Keys.W))
                moveVector += new Vector3(0, 0, -1);
            if (keyState.IsKeyDown(Keys.S))
                moveVector += new Vector3(0, 0, 1);                
            if (keyState.IsKeyDown(Keys.D))
                moveVector += new Vector3(1, 0, 0);                
            if (keyState.IsKeyDown(Keys.A))
                moveVector += new Vector3(-1, 0, 0);                
            if (keyState.IsKeyDown(Keys.Space))
                moveVector += new Vector3(0, 1, 0);                
            if (keyState.IsKeyDown(Keys.LeftControl))
                moveVector += new Vector3(0, -1, 0);
            #endregion

            this.Position += MovementSpeed * Localized(moveVector * amount);
            UpdateViewMatrix();
        }

        #endregion

        #region Private Methods

        private void UpdateViewMatrix()
        {
            this.View = Matrix.LookAtRH(this.Position, 
                this.Position + Localized(-Vector3.UnitZ), 
                Localized(Vector3.UnitY));
        }
        #endregion
    }
}
