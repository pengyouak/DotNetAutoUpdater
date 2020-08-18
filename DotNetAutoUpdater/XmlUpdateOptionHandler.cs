namespace DotNetAutoUpdater
{
    internal class XmlUpdateOptionHandler : IUpdateOptionHandler
    {
        public UpdateOption ParseUpdateOption(string str)
        {
            return XmlSerializerHelper.XmlDeSerializeObject<UpdateOption>(str);
        }
    }
}