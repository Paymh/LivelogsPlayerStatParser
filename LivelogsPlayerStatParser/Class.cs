using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivelogsPlayerStatParser
{
    public class Class
    {
        public enum ClassTypes
        {
            Medic,
            Soldier,
            Scout,
            Demoman,
            Heavyweapons,
            Pyro,
            Spy,
            Sniper,
            Engineer
        }

        public Class(ClassTypes _classType)
        {
            classType = _classType;
        }

        public ClassTypes classType;
    }
}
