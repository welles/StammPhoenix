using FakeItEasy;
using NUnit.Framework;
using StammPhoenix.Web.Core;

namespace StammPhoenix.Test.Core;

[TestFixture]
public static class WebMapperTests
{
    [Test]
    public static void ValidateConfiguration()
    {
        var mapper = WebMapper.Create(A.Fake<IServiceProvider>());

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}