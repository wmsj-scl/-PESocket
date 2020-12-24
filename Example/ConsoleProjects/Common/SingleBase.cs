using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SingleBase<T> where T : new()
    {
        private static T single;
        public static T Single
        {
            get
            {
                if (single == null)
                {
                    single = new T();

                }
                return single;
            }
        }
    }
}
