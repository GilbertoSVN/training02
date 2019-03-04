using FluentNHibernate.Mapping;
using NhSamples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhSamples.Mapping
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("Category");

            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.Name).Column("Name").Length(50).Not.Nullable();
            
            HasMany(x => x.Products).KeyColumn("Category_Id").Inverse();
        }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            DynamicUpdate();

            Table("Product");

            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.Code).Column("Code").Length(30).Not.Nullable();
            Map(x => x.Name).Column("Name").Length(50).Not.Nullable();
            Map(x => x.Price).Column("UnitPrice").Precision(18).Scale(2).Not.Nullable();
            Map(x => x.Stock).Column("Stock").Not.Nullable();
            
            Map(x => x.CategoryId).Column("Category_Id").Not.Nullable().ReadOnly();
            References(x => x.Category).Column("Category_Id").Not.Nullable();
        }
    }

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("\"Order\"");

            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.OrderDate).Column("OrderDate").Not.Nullable();
            Map(x => x.Total).Column("Total").Not.Nullable();

            HasMany(x => x.Items).KeyColumn("Order_Id").Inverse().Cascade.AllDeleteOrphan();
        }
    }

    public class OrderItemMap : ClassMap<OrderItem>
    {
        public OrderItemMap()
        {
            Table("OrderItem");

            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.Price).Column("Price").Precision(18).Scale(2).Not.Nullable();
            Map(x => x.Quantity).Column("Quantity").Not.Nullable();
            Map(x => x.Total).Column("Total").Precision(18).Scale(2).Not.Nullable();

            Map(x => x.ProductId).Column("Product_Id").Not.Nullable().ReadOnly();
            References(x => x.Product).Column("Product_Id").Not.Nullable();

            Map(x => x.OrderId).Column("Order_Id").Not.Nullable().ReadOnly();
            References(x => x.Order).Column("Order_Id").Not.Nullable();
        }
    }

    public class PurchaseOrderMap : SubclassMap<PurchaseOrder>
    {
        public PurchaseOrderMap()
        {
            Table("PurchaseOrder");
            
            KeyColumn("Id");

            Map(x => x.SupplierName).Length(100).Not.Nullable();
        }
    }

    public class SaleOrderMap : SubclassMap<SaleOrder>
    {
        public SaleOrderMap()
        {
            Table("SaleOrder");

            KeyColumn("Id");

            Map(x => x.CustomerName).Length(100).Not.Nullable();
        }
    }
}
