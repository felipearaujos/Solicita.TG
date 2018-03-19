using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Solicita.TG.Utils
{
    public static class Enumerations
    {
        public static String GetEnumDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memberInfo = type.GetMember(en.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                Object[] customAttributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (customAttributes != null && customAttributes.Length > 0)
                {
                    return ((DescriptionAttribute)customAttributes[0]).Description;
                }

            }

            return en.ToString();
        }
    }
}
