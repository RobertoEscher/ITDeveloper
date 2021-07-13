using System;
using System.Linq;
using System.Reflection;

namespace Coopership.ITDeveloper.DomainCore.Extentions
{
    public static class GenericEnumExtensionDescription
    {
        public static string ObterDescricao(this Enum _enum)
        {
            Type generEnumType = _enum.GetType();
            MemberInfo[] memberInfo = generEnumType.GetMember(_enum.ToString());
            if ((memberInfo.Length <= 0)) return _enum.ToString();

            var attibs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            return attibs.Any()
                ? ((System.ComponentModel.DescriptionAttribute) attibs.ElementAt(0)).Description
                : _enum.ToString();
        } 
    }
}
