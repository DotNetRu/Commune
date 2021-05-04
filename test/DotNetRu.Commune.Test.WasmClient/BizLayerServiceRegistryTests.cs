using System;
using DotNetRu.Commune.WasmClient;
using Xunit;

namespace DotNetRu.Commune.Test.WasmClient
{
    public class BizLayerServiceRegistryTests
    {
        [Fact]
        public void AddBizLogic_WhenServiceCollectionIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => BizLayerServiceRegistry.AddBizLogic(null!));
        }


    }
}
