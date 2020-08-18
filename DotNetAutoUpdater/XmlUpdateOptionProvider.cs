namespace DotNetAutoUpdater
{
    internal class XmlUpdateOptionProvider : IUpdateOptionProvider
    {
        public UpdateOption ParseUpdateOption(string str)
        {
            return XmlSerializerHelper.XmlDeSerializeObject<UpdateOption>(str);
        }
    }
}