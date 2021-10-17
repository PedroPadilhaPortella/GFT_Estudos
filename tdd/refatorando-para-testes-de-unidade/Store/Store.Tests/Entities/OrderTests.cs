using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        //RED GREEN REFACTOR

        // Estas propriedades estão sendo declaradas aqui porque elas serão usadas em praticamente todos os testes,
        // mas poderiam ser declaradas cada uma em seu metodo.
        private readonly Customer _customer = new Customer("Pedro Portella", "pedro@gmail.com");
        private readonly Product _product = new Product("Produto Teste", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(8, order.Number.Length);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(EOrderStatus.WaitingPayment, order.Status);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_pagamento_do_pedido_seu_status_deve_ser_aguardando_entrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);
            order.Pay(10);
            Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_pedido_negado_seu_status_deve_ser_cancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, order.Status);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null, 12);
            Assert.AreEqual(0, order.Items.Count);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_novo_item_com_quantidade_zero_ou_menor_ele_nao_pode_ser_adicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, -3);
            order.AddItem(_product, 0);
            Assert.AreEqual(0, order.Items.Count);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_novo_pedido_valido_seu_total_deve_ser_50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_novo_pedido_expirado_seu_total_deve_ser_60()
        {
            var discount = new Discount(10, DateTime.Now.AddDays(-5));
            var order = new Order(_customer, 10, discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_desconto_invalido_seu_total_deve_ser_60()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_desconto_de_10_seu_total_deve_ser_50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }


        [TestMethod]
        [TestCategory("Domain")]
        public void dado_uma_taxa_de_entrega_de_10_seu_total_deve_ser_60()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 6);
            Assert.AreEqual(order.Total(), 60);
        }
        

        [TestMethod]
        [TestCategory("Domain")]
        public void dado_um_pedido_sem_cliente_ele_deve_ser_invalido()
        {
            var order = new Order(null , 10, _discount);
            Assert.AreEqual(order.Valid, false);
        }
        
    }
}