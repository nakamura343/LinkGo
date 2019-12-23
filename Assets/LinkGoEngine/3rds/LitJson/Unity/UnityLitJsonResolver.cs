using UnityEngine;

namespace LitJson.Unity
{
    public static class TypeConvert
    {
        public static string ToString(Vector2 vector)
        {
            return string.Format("<{0},{1}>",vector.x, vector.y);
        }

        public static Vector2 ToVector2(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');

            float x = 0;
            float y = 0;
            if (arr != null && arr.Length > 0)
            {
                float.TryParse(arr[0], out x);
            }
            if (arr != null && arr.Length > 1)
            {
                float.TryParse(arr[1], out y);
            }

            return new Vector2(x,y);
        }

        public static string ToString(Vector2Int vector)
        {
            return string.Format("<{0},{1}>", vector.x, vector.y);
        }

        public static Vector2Int ToVector2Int(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');

            int x = 0;
            int y = 0;
            if (arr != null && arr.Length > 0)
            {
                int.TryParse(arr[0], out x);
            }
            if (arr != null && arr.Length > 1)
            {
                int.TryParse(arr[1], out y);
            }

            return new Vector2Int(x, y);
        }

        public static string ToString(Vector3 vector)
        {
            return string.Format("<{0},{1},{2}>", vector.x, vector.y, vector.z);
        }

        public static Vector3 ToVector3(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');
            float x = 0;
            float y = 0;
            float z = 0;
            if(arr != null && arr.Length > 0)
            {
                float.TryParse(arr[0], out x);
            }
            if (arr != null && arr.Length > 1)
            {
                float.TryParse(arr[1], out y);
            }
            if (arr != null && arr.Length > 2)
            {
                float.TryParse(arr[2], out z);
            }
            return new Vector3(x, y, z);
        }

        public static string ToString(Vector3Int vector)
        {
            return string.Format("<{0},{1},{2}>", vector.x, vector.y, vector.z);
        }

        public static Vector3Int ToVector3Int(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');
            int x = 0;
            int y = 0;
            int z = 0;
            if (arr != null && arr.Length > 0)
            {
                int.TryParse(arr[0], out x);
            }
            if (arr != null && arr.Length > 1)
            {
                int.TryParse(arr[1], out y);
            }
            if (arr != null && arr.Length > 2)
            {
                int.TryParse(arr[2], out z);
            }
            return new Vector3Int(x, y, z);
        }

        public static string ToString(Vector4 vector)
        {
            return string.Format("<{0},{1},{2},{3}>", vector.x, vector.y, vector.z, vector.w);
        }
        
        public static Vector4 ToVector4(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');
            float x = 0;
            float y = 0;
            float z = 0;
            float w = 0;
            if (arr != null && arr.Length > 0)
            {
                float.TryParse(arr[0], out x);
            }
            if (arr != null && arr.Length > 1)
            {
                float.TryParse(arr[1], out y);
            }
            if (arr != null && arr.Length > 2)
            {
                float.TryParse(arr[2], out z);
            }
            if (arr != null && arr.Length > 3)
            {
                float.TryParse(arr[3], out w);
            }
            return new Vector4(x, y, z, w);
        }

        public static string ToString(Rect rect)
        {
            return string.Format("<{0},{1},{2},{3}>", rect.x, rect.y, rect.width, rect.height);
        }

        public static Rect ToRect(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');
            float x = 0;
            float y = 0;
            float width = 0;
            float height = 0;
            if (arr != null && arr.Length > 0)
            {
                float.TryParse(arr[0], out x);
            }
            if (arr != null && arr.Length > 1)
            {
                float.TryParse(arr[1], out y);
            }
            if (arr != null && arr.Length > 2)
            {
                float.TryParse(arr[2], out width);
            }
            if (arr != null && arr.Length > 3)
            {
                float.TryParse(arr[3], out height);
            }
            return new Rect(x, y, width, height);
        }

        public static string ToString(RectInt rect)
        {
            return string.Format("<{0},{1},{2},{3}>", rect.x, rect.y, rect.width, rect.height);
        }

        public static RectInt ToRectInt(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            if (arr != null && arr.Length > 0)
            {
                int.TryParse(arr[0], out x);
            }
            if (arr != null && arr.Length > 1)
            {
                int.TryParse(arr[1], out y);
            }
            if (arr != null && arr.Length > 2)
            {
                int.TryParse(arr[2], out width);
            }
            if (arr != null && arr.Length > 3)
            {
                int.TryParse(arr[3], out height);
            }
            return new RectInt(x, y, width, height);
        }

        public static string ToString(Color32 color)
        {
            return string.Format("<{0},{1},{2},{3}>", color.r, color.g, color.b, color.a);
        }

        public static Color32 ToColor32(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');
            byte r = 0;
            byte g = 0;
            byte b = 0;
            byte a = 0;
            if (arr != null && arr.Length > 0)
            {
                byte.TryParse(arr[0], out r);
            }
            if (arr != null && arr.Length > 1)
            {
                byte.TryParse(arr[1], out g);
            }
            if (arr != null && arr.Length > 2)
            {
                byte.TryParse(arr[2], out b);
            }
            if (arr != null && arr.Length > 3)
            {
                byte.TryParse(arr[3], out a);
            }
            return new Color32(r, g, b, a);
        }

