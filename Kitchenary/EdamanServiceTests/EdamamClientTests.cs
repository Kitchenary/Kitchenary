using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EdamamService;


namespace EdamamServiceTests
{
    [TestClass]
    public class EdamamClientTest
    {
        EdamamClient client;

        [TestInitialize]
        public void Initialize()
        {
            client = new EdamamClient("b14df9ff", "46f071168e3ab576ca2144ea74109b37");
        }

        [TestMethod]
        public async Task SearchRecipesTest()
        {
            var stuff = await client.SearchRecipes("chicken");
        }
    }
}
