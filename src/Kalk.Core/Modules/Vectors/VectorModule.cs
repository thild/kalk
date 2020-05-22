﻿using System;
using System.Text;
using Scriban.Runtime;
using Scriban.Syntax;

namespace Kalk.Core.Modules
{
    public partial class VectorModule : KalkModuleWithFunctions
    {
        public const string CategoryTypeConstructors = "Type Constructors";
        public const string CategoryVectorTypeConstructors = "Type Vector Constructors";
        public const string CategoryMathVectorMatrixFunctions = "Math Vector/Matrix Functions";
        private static readonly KalkVectorConstructor<int> Int2Constructor = new KalkVectorConstructor<int>(2);
        private static readonly KalkVectorConstructor<int> Int3Constructor = new KalkVectorConstructor<int>(3);
        private static readonly KalkVectorConstructor<int> Int4Constructor = new KalkVectorConstructor<int>(4);
        private static readonly KalkVectorConstructor<int> Int8Constructor = new KalkVectorConstructor<int>(8);
        private static readonly KalkVectorConstructor<int> Int16Constructor = new KalkVectorConstructor<int>(16);
        private static readonly KalkVectorConstructor<bool> Bool2Constructor = new KalkVectorConstructor<bool>(2);
        private static readonly KalkVectorConstructor<bool> Bool3Constructor = new KalkVectorConstructor<bool>(3);
        private static readonly KalkVectorConstructor<bool> Bool4Constructor = new KalkVectorConstructor<bool>(4);
        private static readonly KalkVectorConstructor<bool> Bool8Constructor = new KalkVectorConstructor<bool>(8);
        private static readonly KalkVectorConstructor<bool> Bool16Constructor = new KalkVectorConstructor<bool>(16);
        private static readonly KalkVectorConstructor<float> Float2Constructor = new KalkVectorConstructor<float>(2);
        private static readonly KalkVectorConstructor<float> Float3Constructor = new KalkVectorConstructor<float>(3);
        private static readonly KalkVectorConstructor<float> Float4Constructor = new KalkVectorConstructor<float>(4);
        private static readonly KalkVectorConstructor<float> Float8Constructor = new KalkVectorConstructor<float>(8);
        private static readonly KalkVectorConstructor<float> Float16Constructor = new KalkVectorConstructor<float>(16);
        private static readonly KalkVectorConstructor<double> Double2Constructor = new KalkVectorConstructor<double>(2);
        private static readonly KalkVectorConstructor<double> Double3Constructor = new KalkVectorConstructor<double>(3);
        private static readonly KalkVectorConstructor<double> Double4Constructor = new KalkVectorConstructor<double>(4);
        private static readonly KalkVectorConstructor<double> Double8Constructor = new KalkVectorConstructor<double>(8);
        private static readonly KalkColorRgbConstructor RgbConstructor = new KalkColorRgbConstructor();
        private static readonly KalkColorRgbaConstructor RgbaConstructor = new KalkColorRgbaConstructor();
        private MathModule _mathModule;

        public VectorModule() : base("Vectors")
        {
            IsBuiltin = true;
            RegisterFunctionsAuto();
        }

        protected override void Initialize()
        {
            _mathModule = Engine.GetOrCreateModule<MathModule>();
        }
        
        [KalkDoc("length", CategoryTypeConstructors)]
        public object Length(object x)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (_mathModule == null) throw new InvalidOperationException($"The module {Name} is not initialized.");
            if (x is KalkVector v)
            {
                return _mathModule.Sqrt(new KalkDoubleValue(KalkVector.Dot(v, v)));
            }
            return _mathModule.Abs(new KalkCompositeValue(x));
        }

        [KalkDoc("dot", CategoryTypeConstructors)]
        public static object Dot(KalkVector x, KalkVector y) => KalkVector.Dot(x, y);

        [KalkDoc("cross", CategoryTypeConstructors)]
        public static object Cross(KalkVector x, KalkVector y) => KalkVector.Cross(x, y);


        [KalkDoc("int", CategoryTypeConstructors)]
        public int CreateInt(object value = null) => value == null ? 0 : Engine.ToObject<int>(0, value);

        [KalkDoc("bool", CategoryTypeConstructors)]
        public bool CreateBool(object value = null) => value != null && Engine.ToObject<bool>(0, value);

        [KalkDoc("float", CategoryTypeConstructors)]
        public float CreateFloat(object value = null) => value == null ? 0.0f : Engine.ToObject<float>(0, value);

