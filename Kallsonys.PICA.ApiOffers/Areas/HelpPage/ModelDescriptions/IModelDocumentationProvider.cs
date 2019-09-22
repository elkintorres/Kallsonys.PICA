using System;
using System.Reflection;

namespace Kallsonys.PICA.ApiOffers.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}