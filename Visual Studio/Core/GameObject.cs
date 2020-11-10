using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrassRendering.Core
{
    class GameObject
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        public GameObject()
        {
            this.Position = Vector3.Zero;
            this.Rotation = Vector3.Zero;
            this.Scale = Vector3.One;
        }
        public GameObject(Vector3 _pos)
        {
            this.Position = _pos;
            this.Rotation = Vector3.Zero;
            this.Scale = Vector3.One;
        }
        public Matrix GetWorldMatrix() => Matrix.Identity *
                                          GetTranslationMatrix() *
                                          GetRotationMatrix() *
                                          GetScaleMatrix();
        public Matrix GetTranslationMatrix() => Matrix.Translation(Position.X, Position.Y, Position.Z);
        public Matrix GetRotationMatrix() => Matrix.RotationX(Rotation.X) *
                                             Matrix.RotationY(Rotation.Y) *
                                             Matrix.RotationZ(Rotation.Z);
        public Matrix GetScaleMatrix() => Matrix.Scaling(Scale.X, Scale.Y, Scale.Z);

        public Vector3 Localized(Vector3 _v3)
        {
            Vector4 _v4 = Vector3.Transform(_v3, GetRotationMatrix());
            return new Vector3(_v4.X, _v4.Y, _v4.Z);
        }
    }
}
