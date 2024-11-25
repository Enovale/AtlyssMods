using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace AtlyssReadOldSaves
{
    public sealed class TypeConverter : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type returntype = null;
            string[] typesToReplace =
                [nameof(CharacterFile), nameof(EquipSyncStruct), nameof(ItemData), nameof(PlayerAppearance_Profile), nameof(ItemStorage_Profile)];
            foreach (var type in typesToReplace)
            {
                if (typeName == type)
                {
                    assemblyName = Assembly.GetExecutingAssembly().FullName;
                    typeName = typeName.Replace(type, nameof(AtlyssReadOldSaves) + '.' + nameof(DataClasses) + '.' + "Old" + type);
                    Console.WriteLine(typeName);
                    returntype =
                        Type.GetType(String.Format("{0}, {1}",
                            typeName, assemblyName));
                }
            }

            return returntype;
        }
    }
}