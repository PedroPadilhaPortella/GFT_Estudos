using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQuerieTests
    {
        private IList<Product> _products;

        public ProductQuerieTests()
        {
            _products = new List<Product>();
            _products.Add(new Product("Produto 1", 10, true));
            _products.Add(new Product("Produto 1", 20, false));
            _products.Add(new Product("Produto 1", 30, true));
            _products.Add(new Product("Produto 1", 40, false));
            _products.Add(new Product("Produto 1", 50, true));
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dada_a_consulta_de_produtos_ativos_deve_retornar_3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(result.Count(), 3);
        }


        [TestMethod]
        [TestCategory("Queries")]
        public void Dada_a_consulta_de_produtos_inativos_deve_retornar_2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.AreEqual(result.Count(), 2);
        }
    }
}