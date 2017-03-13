namespace OMF
{
    public class ClassBase
    {
        public ClassBase()
        {
            __class__ = this.GetType().Name;
        }
        public string __class__ { get; set; }
    }
}