        [KalkDoc("double", CategoryTypeConstructors)]
        public double CreateDouble(object value = null) => value == null ? 0.0 : Engine.ToObject<double>(0, value);

        [KalkDoc("int2", CategoryTypeConstructors)]
        public KalkVector<int> CreateInt2(params object[] arguments) => Int2Constructor.Invoke(Engine, arguments);

        [KalkDoc("int3", CategoryTypeConstructors)]
        public KalkVector<int> CreateInt3(params object[] arguments) => Int3Constructor.Invoke(Engine, arguments);

        [KalkDoc("int4", CategoryTypeConstructors)]
        public KalkVector<int> CreateInt4(params object[] arguments) => Int4Constructor.Invoke(Engine, arguments);

        [KalkDoc("int8", CategoryTypeConstructors)]
        public KalkVector<int> CreateInt8(params object[] arguments) => Int8Constructor.Invoke(Engine, arguments);

        [KalkDoc("int16", CategoryTypeConstructors)]
        public KalkVector<int> CreateInt16(params object[] arguments) => Int16Constructor.Invoke(Engine, arguments);

        [KalkDoc("bool2", CategoryTypeConstructors)]
        public KalkVector<bool> CreateBool2(params object[] arguments) => Bool2Constructor.Invoke(Engine, arguments);

        [KalkDoc("bool3", CategoryTypeConstructors)]
        public KalkVector<bool> CreateBool3(params object[] arguments) => Bool3Constructor.Invoke(Engine, arguments);

        [KalkDoc("bool4", CategoryTypeConstructors)]
        public KalkVector<bool> CreateBool4(params object[] arguments) => Bool4Constructor.Invoke(Engine, arguments);

        [KalkDoc("bool8", CategoryTypeConstructors)]
        public KalkVector<bool> CreateBool8(params object[] arguments) => Bool8Constructor.Invoke(Engine, arguments);

        [KalkDoc("bool16", CategoryTypeConstructors)]
        public KalkVector<bool> CreateBool16(params object[] arguments) => Bool16Constructor.Invoke(Engine, arguments);

        [KalkDoc("float2", CategoryTypeConstructors)]
        public KalkVector<float> CreateFloat2(params object[] arguments) => Float2Constructor.Invoke(Engine, arguments);

        [KalkDoc("float3", CategoryTypeConstructors)]
        public KalkVector<float> CreateFloat3(params object[] arguments) => Float3Constructor.Invoke(Engine, arguments);

        [KalkDoc("float4", CategoryTypeConstructors)]
        public KalkVector<float> CreateFloat4(params object[] arguments) => Float4Constructor.Invoke(Engine, arguments);

        [KalkDoc("float8", CategoryTypeConstructors)]
        public KalkVector<float> CreateFloat8(params object[] arguments) => Float8Constructor.Invoke(Engine, arguments);

        [KalkDoc("float16", CategoryTypeConstructors)]
        public KalkVector<float> CreateFloat16(params object[] arguments) => Float16Constructor.Invoke(Engine, arguments);

        [KalkDoc("double2", CategoryTypeConstructors)]
        public KalkVector<double> CreateDouble2(params object[] arguments) => Double2Constructor.Invoke(Engine, arguments);

        [KalkDoc("double3", CategoryTypeConstructors)]
        public KalkVector<double> CreateDouble3(params object[] arguments) => Double3Constructor.Invoke(Engine, arguments);

        [KalkDoc("double4", CategoryTypeConstructors)]
        public KalkVector<double> CreateDouble4(params object[] arguments) => Double4Constructor.Invoke(Engine, arguments);

        [KalkDoc("double8", CategoryTypeConstructors)]
        public KalkVector<double> CreateDouble8(params object[] arguments) => Double8Constructor.Invoke(Engine, arguments);

