using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MessagePack.Editor
{
    #region base classes
    public abstract class TypeBase
    {
        public readonly bool isValueType;

        protected TypeBase(bool isValueType)
        {
            this.isValueType = isValueType;
        }

        public virtual string GetPropertyName(string fieldName)
        {
            return fieldName;
        }

        public abstract void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member);

        protected void GenerateComments(StringBuilder code, string indent, string comments)
        {
            if (!string.IsNullOrEmpty(comments))
            {
                StringReader commentsReader = new StringReader(comments);
                code.AppendLine(string.Format("{0}/// <summary>", indent));
                while (true)
                {
                    string line = commentsReader.ReadLine();
                    if (line == null) { break; }
                    code.AppendLine(string.Format("{0}/// {1}", indent, line));
                }
                code.AppendLine(string.Format("{0}/// </summary>", indent));
                commentsReader.Close();
            }
        }

    }

    public interface ITypeDecleration
    {
        void AddTypeComments(string comments);
        void GenerateTypeDecleration(StringBuilder code, bool xml, string indent, string sheetName, bool strKey, bool keyToMulti, bool generateToString, bool useProtectedGetMethod);
    }

    public abstract class TypeValueBase : TypeBase
    {

        protected TypeValueBase()
            : base(true) { }

        public override string ToString() { return GetType().Name; }

    }

    public abstract class TypeDirectValueBase : TypeValueBase
    {
        protected abstract string valueIdentifier { get; }

        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            if (isList)
            {
                string listIdentifier = string.Format("System.Collections.Generic.List<{0}>", valueIdentifier);
                code.AppendLine(string.Format("{0}private {1} _{2} = new {1}();", indent, listIdentifier, fieldName));
                GenerateComments(code, indent, comments);
                code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlElement(@\"{1}\")]", indent, fieldName));
                code.AppendLine("#endif");
                code.AppendLine(string.Format("{0}public {1} {2}", indent, listIdentifier, fieldName));
                code.AppendLine(string.Format("{0}{{", indent));
                code.AppendLine(string.Format("{0}\tget {{ return _{1}; }}", indent, fieldName));
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}\tset {{ _{1} = value; }}", indent, fieldName));
                code.AppendLine("#endif");
                code.AppendLine(string.Format("{0}}}", indent));
            }
            else
            {
                if (string.IsNullOrEmpty(defaultVal))
                {
                    defaultVal = string.Format("default({0})", valueIdentifier);
                }
                code.AppendLine(string.Format("{0}private {1} _{2} = {3};", indent, valueIdentifier, fieldName, defaultVal));
                GenerateComments(code, indent, comments);
                code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlAttribute(@\"{1}\")]", indent, fieldName));
                code.AppendLine("#endif");
                code.AppendLine(string.Format("{0}public {1} {2}", indent, valueIdentifier, fieldName));
                code.AppendLine(string.Format("{0}{{", indent));
                code.AppendLine(string.Format("{0}\tget {{ return _{1}; }}", indent, fieldName));
                code.AppendLine(string.Format("{0}\tset {{ _{1} = value; }}", indent, fieldName));
                code.AppendLine(string.Format("{0}}}", indent));
            }
        }

    }

    #endregion base classes

    #region CustomType //for unknown type when parsing
    public sealed class ShellType : TypeBase, ITypeDecleration
    {

        public readonly string typeName;

        private ITypeDecleration mType;
        private string mTypeComments;

        public ShellType(string typeName)
            : base(false)
        {
            this.typeName = typeName;
        }

        public ITypeDecleration type
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
                if (mType != null) { mType.AddTypeComments(mTypeComments); }
            }
        }

        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            if (mType != null && mType is TypeBase)
            {
                (mType as TypeBase).GenerateCode(code, indent, fieldName, isList, defaultVal, comments, member);
            }
        }

        public override string GetPropertyName(string fieldName)
        {
            if (mType != null && mType is TypeBase)
            {
                return (mType as TypeBase).GetPropertyName(fieldName);
            }
            return base.GetPropertyName(fieldName);
        }

        public void AddTypeComments(string comments)
        {
            mTypeComments = comments;
            if (mType != null) { mType.AddTypeComments(mTypeComments); }
        }

        public void GenerateTypeDecleration(StringBuilder code, bool xml, string indent, string sheetName, bool strKey, bool keyToMulti, bool generateToString, bool useProtectedGetMethod)
        {
            if (mType != null) { mType.GenerateTypeDecleration(code, xml, indent, sheetName, strKey, keyToMulti, generateToString, useProtectedGetMethod); }
        }

        public override string ToString()
        {
            if (mType != null) { return mType.ToString(); }
            return base.ToString();
        }

    }
    #endregion CustomType

    #region value types

    #region direct value types
    public sealed class TypeBool : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "bool"; } }
    }

    public sealed class TypeByte : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "byte"; } }
    }

    public sealed class TypeBytes : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "byte[]"; } }
    }

    public sealed class TypeShort : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "short"; } }
    }

    public sealed class TypeUShort : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "ushort"; } }
    }

    public sealed class TypeInt : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "int"; } }
    }

    public sealed class TypeUInt : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "uint"; } }
    }

    public sealed class TypeLong : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "long"; } }
    }

    public sealed class TypeULong : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "ulong"; } }
    }

    public sealed class TypeFloat : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "float"; } }
    }

    public sealed class TypeDouble : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "double"; } }
    }

    public sealed class TypeString : TypeDirectValueBase
    {
        protected override string valueIdentifier { get { return "string"; } }
    }
    #endregion direct value types

    #region unity types

    public sealed class TypeVector2 : TypeValueBase
    {
        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            string saveValueIdentifier = "System.Collections.Generic.List<float>";
            string valueIdentifier = "UnityEngine.Vector2";
            code.AppendLine(string.Format("{0}private {1} _{2}_ = new {1}();", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
            code.AppendLine(string.Format("{0}protected {1} _{2}", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget {{ return _{1}_; }}", indent, fieldName));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#if UNITY_EDITOR");
            code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlAttribute(@\"{1}\")]", indent, fieldName));
            code.AppendLine(string.Format("{0}public string _{1}_X", indent, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x = {2};", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\treturn string.Concat(\"[\", _{1}_x.x, \",\", _{1}_x.y, \"]\");", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x;", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\tif (Linekong.Common.Config.TypeParser.ParseVector2(value, out _{1}_x))", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t\t{1} = _{1}_x;", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t}}", indent));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#endif");
            GenerateComments(code, indent, comments);
            code.AppendLine(string.Format("{0}public {1} {2}", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\tif (_{1}.Count != 2) {{ return new {2}(); }}", indent, fieldName, valueIdentifier));
            code.AppendLine(string.Format("{0}\t\treturn new {1}(_{2}[0], _{2}[1]);", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t_{1}.Clear();", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.x);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.y);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
        }
        public override string GetPropertyName(string fieldName)
        {
            return "_" + fieldName;
        }
    }

    public sealed class TypeVector3 : TypeValueBase
    {
        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            string saveValueIdentifier = "System.Collections.Generic.List<float>";
            string valueIdentifier = "UnityEngine.Vector3";
            code.AppendLine(string.Format("{0}private {1} _{2}_ = new {1}();", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
            code.AppendLine(string.Format("{0}protected {1} _{2}", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget {{ return _{1}_; }}", indent, fieldName));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#if UNITY_EDITOR");
            code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlAttribute(@\"{1}\")]", indent, fieldName));
            code.AppendLine(string.Format("{0}public string _{1}_X", indent, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x = {2};", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\treturn string.Concat(\"[\", _{1}_x.x, \",\", _{1}_x.y, \",\", _{1}_x.z, \"]\");", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x;", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\tif (Linekong.Common.Config.TypeParser.ParseVector3(value, out _{1}_x))", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t\t{1} = _{1}_x;", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t}}", indent));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#endif");
            GenerateComments(code, indent, comments);
            code.AppendLine(string.Format("{0}public {1} {2}", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\tif (_{1}.Count != 3) {{ return new {2}(); }}", indent, fieldName, valueIdentifier));
            code.AppendLine(string.Format("{0}\t\treturn new {1}(_{2}[0], _{2}[1], _{2}[2]);", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t_{1}.Clear();", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.x);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.y);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.z);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
        }
        public override string GetPropertyName(string fieldName)
        {
            return "_" + fieldName;
        }
    }

    public sealed class TypeVector4 : TypeValueBase
    {
        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            string saveValueIdentifier = "System.Collections.Generic.List<float>";
            string valueIdentifier = "UnityEngine.Vector4";
            code.AppendLine(string.Format("{0}private {1} _{2}_ = new {1}();", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
            code.AppendLine(string.Format("{0}protected {1} _{2}", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget {{ return _{1}_; }}", indent, fieldName));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#if UNITY_EDITOR");
            code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlAttribute(@\"{1}\")]", indent, fieldName));
            code.AppendLine(string.Format("{0}public string _{1}_X", indent, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x = {2};", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\treturn string.Concat(\"[\", _{1}_x.x, \",\", _{1}_x.y, \",\", _{1}_x.z, \",\", _{1}_x.w, \"]\");", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x;", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\tif (Linekong.Common.Config.TypeParser.ParseVector4(value, out _{1}_x))", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t\t{1} = _{1}_x;", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t}}", indent));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#endif");
            GenerateComments(code, indent, comments);
            code.AppendLine(string.Format("{0}public {1} {2}", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\tif (_{1}.Count != 4) {{ return new {2}(); }}", indent, fieldName, valueIdentifier));
            code.AppendLine(string.Format("{0}\t\treturn new {1}(_{2}[0], _{2}[1], _{2}[2], _{2}[3]);", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t_{1}.Clear();", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.x);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.y);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.z);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.w);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
        }
        public override string GetPropertyName(string fieldName)
        {
            return "_" + fieldName;
        }
    }

    public sealed class TypeRect : TypeValueBase
    {
        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            string saveValueIdentifier = "System.Collections.Generic.List<float>";
            string valueIdentifier = "UnityEngine.Rect";
            code.AppendLine(string.Format("{0}private {1} _{2}_ = new {1}();", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
            code.AppendLine(string.Format("{0}protected {1} _{2}", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget {{ return _{1}_; }}", indent, fieldName));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#if UNITY_EDITOR");
            code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlAttribute(@\"{1}\")]", indent, fieldName));
            code.AppendLine(string.Format("{0}public string _{1}_X", indent, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x = {2};", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\treturn string.Concat(\"[\", _{1}_x.x, \",\", _{1}_x.y, \",\", _{1}_x.width, \",\", _{1}_x.height, \"]\");", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x;", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\tif (Linekong.Common.Config.TypeParser.ParseRect(value, out _{1}_x))", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t\t{1} = _{1}_x;", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t}}", indent));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#endif");
            GenerateComments(code, indent, comments);
            code.AppendLine(string.Format("{0}public {1} {2}", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\tif (_{1}.Count != 4) {{ return new {2}(); }}", indent, fieldName, valueIdentifier));
            code.AppendLine(string.Format("{0}\t\treturn new {1}(_{2}[0], _{2}[1], _{2}[2], _{2}[3]);", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t_{1}.Clear();", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.x);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.y);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.width);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t_{1}.Add(value.height);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
        }
        public override string GetPropertyName(string fieldName)
        {
            return "_" + fieldName;
        }
    }

    public sealed class TypeColor : TypeValueBase
    {
        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            string saveValueIdentifier = "int";
            string valueIdentifier = "UnityEngine.Color32";
            code.AppendLine(string.Format("{0}private {1} _{2}_ = default({1});", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
            code.AppendLine(string.Format("{0}protected {1} _{2}", indent, saveValueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget {{ return _{1}_; }}", indent, fieldName));
            code.AppendLine(string.Format("{0}\tset {{ _{1}_ = value; }}", indent, fieldName));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#if UNITY_EDITOR");
            code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlAttribute(@\"{1}\")]", indent, fieldName));
            code.AppendLine(string.Format("{0}public string _{1}_X", indent, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x = {2};", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\treturn string.Format(\"{{0:X2}}{{1:X2}}{{2:X2}}{{3:X2}}\", _{1}_x.r, _{1}_x.g, _{1}_x.b, _{1}_x.a);", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t{1} _{2}_x;", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t\tif (Linekong.Common.Config.TypeParser.ParseColor(value, out _{1}_x))", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t\t{1} = _{1}_x;", indent, fieldName));
            code.AppendLine(string.Format("{0}\t\t}}", indent));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
            code.AppendLine("#endif");
            GenerateComments(code, indent, comments);
            code.AppendLine(string.Format("{0}public {1} {2}", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine(string.Format("{0}\tget", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\treturn new {1}((byte)(_{2} >> 24), (byte)(_{2} >> 16), (byte)(_{2} >> 8), (byte)_{2});", indent, valueIdentifier, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}\tset", indent));
            code.AppendLine(string.Format("{0}\t{{", indent));
            code.AppendLine(string.Format("{0}\t\t_{1} = ((int)value.r << 24) | ((int)value.g << 16) | ((int)value.b << 8) | (int)value.a;", indent, fieldName));
            code.AppendLine(string.Format("{0}\t}}", indent));
            code.AppendLine(string.Format("{0}}}", indent));
        }
        public override string GetPropertyName(string fieldName)
        {
            return "_" + fieldName;
        }
    }

    #endregion

    public sealed class TypeEnum : TypeDirectValueBase, ITypeDecleration
    {

        public readonly string enumName;
        private List<string> mEnumItems = new List<string>();
        private List<string> mEnumComments = new List<string>();

        //private Type mType;
        private Dictionary<string, int> mEnumValues = new Dictionary<string, int>();

        private string mTypeComments;

        public TypeEnum(string enumName, bool genDefault)
            : base()
        {
            this.enumName = enumName;
            if (genDefault)
            {
                mEnumItems.Add("Default");
                mEnumComments.Add(null);
            }
        }

        public void AddEnum(string enumItem)
        {
            AddEnum(enumItem, null);
        }
        public void AddEnum(string enumItem, string comments)
        {
            if (!mEnumItems.Contains(enumItem))
            {
                mEnumItems.Add(enumItem);
                mEnumComments.Add(comments);
            }
        }
        public void AddEnum(string enumItem, int enumVal)
        {
            AddEnum(enumItem, null, enumVal);
        }
        public void AddEnum(string enumItem, string comments, int enumVal)
        {
            if (!mEnumItems.Contains(enumItem))
            {
                mEnumItems.Add(enumItem);
                mEnumComments.Add(comments);
                mEnumValues.Add(enumItem, enumVal);
            }
        }

        protected override string valueIdentifier { get { return enumName; } }

        public void AddTypeComments(string comments)
        {
            mTypeComments = comments;
        }

        public void GenerateTypeDecleration(StringBuilder code, bool xml, string indent, string sheetName, bool strKey, bool keyToMulti, bool generateToString, bool useProtectedGetMethod)
        {
            GenerateComments(code, indent, mTypeComments);
            code.AppendLine(string.Format("{0}[ProtoBuf.ProtoContract]", indent));
            if (xml)
            {
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlType(@\"{1}\")]", indent, enumName));
                code.AppendLine("#endif");
            }
            code.AppendLine(string.Format("{0}public enum {1}", indent, enumName));
            code.AppendLine(string.Format("{0}{{", indent));
            int enumValue = 1;
            foreach (KeyValuePair<string, int> kv in mEnumValues)
            {
                enumValue = Math.Max(enumValue, kv.Value + 1);
            }
            for (int i = 0, imax = mEnumItems.Count; i < imax; i++)
            {
                bool last = i + 1 == imax;
                string enumItem = mEnumItems[i];
                int ev;
                if (!mEnumValues.TryGetValue(enumItem, out ev))
                {
                    ev = enumValue;
                    enumValue++;
                }
                GenerateComments(code, indent + "\t", mEnumComments[i]);
                code.AppendLine(string.Format("{0}\t[ProtoBuf.ProtoEnum(Name = @\"{1}\", Value = {2})]", indent, enumItem, ev));
                if (xml)
                {
                    code.AppendLine("#if UNITY_EDITOR");
                    code.AppendLine(string.Format("{0}\t[System.Xml.Serialization.XmlEnum(@\"{1}\")]", indent, enumItem));
                    code.AppendLine("#endif");
                }
                code.AppendLine(string.Format("{0}\t{1} = {2}{3}", indent, enumItem, ev, last ? "" : ","));
                if (!last) { code.AppendLine(); }
            }
            code.AppendLine(string.Format("{0}}}", indent));
        }

    }

    #endregion value types

    public struct CustomTypeField
    {
        public string fieldName;
        public TypeBase fieldType;
        public bool isList;
        public CustomTypeField(string fieldName, TypeBase fieldType, bool isList)
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.isList = isList;
        }
    }

    public sealed class TypeCustom : TypeBase, ITypeDecleration, IEnumerable<CustomTypeField>
    {

        public readonly string typeName;

        public bool isList = false;

        public List<string> mFieldNames = new List<string>();
        public List<TypeBase> mFieldTypes = new List<TypeBase>();
        private List<bool> mFieldList = new List<bool>();
        private List<string> mFieldComments = new List<string>();
        private List<string> mFiledDefaultVals = new List<string>();

        private string mBaseType = null;

        private Dictionary<string, int> mPropertyMembers = new Dictionary<string, int>();

        private List<ITypeDecleration> mSubTypes = new List<ITypeDecleration>();

        private string mTypeComments;

        public TypeCustom(string typeName)
            : base(false)
        {
            this.typeName = typeName;
        }

        public void AddField(string fieldName, TypeBase fieldType, bool isList, string defaultVal, int protoMember)
        {
            AddField(fieldName, fieldType, isList, defaultVal, null, protoMember);
        }
        public void AddField(string fieldName, TypeBase fieldType, bool isList, string defaultVal, string comments, int protoMember)
        {
            AddField(fieldName, fieldType, isList, defaultVal, comments);
            string pmKey = fieldType.GetPropertyName(fieldName);
            mPropertyMembers.Remove(pmKey);
            mPropertyMembers.Add(pmKey, protoMember);
        }
        public void AddField(string fieldName, TypeBase fieldType, bool isList, string defaultVal)
        {
            AddField(fieldName, fieldType, isList, defaultVal, null);
        }
        public void AddField(string fieldName, TypeBase fieldType, bool isList, string defaultVal, string comments)
        {
            if (mFieldNames.Contains(fieldName))
            {
                throw new Exception(string.Format("Type '{0}' has already contained the field '{1}'!", typeName, fieldName));
            }
            mFieldNames.Add(fieldName);
            mFieldTypes.Add(fieldType);
            mFieldList.Add(isList);
            mFieldComments.Add(comments);
            mFiledDefaultVals.Add(defaultVal);
        }

        public void SetTypeExtend(string baseType)
        {
            mBaseType = baseType;
        }

        public TypeBase GetFieldType(string fieldName)
        {
            int index = mFieldNames.IndexOf(fieldName);
            return index < 0 ? null : mFieldTypes[index];
        }

        public TypeBase RemoveField(string fieldName)
        {
            int index = mFieldNames.IndexOf(fieldName);
            if (index < 0) { return null; }
            TypeBase fieldType = mFieldTypes[index];
            mFieldNames.RemoveAt(index);
            mFieldTypes.RemoveAt(index);
            return fieldType;
        }

        public int FieldCount { get { return mFieldNames.Count; } }

        public IEnumerator<CustomTypeField> GetEnumerator()
        {
            for (int i = 0, imax = mFieldNames.Count; i < imax; i++)
            {
                yield return new CustomTypeField(mFieldNames[i], mFieldTypes[i], mFieldList[i]);
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0, imax = mFieldNames.Count; i < imax; i++)
            {
                yield return new CustomTypeField(mFieldNames[i], mFieldTypes[i], mFieldList[i]);
            }
            yield break;
        }

        public void AddSubType(ITypeDecleration subType)
        {
            if (subType != null && !mSubTypes.Contains(subType))
            {
                mSubTypes.Add(subType);
            }
        }

        public override void GenerateCode(StringBuilder code, string indent, string fieldName, bool isList, string defaultVal, string comments, int member)
        {
            if (isList)
            {
                string listIdentifier = string.Format("System.Collections.Generic.List<{0}>", typeName);
                code.AppendLine(string.Format("{0}private {1} _{2} = new {1}();", indent, listIdentifier, fieldName));
                GenerateComments(code, indent, comments);
                code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlElement(@\"{1}\")]", indent, fieldName));
                code.AppendLine("#endif");
                code.AppendLine(string.Format("{0}public {1} {2}", indent, listIdentifier, fieldName));
                code.AppendLine(string.Format("{0}{{", indent));
                code.AppendLine(string.Format("{0}\tget {{ return _{1}; }}", indent, fieldName));
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}\tset {{ _{1} = value; }}", indent, fieldName));
                code.AppendLine("#endif");
                code.AppendLine(string.Format("{0}}}", indent));
            }
            else
            {
                code.AppendLine(string.Format("{0}private {1} _{2} = null;", indent, typeName, fieldName));
                GenerateComments(code, indent, comments);
                code.AppendLine(string.Format("{0}[ProtoBuf.ProtoMember({1})]", indent, member));
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlElement(@\"{1}\")]", indent, fieldName));
                code.AppendLine("#endif");
                code.AppendLine(string.Format("{0}public {1} {2}", indent, typeName, fieldName));
                code.AppendLine(string.Format("{0}{{", indent));
                code.AppendLine(string.Format("{0}\tget {{ return _{1}; }}", indent, fieldName));
                code.AppendLine(string.Format("{0}\tset {{ _{1} = value; }}", indent, fieldName));
                code.AppendLine(string.Format("{0}}}", indent));
            }
        }

        public void AddTypeComments(string comments)
        {
            mTypeComments = comments;
        }

        public void GenerateTypeDecleration(StringBuilder code, bool xml, string indent, string sheetName, bool strKey, bool keyToMulti, bool generateToString, bool useProtectedGetMethod)
        {
            if (!string.IsNullOrEmpty(mTypeComments))
            {
                StringReader comments = new StringReader(mTypeComments);
                code.AppendLine(string.Format("{0}/// <summary>", indent));
                while (true)
                {
                    string line = comments.ReadLine();
                    if (line == null) { break; }
                    code.AppendLine(string.Format("{0}/// {1}", indent, line));
                }
                code.AppendLine(string.Format("{0}/// </summary>", indent));
                comments.Close();
            }
            code.AppendLine(string.Format("{0}[ProtoBuf.ProtoContract]", indent));
            if (xml)
            {
                code.AppendLine("#if UNITY_EDITOR");
                code.AppendLine(string.Format("{0}[System.Xml.Serialization.XmlType(@\"{1}\")]", indent, typeName));
                code.AppendLine("#endif");
            }
            if (string.IsNullOrEmpty(mBaseType))
            {
                code.AppendLine(string.Format("{0}public partial class {1}", indent, typeName));
            }
            else
            {
                code.AppendLine(string.Format("{0}public partial class {1} : {2}", indent, typeName, mBaseType));
            }
            code.AppendLine(string.Format("{0}{{", indent));
            code.AppendLine();
            if (!string.IsNullOrEmpty(sheetName))
            {
                code.AppendLine(string.Format("{0}\tpublic const string SHEET_NAME = \"{1}\";", indent, sheetName));
                code.AppendLine();
            }
            for (int i = 0, imax = mSubTypes.Count; i < imax; i++)
            {
                ITypeDecleration td = mSubTypes[i];
                if (td == null) { continue; }
                td.GenerateTypeDecleration(code, xml, indent + "\t", null, false, false, generateToString, false);
                code.AppendLine();
            }
            int protoMem = 1;
            foreach (KeyValuePair<string, int> kv in mPropertyMembers)
            {
                protoMem = Math.Max(kv.Value + 1, protoMem);
            }
            for (int i = 0, imax = mFieldNames.Count; i < imax; i++)
            {
                string fieldName = mFieldNames[i];
                TypeBase fieldType = mFieldTypes[i];
                int pm;
                if (!mPropertyMembers.TryGetValue(fieldType.GetPropertyName(fieldName), out pm))
                {
                    pm = protoMem;
                    protoMem++;
                }
                mFieldTypes[i].GenerateCode(code, indent + "\t", fieldName, mFieldList[i], mFiledDefaultVals[i], mFieldComments[i], pm);
                code.AppendLine();
            }
            if (generateToString)
            {
                int toStrBase = string.IsNullOrEmpty(mBaseType) ? 0 : 1;
                code.AppendLine(string.Format("{0}\tpublic override string ToString()", indent));
                code.AppendLine(string.Format("{0}\t{{", indent));
                List<string> toStringFormats = new List<string>();
                List<string> toStringValues = new List<string>();
                bool toStringContainsArray = false;
                for (int i = 0, imax = mFieldNames.Count; i < imax; i++)
                {
                    string fieldName = mFieldNames[i];
                    toStringFormats.Add(string.Format("{0}:{{{1}}}", fieldName, i + toStrBase));
                    if (mFieldList[i])
                    {
                        toStringValues.Add(string.Format("list2string({0})", fieldName));
                        toStringContainsArray = true;
                    }
                    else
                    {
                        toStringValues.Add(fieldName);
                    }
                }
                if (toStrBase > 0)
                {
                    List<string> fs = new List<string>(toStringFormats);
                    List<string> fv = new List<string>(toStringValues);
                    fs.Insert(0, "{0}");
                    fv.Insert(0, "str.Substring(i1, i2 - i1)");
                    code.AppendLine(string.Format("{0}\t\tstring str = base.ToString();", indent));
                    code.AppendLine(string.Format("{0}\t\tint i1 = str.IndexOf('{{') + 1;", indent));
                    code.AppendLine(string.Format("{0}\t\tint i2 = str.LastIndexOf('}}');", indent));
                    code.AppendLine(string.Format("{0}\t\tif (i1 > 0 && i2 > 0 && i1 < i2)", indent));
                    code.AppendLine(string.Format("{0}\t\t{{", indent));
                    code.AppendLine(string.Format("{0}\t\t\treturn string.Format(\"[{1}]{{{{{2}}}}}\",", indent, typeName, string.Join(", ", fs.ToArray())));
                    code.AppendLine(string.Format("{0}\t\t\t\t{1});", indent, string.Join(", ", fv.ToArray())));
                    code.AppendLine(string.Format("{0}\t\t}}", indent));
                }
                code.AppendLine(string.Format("{0}\t\treturn string.Format(\"[{1}]{{{{{2}}}}}\",", indent, typeName, string.Join(", ", toStringFormats.ToArray())));
                code.AppendLine(string.Format("{0}\t\t\t{1});", indent, string.Join(", ", toStringValues.ToArray())));
                code.AppendLine(string.Format("{0}\t}}", indent));
                code.AppendLine();
                if (toStringContainsArray)
                {
                    code.AppendLine(string.Format("{0}\tprivate string list2string(System.Collections.IList list)", indent));
                    code.AppendLine(string.Format("{0}\t{{", indent));
                    code.AppendLine(string.Format("{0}\t\tint len = list.Count;", indent));
                    code.AppendLine(string.Format("{0}\t\tstring[] items = new string[len];", indent));
                    code.AppendLine(string.Format("{0}\t\tfor (int i = 0; i < len; i++)", indent));
                    code.AppendLine(string.Format("{0}\t\t{{", indent));
                    code.AppendLine(string.Format("{0}\t\t\titems[i] = string.Format(\"{{0}}\", list[i]);", indent));
                    code.AppendLine(string.Format("{0}\t\t}}", indent));
                    code.AppendLine(string.Format("{0}\t\treturn string.Concat(\"[\", string.Join(\", \", items), \"]\");", indent));
                    code.AppendLine(string.Format("{0}\t}}", indent));
                    code.AppendLine();
                }
            }

            if (!string.IsNullOrEmpty(sheetName))
            {
                string access = useProtectedGetMethod ? "protected" : "public";
                string keyType = strKey ? "string" : "int";
                string keyName = strKey ? "key" : "id";
                if (keyToMulti)
                {
                    code.AppendLine(string.Format("{0}\t{1} static System.Collections.Generic.List<{2}> Get({3} {4})", indent, access, typeName, keyType, keyName));
                    code.AppendLine(string.Format("{0}\t{{", indent));
                    code.AppendLine(string.Format("{0}\t\treturn Linekong.Common.Config.SheetDatabase.Instance.GetSheetItems<{1}>(SHEET_NAME, {2});", indent, typeName, keyName));
                    code.AppendLine(string.Format("{0}\t}}", indent));
                }
                else
                {
                    code.AppendLine(string.Format("{0}\t{1} static {2} Get({3} {4})", indent, access, typeName, keyType, keyName));
                    code.AppendLine(string.Format("{0}\t{{", indent));
                    code.AppendLine(string.Format("{0}\t\treturn Linekong.Common.Config.SheetDatabase.Instance.GetSheetItem<{1}>(SHEET_NAME, {2});", indent, typeName, keyName));
                    code.AppendLine(string.Format("{0}\t}}", indent));
                }
                code.AppendLine();
            }
            code.AppendLine(string.Format("{0}}}", indent));
        }

        public override string ToString()
        {
            string[] fields = new string[mFieldNames.Count];
            for (int i = 0, imax = fields.Length; i < imax; i++)
            {
                fields[i] = string.Format("{0}:{1}", mFieldNames[i], mFieldTypes[i]);
            }
            return string.Format("[{0}]{{{1}}}", typeName, string.Join(", ", fields));
        }

    }


    public class TypeHelper
    {
        public static TypeValueBase GetXmlValueType(string t)
        {
            TypeValueBase ret = null;
            switch (t.ToLower())
            {
                case "bool":
                    ret = new TypeBool();
                    break;
                case "byte":
                    ret = new TypeByte();
                    break;
                case "bytes":
                    ret = new TypeBytes();
                    break;
                case "short":
                case "int16":
                    ret = new TypeShort();
                    break;
                case "ushort":
                case "uint16":
                    ret = new TypeUShort();
                    break;
                case "int":
                case "int32":
                    ret = new TypeInt();
                    break;
                case "uint":
                case "uint32":
                    ret = new TypeUInt();
                    break;
                case "long":
                case "int64":
                    ret = new TypeLong();
                    break;
                case "ulong":
                case "uint64":
                    ret = new TypeULong();
                    break;
                case "float":
                    ret = new TypeFloat();
                    break;
                case "double":
                    ret = new TypeDouble();
                    break;
                case "string":
                    ret = new TypeString();
                    break;
                case "vector2":
                    ret = new TypeVector2();
                    break;
                case "vector3":
                    ret = new TypeVector3();
                    break;
                case "vector4":
                    ret = new TypeVector4();
                    break;
                case "rect":
                    ret = new TypeRect();
                    break;
                case "color":
                    ret = new TypeColor();
                    break;
            }
            return ret;
        }
    }

    /// <summary>
    /// 屏蔽注释，并实现逐个字符读取
    /// </summary>
    public class MsgCodeReader
	{
		private const int MAX_PEEK_TIMES_PER_NEXT = 10000;			//为避免可能的死循环，限定同一个位置Peek的最大次数;
		private const int BUFFER_SIZE = 256;

		private TextReader mReader;
		private char[] mBuffer = new char[BUFFER_SIZE];
		private int mBufferIndex = -1;
		private int mBufferLength = 0;
		private int mLineCount = 1;
		private int mColumn = 0;
		private bool mCanPeek = false;
		private int mPeekCount = -1;

		private bool mIsEmptyLine = true;
		private StringBuilder mLastComments = new StringBuilder();

		/// <summary>
		/// CodeReader构造函数
		/// </summary>
		/// <param name="reader">输入的字符流（StreamReader or StringReader）</param>
		public MsgCodeReader(TextReader reader)
		{
			mReader = reader;
		}

		/// <summary>
		/// 上一行的注释（独占一行的注释，非行尾注释）
		/// </summary>
		public string LastComment { get { return mLastComments.Length > 0 ? mLastComments.ToString() : null; } }

		/// <summary>
		/// 当前的字符所在的行
		/// </summary>
		public int LineCount { get { return mLineCount; } }

		/// <summary>
		/// 当前的字符所在的列
		/// </summary>
		public int Column { get { return mColumn; } }

		/// <summary>
		/// 将当前位置移到下一个非注释的字符处
		/// </summary>
		/// <returns>能否读取下一个字符</returns>
		public bool Next()
		{
			mPeekCount = MAX_PEEK_TIMES_PER_NEXT;
			if (!MoveToNext()) { return false; }
			char chr = Peek();
			switch (chr)
			{
				case '/':
					int nextChar = NextChar();
					if (nextChar >= 0)
					{
						char nc = (char)nextChar;
						if (nc == '/')
						{
							if (mIsEmptyLine && mLastComments.Length > 0) { mLastComments.Append('\n'); }
							MoveToNext();
							while (MoveToNext())
							{
								if (Peek() == '\n') { return true; }
								if (mIsEmptyLine) { mLastComments.Append(Peek()); }
							}
						}
						else if (nc == '*')
						{
							if (MoveToNext() && MoveToNext())
							{
								char lc = Peek();
								while (MoveToNext())
								{
									char cc = Peek();
									if (lc == '*' && cc == '/') { return MoveToNext(); }
									if (mIsEmptyLine) { mLastComments.Append(lc); }
									lc = cc;
								}
								return false;
							}
							else
							{
								return false;
							}
						}
					}
					break;
				case ' ':
				case '\t':
				case '\r':
					break;
				case '\n':
					if (!mIsEmptyLine)
					{
						mLastComments.Length = 0;
					}
					mIsEmptyLine = true;
					break;
				default:
					mIsEmptyLine = false;
					break;
			}
			return true;
		}

		/// <summary>
		/// 能否从当前位置读取字符，表示是否没有读取完所有字符
		/// </summary>
		/// <returns>能否从当前位置读取</returns>
		public bool CanPeek()
		{
			return mCanPeek;
		}

		/// <summary>
		/// 读取当前位置的字符
		/// </summary>
		/// <returns>当前位置的字符</returns>
		public char Peek()
		{
			if (mPeekCount > 0)
			{
				mPeekCount--;
				if (mPeekCount <= 0)
				{
					throw new Exception("Max Peek Count reached ! check infinite loop !");
				}
			}
			if (!mCanPeek)
			{
				throw new Exception("No char to peek !");
			}
			return mBuffer[mBufferIndex];
		}

		private bool MoveToNext()
		{
			mCanPeek = true;
			mBufferIndex++;
			if (mBufferIndex >= mBufferLength - 1)
			{
				if (mBufferLength > mBufferIndex)
				{
					mBuffer[0] = mBuffer[mBufferLength - 1];
					mBufferLength = mReader.Read(mBuffer, 1, BUFFER_SIZE - 1) + 1;
				}
				else
				{
					mBufferLength = mReader.Read(mBuffer, 0, BUFFER_SIZE);
				}
				mBufferIndex = 0;
				if (mBufferLength <= 0) { mCanPeek = false; }
			}
			if (mCanPeek)
			{
				char chr = mBuffer[mBufferIndex];
				mColumn++;
				if (chr == '\n')
				{
					mLineCount++;
					mColumn = 0;
				}
			}
			return mCanPeek;
		}

		private int NextChar()
		{
			if (mBufferIndex + 1 >= mBufferLength)
			{
				return -1;
			}
			return mBuffer[mBufferIndex + 1];
		}
	}
}
