using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace MessagePack
{
	public class MsgGen
	{
		const string WHITE_CHARS = " \t\r\n";

		static StringBuilder str_builder = new StringBuilder();

		[MenuItem("MessagePack/ProtoGen", false, 0)]
		static void MsgGenerate()
		{
			string msgPath = EditorUtility.OpenFilePanel("Select a Msg Define File", last_proto_dir, "msg");
			if (string.IsNullOrEmpty(msgPath))
			{
				EditorUtility.DisplayDialog("Warning","Please a msg define file!","ok");
				return;
			}

			string codeDir = EditorUtility.SaveFolderPanel("Select a Code Save Dir", last_code_dir, null);
			if (string.IsNullOrEmpty(codeDir))
			{
				EditorUtility.DisplayDialog("Warning", "Please a Code Save Dir!", "ok");
				return;
			}

			string codePath = "";
			if (!string.IsNullOrEmpty(GenerateScript(msgPath, codeDir, ref codePath)))
			{
				Debug.Log("Code generated at " + codePath);
				AssetDatabase.Refresh();
			}
		}

		private static string GenerateScript(string msgPath, string codeDir, ref string codePath)
		{
			
			if (string.IsNullOrEmpty(msgPath)) { return null; }
			last_proto_dir = Path.GetDirectoryName(msgPath);
			string ns = null;
			string cn = null;
			StreamReader reader = null;
			MsgCodeReader CodeReader = null;
			try
			{
				reader = new StreamReader(msgPath, Encoding.UTF8);
                CodeReader = new MsgCodeReader(reader);
                CodeReader.Next();
				List<string> usings;
				Dictionary<string, List<ITypeDecleration>> types = ReadProto(CodeReader, out usings);
				reader.Close();
				reader = null;

				StringBuilder code = new StringBuilder();
				code.AppendLine("/**************************************************************************************************");
				code.AppendLine("                                  自动生成的MsgPack代码  请勿手动修改");
				code.AppendLine("**************************************************************************************************/");
				code.AppendLine();

				//写入using
				for (int i = 0, imax = usings.Count; i < imax; i++)
				{
					code.AppendLine(string.Format("using {0};", usings[i]));
				}
				if (usings.Count > 0)
				{
					code.AppendLine();
				}

				//
				foreach (KeyValuePair<string, List<ITypeDecleration>> kv in types)
				{
					if (string.IsNullOrEmpty(ns) && string.IsNullOrEmpty(cn))
					{
						for (int i = 0, imax = kv.Value.Count; i < imax; i++)
						{
							ITypeDecleration td = kv.Value[i];
							TypeCustom t = td is ShellType ? ((td as ShellType).type as TypeCustom) : (td as TypeCustom);
							if (t == null) { continue; }
							ns = kv.Key;
							cn = t.typeName;
							break;
						}
					}
					string indent = "\t";
					if (kv.Key == "global")
					{
						indent = "";
					}
					else
					{
						code.AppendLine(string.Format("namespace {0}", kv.Key));
						code.AppendLine("{");
						code.AppendLine();
					}
					for (int i = 0, imax = kv.Value.Count; i < imax; i++)
					{
						kv.Value[i].GenerateTypeDecleration(code, true, indent, null, false, false, true, false);
						code.AppendLine();
					}
					if (kv.Key != "global")
					{
						code.AppendLine("}");
					}
				}

				if (string.IsNullOrEmpty(codeDir))
				{
					codeDir = last_code_dir;
				}
				if (string.IsNullOrEmpty(codePath))
				{
					codePath = EditorUtility.SaveFilePanelInProject("Select Code Path", cn, "cs", "", codeDir);
				}
				if (string.IsNullOrEmpty(codePath))
				{
					ns = null;
					cn = null;
				}
				else
				{
					last_code_dir = Path.GetDirectoryName(codePath);
					bool toWrite = true;
					if (File.Exists(codePath))
					{
						MD5CryptoServiceProvider md5calc = new MD5CryptoServiceProvider();
						byte[] bs = Encoding.UTF8.GetBytes(code.ToString());
						string h1 = BitConverter.ToString(md5calc.ComputeHash(bs));
						using (FileStream fs = File.OpenRead(codePath))
						{
							if (!(fs.ReadByte() == 0xef && fs.ReadByte() == 0xbb && fs.ReadByte() == 0xbf))
							{
								fs.Position = 0L;
							}
							string h2 = BitConverter.ToString(md5calc.ComputeHash(fs));
							toWrite = h1 != h2;
						}
					}
					if (toWrite)
					{
						File.WriteAllText(codePath, code.ToString(), Encoding.UTF8);
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				string errorpos = string.Format("At {0}:{1},{2}", msgPath, CodeReader.LineCount, CodeReader.Column);
				Debug.LogError(errorpos);
				EditorUtility.DisplayDialog("ProtoGen Error", string.Concat(e.Message, "\n\n", errorpos), "OK");
				ns = null;
				cn = null;
			}
			if (reader != null) { reader.Close(); }
			if (string.IsNullOrEmpty(ns) || string.IsNullOrEmpty(cn)) { return null; }
			return ns == "global" ? cn : string.Concat(ns, ".", cn);
		}

		public static Dictionary<string, List<ITypeDecleration>> ReadProto(MsgCodeReader reader, out List<string> usings)
		{
			usings = new List<string>();

			Dictionary<string, List<ITypeDecleration>> ret = new Dictionary<string, List<ITypeDecleration>>();

			int requiredChar = -1;
			Dictionary<string, TypeDict> typeDicts = new Dictionary<string, TypeDict>();

			TypeDict typeDict = null;
			List<ITypeDecleration> types = null;

			int curState = 0; //1 for package, 2 for import, 4 for message, 5 for enum
			string curNameSpace = "global";
			bool headsOver = false;

			while (reader.CanPeek())
			{
				char chr = (char)reader.Peek();
				if (WHITE_CHARS.IndexOf(chr) >= 0)
				{
					reader.Next();
					continue;
				}
				if (requiredChar >= 0)
				{
					if ((char)requiredChar != chr)
					{
						throw new Exception(string.Format("'{0}' is required !", (char)requiredChar));
					}
					requiredChar = -1;
					reader.Next();
					continue;
				}
				if (chr == '\n')
				{
					reader.Next();
					continue;
				}

				if (curState == 0)
				{
					string kw = ReadContentWord(reader);
					if (kw == "package")
					{
						curState = 1;
					}
					else if (kw == "import")
					{
						if (headsOver)
						{
							throw new Exception("'import' should be in front of all types !");
						}
						curState = 2;
					}
					else if (kw == "message")
					{
						curState = 4;
					}
					else if (kw == "enum")
					{
						curState = 5;
					}
					else
					{
						Debug.LogError(chr + "   " + (int)chr + "    at line " + reader.LineCount + "  column " + reader.Column);
						throw new Exception(string.Format("Unknows keyword '{0}' !", kw));
					}
				}
				else if (curState == 1)//package
				{
					curNameSpace = ReadContentWord(reader, ".");
					requiredChar = ';';
					types = null;
					typeDict = null;
					curState = 0;
				}
				else if (curState == 2) //import
				{
					bool hasQuot = chr == '"';
					if (hasQuot) { reader.Next(); }
					string importNs = ReadContentWord(reader, ".");
					if (string.IsNullOrEmpty(importNs))
					{
						throw new Exception("Namespace after 'import' is required !");
					}
					if (hasQuot)
					{
						if (reader.Peek() != '"')
						{
							throw new Exception("'\"' should be appeared in pairs !");
						}
						reader.Next();
					}
					usings.Add(importNs);
					requiredChar = ';';
					curState = 0;
				}
				else if (curState == 4 || curState == 5)
				{
					string comments = reader.LastComment;
					headsOver = true;
					if (types == null && !ret.TryGetValue(curNameSpace, out types))
					{
						types = new List<ITypeDecleration>();
						ret.Add(curNameSpace, types);
					}
					if (typeDict == null && !typeDicts.TryGetValue(curNameSpace, out typeDict))
					{
						typeDict = new TypeDict();
						typeDicts.Add(curNameSpace, typeDict);
					}
					if (curState == 4)//message
					{
						ITypeDecleration type = ReadProtoType(reader, curNameSpace, null, typeDict, false, usings);
						if (!string.IsNullOrEmpty(comments)) { type.AddTypeComments(comments); }
						types.Add(type);
						curState = 0;
					}
					else if (curState == 5) //enum
					{
						ITypeDecleration type = ReadEnumType(reader, curNameSpace, null, typeDict, false);
						if (!string.IsNullOrEmpty(comments)) { type.AddTypeComments(comments); }
						types.Add(type);
						curState = 0;
					}
				}
			}
			return ret;
		}

		static ITypeDecleration ReadProtoType(MsgCodeReader reader, string ns, string[] parentTypes, TypeDict typeDict, bool requireMsgKeyWord, List<string> usings = null)
		{
			string typeName = null;
			ShellType ret = null;
			TypeCustom type = null;
			bool typeExtend = false;
			string baseType = null;
			bool done = false;
			bool isProperty = false;
			bool isArrayProperty = false;
			string propertyName = null;
			string propertyType = null;
			bool protoMemberEq = false;
			int protoMember = -1;
			int hasDefaultVal = 0;
			string defaultValue = null;
			string[] newParentTypes = null;
			string comments = null;
			while (reader.CanPeek())
			{
				char chr = (char)reader.Peek();
				if (chr == '\n')
				{
					reader.Next();
					continue;
				}
				if (WHITE_CHARS.IndexOf(chr) >= 0)
				{
					reader.Next();
					continue;
				}
				//Debug.LogFormat("'{0}' at Line {1} Column {2}", chr, reader.LineCount, reader.Column - 1);
				if (string.IsNullOrEmpty(typeName))
				{
					typeName = ReadContentWord(reader);
					if (requireMsgKeyWord && typeName == "message")
					{
						typeName = null;
						requireMsgKeyWord = false;
					}
					continue;
				}
				else
				{
					bool typeDone = false;
					if (type == null)
					{
						if (chr != '{')
						{
							string w = ReadContentWord(reader, ".");
							if (typeExtend)
							{
								if (string.IsNullOrEmpty(baseType))
								{
									baseType = w;
									continue;
								}
							}
							else
							{
								if (w == "extend")
								{
									typeExtend = true;
									continue;
								}
							}
							Debug.LogWarning(w);
							throw new Exception("'{' is required after message name !");
						}
						if (newParentTypes == null)
						{
							if (parentTypes == null)
							{
								parentTypes = new string[0];
							}
							newParentTypes = new string[parentTypes.Length + 1];
							Array.Copy(parentTypes, 0, newParentTypes, 0, parentTypes.Length);
							newParentTypes[parentTypes.Length] = typeName;
						}
						ret = typeDict.GetType(ns, parentTypes, typeName, false, usings);
						if (ret != null && ret.type is TypeCustom)
						{
							type = ret.type as TypeCustom;
						}
						else
						{
							if (ret == null)
							{
								ret = new ShellType(typeName);
							}
							type = new TypeCustom(typeName);
							ret.type = type;
						}
						typeDict.AddType(ns, parentTypes, typeName, ret);
						type.SetTypeExtend(baseType);
						reader.Next();
					}
					else if (chr == '}')
					{
						//TODO is illegal
						reader.Next();
						done = true;
						break;
					}
					else if (!isProperty)
					{
						comments = reader.LastComment;
						string kw = ReadContentWord(reader);
						if (kw == "optional" || kw == "required")
						{
							isProperty = true;
							isArrayProperty = false;
						}
						else if (kw == "repeated")
						{
							isProperty = true;
							isArrayProperty = true;
						}
						else if (kw == "message")
						{
							isProperty = false;
							isArrayProperty = false;
							ITypeDecleration subType = ReadProtoType(reader, ns, newParentTypes, typeDict, false);
							if (!string.IsNullOrEmpty(comments)) { subType.AddTypeComments(comments); }
							comments = null;
							type.AddSubType(subType);
						}
						else if (kw == "enum")
						{
							ITypeDecleration subType = ReadEnumType(reader, ns, newParentTypes, typeDict, false);
							if (!string.IsNullOrEmpty(comments)) { subType.AddTypeComments(comments); }
							comments = null;
							type.AddSubType(subType);
						}
						else
						{
							throw new Exception(string.Format("Unknown key word '{0}' !", kw));
						}
					}
					else if (string.IsNullOrEmpty(propertyType))
					{
						propertyType = ReadContentWord(reader);
						if (string.IsNullOrEmpty(propertyType))
						{
							throw new Exception("Property Type is required !");
						}
					}
					else if (string.IsNullOrEmpty(propertyName))
					{
						propertyName = ReadContentWord(reader);
						if (string.IsNullOrEmpty(propertyName))
						{
							throw new Exception("Property Name is required !");
						}
					}
					else if (!protoMemberEq)
					{
						if (chr != '=')
						{
							throw new Exception("'=' is required after property name !");
						}
						protoMemberEq = true;
						reader.Next();
					}
					else if (protoMember <= 0)
					{
						string pm = ReadContentWord(reader);
						//Debug.LogWarning("pm : " + pm + "    char " + reader.Peek());
						if (!TryParseInt(pm, out protoMember))
						{
							throw new Exception(string.Format("Cannot parse proto member '{0}' into int !", pm));
						}
						if (protoMember <= 0)
						{
							throw new Exception("Proto member should be greater than or equal to 1");
						}

					}
					else if (hasDefaultVal == 0)
					{
						if (chr == ';')
						{
							typeDone = true;
						}
						else
						{
							if (chr != '[')
							{
								throw new Exception("';' or '[default=xxx]' is required in property definition !");
							}
							hasDefaultVal = 1;
						}
						reader.Next();
					}
					else if (hasDefaultVal == 1)
					{
						string def = ReadContentWord(reader);
						if (def != "default")
						{
							Debug.LogError(def);
							throw new Exception("'[default=xxx]' is required in property definition !");
						}
						hasDefaultVal = 2;
					}
					else if (hasDefaultVal == 2)
					{
						if (chr != '=')
						{
							throw new Exception("'[default=xxx]' is required in property definition !");
						}
						hasDefaultVal = 3;
						reader.Next();
					}
					else if (hasDefaultVal == 3)
					{
						defaultValue = ReadContentString(reader, "]\n");
						if (string.IsNullOrEmpty(defaultValue))
						{
							throw new Exception("Illegal default value in property definition !");
						}
						Debug.LogWarning("default value : " + defaultValue);
						hasDefaultVal = 4;
					}
					else if (hasDefaultVal == 4)
					{
						if (chr != ']')
						{
							throw new Exception("'[default=xxx]' is required in property definition !");
						}
						hasDefaultVal = 5;
						reader.Next();
					}
					else
					{
						if (chr != ';')
						{
							throw new Exception("';' is required after property definition !");
						}
						typeDone = true;
						reader.Next();
					}
					if (typeDone)
					{
						TypeBase t = TypeHelper.GetXmlValueType(propertyType);
						if (t == null)
						{
							t = typeDict.GetType(ns, newParentTypes, propertyType, true, usings);
						}
						//Debug.LogWarningFormat("\t{0}{1} {2} = {3}", propertyType, isArrayProperty ? "[]" : "", propertyName, protoMember);
						type.AddField(propertyName, t, isArrayProperty, defaultValue, comments, protoMember);
						propertyType = null;
						propertyName = null;
						protoMemberEq = false;
						isArrayProperty = false;
						protoMember = -1;
						isProperty = false;
						comments = null;
						hasDefaultVal = 0;
						defaultValue = null;
					}
				}
			}
			if (!done)
			{
				throw new Exception(string.Format("message '{0}' is not completed !", typeName));
			}
			//Debug.Log(ret);
			return ret;
		}

		static ITypeDecleration ReadEnumType(MsgCodeReader reader, string ns, string[] parentTypes, TypeDict typeDict, bool requireEnumKeyWord)
		{
			TypeEnum type = null;
			ShellType ret = null;
			int requiredChar = -1;
			string enumName = null;
			string[] newParentTypes = null;
			string enumValue = null;
			bool enumEqual = false;
			bool isEnumInt = false;
			int enumInt = -1;
			bool done = false;
			while (reader.CanPeek())
			{
				char chr = (char)reader.Peek();
				//Debug.Log(chr);
				if (WHITE_CHARS.IndexOf(chr) >= 0)
				{
					reader.Next();
					continue;
				}
				if (requiredChar >= 0)
				{
					if ((char)requiredChar != chr)
					{
						throw new Exception(string.Format("'{0}' is required !", (char)requiredChar));
					}
					requiredChar = -1;
					reader.Next();
					continue;
				}
				if (chr == '\n')
				{
					reader.Next();
					continue;
				}
				//Debug.LogWarning(chr);
				if (string.IsNullOrEmpty(enumName))
				{
					enumName = ReadContentWord(reader);
					if (requireEnumKeyWord && enumName == "enum")
					{
						enumName = null;
						requireEnumKeyWord = false;
					}
					continue;
				}
				else
				{
					if (type == null)
					{
						if (chr != '{')
						{
							throw new Exception("'{' is required after enum name !");
						}
						if (newParentTypes == null)
						{
							if (parentTypes == null)
							{
								parentTypes = new string[0];
							}
							newParentTypes = new string[parentTypes.Length + 1];
							Array.Copy(parentTypes, 0, newParentTypes, 0, parentTypes.Length);
							newParentTypes[parentTypes.Length] = enumName;
						}
						ret = typeDict.GetType(ns, parentTypes, enumName, false);
						if (ret != null && ret.type is TypeEnum)
						{
							type = ret.type as TypeEnum;
						}
						else
						{
							if (ret == null)
							{
								ret = new ShellType(enumName);
							}
							type = new TypeEnum(enumName, false);
							ret.type = type;
						}
						typeDict.AddType(ns, parentTypes, enumName, ret);
						reader.Next();
					}
					else if (chr == '}')
					{
						//TODO is illegal
						reader.Next();
						done = true;
						break;
					}
					else if (string.IsNullOrEmpty(enumValue))
					{
						enumValue = ReadContentWord(reader);
						if (string.IsNullOrEmpty(enumValue))
						{
							throw new Exception("Enum Value is required !");
						}
					}
					else if (!enumEqual)
					{
						if (chr != '=')
						{
							throw new Exception("'=' is required between enum value and its int value !");
						}
						enumEqual = true;
						reader.Next();
					}
					else if (!isEnumInt)
					{
						string v = ReadContentWord(reader);
						if (!TryParseInt(v, out enumInt))
						{
							throw new Exception(string.Format("Cannot parse enum int value '{0}' into int !", v));
						}
						isEnumInt = true;
					}
					else
					{
						if (chr != ';')
						{
							throw new Exception("';' is required after enum value definition !");
						}
						type.AddEnum(enumValue, reader.LastComment, enumInt);
						enumValue = null;
						enumEqual = false;
						isEnumInt = false;
						enumInt = -1;
						reader.Next();
					}
				}
			}
			if (!done)
			{
				throw new Exception(string.Format("Enum type '{0}' is not completed !", enumName));
			}
			//Debug.Log(ret);
			return ret;
		}

		static string ReadContentWord(MsgCodeReader reader, string includedChars = "")
		{
			while (reader.CanPeek())
			{
				char chr = reader.Peek();
				if (!IsContentChar(chr) && includedChars.IndexOf(chr) < 0) { break; }
				str_builder.Append(chr);
				reader.Next();
			}
			string ret = str_builder.ToString();
			str_builder.Length = 0;
			return ret;
		}

		static bool IsContentChar(char chr)
		{
			return (chr >= 'a' && chr <= 'z') ||
				(chr >= 'A' && chr <= 'Z') ||
				(chr >= '0' && chr <= '9') ||
				chr == '_';
		}

		static string ReadContentString(MsgCodeReader reader, string readUntil)
		{
			if (string.IsNullOrEmpty(readUntil)) { return null; }
			bool shift = false;
			bool quot = false;
			while (reader.CanPeek())
			{
				char chr = reader.Peek();
				if (chr == '\"')
				{
					if (!shift) { quot = !quot; }
				}
				else if (!shift && !quot)
				{
					if (readUntil.IndexOf(chr) >= 0) { break; }
				}
				else if (shift && readUntil.IndexOf(chr) >= 0)
				{
					str_builder.Length--;
				}
				str_builder.Append(chr);
				shift = !shift && chr == '\\';
				reader.Next();
			}
			string ret = str_builder.ToString();
			str_builder.Length = 0;
			return ret;
		}

		static bool TryParseInt(string s, out int v)
		{
			if (int.TryParse(s, out v)) { return true; }
			try
			{
				if (s.StartsWith("0x"))
				{
					v = Convert.ToInt32(s, 16);
					return true;
				}
			}
			catch { }
			return false;
		}

		static string last_proto_dir
		{
			get
			{
				return EditorPrefs.GetString("protogen_last_proto_dir", ".");
			}
			set
			{
				EditorPrefs.SetString("protogen_last_proto_dir", value);
			}
		}

		static string last_code_dir
		{
			get
			{
				return EditorPrefs.GetString("protogen_last_code_dir", "");
			}
			set
			{
				EditorPrefs.SetString("protogen_last_code_dir", value);
			}
		}

		private class TypeDict
		{

			private Dictionary<string, ShellType> mTypes = new Dictionary<string, ShellType>();
			private Dictionary<string, ShellType> mRents = new Dictionary<string, ShellType>();

			public ShellType GetType(string ns, string[] pTypes, string typeName, bool rentIfNull, List<string> usings = null)
			{
				if (string.IsNullOrEmpty(typeName)) { return null; }
				ShellType type;
				for (int len = pTypes.Length; len >= 0; len--)
				{
					string typeKey = GetTypeKey(ns, pTypes, len, typeName);
					//Debug.LogError("get type : " + typeKey);
					if (mTypes.TryGetValue(typeKey, out type))
					{
						return type;
					}
				}
				if (mRents.TryGetValue(typeName, out type))
				{
					return type;
				}
				if (rentIfNull)
				{
					Type type2 = Type.GetType(typeName);
					if ( type2 == null && usings != null)
					{
						for ( int i=0; i<usings.Count; i++ )
						{
							string tempTypeName = string.Format("{0}.{1},Assembly-CSharp", usings[i], typeName);
                            type2 = Type.GetType(tempTypeName);
							if ( type2 != null )
							{
								break;
							}
						}
					}
					if ( type2 != null && type2.IsEnum)
					{
						type = new ShellType(typeName);
						type.type = new TypeEnum(typeName, false);
						mRents.Add(typeName, type);
						return type;
					}

					type = new ShellType(typeName);
					type.type = new TypeCustom(typeName);
					mRents.Add(typeName, type);
					return type;
				}
				return null;
			}

			public void AddType(string ns, string[] pTypes, string typeName, ShellType t)
			{
				if (string.IsNullOrEmpty(typeName)) { return; }
				string typeKey = GetTypeKey(ns, pTypes, pTypes.Length, typeName);
				mTypes.Remove(typeKey);
				ShellType type;
				if (mRents.TryGetValue(typeName, out type))
				{
					mRents.Remove(typeName);
					if (t != null && t != type)
					{
						type = t;
					}
				}
				else
				{
					if (t == null)
					{
						type = new ShellType(typeName);
						type.type = new TypeCustom(typeName);
					}
					else
					{
						type = t;
					}
				}
				//Debug.LogError("add type : " + typeKey);
				mTypes.Add(typeKey, type);
			}

			private string GetTypeKey(string ns, string[] pTypes, int count, string typeName)
			{
				if (pTypes == null || pTypes.Length <= 0 || count <= 0)
				{
					if (string.IsNullOrEmpty(ns)) { return typeName; }
					return string.Concat(ns, ".", typeName);
				}
				if (count > pTypes.Length) { return null; }
				if (string.IsNullOrEmpty(ns))
				{
					return string.Concat(string.Join("+", pTypes, 0, count), "+", typeName);
				}
				return string.Concat(ns, ".", string.Join("+", pTypes, 0, count), "+", typeName);
			}

		}

	}

}