        public static string ToString(Color color)
        {
            return string.Format("<{0},{1},{2},{3}>", color.r, color.g, color.b, color.a);
        }

        public static Color ToColor(string str)
        {
            str = str.Substring(1, str.Length - 2);
            string[] arr = str.Split(',');
            float r = 0;
            float g = 0;
            float b = 0;
            float a = 0;
            if (arr != null && arr.Length > 0)
            {
                float.TryParse(arr[0], out r);
            }
            if (arr != null && arr.Length > 1)
            {
                float.TryParse(arr[1], out g);
            }
            if (arr != null && arr.Length > 2)
            {
                float.TryParse(arr[2], out b);
            }
            if (arr != null && arr.Length > 3)
            {
                float.TryParse(arr[3], out a);
            }
            return new Color(r, g, b, a);
        }
    }

    public static class UnityLitJsonResolver
    {
        public static void Register()
        {
            RegisterUnityExporters();
            RegisterUnityImporters();
        }

        private static void RegisterUnityExporters()
        {
            ExporterFunc<Vector2> vec2exporter;
            vec2exporter = new ExporterFunc<Vector2>(delegate (Vector2 obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Vector2>(vec2exporter);

            ExporterFunc<Vector3> vec3exporter;
            vec3exporter = new ExporterFunc<Vector3>(delegate (Vector3 obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Vector3>(vec3exporter);

            ExporterFunc<Vector4> vec4exporter;
            vec4exporter = new ExporterFunc<Vector4>(delegate (Vector4 obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Vector4>(vec4exporter);

            ExporterFunc<Vector2Int> vec2Intexporter;
            vec2Intexporter = new ExporterFunc<Vector2Int>(delegate (Vector2Int obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Vector2Int>(vec2Intexporter);

            ExporterFunc<Vector3Int> vec3Intexporter;
            vec3Intexporter = new ExporterFunc<Vector3Int>(delegate (Vector3Int obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Vector3Int>(vec3Intexporter);

            ExporterFunc<Rect> rectexporter;
            rectexporter = new ExporterFunc<Rect>(delegate (Rect obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Rect>(rectexporter);

            ExporterFunc<RectInt> rectIntexporter;
            rectIntexporter = new ExporterFunc<RectInt>(delegate (RectInt obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<RectInt>(rectIntexporter);

            ExporterFunc<Color> colorexporter;
            colorexporter = new ExporterFunc<Color>(delegate (Color obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Color>(colorexporter);

            ExporterFunc<Color32> color32exporter;
            color32exporter = new ExporterFunc<Color32>(delegate (Color32 obj, JsonWriter writer)
            {
                writer.Write(TypeConvert.ToString(obj));
            });
            JsonMapper.RegisterExporter<Color32>(color32exporter);
        }

        //support unity
        private static void RegisterUnityImporters()
        {
            ImporterFunc<string, Vector2> vec2importer;
            vec2importer = new ImporterFunc<string, Vector2>(delegate (string input)
            {
                return TypeConvert.ToVector2(input);
            });
            JsonMapper.RegisterImporter(vec2importer);

            ImporterFunc<string, Vector3> vec3importer;
            vec3importer = new ImporterFunc<string, Vector3>(delegate (string input)
            {
                return TypeConvert.ToVector3(input);
            });
            JsonMapper.RegisterImporter(vec3importer);

            ImporterFunc<string, Vector4> vec4importer;
            vec4importer = new ImporterFunc<string, Vector4>(delegate (string input)
            {
                return TypeConvert.ToVector4(input);
            });
            JsonMapper.RegisterImporter(vec4importer);

            ImporterFunc<string, Vector2Int> vec2Intimporter;
            vec2Intimporter = new ImporterFunc<string, Vector2Int>(delegate (string input)
            {
                return TypeConvert.ToVector2Int(input);
            });
            JsonMapper.RegisterImporter(vec2Intimporter);

            ImporterFunc<string, Vector3Int> vec3Intimporter;
            vec3Intimporter = new ImporterFunc<string, Vector3Int>(delegate (string input)
            {
                return TypeConvert.ToVector3Int(input);
            });
            JsonMapper.RegisterImporter(vec3Intimporter);

            ImporterFunc<string, Rect> rectimporter;
            rectimporter = new ImporterFunc<string, Rect>(delegate (string input)
            {
                return TypeConvert.ToRect(input);
            });
            JsonMapper.RegisterImporter(rectimporter);

            ImporterFunc<string, RectInt> rectIntimporter;
            rectIntimporter = new ImporterFunc<string, RectInt>(delegate (string input)
            {
                return TypeConvert.ToRectInt(input);
            });
            JsonMapper.RegisterImporter(rectIntimporter);

            ImporterFunc<string, Color> colorimporter;
            colorimporter = new ImporterFunc<string, Color>(delegate (string input)
            {
                return TypeConvert.ToColor(input);
            });
            JsonMapper.RegisterImporter(colorimporter);

            ImporterFunc<string, Color32> color32importer;
            color32importer = new ImporterFunc<string, Color32>(delegate (string input)
            {
                return TypeConvert.ToColor32(input);
            });
            JsonMapper.RegisterImporter(color32importer);
        }
    }
}
