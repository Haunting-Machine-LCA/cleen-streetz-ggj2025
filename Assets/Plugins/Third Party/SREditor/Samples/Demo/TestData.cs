using System;
using SerializeReferenceEditor;

namespace Demo
{
    public abstract class BaseTestData
    {
    }
    
    [Serializable, SRName("Old Test")]
    public class OldTestData : BaseTestData
    {
        public int Value;
    }
}
