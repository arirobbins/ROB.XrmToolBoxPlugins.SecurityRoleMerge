using System;

namespace ROB.XrmToolBoxPlugins.SecurityRoleMerge
{
    public class RoleOptions
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid ID { get; set; }
        //public string ID { get; set; }

        public RoleOptions(int value, string name, Guid id)
        {
            Value = value;
            Name = name;
            ID =id;
        }
    }
}