        [KalkDoc("vector", CategoryTypeConstructors)]
        public object CreateVector(ScriptVariable name, int dimension, params object[] arguments)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (dimension <= 1) throw new ArgumentOutOfRangeException(nameof(dimension), "Invalid dimension. Expecting a value > 1.");
            switch (name.Name)
            {
                case "int":
                    switch (dimension)
                    {
                        case 2: return CreateInt2(arguments);
                        case 3: return CreateInt3(arguments);
                        case 4: return CreateInt4(arguments);
                        case 8: return CreateInt8(arguments);
                        case 16: return CreateInt16(arguments);
                    }
                    return new KalkVectorConstructor<int>(dimension).Invoke(Engine, arguments);
                case "bool":
                    switch (dimension)
                    {
                        case 2: return CreateBool2(arguments);
                        case 3: return CreateBool3(arguments);
                        case 4: return CreateBool4(arguments);
                        case 8: return CreateBool8(arguments);
                        case 16: return CreateBool16(arguments);
                    }
                    return new KalkVectorConstructor<bool>(dimension).Invoke(Engine, arguments);

                case "float":
                    switch (dimension)
                    {
                        case 2: return CreateFloat2(arguments);
                        case 3: return CreateFloat3(arguments);
                        case 4: return CreateFloat4(arguments);
                        case 8: return CreateFloat8(arguments);
                        case 16: return CreateFloat16(arguments);
                    }
                    return new KalkVectorConstructor<float>(dimension).Invoke(Engine, arguments);

                case "double":
                    switch (dimension)
                    {
                        case 2: return CreateDouble2(arguments);
                        case 3: return CreateDouble3(arguments);
                        case 4: return CreateDouble4(arguments);
                        case 8: return CreateDouble8(arguments);
                    }
                    return new KalkVectorConstructor<double>(dimension).Invoke(Engine, arguments);
            }

            throw new ArgumentException($"Unsupported vector type {name.Name}. Only bool, int, float and double are supported", nameof(name));
        }


        [KalkDoc("rgb", CategoryTypeConstructors)]
        public KalkColorRgb CreateRgb(params object[] arguments) => (KalkColorRgb)RgbConstructor.Invoke(Engine, arguments);

        [KalkDoc("rgba", CategoryTypeConstructors)]
        public KalkColorRgba CreateRgba(params object[] arguments) => (KalkColorRgba)RgbaConstructor.Invoke(Engine, arguments);
        
        [KalkDoc("colors", KalkEngine.CategoryMisc)]
        public object Colors()
        {
            var colors = KalkColorRgb.GetKnownColors();
            if (Engine.CurrentNode?.Parent?.GetType() != typeof(ScriptExpressionStatement))
            {
                return new ScriptArray(colors);
            }

            var builder = new StringBuilder();
            int count = 0;
            const int PerColumn = 2;
            foreach (var knownColor in colors)
            {
                var colorName = knownColor.ToString("aligned", Engine);
                builder.Append(colorName);
                count++;

                if (count == PerColumn)
                {
                    Engine.WriteHighlightLine(builder.ToString());
                    builder.Clear();
                    count = 0;
                }
                else
                {
                    builder.Append(" ");
                }
            }

            if (builder.Length > 0)
            {
                Engine.WriteHighlightLine(builder.ToString());
                builder.Clear();
            }

            return null;
        }


        // Credits to http://www.chilliant.com/rgb2hsv.html

        [KalkDoc("hue_to_rgb", CategoryMathVectorMatrixFunctions)]
        public KalkVector<float> HUEtoRGB(float hue)
        {
            float R = Math.Abs(hue * 6 - 3) - 1;
            float G = 2 - Math.Abs(hue * 6 - 2);
            float B = 2 - Math.Abs(hue * 6 - 4);
            return new KalkVector<float>(Saturate(R), Saturate(G), Saturate(B));
        }

        [KalkDoc("rgb_to_hsv", CategoryMathVectorMatrixFunctions)]
        public KalkVector<float> RGBtoHCV(KalkVector<float> rgb)
        {
            if (rgb.Length != 3) throw new ArgumentException("Supporting only float3");

            // Based on work by Sam Hocevar and Emil Persson
            var P = (rgb.g < rgb.b) ? float4(rgb.b, rgb.g, -1.0f, 2.0f / 3.0f) : float4(rgb.g, rgb.b, 0.0f, -1.0f / 3.0f);
            var Q = (rgb.r < P.x) ? float4(P.x, P.y, P.w, rgb.r) : float4(rgb.r, P.y, P.z, P.x);
            float C = Q.x - Math.Min(Q.w, Q.y);
            float H = Math.Abs((Q.w - Q.y) / (6 * C + Epsilon) + Q.z);
            return float3(H, C, Q.x);
        }

        const float Epsilon = 1e-10f;

        private static KalkVector<float> float4(float x, float y, float z, float w)
        {
            return new KalkVector<float>(x, y, z, w);
        }

        private static KalkVector<float> float3(float x, float y, float z)
        {
            return new KalkVector<float>(x, y, z);
        }
        
        private static float Saturate(float x) => x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
    }
}