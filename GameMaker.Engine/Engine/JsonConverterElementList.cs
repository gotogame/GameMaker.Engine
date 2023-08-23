namespace GameMaker.Engine
{
    /// <summary>
    /// 元素列表
    /// </summary>
    public class JsonConverterElementList : JsonConverter<List<Element>>
    {
        /// <summary>
        /// 该类型内部的序列化器默认使用该选项
        /// </summary>
        public static JsonSerializerOptions Options { get; set; } = new JsonSerializerOptions();

        public override bool HandleNull => true;

        public override List<Element> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<Element> elements = new List<Element>();

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.String)
                    {
                        string value = reader.GetString();
                        elements.Add(ToElement(value) ?? new NullElement(value));
                    }
                }
            }

            return elements;
        }

        public override void Write(Utf8JsonWriter writer, List<Element> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            if (value != null && value.Count > 0)
            {
                foreach (Element element in value)
                {
                    if (element != null)
                    {
                        if (element is NullElement nullElement)
                        {
                            writer.WriteStringValue(nullElement.Text);
                        }
                        else
                        {
                            Type classType = element.GetType();
                            string classFullName = classType.FullName;
                            string jsonString = JsonSerializer.Serialize(element, classType, Options);
                            writer.WriteStringValue(classFullName + jsonString);
                        }
                    }
                }
            }

            writer.WriteEndArray();
        }

        private Element ToElement(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            int classFullNameLength = value.IndexOf('{');
            if (classFullNameLength <= 0)
                return null;

            string classFullName = value.Substring(0, classFullNameLength);
            if (string.IsNullOrWhiteSpace(classFullName))
                return null;

            string jsonString = value.Substring(classFullNameLength);
            if (string.IsNullOrWhiteSpace(jsonString))
                return null;

            if (classFullName == "GameMaker.Engine.Element")
            {
                return null;
            }
            else if (classFullName == "GameMaker.Engine.NullElement")
            {
                return null;
            }
            else
            {
                foreach (AssemblyLoadContext alc in AssemblyLoadContext.All)
                {
                    if (alc != null && alc.Name == "ScriptALC")
                    {
                        foreach (Assembly assembly in alc.Assemblies)
                        {
                            if (assembly != null)
                            {
                                foreach (Type type in assembly.GetTypes())
                                {
                                    if (type != null && type.FullName == classFullName && type.IsAbstract == false && type.IsPublic && type.IsClass)
                                    {
                                        return JsonSerializer.Deserialize(jsonString, type, Options) as Element;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }

}
